using ILGPU;
using System;

namespace SMWControlLibRendering.KernelStrategies.IndexedBitmapBuffer
{
    /// <summary>
    /// The create bitmap with zoom.
    /// </summary>
    class CreateBitmapBufferWithZoom<T, U> : KernelStrategy<Index2, ArrayView2D<T>, ArrayView<U>, ArrayView<U>, int, int> where T : struct
                                                                                                                    where U : struct
    {
        private static readonly CreateBitmapBufferWithZoom<T, U> instance = new CreateBitmapBufferWithZoom<T, U>();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="indexedBitmapBuffer">The indexed bitmap buffer.</param>
        /// <param name="destBitmap">The dest bitmap.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="flip">The flip.</param>
        public static void Execute(Index2 index, ArrayView2D<T> indexedBitmapBuffer, ArrayView<U> destBitmap, ArrayView<U> colors, int zoom, int flip)
        {
            instance.strategy(index, indexedBitmapBuffer, destBitmap, colors, zoom, flip);
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
        protected override void strategy(Index2 index, ArrayView2D<T> indexedBitmapBuffer, ArrayView<U> destBitmap, ArrayView<U> colors, int zoom, int flip)
        {
            int ind = Convert.ToInt32(indexedBitmapBuffer[index]);
            if (ind == 0) return;

            int y = index.Y;
            if ((flip & 0x2) != 0)
                y = indexedBitmapBuffer.Height - y - 1;
            int x = index.X;
            if ((flip & 0x1) != 0)
                x = indexedBitmapBuffer.Width - x - 1;

            U color = colors[ind];

            int wz = indexedBitmapBuffer.Width * zoom;
            int offset = ((y * indexedBitmapBuffer.Width) + x) * zoom;
            int wzz = wz * zoom;
            for (int j = 0; j < wzz; j += wz)
            {
                for (int i = 0; i < zoom; i++)
                {
                    destBitmap[offset + j] = color;
                }
            }
        }
    }
}
