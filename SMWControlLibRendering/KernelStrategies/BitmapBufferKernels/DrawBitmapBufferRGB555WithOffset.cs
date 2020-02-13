using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    /// <summary>
    /// The draw bitmap buffer.
    /// </summary>
    public static class DrawBitmapBufferRGB555WithOffsetKernel
    {
        private static readonly Action<Index2, ArrayView<byte>, ArrayView<byte>, int, int, int, int, int, int> kernel = 
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView<byte>, ArrayView<byte>, int, int, int, int, int, int>
            (strategy);
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="dstWidth">The dst width.</param>
        /// <param name="srcWidth">The src width.</param>
        public static void Execute(Index2 index, ArrayView<byte> destBuffer, ArrayView<byte> srcBuffer, int dstX, int dstY, int dstWidth, int srcX, int srcY, int srcWidth)
        {
            kernel(index, destBuffer, srcBuffer, dstX, dstY, dstWidth, srcX, srcY, srcWidth);
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
        private static void strategy(Index2 index, ArrayView<byte> destBuffer, ArrayView<byte> srcBuffer,int dstX, int dstY, int dstWidth, int srcX, int srcY, int srcWidth)
        {
            int dstInd = (((dstY + index.Y) * dstWidth) + dstX + index.X) * 3;
            int srcInd = (((srcY + index.Y) * srcWidth) + srcX + index.X) * 3;
            destBuffer[dstInd] = srcBuffer[srcInd];
            destBuffer[dstInd + 1] = srcBuffer[srcInd + 1];
            destBuffer[dstInd + 2] = srcBuffer[srcInd + 2];
        }
    }
}
