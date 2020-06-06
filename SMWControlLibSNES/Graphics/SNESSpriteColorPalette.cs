using SMWControlLibSNES.Enumerators.Graphics;

namespace SMWControlLibSNES.Graphics
{
    /// <summary>
    /// The sprite color palette.
    /// </summary>
    public class SNESSpriteColorPalette : SNESColorPalette
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SNESSpriteColorPalette"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="bitplanes">The bitplanes.</param>
        public SNESSpriteColorPalette(SpriteColorPaletteIndex index, int size) : base(index, size)
        {
        }
    }
}
