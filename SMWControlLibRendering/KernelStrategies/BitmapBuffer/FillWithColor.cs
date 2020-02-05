using ILGPU;

namespace SMWControlLibRendering.KernelStrategies.BitmapBuffer
{
    /// <summary>
    /// The fill with color.
    /// </summary>
    public class FillWithColor<T> : KernelStrategy<Index, ArrayView<T>, T> where T : struct
    {
        private static readonly FillWithColor<T> instance = new FillWithColor<T>();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="backgroundColor">The background color.</param>
        public static void Execute(Index index, ArrayView<T> destBuffer, T backgroundColor)
        {
            instance.strategy(index, destBuffer, backgroundColor);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="backgroundColor">The background color.</param>
        protected override void strategy(Index index, ArrayView<T> destBuffer, T backgroundColor)
        {
            destBuffer[index] = backgroundColor;
        }
    }
}
