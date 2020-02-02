using SMWControlLibBackend.Enumerators.Graphics;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The b g color palette.
    /// </summary>
    public class SNESBGColorPalette : SNESColorPalette
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SNESBGColorPalette"/> class.
        /// </summary>
        /// <param name="bitsplanes">The bitsplanes.</param>
        /// <param name="index">The index.</param>
        public SNESBGColorPalette(BPP bitsplanes, BGColorPaletteIndex index) : base(bitsplanes, index)
        {

        }
    }
}
