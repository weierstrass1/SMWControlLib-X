using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Graphics;

namespace SMWControlLibBackend.Keys.Graphics
{
    /// <summary>
    /// The zoom palette key.
    /// </summary>
    public class ZoomPaletteKey : DualKey<Zoom, ColorPalette>
    {
        /// <summary>
        /// Gets the zoom.
        /// </summary>
        public Zoom Zoom => element1;
        /// <summary>
        /// Gets the palette.
        /// </summary>
        public ColorPalette Palette => element2;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomPaletteKey"/> class.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <param name="cp">The cp.</param>
        public ZoomPaletteKey(Zoom z, ColorPalette cp) : base(z, cp)
        {
        }
    }
}
