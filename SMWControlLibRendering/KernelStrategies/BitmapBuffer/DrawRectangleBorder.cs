using ILGPU;

namespace SMWControlLibRendering.KernelStrategies.BitmapBuffer
{
    /// <summary>
    /// The draw rectangle border.
    /// </summary>
    public class DrawRectangleBorder<T> : KernelStrategy<Index, ArrayView<T>, int, int, int, int, int, int, T> where T : struct
    {
        private static readonly DrawRectangleBorder<T> instance = new DrawRectangleBorder<T>();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="dstWidth">The dst width.</param>
        /// <param name="xoffset">The xoffset.</param>
        /// <param name="yoffset">The yoffset.</param>
        /// <param name="rWidth">The r width.</param>
        /// <param name="rHeight">The r height.</param>
        /// <param name="rectangleColor">The rectangle color.</param>
        public static void Execute(Index index, ArrayView<T> destBuffer, int offset, int dstWidth,
            int xoffset, int yoffset, int rWidth, int rHeight, T rectangleColor)
        {
            instance.strategy(index, destBuffer, offset, dstWidth, xoffset, yoffset, rWidth, rHeight, rectangleColor);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="dstWidth">The dst width.</param>
        /// <param name="xoffset">The xoffset.</param>
        /// <param name="yoffset">The yoffset.</param>
        /// <param name="rWidth">The r width.</param>
        /// <param name="rHeight">The r height.</param>
        /// <param name="rectangleColor">The rectangle color.</param>
        protected override void strategy(Index index, ArrayView<T> destBuffer, int offset, int dstWidth, 
            int xoffset, int yoffset, int rWidth, int rHeight, T rectangleColor)
        {
            if (index <= rWidth)
            {
                destBuffer[offset + index] = rectangleColor;
                destBuffer[xoffset + index] = rectangleColor;
            }
            if (index <= rHeight)
            {
                int indw = index * dstWidth;
                destBuffer[offset + indw] = rectangleColor;
                destBuffer[yoffset + indw] = rectangleColor;
            }
        }
    }
}
