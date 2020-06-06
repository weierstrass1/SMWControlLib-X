using ILGPU;
using ILGPU.Runtime;
using SMWControlLibOptimization.Keys;
using SMWControlLibRendering;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.PaletteOptimizer.KernelStrategies
{
    public static class ReducePaletteColors
    {
        private static Action<Index2, ArrayView3D<int>, ArrayView3D<int>, ArrayView2D<int>, ArrayView2D<int>, ArrayView2D<int>> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView3D<int>, ArrayView3D<int>, ArrayView2D<int>, ArrayView2D<int>, ArrayView2D<int>>
            (strategy);
        private static Action<Index1, ArrayView3D<int>> kernelBestEach =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView3D<int>>
            (strategyBestEach);
        private static Action<Index1, ArrayView3D<int>, ArrayView3D<int>, ArrayView2D<int>, ArrayView2D<int>, ArrayView2D<int>> kernelReduceColors =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView3D<int>, ArrayView3D<int>, ArrayView2D<int>, ArrayView2D<int>, ArrayView2D<int>>
            (strategyReduceColors);
        public static void Execute(ConcurrentDictionary<ConcurrentDictionary<Int32, int>, ConcurrentDictionary<TileKey, int>> tilesperPal, ConcurrentDictionary<TileKey, ConcurrentDictionary<Int32, int>> tilePals)
        {
            int l = 0;
            TileKey[] tilespositions = new TileKey[tilePals.Count];
            int i = 0;
            foreach(var kvp in tilePals)
            {
                tilespositions[i] = kvp.Key;
                i++;
            }

            foreach(var kvp in tilesperPal)
            {
                if (kvp.Key.Count > l)
                    l = kvp.Key.Count;
            }

            int[,] pals = new int[tilesperPal.Count, l];

            Parallel.For(0, pals.GetLength(0), k =>
            {
                Parallel.For(0, pals.GetLength(1), q =>
                {
                    pals[k, q] = 0;
                });
            });
            i = 0;
            int j;
            int[,] tilepals = new int[tilePals.Count, l];

            l = 0;
            foreach (var kvp1 in tilesperPal)
            {
                j = 0;
                foreach (var kvp2 in kvp1.Key)
                {
                    pals[i, j] = kvp2.Key;
                    j++;
                }
                if (kvp1.Value.Count > l)
                    l = kvp1.Value.Count;
                i++;
            }

            int[,] tilesPerPal = new int[tilesperPal.Count, l];

            Parallel.For(0, tilepals.GetLength(0), k =>
            {
                Parallel.For(0, tilepals.GetLength(1), q =>
                {
                    tilepals[k, q] = 0;
                });
            });

            Parallel.For(0, tilesPerPal.GetLength(0), k =>
            {
                Parallel.For(0, tilesPerPal.GetLength(1), q =>
                {
                    tilesPerPal[k, q] = -1;
                });
            });

            i = 0;
            foreach (var kvp1 in tilePals)
            {
                j = 0;
                foreach (var kvp2 in kvp1.Value)
                {
                    tilepals[i, j] = kvp2.Key;
                    j++;
                }
                i++;
            }

            i = 0;
            foreach(var kvp1 in tilesperPal)
            {
                j = 0;
                foreach (var kvp2 in kvp1.Value)
                {
                    for (int k = 0; k < tilespositions.GetLength(0); k++)
                    {
                        if (kvp2.Key.X == tilespositions[k].X && kvp2.Key.Y == tilespositions[k].Y) 
                        {
                            tilesPerPal[i, j] = k;
                            break;
                        }
                    }
                    j++;
                }
                i++;
            }

            int[,,] results = new int[pals.GetLength(0), pals.GetLength(0), 1 + pals.GetLength(1)];
            int[,,] results2 = new int[pals.GetLength(0), pals.GetLength(0), tilesPerPal.GetLength(1)];

            using (MemoryBuffer2D<int> palsBuff = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(pals.GetLength(0), pals.GetLength(1)))
            using (MemoryBuffer2D<int> tilepalsBuff = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(tilepals.GetLength(0), tilepals.GetLength(1)))
            using (MemoryBuffer2D<int> tilesPerPalBuff = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(tilesPerPal.GetLength(0), tilesPerPal.GetLength(1)))
            using (MemoryBuffer3D<int> palres = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(pals.GetLength(0), pals.GetLength(0), 1 + pals.GetLength(1)))
            using (MemoryBuffer3D<int> palres2 = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(pals.GetLength(0), pals.GetLength(0), tilesPerPal.GetLength(1)))
            {
                palsBuff.CopyFrom(pals, Index2.Zero, Index2.Zero, palsBuff.Extent);
                tilepalsBuff.CopyFrom(tilepals, Index2.Zero, Index2.Zero, tilepalsBuff.Extent);
                tilesPerPalBuff.CopyFrom(tilesPerPal, Index2.Zero, Index2.Zero, tilesPerPalBuff.Extent);

                bool change = true;
                while (change)
                {
                    palres.MemSetToZero();
                    palres2.MemSetToZero();
                    change = false;
                    kernel(palres.Extent.XY, palres, palres2, palsBuff, tilepalsBuff, tilesPerPalBuff);
                    HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                    kernelBestEach(palres.Extent.X, palres);
                    HardwareAcceleratorManager.GPUAccelerator.Synchronize();

                    palres.CopyTo(results, Index3.Zero, Index3.Zero, palres.Extent);
                    palres2.CopyTo(results2, Index3.Zero, Index3.Zero, palres2.Extent);

                    int bestOfBest = int.MaxValue;
                    List<int> inds = new List<int>();
                    List<int> inds2 = new List<int>();

                    for (i = 0; i < results.GetLength(0); i++)
                    {
                        if (results[i, i, 0] >= 0)
                        {
                            if (results[i, i, 1] < bestOfBest)
                            {
                                bestOfBest = results[i, i, 1];
                                inds2.Clear();
                                inds.Clear();
                                inds.Add(i);
                                inds2.Add(results[i, i, 0]);
                            }
                            else if (results[i, i, 1] == bestOfBest)
                            {
                                inds.Add(i);
                                inds2.Add(results[i, i, 0]);
                            }
                        }
                    }
                    if (inds.Count > 0)
                    {
                        change = true;
                        while (change) 
                        {
                            change = false;

                            int addMax = 0;
                            int remIndex = 0;
                            int add = 0;
                            int curindex = 0;
                            foreach (var id1 in inds)
                            {
                                add = 0;
                                foreach (var id2 in inds2)
                                {
                                    if (id1 == id2)
                                        add++;
                                }
                                if(add > addMax)
                                {
                                    addMax = add;
                                    remIndex = curindex;
                                }
                                curindex++;
                            }
                            if (addMax > 0)
                            {
                                change = true;
                                inds.RemoveAt(remIndex);
                                inds2.RemoveAt(remIndex);
                            }
                        }

                        int[,] reduceInds = new int[inds.Count, 2];

                        IEnumerator<int> en1 = inds.GetEnumerator();
                        IEnumerator<int> en2 = inds2.GetEnumerator();
                        en1.Reset();
                        en2.Reset();
                        for (int en = 0; en < inds.Count; en++)
                        {
                            en1.MoveNext();
                            en2.MoveNext();
                            reduceInds[en, 0] = en1.Current;
                            reduceInds[en, 1] = en2.Current;
                        }

                        using (MemoryBuffer2D<int> indsbuff = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(reduceInds.GetLength(0), reduceInds.GetLength(1)))
                        {
                            indsbuff.CopyFrom(reduceInds, Index2.Zero, Index2.Zero, indsbuff.Extent);
                            kernelReduceColors(indsbuff.Extent.X, palres, palres2, palsBuff, indsbuff, tilesPerPalBuff);
                            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                        }
                        change = true;
                    }
                    else
                    {
                        palsBuff.CopyTo(pals, Index2.Zero, Index2.Zero, palsBuff.Extent);
                        tilesPerPalBuff.CopyTo(tilesPerPal, Index2.Zero, Index2.Zero, tilesPerPalBuff.Extent);
                    }
                }
            }
            tilesperPal.Clear();
            ConcurrentDictionary<Int32, int> curpal;
            ConcurrentDictionary<TileKey, int> curtiles;
            for (i = 0; i < tilesPerPal.GetLength(0); i++)
            {
                curpal = new ConcurrentDictionary<Int32, int>();
                curtiles = new ConcurrentDictionary<TileKey, int>();
                if (tilesPerPal[i, 0] >= 0)
                {
                    for (j = 0; j < tilesPerPal.GetLength(1); j++)
                    {
                        if (tilesPerPal[i, j] >= 0)
                            curtiles.TryAdd(tilespositions[tilesPerPal[i, j]], 0);
                    }
                    for (j = 0; j < pals.GetLength(1); j++)
                    {
                        if (pals[i, j] != 0)
                            curpal.TryAdd(pals[i, j], 0);
                    }
                    tilesperPal.TryAdd(curpal, curtiles);
                }
            }
        }

        private static void strategy(Index2 index, ArrayView3D<int> palres, ArrayView3D<int> palres2, ArrayView2D<int> palsBuff, ArrayView2D<int> tilepalsBuff, ArrayView2D<int> tilesPerPalBuff)
        {
            int i1 = index.X;
            int i2 = index.Y;
            if (i1 == i2)
            {
                palres[i1, i2, 0] = -1;
                return;
            }

            for (int i = 0; i < palres2.Extent.Z; i++)
            {
                palres2[i1, i2, i] = -1;
            }

            byte eqs;
            int lastcID = 1;
            int lastTileID = 0;
            int tid;
            for (int i = 0; i < tilesPerPalBuff.Extent.Y && lastcID < palres2.Extent.Z; i++)
            {
                tid = tilesPerPalBuff[i1, i];
                if (tid >= 0)
                {
                    eqs = 0;
                    for (int j = 0; j < tilesPerPalBuff.Extent.Y; j++)
                    {
                        if (tid == tilesPerPalBuff[i2, j])
                        {
                            eqs++;
                            break;
                        }
                    }

                    if(eqs == 0)
                    {
                        for (int q = 0; q < tilepalsBuff.Extent.Y; q++)
                        {
                            eqs = 1;
                            if (tilepalsBuff[tid, q] != 0)
                            {
                                eqs = 0;
                                for (int k = 1; k < lastcID && lastcID < palres2.Extent.Z; k++)
                                {
                                    if (tilepalsBuff[tid, q] == palres[i1, i2, k])
                                    {
                                        eqs++;
                                        break;
                                    }
                                }
                            }
                            if (eqs == 0)
                            {
                                palres[i1, i2, lastcID] = tilepalsBuff[tid, q];
                                lastcID++;
                            }
                        }
                        palres2[i1, i2, lastTileID] = tid;
                        lastTileID++;
                    }
                }
            }

            int palL = lastcID - 1;
            int totalL = 0;

            for (int i = 0; i < palsBuff.Extent.Y; i++)
            {
                if (palsBuff[i1, i] != 0)
                    totalL++;
            }

            if (palL >= totalL)
                palres[i1, i2, 0] = -1;
            else
                palres[i1, i2, 0] = palL;
        }

        private static void strategyBestEach(Index1 index, ArrayView3D<int> palres)
        {
            int min = palres.Extent.Z;
            int ind = 0;
            int changed = 0;
            for (int i = 0; i < palres.Extent.Y; i++) 
            {
                if (index != i && palres[index, i, 0] < min && palres[index, i, 0] >= 0)  
                {
                    min = palres[index, i, 0];
                    ind = i;
                    changed = 1;
                }
            }

            if(changed == 1)
            {
                palres[index, index, 0] = ind;
                palres[index, index, 1] = min;
            }
            else
            {
                palres[index, index, 0] = -1;
            }
        }
        private static void strategyReduceColors(Index1 index, ArrayView3D<int> palres, ArrayView3D<int> palres2, ArrayView2D<int> palsBuff, ArrayView2D<int> indsbuff, ArrayView2D<int> tilesPerPalBuff)
        {
            int i1 = indsbuff[index, 0];
            int i2 = indsbuff[index, 1];

            for (int i = 0; i < palsBuff.Extent.Y; i++)
            {
                palsBuff[i1, i] = palres[i1, i2, i + 1];
            }
            for (int i = 0; i < tilesPerPalBuff.Extent.Y; i++)
            {
                tilesPerPalBuff[i1, i] = palres2[i1, i2, i];
            }
        }
    }
}
