using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibOptimization.Clustering
{
    public static class OptimizeSet
    {
        private static Action<Index2, ArrayView2D<int>, ArrayView3D<int>> kernel =
           HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<int>, ArrayView3D<int>>
           (strategy);
        private static Action<Index1, ArrayView3D<int>, ArrayView2D<int>> kernelBests =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView3D<int>, ArrayView2D<int>>
            (strategyBest);
        private static Action<Index1, ArrayView<int>, ArrayView2D<int>, ArrayView2D<int>> kernelCopyNewPal =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView<int>, ArrayView2D<int>, ArrayView2D<int>>
            (strategyCopyNewPal);
        private static Action<Index1, ArrayView<int>, ArrayView<int>, ArrayView2D<int>> kernelFilter =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView<int>, ArrayView<int>, ArrayView2D<int>>
            (strategyFilter);
        private static Action<Index2, ArrayView2D<int>> kernelRemoveRepeated =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<int>>
            (strategyRemoveRepeated);
        private static Action<Index1, ArrayView<int>, ArrayView<int>, ArrayView2D<int>, ArrayView2D<int>> kernelInvalidateCluster =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView<int>, ArrayView<int>, ArrayView2D<int>, ArrayView2D<int>>
            (strategyInvalidateCluster);
        public static void Execute(int[,] clusters)
        {
            using (MemoryBuffer2D<int> clusterBuff = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(clusters.GetLength(0), clusters.GetLength(1)))
            {
                clusterBuff.CopyFrom(clusters, Index2.Zero, Index2.Zero, clusterBuff.Extent);
                using (MemoryBuffer3D<int> diffs = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(clusterBuff.Extent.X, clusterBuff.Extent.X, 3))
                using (MemoryBuffer2D<int> bests = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(clusterBuff.Extent.X, 3 + clusterBuff.Extent.X))
                {
                    bool change = true;
                    while (change)
                    {
                        change = false;
                        diffs.MemSetToZero();
                        bests.MemSetToZero();
                        kernel(new Index2(clusterBuff.Extent.X, clusterBuff.Extent.X), clusterBuff, diffs);
                        HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                        kernelBests(bests.Extent.X, diffs, bests);
                        HardwareAcceleratorManager.GPUAccelerator.Synchronize();

                        int[,] best = new int[bests.Extent.X, bests.Extent.Y];
                        bests.CopyTo(best, Index2.Zero, Index2.Zero, bests.Extent);

                        int eq = -1;
                        int diffl = -1;
                        int ts = -1;
                        for (int i = 0; i < best.GetLength(0); i++)
                        {
                            if (best[i, 0] > eq)
                            {
                                eq = best[i, 0];
                                diffl = best[i, 1];
                                ts = best[i, 2];
                            }
                            else if (best[i, 0] == eq && best[i, 1] > diffl)
                            {
                                diffl = best[i, 1];
                                ts = best[i, 2];
                            }
                            else if (best[i, 0] == eq && best[i, 1] == diffl && ts < best[i, 2])
                            {
                                ts = best[i, 2];
                            }
                        }

                        if (eq >= 0)
                        {
                            change = true;

                            List<int> bestofbest = new List<int>();
                            List<int> bestofbestID = new List<int>();

                            for (int i = 0; i < best.GetLength(0); i++)
                            {
                                if (eq == best[i, 0] && diffl == best[i, 1] && ts == best[i, 2])
                                {
                                    for (int j = 3; j < best.GetLength(1); j++)
                                    {
                                        if (best[i, j] < 0)
                                            break;
                                        else
                                        {
                                            bestofbest.Add(i);
                                            bestofbestID.Add(best[i, j]);
                                        }
                                    }
                                }
                            }

                            int[] bob1 = bestofbest.ToArray();
                            int[] bob2 = bestofbestID.ToArray();
                            int[] bob3 = new int[bob1.Length];
                            bool repeated;

                            for (int i = 0, k = 0; i < bob1.Length; i++)
                            {
                                bob3[i] = bob1[i];
                                repeated = false;
                                for (int j = 0; j < i; j++)
                                {
                                    if (bob3[i] == bob3[j])
                                    {
                                        repeated = true;
                                    }
                                }
                                if (repeated)
                                {
                                    for (int j = k; j < bob2.Length; j++)
                                    {
                                        repeated = false;
                                        for (int q = 0; q < i; q++)
                                        {
                                            if (bob3[q] == bob2[j])
                                            {
                                                repeated = true;
                                                break;
                                            }
                                        }
                                        if (!repeated)
                                        {
                                            bob3[i] = bob2[j];
                                            k = j;
                                            break;
                                        }
                                    }
                                }
                            }

                            using (MemoryBuffer<int> bobbuff1 = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(bestofbest.Count))
                            using (MemoryBuffer<int> bobbuff2 = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(bestofbestID.Count))
                            using (MemoryBuffer<int> newPos = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(bestofbest.Count))
                            using (MemoryBuffer2D<int> newPals = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(bestofbestID.Count, clusterBuff.Extent.Y))
                            {
                                newPals.MemSetToZero();
                                bobbuff1.CopyFrom(bob1, 0, 0, bobbuff1.Extent);
                                bobbuff2.CopyFrom(bob2, 0, 0, bobbuff2.Extent);
                                newPos.CopyFrom(bob3, 0, 0, newPos.Extent);
                                kernelInvalidateCluster(bobbuff1.Extent, bobbuff1, bobbuff2, newPals, clusterBuff);
                                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                                kernelCopyNewPal(newPos.Extent, newPos, newPals, clusterBuff);
                                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                                kernelFilter(bobbuff1.Extent, bobbuff1, newPos, clusterBuff);
                                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                                kernelFilter(bobbuff2.Extent, bobbuff2, newPos, clusterBuff);
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
            if (i1 >= i2 || clusbuff[i1, 0] == 0 || clusbuff[i2, 0] == 0)
            {
                diffs[i1, i2,0] = -1;
                diffs[i1, i2, 1] = -1;
                diffs[i1, i2, 2] = -1;
                return;
            }

            int maxlen = 0;
            int l1 = 0;
            int l2 = 0;

            for (int i = 0; i < clusbuff.Extent.Y; i++)
            {
                if (clusbuff[i1, i] != 0 || clusbuff[i2, i] != 0) maxlen = i + 1;
                if (clusbuff[i1, i] != 0) l1 = i + 1;
                if (clusbuff[i2, i] != 0) l2 = i + 1;
            }

            if (l2 > l1) 
            {
                int aux = i1;
                i1 = i2;
                i2 = aux;
            }

            int notFound = 0;
            byte found, add;
            for (int i = 0; i < clusbuff.Extent.Y; i++)
            {
                if (clusbuff[i1, i] != 0)
                {
                    found = 0;
                    for (int j = 0; j < clusbuff.Extent.Y; j++)
                    {
                        if (clusbuff[i1, i] == clusbuff[i2, i])
                        {
                            found = 1;
                            break;
                        }
                    }
                    if(found == 0)
                    {
                        notFound++;
                    }
                }
            }
            if (maxlen + notFound > clusbuff.Extent.Y)
            {
                i1 = index.X;
                i2 = index.Y;

                diffs[i1, i2, 0] = -1;
                diffs[i1, i2, 1] = -1;
                diffs[i1, i2, 2] = -1;
                return;
            }

            int curScore = 1;
            for (int i = 0; i < clusbuff.Extent.X; i++)
            {
                if (i != i1 && i != i2)
                {
                    add = 1;
                    for (int j = 0; j < clusbuff.Extent.Y; j++)
                    {
                        if (clusbuff[i, j] != 0)
                        {
                            found = 0;
                            for (int k = 0; k < clusbuff.Extent.Y; k++)
                            {
                                if (clusbuff[i, j] == clusbuff[i1, k] || clusbuff[i, j] == clusbuff[i2, k])
                                {
                                    found = 1;
                                    break;
                                }
                            }
                            if (found == 0)
                            {
                                add = 0;
                                break;
                            }
                        }
                    }
                    if (add == 1)
                        curScore++;
                }
            }
            diffs[i1, i2, 0] = curScore;
            diffs[i1, i2, 1] = -1;
            diffs[i1, i2, 2] = -1;
        }
        private static void strategyBest(Index1 index, ArrayView3D<int> diffs, ArrayView2D<int> bests)
        {
            int b = -2;
            int dl = -2;
            int ts = -2;
            int curID = 0;
            for (int i = index + 1; i < diffs.Extent.Y; i++)
            {
                if (diffs[index, i, 0] > b)
                {
                    b = diffs[index, i, 0];
                    dl = diffs[index, i, 1];
                    ts = diffs[index, i, 2];
                    bests[index, 3] = i;
                    curID = 4;
                }
                else if (diffs[index, i, 0] == b && diffs[index, i, 1] > dl)
                {
                    dl = diffs[index, i, 1];
                    ts = diffs[index, i, 2];
                    bests[index, 3] = i;
                    curID = 4;
                }
                else if (diffs[index, i, 0] == b && diffs[index, i, 1] == dl && ts < diffs[index, i, 2])
                {
                    ts = diffs[index, i, 2];
                    bests[index, 3] = i;
                    curID = 4;
                }
                else if (diffs[index, i, 0] == b && diffs[index, i, 1] == dl && ts == diffs[index, i, 2])
                {
                    bests[index, curID] = i;
                    curID++;
                }
            }
            for (int i = curID; i < bests.Extent.Y; i++)
            {
                bests[index, i] = -1;
            }
            bests[index, 0] = b;
            bests[index, 1] = dl;
            bests[index, 2] = ts;
        }
        private static void strategyInvalidateCluster(Index1 index, ArrayView<int> bobbuff, ArrayView<int> bobbuff2, ArrayView2D<int> newPals, ArrayView2D<int> clusbuff)
        {
            int i1 = bobbuff[index];
            int i2 = bobbuff2[index];

            int i;
            for (i = 0; i < clusbuff.Extent.Y; i++)
            {
                if (clusbuff[i1, i] == 0)
                    break;
                else
                {
                    newPals[index, i] = clusbuff[i1, i];
                }
            }

            int found;
            for (int j = 0; j < clusbuff.Extent.Y; j++)
            {
                found = 0;
                if (clusbuff[i2, j] != 0)
                {
                    for (int k = 0; k < clusbuff.Extent.Y; k++)
                    {
                        if (newPals[index, k] == clusbuff[i2, j])
                        {
                            found = 1;
                            break;
                        }
                    }
                }
                if (found == 0)
                {
                    newPals[index, i] = clusbuff[i2, j];
                    i++;
                }
            }
        }
        private static void strategyFilter(Index1 index, ArrayView<int> bobbuff, ArrayView<int> newpos, ArrayView2D<int> clusbuff)
        {
            int i1 = bobbuff[index];
            byte found = 0;
            for (int i = 0; i < newpos.Length; i++)
            {
                if (newpos[i] == i1)
                {
                    found = 1;
                    break;
                }
            }
            if (found == 0)
            {
                clusbuff[i1, 0] = 0;
            }
        }
        private static void strategyCopyNewPal(Index1 index, ArrayView<int> bobbuff, ArrayView2D<int> newPals, ArrayView2D<int> clusbuff)
        {
            int i1 = bobbuff[index];
            for (int i = 0; i < clusbuff.Extent.Y; i++)
            {
                clusbuff[i1, i] = newPals[index, i];
            }
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

        private static void strategyGetInfo(Index2 index, ArrayView2D<int> set, ArrayView2D<int> score)
        {
            int i1 = index.X;
            int i2 = index.Y;
            if (i1 >= i2)
            {
                score[i1, i2] = -1;
                return;
            }
            int curScore = 0;
            byte found, add;
            for (int i = 0; i < set.Extent.X; i++)
            {
                if (i != i1 && i != i2) 
                {
                    add = 1;
                    for (int j = 0; j < set.Extent.Y; j++)
                    {
                        if (set[i, j] != 0)
                        {
                            found = 0;
                            for (int k = 0; k < set.Extent.Y; k++)
                            {
                                if (set[i, j] == set[i1, k] || set[i, j] == set[i2, k]) 
                                {
                                    found = 1;
                                    break;
                                }
                            }
                            if (found == 0)
                            {
                                add = 0;
                                break;
                            }
                        }
                    }
                    if (add == 1)
                        curScore++;
                }
            }
        }
    }
}
