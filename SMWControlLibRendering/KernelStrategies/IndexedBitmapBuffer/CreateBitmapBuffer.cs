using ILGPU;
using System;

namespace SMWControlLibRendering.KernelStrategies.IndexedBitmapBuffer
{
    /// <summary>
    /// The create bitmap.
    /// </summary>
    public class CreateBitmapBuffer<T, U> :  KernelStrategy<Index2, ArrayView2D<T>, ArrayView<U>, ArrayView<U>, int> where T : struct
                                                                                                                where U : struct
    {
        private static readonly CreateBitmapBuffer<T, U> instance = new CreateBitmapBuffer<T, U>();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="indexedBitmapBuffer">The indexed bitmap buffer.</param>
        /// <param name="destBitmap">The dest bitmap.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="flip">The flip.</param>
        public static void Execute(Index2 index, ArrayView2D<T> indexedBitmapBuffer, ArrayView<U> destBitmap, ArrayView<U> colors, int flip)
        {
            instance.strategy(index, indexedBitmapBuffer, destBitmap, colors, flip);
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
        protected override void strategy(Index2 index, ArrayView2D<T> indexedBitmapBuffer, ArrayView<U> destBitmap, ArrayView<U> colors, int flip)
        {
            int y = index.Y;
            if ((flip & 0x2) != 0)
                y = indexedBitmapBuffer.Height - y - 1;
            int x = index.X;
            if ((flip & 0x1) != 0)
                x = indexedBitmapBuffer.Width - x - 1;

            destBitmap[(y * indexedBitmapBuffer.Width) + x] =
            colors[Convert.ToInt32(indexedBitmapBuffer[index])];
        }
    }
}
