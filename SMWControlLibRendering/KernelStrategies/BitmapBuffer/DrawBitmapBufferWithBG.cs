using ILGPU;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBuffer
{
    /// <summary>
    /// The draw bitmap buffer with b g.
    /// </summary>
    public class DrawBitmapBufferWithBG<T> : KernelStrategy<Index2, ArrayView<T>, ArrayView<T>, int, int, int, T> where T : struct
    {
        private static readonly DrawBitmapBufferWithBG<T> instance = new DrawBitmapBufferWithBG<T>();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="dstWidth">The dst width.</param>
        /// <param name="srcWidth">The src width.</param>
        /// <param name="backgroundColor">The background color.</param>
        public static void Execute(Index2 index, ArrayView<T> destBuffer, ArrayView<T> srcBuffer, int offset,
            int dstWidth, int srcWidth, T backgroundColor)
        {
            instance.strategy(index, destBuffer, srcBuffer, offset, dstWidth, srcWidth, backgroundColor);
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
        /// <param name="backgroundColor">The background color.</param>
        protected override void strategy(Index2 index, ArrayView<T> destBuffer, ArrayView<T> srcBuffer, int offset,
            int dstWidth, int srcWidth, T backgroundColor)
        {
            T color = srcBuffer[(index.Y * srcWidth) + index.X];

            if (Convert.ToBoolean(color))
                color = backgroundColor;

            destBuffer[(index.Y * dstWidth) + index.X + offset] = color;
        }
    }
}
