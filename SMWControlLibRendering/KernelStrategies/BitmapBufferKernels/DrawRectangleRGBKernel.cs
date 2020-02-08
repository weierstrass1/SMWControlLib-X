using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    /// <summary>
    /// The draw rectangle.
    /// </summary>
    public static class DrawRectangleRGBKernel
    {
        private static readonly Action<Index2, ArrayView<byte>, int, int, byte, byte, byte> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView<byte>, int, int, byte, byte, byte>
            (strategy);
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="width">The width.</param>
        /// <param name="backgroundColor">The background color.</param>
        public static void Execute(Index2 index, ArrayView<byte> destBuffer, int offset, int width, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            kernel(index, destBuffer, offset, width, backgroundColorR, backgroundColorG, backgroundColorB);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="width">The width.</param>
        /// <param name="backgroundColor">The background color.</param>
        private static void strategy(Index2 index, ArrayView<byte> destBuffer, int offset, int width, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            int ind = (offset + (index.Y * width) + index.X) * 3;
            destBuffer[ind] = backgroundColorR;
            destBuffer[ind + 1] = backgroundColorG;
            destBuffer[ind + 2] = backgroundColorB;
        }
    }
}
