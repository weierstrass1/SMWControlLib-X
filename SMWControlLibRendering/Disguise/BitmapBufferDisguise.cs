using SMWControlLibRendering.Factory;
using SMWControlLibUtils;

namespace SMWControlLibRendering.Disguise
{
    /// <summary>
    /// The bitmap buffer disguise.
    /// </summary>
    public class BitmapBufferDisguise : Disguise<BitmapBufferFactory, BitmapBuffer, byte[], int>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapBufferDisguise"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        public BitmapBufferDisguise(byte[] param1, int param2) : base(param1, param2)
        {
        }
    }
}
