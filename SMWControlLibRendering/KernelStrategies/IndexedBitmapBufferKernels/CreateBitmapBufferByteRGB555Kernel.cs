using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.IndexedBitmapBufferKernels
{
    /// <summary>
    /// The create bitmap.
    /// </summary>
    public static class CreateBitmapBufferByteRGB555Kernel
    {
        private static readonly Action<Index2, ArrayView2D<byte>, ArrayView3D<byte>, ArrayView<byte>, int> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<byte>, ArrayView3D<byte>, ArrayView<byte>, int>
            (strategy);
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="indexedBitmapBuffer">The indexed bitmap buffer.</param>
        /// <param name="destBitmap">The dest bitmap.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="flip">The flip.</param>
        public static void Execute(Index2 index, ArrayView2D<byte> indexedBitmapBuffer, ArrayView3D<byte> destBitmap, ArrayView<byte> colors, int flip)
        {
            kernel(index, indexedBitmapBuffer, destBitmap, colors, flip);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="indexedBitmapBuffer">The indexed bitmap buffer.</param>
        /// <param name="destBitmap">The dest bitmap.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="flip">The flip.</param>
        private static void strategy(Index2 index, ArrayView2D<byte> indexedBitmapBuffer, ArrayView3D<byte> destBitmap, ArrayView<byte> colors, int flip)
        {
            int y = index.Y;
            if ((flip & 0x2) != 0)
                y = indexedBitmapBuffer.Height - y - 1;
            int x = index.X;
            if ((flip & 0x1) != 0)
                x = indexedBitmapBuffer.Width - x - 1;

            int ind = ((y * indexedBitmapBuffer.Width) + x) * 3;
            int colind = indexedBitmapBuffer[index] * 3;
            int invalidate = 0;
            if (colind == 0) invalidate = 0x01;

            destBitmap[new Index3(0, index)] = (byte)(colors[colind] | invalidate);
            destBitmap[new Index3(1, index)] = (byte)(colors[colind + 1] | invalidate);
            destBitmap[new Index3(2, index)] = (byte)(colors[colind + 2] | invalidate);
        }
    }
}
