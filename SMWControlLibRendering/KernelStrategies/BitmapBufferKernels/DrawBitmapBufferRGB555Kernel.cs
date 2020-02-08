using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    /// <summary>
    /// The draw bitmap buffer.
    /// </summary>
    public static class DrawBitmapBufferRGB555Kernel
    {
        private static readonly Action<Index2, ArrayView<byte>, ArrayView<byte>, int, int, int> kernel = 
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView<byte>, ArrayView<byte>, int, int, int>
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
        public static void Execute(Index2 index, ArrayView<byte> destBuffer, ArrayView<byte> srcBuffer, int offset,
            int dstWidth, int srcWidth)
        {
            kernel(index, destBuffer, srcBuffer, offset, dstWidth, srcWidth);
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
        private static void strategy(Index2 index, ArrayView<byte> destBuffer, ArrayView<byte> srcBuffer, int offset,
            int dstWidth, int srcWidth)
        {
            int indsrc = ((index.Y * srcWidth) + index.X) * 3;
            byte colorR = srcBuffer[indsrc];
            byte colorG = srcBuffer[indsrc + 1];
            byte colorB = srcBuffer[indsrc + 2];

            if ((colorR & 0x7) != 0 || (colorG & 0x7) != 0 || (colorB & 0x7) != 0) return;

            int ind = ((index.Y * dstWidth) + index.X + offset) * 3;
            destBuffer[ind] = colorR;
            destBuffer[ind + 1] = colorG;
            destBuffer[ind + 2] = colorB;
        }
    }
}
