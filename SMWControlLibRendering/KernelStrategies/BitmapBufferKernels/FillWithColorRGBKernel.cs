using ILGPU;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    /// <summary>
    /// The fill with color.
    /// </summary>
    public class FillWithColorRGBKernel : KernelStrategy<Index, ArrayView<byte>, byte, byte, byte>
    {
        private static readonly FillWithColorRGBKernel instance = new FillWithColorRGBKernel();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="backgroundColor">The background color.</param>
        public static void Execute(Index index, ArrayView<byte> destBuffer, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            instance.kernel(index, destBuffer, backgroundColorR, backgroundColorG, backgroundColorB);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="backgroundColor">The background color.</param>
        protected override void strategy(Index index, ArrayView<byte> destBuffer, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            int ind = index * 3;
            destBuffer[ind] = backgroundColorR;
            destBuffer[ind + 1] = backgroundColorG;
            destBuffer[ind + 2] = backgroundColorB;
        }
    }
}
