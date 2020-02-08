﻿using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.IndexedBitmapBufferKernels
{
    /// <summary>
    /// The copy from.
    /// </summary>
    public static class DrawIndexedBitmapBufferByteKernel
    {
        private static readonly Action<Index2, ArrayView2D<byte>, ArrayView2D<byte>, int, int> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<byte>, ArrayView2D<byte>, int, int>
            (strategy);
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static void Execute(Index2 index, ArrayView2D<byte> destBuffer, ArrayView2D<byte> srcBuffer, int x, int y)
        {
            kernel(index, destBuffer, srcBuffer, x, y);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        private static void strategy(Index2 index, ArrayView2D<byte> destBuffer, ArrayView2D<byte> srcBuffer, int x, int y)
        {
            destBuffer[index.X + x, index.Y + y] = srcBuffer[index];
        }
    }
}
