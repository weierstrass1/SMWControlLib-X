using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.Clustering
{
    public class GetNearestClusterKernel
    {
        private static Action<Index2, ArrayView2D<int>, ArrayView3D<int>> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<int>, ArrayView3D<int>>
            (strategy);
        private static Action<Index1, ArrayView3D<int>, ArrayView2D<int>> kernelBests =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView3D<int>, ArrayView2D<int>>
            (strategyBest);
        private static Action<Index1, ArrayView<int>, ArrayView<int>, ArrayView2D<int>> kernelFilter =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView<int>, ArrayView<int>, ArrayView2D<int>>
            (strategyFilter);
        private static Action<Index2, ArrayView2D<int>> kernelRemoveRepeated =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<int>>
            (strategyRemoveRepeated);
        private static Action<Index1, ArrayView<int>, ArrayView<int>, ArrayView2D<int>> kernelInvalidateCluster =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView<int>, ArrayView<int>, ArrayView2D<int>>
            (strategyInvalidateCluster);
        public static void Execute(int[,] clusters)
        {
            int[,] a = new int[clusters.GetLength(0), clusters.GetLength(1)];
            using (MemoryBuffer2D<int> clusterBuff = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(clusters.GetLength(0), clusters.GetLength(1)))
            {
                clusterBuff.CopyFrom(clusters, Index2.Zero, Index2.Zero, clusterBuff.Extent);
                using (MemoryBuffer3D<int> diffs = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(clusterBuff.Extent.X, clusterBuff.Extent.X, 2))
                using (MemoryBuffer2D<int> bests = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(clusterBuff.Extent.X, 4))
                {
                    bool change = true;
                    while (change)
                    {
                        clusterBuff.CopyTo(a, Index2.Zero, Index2.Zero, clusterBuff.Extent);
                        change = false;
                        diffs.MemSetToZero();
                        kernel(new Index2(clusterBuff.Extent.X, clusterBuff.Extent.X), clusterBuff, diffs);
                        HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                        kernelBests(bests.Extent.X, diffs, bests);
                        HardwareAcceleratorManager.GPUAccelerator.Synchronize();

                        int[,] best = new int[bests.Extent.X, 4];
                        bests.CopyTo(best, Index2.Zero, Index2.Zero, bests.Extent);
                        List<int> bestofbest = new List<int>();
                        List<int> bestofbestID = new List<int>();

                        int eq = -1;
                        int diffl = -1;
                        int num = 0;
                        for (int i = 0; i < best.GetLength(0); i++)
                        {
                            if (best[i, 0] > eq)
                            {
                                eq = best[i, 0];
                                diffl = best[i, 1];
                                num = best[i, 2];
                                bestofbest.Clear();
                                bestofbestID.Clear();
                                bestofbest.Add(i);
                                bestofbestID.Add(best[i, 3]);
                            }
                            else if (best[i, 0] == eq && best[i, 1] < diffl)
                            {
                                diffl = best[i, 1];
                                num = best[i, 2];
                                bestofbest.Clear();
                                bestofbestID.Clear();
                                bestofbest.Add(i);
                                bestofbestID.Add(best[i, 3]);
                            }
                            else if (best[i, 0] == eq && best[i, 1] == diffl)
                            {
                                bestofbest.Add(i);
                                bestofbestID.Add(best[i, 3]);
                                num += best[i, 2];
                            }
                        }
                        if (eq >= 0)
                        {
                            change = true;
                            using (MemoryBuffer<int> bobbuff1 = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(bestofbest.Count))
                            using (MemoryBuffer<int> bobbuff2 = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(bestofbestID.Count))
                            {
                                bobbuff1.CopyFrom(bestofbest.ToArray(), 0, 0, bobbuff1.Extent);
                                bobbuff2.CopyFrom(bestofbestID.ToArray(), 0, 0, bobbuff2.Extent);
                                kernelFilter(bobbuff1.Extent, bobbuff1, bobbuff2, clusterBuff);
                                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                                kernelInvalidateCluster(bobbuff1.Extent, bobbuff1, bobbuff2, clusterBuff);
                                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                            }
                            kernelRemoveRepeated(new Index2(clusterBuff.Extent.X, clusterBuff.Extent.X), clusterBuff);
                            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                        }
                    }
                }
                clusterBuff.CopyTo(clusters, Index2.Zero, Index2.Zero, clusterBuff.Extent);
            }
        }

        private static void strategy(Index2 index, ArrayView2D<int> clusbuff, ArrayView3D<int> diffs)
        {
            int i1 = index.X;
            int i2 = index.Y;

            if (i1 == i2) 
            {
                diffs[i1, i2, 0] = -1;
                diffs[i1, i2, 1] = -1;
                return;
            }
            if (clusbuff[i1, 0] == 0 || clusbuff[i2, 0] == 0)
            {
                diffs[i1, i2, 0] = -1;
                diffs[i1, i2, 1] = -1;
                return;
            }

            int l1 = 0;
            int l2 = 0;

            for (int i = 0; i < clusbuff.Extent.Y; i++)
            {
                if (clusbuff[i1, i] != 0) l1 = i + 1;
                if (clusbuff[i2, i] != 0) l2 = i + 1;
            }

            int eqs = 0;

            for (int i = 0; i < l1; i++)
            {
                if (clusbuff[i1, i] != 0)
                {
                    for (int j = 0; j < l2; j++)
                    {
                        if (clusbuff[i1, i] == clusbuff[i2, j])
                        {
                            eqs++;
                            break;
                        }
                    }
                }
            }

            int max = l1;
            int min = l2;
            if (l2 > l1)
            {
                max = l2;
                min = l1;
            }

            int diffl = max - min;

            if (max + (min - eqs) <= clusbuff.Extent.Y)
            {
                diffs[i1, i2, 0] = eqs;
                diffs[i1, i2, 1] = diffl;
            }
            else
            {
                diffs[i1, i2, 0] = -1;
                diffs[i1, i2, 1] = -1;
            }
        }
        private static void strategyBest(Index1 index, ArrayView3D<int> diffs, ArrayView2D<int> bests)
        {
            int b = -2;
            int dl = -2;
            int num = 0;
            int id = -1;
            for (int i = index + 1; i < diffs.Extent.Y; i++) 
            {
                if (diffs[index, i, 0] > b)
                {
                    b = diffs[index, i, 0];
                    dl = diffs[index, i, 1];
                    num = 1;
                    id = i;
                }
                else if (diffs[index, i, 0] == b && diffs[index, i, 1] < dl)
                {
                    dl = diffs[index, i, 1];
                    num++;
                }
            }
            bests[index, 0] = b;
            bests[index, 1] = dl;
            bests[index, 2] = num;
            bests[index, 3] = id;
        }
        private static void strategyInvalidateCluster(Index1 index, ArrayView<int> bobbuff, ArrayView<int> bobbuff2, ArrayView2D<int> clusbuff)
        {
            int i1 = bobbuff[index];
            int i2 = bobbuff2[index];

            int found = 0;
            for (int i = 0; i < index; i++)
            {
                if (bobbuff[i] == i1) 
                {
                    found = 1;
                    break;
                }
            }

            if (found == 0)
            {
                int ind = index;
                int i;

                for (i = 0; i < clusbuff.Extent.Y; i++)
                {
                    if (clusbuff[i1, i] == 0)
                    {
                        break;
                    }
                }

                found = 1;
                while (found == 1)
                {
                    if (i >= clusbuff.Extent.Y) 
                    {
                        break;
                    }
                    int starti = i;
                    for (int j = 0; j < clusbuff.Extent.Y; j++)
                    {
                        found = 1;
                        if (clusbuff[i2, j] != 0)
                        {
                            found = 0;
                            for (int k = 0; k < clusbuff.Extent.Y; k++)
                            {
                                if (clusbuff[i1, k] == clusbuff[i2, j])
                                {
                                    found = 1;
                                    break;
                                }
                            }
                        }
                        if (found == 0)
                        {
                            if (i >= clusbuff.Extent.Y)
                            {
                                for (int k = starti; k < clusbuff.Extent.Y; k++)
                                {
                                    clusbuff[i1, k] = 0;
                                }
                                i = starti;
                                break;
                            }
                            else
                            {
                                clusbuff[i1, i] = clusbuff[i2, j];
                                i++;
                            }
                        }
                    }

                    found = 0;
                    for (int j = ind + 1; j < bobbuff.Extent.X; j++)
                    {
                        if (bobbuff[j] == i1)
                        {
                            found = 1;
                            i2 = bobbuff2[j];
                            ind = j;
                            break;
                        }
                    }
                }
            }
        }
        private static void strategyFilter(Index1 index, ArrayView<int> bobbuff, ArrayView<int> bobbuff2, ArrayView2D<int> clusbuff)
        {
            int i1 = bobbuff[index];
            int i2 = bobbuff2[index];
            int l1 = 0, l2 = 0;

            for (int i = 0; i < clusbuff.Extent.Y; i++)
            {
                if (clusbuff[i1, i] != 0) l1 = i + 1;
                if (clusbuff[i2, i] != 0) l2 = i + 1;
            }

            if(l2 > l1)
            {
                int aux = i2;
                i2 = i1;
                i1 = aux;
            }

            bobbuff[index] = i1;
            bobbuff2[index] = i2;
        }
        private static void strategyRemoveRepeated(Index2 index, ArrayView2D<int> clusbuff)
        {
            int i1 = index.X;
            int i2 = index.Y;
            if (i1 >= i2) return;
            if (clusbuff[i1, 0] == 0 || clusbuff[i2, 0] == 0) return;

            int l1 = 0;
            int l2 = 0;

            for (int i = 0; i < clusbuff.Extent.Y; i++)
            {
                if (clusbuff[i1, i] != 0) l1 = i + 1;
                if (clusbuff[i2, i] != 0) l2 = i + 1;
            }

            if (l2 > l1)
            {
                int aux = l2;
                l2 = l1;
                l1 = aux;
                aux = i2;
                i2 = i1;
                i1 = aux;
            }

            int eqs = 0;

            for (int i = 0; i < clusbuff.Extent.Y; i++)
            {
                if (clusbuff[i1, i] != 0)
                {
                    for (int j = 0; j < clusbuff.Extent.Y; j++)
                    {
                        if (clusbuff[i1, i] == clusbuff[i2, j])
                        {
                            eqs++;
                            break;
                        }
                    }
                }
            }

            if (eqs == l2) 
            {
                clusbuff[i2, 0] = 0;
            }
        }
    }
}
