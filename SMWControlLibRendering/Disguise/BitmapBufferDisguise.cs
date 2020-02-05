using SMWControlLibRendering.Factory;
using SMWControlLibUtils;

namespace SMWControlLibRendering.Disguise
{
    /// <summary>
    /// The bitmap buffer disguise.
    /// </summary>
    public class BitmapBufferDisguise<T> : Disguise<BitmapBufferFactory<T>, BitmapBuffer<T>, T[], int> where T : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapBufferDisguise"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        public BitmapBufferDisguise(T[] param1, int param2) : base(param1, param2)
        {
        }
    }
}
