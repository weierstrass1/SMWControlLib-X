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
        private static readonly Action<Index2, ArrayView3D<byte>, ArrayView3D<byte>, Index2, Index2> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView3D<byte>, ArrayView3D<byte>, Index2, Index2>
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
        public static void Execute(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer, Index2 dstOffset, Index2 srcOffset)
        {
            kernel(index, destBuffer, srcBuffer, dstOffset, srcOffset);
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
        private static void strategy(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer, Index2 dstOffset, Index2 srcOffset)
        {
            Index2 dstInd = index + dstOffset;
            Index2 srcInd = index + srcOffset;
            destBuffer[new Index3(0, dstInd)] = srcBuffer[new Index3(0, srcInd)];
            destBuffer[new Index3(1, dstInd)] = srcBuffer[new Index3(1, srcInd)];
            destBuffer[new Index3(2, dstInd)] = srcBuffer[new Index3(2, srcInd)];
        }
    }
}
