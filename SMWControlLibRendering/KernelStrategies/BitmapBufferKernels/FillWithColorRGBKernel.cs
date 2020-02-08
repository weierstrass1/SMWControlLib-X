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
        private static readonly Action<Index, ArrayView<byte>, byte, byte, byte> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index, ArrayView<byte>, byte, byte, byte>
            (strategy);
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="backgroundColor">The background color.</param>
        public static void Execute(Index index, ArrayView<byte> destBuffer, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
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
        private static void strategy(Index index, ArrayView<byte> destBuffer, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            int ind = index * 3;
            destBuffer[ind] = backgroundColorB;
            destBuffer[ind + 1] = backgroundColorG;
            destBuffer[ind + 2] = backgroundColorR;
        }
    }
}
