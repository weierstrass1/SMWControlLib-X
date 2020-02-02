using SMWControlLibBackend.Enumerators.Graphics;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The sprite color palette.
    /// </summary>
    public class SNESSpriteColorPalette : SNESColorPalette
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SNESSpriteColorPalette"/> class.
        /// </summary>
        /// <param name="bitsplanes">The bitsplanes.</param>
        /// <param name="index">The index.</param>
        public SNESSpriteColorPalette(BPP bitsplanes, SpriteColorPaletteIndex index) : base(bitsplanes, index)
        {

        }
    }
}
