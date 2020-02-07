using SMWControlLibRendering.Enumerators.Graphics;

namespace SMWControlLibSNES.Enumerators.Graphics
{
    /// <summary>
    /// The b g color palette index.
    /// </summary>
    public class BGColorPaletteIndex : ColorPaletteIndex
    {
        public static BGColorPaletteIndex BGPalette0 = new BGColorPaletteIndex(0, 0);
        public static BGColorPaletteIndex BGPalette1 = new BGColorPaletteIndex(1, 16);
        public static BGColorPaletteIndex BGPalette2 = new BGColorPaletteIndex(2, 32);
        public static BGColorPaletteIndex BGPalette3 = new BGColorPaletteIndex(3, 48);
        public static BGColorPaletteIndex BGPalette4 = new BGColorPaletteIndex(4, 64);
        public static BGColorPaletteIndex BGPalette5 = new BGColorPaletteIndex(5, 80);
        public static BGColorPaletteIndex BGPalette6 = new BGColorPaletteIndex(6, 96);
        public static BGColorPaletteIndex BGPalette7 = new BGColorPaletteIndex(7, 112);
        /// <summary>
        /// Prevents a default instance of the <see cref="BGColorPaletteIndex"/> class from being created.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        private BGColorPaletteIndex(int value, int offset) : base(value, offset)
        {

        }
    }
}
