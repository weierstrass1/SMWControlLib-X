﻿using ILGPU;

namespace SMWControlLibRendering.KernelStrategies.IndexedBitmapBufferKernels
{
    /// <summary>
    /// The create bitmap.
    /// </summary>
    public class CreateBitmapBufferByteRGB555Kernel :  KernelStrategy<Index2, ArrayView2D<byte>, ArrayView<byte>, ArrayView<byte>, int>
    {
        private static readonly CreateBitmapBufferByteRGB555Kernel instance = new CreateBitmapBufferByteRGB555Kernel();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="indexedBitmapBuffer">The indexed bitmap buffer.</param>
        /// <param name="destBitmap">The dest bitmap.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="flip">The flip.</param>
        public static void Execute(Index2 index, ArrayView2D<byte> indexedBitmapBuffer, ArrayView<byte> destBitmap, ArrayView<byte> colors, int flip)
        {
            instance.kernel(index, indexedBitmapBuffer, destBitmap, colors, flip);
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
        protected override void strategy(Index2 index, ArrayView2D<byte> indexedBitmapBuffer, ArrayView<byte> destBitmap, ArrayView<byte> colors, int flip)
        {
            int y = index.Y;
            if ((flip & 0x2) != 0)
                y = indexedBitmapBuffer.Height - y - 1;
            int x = index.X;
            if ((flip & 0x1) != 0)
                x = indexedBitmapBuffer.Width - x - 1;

            int ind = ((y * indexedBitmapBuffer.Width) + x) * 3;
            int colind = indexedBitmapBuffer[index] * 3;
            if (colind != 0)
            {
                destBitmap[ind] = colors[colind];
                destBitmap[ind + 1] = colors[colind + 1];
                destBitmap[ind + 2] = colors[colind + 2];
            }
        }
    }
}