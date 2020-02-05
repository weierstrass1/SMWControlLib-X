using SMWControlLibUtils;

namespace SMWControlLibRendering.Factory
{
    /// <summary>
    /// The indexed bitmap buffer factory.
    /// </summary>
    public class IndexedBitmapBufferFactory<T, U> : ObjectFactoryWithObjsParams<IndexedBitmapBuffer<T, U>> where T : struct
                                                                                                            where U : struct
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <returns>An IndexedBitmapBuffer.</returns>
        public override IndexedBitmapBuffer<T, U> GenerateObject(params object[] args)
        {
            if (HardwareAcceleratorManager.IsGPUAvailable()) return new IndexedGPUBitmapBuffer<T, U>((int)args[0], (int)args[1]);
            return null;
        }
    }
}
