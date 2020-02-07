using SMWControlLibRendering.Factory;
using SMWControlLibUtils;

namespace SMWControlLibRendering.Disguise
{
    /// <summary>
    /// The indexed bitmap buffer disguise.
    /// </summary>
    public class IndexedBitmapBufferDisguise : DisguiseWithObjsParams<IndexedBitmapBufferFactory, IndexedBitmapBuffer>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedBitmapBufferDisguise"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IndexedBitmapBufferDisguise(int width, int height) : base (width, height)
        {

        }
    }
}
