using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    /// <summary>
    /// The draw rectangle border.
    /// </summary>
    public static class DrawRectangleBorderRGBKernel
    {
        private static readonly Action<Index, ArrayView3D<byte>, Index2, int, int, byte, byte, byte> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index, ArrayView3D<byte>, Index2, int, int, byte, byte, byte>
            (strategy);
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
        public static void Execute(Index index, ArrayView3D<byte> destBuffer, 
            Index2 offset, int rWidth, int rHeight, byte backgroundColorR, 
            byte backgroundColorG, byte backgroundColorB)
        {
            kernel(index, destBuffer, offset, rWidth, rHeight, backgroundColorR, backgroundColorG, backgroundColorB);
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
        private static void strategy(Index index, ArrayView3D<byte> destBuffer, Index2 offset, 
            int rWidth, int rHeight, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            if (index <= rWidth)
            {
                Index2 indw0 = offset + new Index2(index, 0);
                destBuffer[new Index3(0, indw0)] = backgroundColorB;
                destBuffer[new Index3(1, indw0)] = backgroundColorG;
                destBuffer[new Index3(2, indw0)] = backgroundColorR;

                Index2 indw1 = indw0 + new Index2(0, rHeight);
                destBuffer[new Index3(0, indw1)] = backgroundColorB;
                destBuffer[new Index3(1, indw1)] = backgroundColorG;
                destBuffer[new Index3(2, indw1)] = backgroundColorR;
            }
            if (index <= rHeight)
            {
                Index2 indh0 = offset + new Index2(0, index);
                destBuffer[new Index3(0, indh0)] = backgroundColorB;
                destBuffer[new Index3(1, indh0)] = backgroundColorG;
                destBuffer[new Index3(2, indh0)] = backgroundColorR;

                Index2 indh1 = indh0 + new Index2(rWidth, 0);
                destBuffer[new Index3(0, indh1)] = backgroundColorB;
                destBuffer[new Index3(1, indh1)] = backgroundColorG;
                destBuffer[new Index3(2, indh1)] = backgroundColorR;
            }
        }
    }
}
