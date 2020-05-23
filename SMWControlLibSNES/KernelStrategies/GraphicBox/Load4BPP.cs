using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering;
using System;

namespace SMWControlLibSNES.KernelStrategies.GraphicBox
{
    /// <summary>
    /// The load.
    /// </summary>
    public static class Load4BPP
    {
        private static readonly Action<Index3, ArrayView2D<byte>, ArrayView<byte>, int> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index3, ArrayView2D<byte>, ArrayView<byte>, int>
            (strategy);
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="offsetdiv32">The offsetdiv32.</param>
        public static void Execute(Index3 index, ArrayView2D<byte> destBuffer,
            ArrayView<byte> srcBuffer, int offset)
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
        /// <param name="offsetdiv32">The offsetdiv32.</param>
        private static void strategy(Index3 index, ArrayView2D<byte> destBuffer,
            ArrayView<byte> srcBuffer, int offset)
        {
            int block = index.X << 5;
            int line = index.Y << 1;
            int pixel = 7 - index.Z;

            int finalOffset = offset + block + line;

            int b1 = (srcBuffer[finalOffset] >> pixel) & 0x01;
            int b2 = ((srcBuffer[finalOffset + 1] >> pixel) & 0x01) << 1;
            int b3 = ((srcBuffer[finalOffset + 16] >> pixel) & 0x01) << 2;
            int b4 = ((srcBuffer[finalOffset + 17] >> pixel) & 0x01) << 3;

            int x = ((index.X & 0x0F) << 3) + index.Z;
            int y = ((index.X >> 4) << 3) + index.Y;

            destBuffer[x, y] = (byte)(b1 | b2 | b3 | b4);
        }
    }
}
