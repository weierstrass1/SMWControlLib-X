using SMWControlLibUtils;

namespace SMWControlLibRendering.Factory
{
    /// <summary>
    /// The bitmap buffer factory.
    /// </summary>
    public class BitmapBufferFactory<T> : ObjectFactory<BitmapBuffer<T>, T[], int> where T : struct
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer<T> GenerateObject(T[] param1, int param2)
        {
            if (HardwareAcceleratorManager.IsGPUAvailable()) return new GPUBitmapBuffer<T>(param1, param2);
            return new CPUBitmapBuffer<T>(param1, param2);
        }
    }
}
