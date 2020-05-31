using ILGPU;
using ILGPU.Runtime;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibRendering.KernelStrategies.IndexedBitmapBufferKernels
{
    public static class BuildFromBitmapKernel
    {
        private static readonly Action<Index2, ArrayView2D<byte>, 
            ArrayView2D<int>, ArrayView<int>> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<byte>, 
                ArrayView2D<int>, ArrayView<int>>
            (strategy);
        public static void Execute(Index2 extent, ArrayView2D<byte> indexedBitmapBuffer, 
            ArrayView2D<int> bitmapTile, ArrayView<int> colors)
        {
            if (extent.X > indexedBitmapBuffer.Extent.X ||
                extent.Y > indexedBitmapBuffer.Extent.Y)
                throw new ArgumentOutOfRangeException(nameof(extent));

            kernel(extent, indexedBitmapBuffer, bitmapTile, colors);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        private static void strategy(Index2 index, ArrayView2D<byte> indexedBitmapBuffer, 
            ArrayView2D<int> bitmapTile, ArrayView<int> colors)
        {
            int c = bitmapTile[index];
            for (int i = 0; i < colors.Length; i++)
            {
                if (colors[i] == c)
                {
                    indexedBitmapBuffer[index] = (byte)i;
                    break;
                }
            }
        }
    }
}
