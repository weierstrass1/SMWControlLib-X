using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    /// <summary>
    /// The draw bitmap buffer with b g.
    /// </summary>
    public static class DrawBitmapBufferWithBGRGB555Kernel
    {
        private static readonly Action<Index2, ArrayView3D<byte>, ArrayView3D<byte>, Index2, byte, byte, byte> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView3D<byte>, ArrayView3D<byte>, Index2, byte, byte, byte>
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
        /// <param name="backgroundColor">The background color.</param>
        public static void Execute(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer,
            Index2 offset, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            kernel(index, destBuffer, srcBuffer, offset, backgroundColorR, backgroundColorG, backgroundColorB);
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
        private static void strategy(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer,
            Index2 offset, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            byte colorR = srcBuffer[new Index3(0, index)];
            byte colorG = srcBuffer[new Index3(1, index)];
            byte colorB = srcBuffer[new Index3(2, index)];

            if ((colorR & 0x7) != 0 || (colorG & 0x7) != 0 || (colorB & 0x7) != 0)
            {
                colorR = backgroundColorB;
                colorG = backgroundColorG;
                colorB = backgroundColorR;
            }

            Index2 ind = index + offset;
            destBuffer[new Index3(0, ind)] = colorR;
            destBuffer[new Index3(1, ind)] = colorG;
            destBuffer[new Index3(2, ind)] = colorB;
        }
    }
}
