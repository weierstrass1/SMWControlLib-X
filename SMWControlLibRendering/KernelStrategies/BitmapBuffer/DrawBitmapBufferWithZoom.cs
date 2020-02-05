using ILGPU;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBuffer
{
    /// <summary>
    /// The draw bitmap buffer with zoom.
    /// </summary>
    public class DrawBitmapBufferWithZoom<T> : KernelStrategy<Index2, ArrayView<T>, ArrayView<T>, int, int, int, int> where T : struct
    {
        private static readonly DrawBitmapBufferWithZoom<T> instance = new DrawBitmapBufferWithZoom<T>();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="dstWidth">The dst width.</param>
        /// <param name="srcWidth">The src width.</param>
        /// <param name="zoom">The zoom.</param>
        public static void Execute(Index2 index, ArrayView<T> destBuffer, ArrayView<T> srcBuffer, int offset,
            int dstWidth, int srcWidth, int zoom)
        {
            instance.strategy(index, destBuffer, srcBuffer, offset, dstWidth, srcWidth, zoom);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="dstWidth">The dst width.</param>
        /// <param name="srcWidth">The src width.</param>
        /// <param name="zoom">The zoom.</param>
        protected override void strategy(Index2 index, ArrayView<T> destBuffer, ArrayView<T> srcBuffer, int offset,
            int dstWidth, int srcWidth, int zoom)
        {
            T color = srcBuffer[(index.Y * srcWidth) + index.X];

            if (Convert.ToBoolean(color))
                return;

            int jw = offset;
            for (int j = 0; j < zoom; j++)
            {
                jw += dstWidth;
                for (int i = 0; i < zoom; i++)
                {
                    destBuffer[jw + i] = color;
                }
            }
        }
    }
}
