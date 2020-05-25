using SMWControlLibRendering.Enumerators.Graphics;
using SMWControlLibSNES.Enumerators.Graphics;

namespace SMWControlLibSNES.Graphics
{
    /// <summary>
    /// The b g color palette.
    /// </summary>
    public class SNESBGColorPalette : SNESColorPalette
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SNESBGColorPalette"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        public SNESBGColorPalette(BGColorPaletteIndex index, int size) : base(index, size)
        {
        }
    }
}
