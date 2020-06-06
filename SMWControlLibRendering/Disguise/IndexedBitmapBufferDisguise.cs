using SMWControlLibRendering.Factory;
using SMWControlLibUtils;

namespace SMWControlLibRendering.Disguise
{
    /// <summary>
    /// The indexed bitmap buffer disguise.
    /// </summary>
    public class IndexedBitmapBufferDisguise : DisguiseWithObjsParams<IndexedBitmapBufferFactory, IndexedBitmapBuffer>, IMustInitialize<int, int>
    {
        public IndexedBitmapBufferDisguise() : base(1, 1)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedBitmapBufferDisguise"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IndexedBitmapBufferDisguise(int width, int height) : base(width, height)
        {

        }

        public static T Generate<T>(int width, int height) where T : IndexedBitmapBufferDisguise, new()
        {
            T t = new T();
            t.Initialize(width, height);
            return t;
        }
        public void Initialize(int width, int height)
        {
            RealObject = DressUp(width, height);
        }
    }
}
