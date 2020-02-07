using ILGPU;
using SMWControlLibRendering;
using SMWControlLibRendering.KernelStrategies;

namespace SMWControlLibSNES.KernelStrategies.GraphicBox
{
    /// <summary>
    /// The load.
    /// </summary>
    public class Load4BPP : KernelStrategy<Index3, ArrayView2D<byte>, ArrayView<byte>, int, int>
    {
        private static readonly Load4BPP instance = new Load4BPP();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="offsetdiv32">The offsetdiv32.</param>
        public static void Execute(Index3 index, ArrayView2D<byte> destBuffer,
            ArrayView<byte> srcBuffer, int offset, int offsetdiv32)
        {
            instance.kernel(index, destBuffer, srcBuffer, offset, offsetdiv32);
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
        protected override void strategy(Index3 index, ArrayView2D<byte> destBuffer, 
            ArrayView<byte> srcBuffer, int offset, int offsetdiv32)
        {
            int block = index.X << 32;
            int line = index.Y << 2;
            int pixel = index.Z;

            int offblock = offset + block;
            int finalOffset = offblock + line;

            int b1 = (srcBuffer[finalOffset] >> pixel) & 0x1;
            int b2 = (srcBuffer[finalOffset + 1] >> pixel) & 0x1;
            int b3 = (srcBuffer[finalOffset + 16] >> pixel) & 0x1;
            int b4 = (srcBuffer[finalOffset + 17] >> pixel) & 0x1;

            int x = (((offsetdiv32 + index.X) % 16) << 3) + pixel;
            int y = (offblock / 128) + index.Y;

            destBuffer[x, y] = (byte)(b1 + (b2 << 1) + (b3 << 2) + (b4 << 3));
        }
    }
}
