using SMWControlLibUtils;

namespace SMWControlLibRendering.Factory
{
    /// <summary>
    /// The indexed bitmap buffer factory.
    /// </summary>
    public class IndexedBitmapBufferFactory : ObjectFactoryWithObjsParams<IndexedBitmapBuffer>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <returns>An IndexedBitmapBuffer.</returns>
        public override IndexedBitmapBuffer GenerateObject(params object[] args)
        {
            if (HardwareAcceleratorManager.IsGPUAvailable()) return new IndexedGPUBitmapBuffer((int)args[0], (int)args[1]);
            return null;
        }
    }
}
