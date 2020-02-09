using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.IndexedBitmapBufferKernels
{
    /// <summary>
    /// The create bitmap with zoom.
    /// </summary>
    public static class CreateBitmapBufferWithZoomByteRGB555Kernel
    {
        private static readonly Action<Index2, ArrayView2D<byte>, ArrayView<byte>, ArrayView<byte>, int, int> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<byte>, ArrayView<byte>, ArrayView<byte>, int, int>
            (strategy);
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="indexedBitmapBuffer">The indexed bitmap buffer.</param>
        /// <param name="destBitmap">The dest bitmap.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="flip">The flip.</param>
        public static void Execute(Index2 index, ArrayView2D<byte> indexedBitmapBuffer, ArrayView<byte> destBitmap, ArrayView<byte> colors, int zoom, int flip)
        {
            kernel(index, indexedBitmapBuffer, destBitmap, colors, zoom, flip);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="indexedBitmapBuffer">The indexed bitmap buffer.</param>
        /// <param name="destBitmap">The dest bitmap.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="flip">The flip.</param>
        private static void strategy(Index2 index, ArrayView2D<byte> indexedBitmapBuffer, ArrayView<byte> destBitmap, ArrayView<byte> colors, int zoom, int flip)
        {
            int colind = indexedBitmapBuffer[index] * 3;

            int y = index.Y;
            if ((flip & 0x2) != 0)
                y = indexedBitmapBuffer.Height - y - 1;
            int x = index.X;
            if ((flip & 0x1) != 0)
                x = indexedBitmapBuffer.Width - x - 1;

            int invalidate = 0;
            if (colind == 0) invalidate = 0x01;

            byte colorR = (byte)(colors[colind] | invalidate);
            byte colorG = (byte)(colors[colind + 1] | invalidate);
            byte colorB = (byte)(colors[colind + 2] | invalidate);

            int wz = indexedBitmapBuffer.Width * zoom;
            int offset = ((y * zoom * indexedBitmapBuffer.Width) + x) * zoom;
            int wzz = wz * zoom;
            for (int j = 0; j < wzz; j += wz)
            {
                int jo = offset + j;
                for (int i = 0; i < zoom; i++)
                {
                    int ind = (jo + i) * 3;
                    destBitmap[ind] = colorB;
                    destBitmap[ind + 1] = colorG;
                    destBitmap[ind + 2] = colorR;
                }
            }
        }
    }
}
