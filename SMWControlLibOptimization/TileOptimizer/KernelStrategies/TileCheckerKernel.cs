using ILGPU;
using ILGPU.Runtime;
using SMWControlLibCommons.Graphics;
using SMWControlLibRendering;
using System;

namespace SMWControlLibOptimization.TileOptimizer.KernelStrategies
{
    public class TileCheckerKernel
    {
        private static readonly Action<Index2, ArrayView2D<int>, Index2, Index2, Index2, ArrayView2D<int>> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<int>, Index2, Index2, Index2, ArrayView2D<int>>
            (strategy);
        private static readonly Action<Index1, ArrayView2D<int>> kernelPerLine =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView2D<int>>
            (strategyPerLine);
        private static readonly Action<Index1, ArrayView2D<int>> kernelResume =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView2D<int>>
            (strategyResume);
        public static int Execute(Index2 extent, ArrayView2D<int> bp, Index2 offT1, Index2 offT2)
        {
            int[,] res = new int[extent.X, extent.Y];
            using (MemoryBuffer2D<int> s = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(extent))
            {
                kernel(extent, bp, offT1, offT2, new Index2(16, 16), s);
                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                s.CopyTo(res, Index2.Zero, Index2.Zero, s.Extent);
                kernelPerLine(extent.X, s);
                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                s.CopyTo(res, Index2.Zero, Index2.Zero, s.Extent);
                kernelResume(1, s);
                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                s.CopyTo(res, Index2.Zero, Index2.Zero, s.Extent);
            }

            int ret = res[0, 0];
            if ((ret & 1) == 1)
                return 0;
            else if ((ret & 2) == 2)
                return 1;
            else if ((ret & 4) == 4)
                return 2;
            else if ((ret & 8) == 8)
                return 3;

            return -1;
        }
        private static void strategyResume(Index1 index, ArrayView2D<int> same)
        {
            for (int i = 1; i < same.Extent.Y; i++)
            {
                same[0, 0] = same[0, 0] & same[i, 0];
            }
        }
        private static void strategyPerLine(Index1 index, ArrayView2D<int> same)
        {
            for (int i = 1; i < same.Extent.Y; i++)
            {
                same[index, 0] = same[index, 0] & same[index, i];
            }
        }
        private static void strategy(Index2 index, ArrayView2D<int> bp, Index2 offT1, Index2 offT2, Index2 tileExtent, ArrayView2D<int> same)
        {
            int x = index.X;
            int fx = tileExtent.X - 1 - x;
            int y = index.Y;
            int fy = tileExtent.Y - 1 - y;

            same[x, y] = 0;
            int c = bp[x + offT1.X, y + offT1.Y];
            if (c == bp[x + offT2.X, y + offT2.Y]) 
                same[x, y]++;
            if (c == bp[fx + offT2.X, y + offT2.Y])
                same[x, y] += 2;
            if (c == bp[x + offT2.X, fy + offT2.Y])
                same[x, y] += 4;
            if (c == bp[fx + offT2.X, fy + offT2.Y])
                same[x, y] += 8;
        }
    }
}
