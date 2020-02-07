using SMWControlLibUtils;

namespace SMWControlLibRendering.Factory
{
    /// <summary>
    /// The bitmap buffer factory.
    /// </summary>
    public class BitmapBufferFactory : ObjectFactory<BitmapBuffer, byte[], int>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer GenerateObject(byte[] param1, int param2)
        {
            if (HardwareAcceleratorManager.IsGPUAvailable()) return new GPUBitmapBuffer(param1, param2);
            return new CPUBitmapBuffer(param1, param2);
        }
    }
}
