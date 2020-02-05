using SMWControlLibRendering;
using SMWControlLibRendering.Colors;
using SMWControlLibRendering.Disguise;
using SMWControlLibRendering.Enumerators.Graphics;

namespace SMWControlLibSNES.Graphics
{
    /// <summary>
    /// The color palette.
    /// </summary>
    public class SNESColorPalette : ColorPaletteDisguise<ColorR5G5B5>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SNESColorPalette"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        public SNESColorPalette(ColorPaletteIndex index, int size) : base(index, size)
        {
        }
    }
}
