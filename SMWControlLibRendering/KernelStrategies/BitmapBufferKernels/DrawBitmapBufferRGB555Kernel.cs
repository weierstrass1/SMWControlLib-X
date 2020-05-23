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
        private static readonly Action<Index2, ArrayView3D<byte>, ArrayView3D<byte>, Index2> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView3D<byte>, ArrayView3D<byte>, Index2>
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
        public static void Execute(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer,
            Index2 offset)
        {
            kernel(index, destBuffer, srcBuffer, offset);
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
        private static void strategy(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer,
            Index2 offset)
        {
            byte colorR = srcBuffer[new Index3(0, index)];
            byte colorG = srcBuffer[new Index3(1, index)];
            byte colorB = srcBuffer[new Index3(2, index)];

            if ((colorR & 0x7) != 0 || (colorG & 0x7) != 0 || (colorB & 0x7) != 0) return;

            Index2 off = offset + index;

            destBuffer[new Index3(0, off)] = colorR;
            destBuffer[new Index3(1, off)] = colorG;
            destBuffer[new Index3(2, off)] = colorB;
        }
    }
}
