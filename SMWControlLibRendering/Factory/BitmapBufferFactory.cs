using SMWControlLibRendering.Enumerator;
using SMWControlLibUtils;

namespace SMWControlLibRendering.Factory
{
    /// <summary>
    /// The bitmap buffer factory.
    /// </summary>
    public class BitmapBufferFactory : ObjectFactory<BitmapBuffer, int, int, BytesPerPixel>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer GenerateObject(int param1, int param2, BytesPerPixel param3)
        {
            if (HardwareAcceleratorManager.IsGPUAvailable()) return new GPUBitmapBuffer(param1, param2, param3);
            return new CPUBitmapBuffer(param1, param2, param3);
        }
    }
}
