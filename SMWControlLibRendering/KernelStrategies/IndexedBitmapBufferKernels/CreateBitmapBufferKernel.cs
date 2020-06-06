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
            int x;
            if ((flip & 1) == 1)
            {
                x = indexedBitmapBuffer.Extent.X - index.Y - 1;
            }
            else
            {
                x = index.Y;
            }

            int y;
            if ((flip & 2) == 2)
            {
                y = indexedBitmapBuffer.Extent.Y - index.Z - 1;
            }
            else
            {
                y = index.Z;
            }

            int z = index.X;

            int colind = (indexedBitmapBuffer[x, y] * destBitmap.Extent.X) + z;

            if (colind == 0)
                return;

            byte color = colors[colind];

            x *= zoom;
            y *= zoom;

            for (int j = 0; j < zoom; j++)
            {
                for (int i = 0; i < zoom; i++)
                {
                    destBitmap[z, x + i, y + j] = color;
                }
            }
        }
    }
}
