using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    /// <summary>
    /// The fill with color.
    /// </summary>
    public static class FillWithColorRGBKernel
    {
        private static readonly Action<Index2, ArrayView3D<byte>, byte, byte, byte> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView3D<byte>, byte, byte, byte>
            (strategy);
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="backgroundColor">The background color.</param>
        public static void Execute(Index2 index, ArrayView3D<byte> destBuffer, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            kernel(index, destBuffer, backgroundColorR, backgroundColorG, backgroundColorB);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="backgroundColor">The background color.</param>
        private static void strategy(Index2 index, ArrayView3D<byte> destBuffer, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            destBuffer[new Index3(0, index)] = backgroundColorB;
            destBuffer[new Index3(1, index)] = backgroundColorG;
            destBuffer[new Index3(2, index)] = backgroundColorR;
        }
    }
}
