using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibRendering;

namespace SMWControlLibCommons.Interfaces.Graphics
{
    public interface ITile
    {
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>A BitmapBuffer.</returns>
        BitmapBuffer GetGraphics(Zoom z);
    }
}