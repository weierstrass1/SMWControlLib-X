using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering.Exceptions;
using System;

namespace SMWControlLibRendering.KernelStrategies.IndexedBitmapBufferKernels
{
    public static class CreateBitmapBufferKernel
    {
        private static readonly Action<Index3, ArrayView2D<byte>, ArrayView3D<byte>, ArrayView<byte>, int, int> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index3, ArrayView2D<byte>, ArrayView3D<byte>, ArrayView<byte>, int, int>
            (strategy);
        public static void Execute(Index3 extent, ArrayView2D<byte> indexedBitmapBuffer, ArrayView3D<byte> destBitmap, ArrayView<byte> colors, int zoom, int flip)
        {
            if (colors.Length % extent.X != 0)
                throw new ArrayLengthNotValid(nameof(colors), $"Must be divisible by {extent.X}.");

            kernel(extent, indexedBitmapBuffer, destBitmap, colors, zoom, flip);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        private static void strategy(Index3 index, ArrayView2D<byte> indexedBitmapBuffer, ArrayView3D<byte> destBitmap, ArrayView<byte> colors, int zoom, int flip)
        {
            int y = index.Z;
            if ((flip & 0x2) != 0)
                y = indexedBitmapBuffer.Height - y - 1;
            int x = index.Y;
            if ((flip & 0x1) != 0)
                x = indexedBitmapBuffer.Width - x - 1;

            int colind = (indexedBitmapBuffer[new Index2(x, y)] * destBitmap.Extent.X) +
                index.X;
            byte color = colors[colind];

            Index2 ind;
            Index2 newInd = new Index2(x * zoom, y * zoom);
            for (int j = 0; j < zoom; j++)
            {
                for (int i = 0; i < zoom; i++)
                {
                    ind = newInd + new Index2(i, j);
                    destBitmap[new Index3(index.X, ind)] = color; 
                }
            }
        }
    }
}
