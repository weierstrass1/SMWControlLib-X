using ILGPU;

namespace SMWControlLibRendering.KernelStrategies.BitmapBuffer
{
    /// <summary>
    /// The draw rectangle.
    /// </summary>
    public class DrawRectangle<T> : KernelStrategy<Index2, ArrayView<T>, int, int, T> where T : struct
    {
        private static readonly DrawRectangle<T> instance = new DrawRectangle<T>();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="width">The width.</param>
        /// <param name="backgroundColor">The background color.</param>
        public static void Execute(Index2 index, ArrayView<T> destBuffer, int offset, int width, T backgroundColor)
        {
            instance.strategy(index, destBuffer, offset, width, backgroundColor);
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
        protected override void strategy(Index2 index, ArrayView<T> destBuffer, int offset, int width, T backgroundColor)
        {
            destBuffer[offset + (index.Y * width) + index.X] = backgroundColor;
        }
    }
}
