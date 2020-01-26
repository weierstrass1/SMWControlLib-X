using SMWControlLibBackend.Enumerators.Graphics;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The sprite color palette.
    /// </summary>
    public class SpriteColorPalette : ColorPalette
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteColorPalette"/> class.
        /// </summary>
        /// <param name="bitsplanes">The bitsplanes.</param>
        /// <param name="index">The index.</param>
        public SpriteColorPalette(BPP bitsplanes, SpriteColorPaletteIndex index) : base(bitsplanes, index)
        {

        }
    }
}
