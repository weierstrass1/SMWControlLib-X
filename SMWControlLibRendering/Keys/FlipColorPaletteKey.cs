using SMWControlLibRendering.Enumerators;
using SMWControlLibUtils;

namespace SMWControlLibRendering.Keys
{
    /// <summary>
    /// The flip color palette key.
    /// </summary>
    public class FlipColorPaletteKey : DualKey<Flip, ColorPalette>
    {
        /// <summary>
        /// Gets the flip.
        /// </summary>
        public Flip Flip => element1;
        /// <summary>
        /// Gets the palette.
        /// </summary>
        public ColorPalette Palette => element2;
        /// <summary>
        /// Initializes a new instance of the <see cref="FlipColorPaletteKey"/> class.
        /// </summary>
        /// <param name="flip">The flip.</param>
        /// <param name="cp">The cp.</param>
        public FlipColorPaletteKey(Flip flip, ColorPalette cp) : base(flip, cp)
        {
        }
    }
}
