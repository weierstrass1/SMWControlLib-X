using ILGPU;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    /// <summary>
    /// The draw rectangle border.
    /// </summary>
    public class DrawRectangleBorderRGBKernel : KernelStrategy<Index, ArrayView<byte>, int, int, int, int, int, int, byte, byte, byte> 
    {
        private static readonly DrawRectangleBorderRGBKernel instance = new DrawRectangleBorderRGBKernel();
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
        public static void Execute(Index index, ArrayView<byte> destBuffer, int offset, int dstWidth,
            int xoffset, int yoffset, int rWidth, int rHeight, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            instance.kernel(index, destBuffer, offset, dstWidth, xoffset, yoffset, rWidth, rHeight, backgroundColorR, backgroundColorG, backgroundColorB);
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
        protected override void strategy(Index index, ArrayView<byte> destBuffer, int offset, int dstWidth, 
            int xoffset, int yoffset, int rWidth, int rHeight, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            if (index <= rWidth)
            {
                int ind = (offset + index) * 3;
                destBuffer[ind] = backgroundColorR;
                destBuffer[ind + 1] = backgroundColorG;
                destBuffer[ind + 2] = backgroundColorB;

                ind = (xoffset + index) * 3;
                destBuffer[ind] = backgroundColorR;
                destBuffer[ind + 1] = backgroundColorG;
                destBuffer[ind + 2] = backgroundColorB;
            }
            if (index <= rHeight)
            {
                int indw = index * dstWidth;
                int ind = (offset + indw) * 3;
                destBuffer[ind] = backgroundColorR;
                destBuffer[ind + 1] = backgroundColorG;
                destBuffer[ind + 2] = backgroundColorB;

                ind = (yoffset + indw) * 3;
                destBuffer[ind] = backgroundColorR;
                destBuffer[ind + 1] = backgroundColorG;
                destBuffer[ind + 2] = backgroundColorB;
            }
        }
    }
}
