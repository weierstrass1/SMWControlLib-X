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
        private static readonly Action<Index2, ArrayView3D<byte>, Index2, byte, byte, byte> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView3D<byte>, Index2, byte, byte, byte>
            (strategy);
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="width">The width.</param>
        /// <param name="backgroundColor">The background color.</param>
        public static void Execute(Index2 index, ArrayView3D<byte> destBuffer, Index2 offset, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            kernel(index, destBuffer, offset, backgroundColorR, backgroundColorG, backgroundColorB);
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
        private static void strategy(Index2 index, ArrayView3D<byte> destBuffer, Index2 offset, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            Index2 ind = index + offset;
            destBuffer[new Index3(0, ind)] = backgroundColorB;
            destBuffer[new Index3(1, ind)] = backgroundColorG;
            destBuffer[new Index3(2, ind)] = backgroundColorR;
        }
    }
}
