using SMWControlLibBackend.Enumerators.Graphics;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The b g color palette.
    /// </summary>
    public class BGColorPalette : ColorPalette
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BGColorPalette"/> class.
        /// </summary>
        /// <param name="bitsplanes">The bitsplanes.</param>
        /// <param name="index">The index.</param>
        public BGColorPalette(BPP bitsplanes, BGColorPaletteIndex index) : base(bitsplanes, index)
        {

        }
    }
}
