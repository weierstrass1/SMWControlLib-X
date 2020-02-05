using ILGPU;

namespace SMWControlLibRendering.KernelStrategies.IndexedBitmapBuffer
{
    /// <summary>
    /// The copy from.
    /// </summary>
    class DrawIndexedBitmapBuffer<T, U> : KernelStrategy<Index2, ArrayView2D<T>, ArrayView2D<T>, int, int> where T : struct
                                                                                            where U : struct
    {
        private static readonly DrawIndexedBitmapBuffer<T, U> instance = new DrawIndexedBitmapBuffer<T, U>();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public static void Execute(Index2 index, ArrayView2D<T> destBuffer, ArrayView2D<T> srcBuffer, int x, int y)
        {
            instance.strategy(index, destBuffer, srcBuffer, x, y);
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
        protected override void strategy(Index2 index, ArrayView2D<T> destBuffer, ArrayView2D<T> srcBuffer, int x, int y)
        {
            destBuffer[index.X + x, index.Y + y] = srcBuffer[index];
        }
    }
}
