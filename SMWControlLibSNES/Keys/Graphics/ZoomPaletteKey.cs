using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibSNES.Graphics;
using SMWControlLibUtils;

namespace SMWControlLibSNES.Keys.Graphics
{
    /// <summary>
    /// The zoom palette key.
    /// </summary>
    public class ZoomPaletteKey : DualKey<Zoom, SNESColorPalette>
    {
        /// <summary>
        /// Gets the zoom.
        /// </summary>
        public Zoom Zoom => element1;
        /// <summary>
        /// Gets the palette.
        /// </summary>
        public SNESColorPalette Palette => element2;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomPaletteKey"/> class.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <param name="cp">The cp.</param>
        public ZoomPaletteKey(Zoom z, SNESColorPalette cp) : base(z, cp)
        {
        }
    }
}
