using SMWControlLibRendering.Enumerators.Graphics;

namespace SMWControlLibSNES.Enumerators.Graphics
{
    /// <summary>
    /// The sprite color palette index.
    /// </summary>
    public class SpriteColorPaletteIndex : ColorPaletteIndex
    {
        public static SpriteColorPaletteIndex SpritePalette0 = new SpriteColorPaletteIndex(8, 128);
        public static SpriteColorPaletteIndex SpritePalette1 = new SpriteColorPaletteIndex(9, 144);
        public static SpriteColorPaletteIndex SpritePalette2 = new SpriteColorPaletteIndex(10, 160);
        public static SpriteColorPaletteIndex SpritePalette3 = new SpriteColorPaletteIndex(11, 176);
        public static SpriteColorPaletteIndex SpritePalette4 = new SpriteColorPaletteIndex(12, 192);
        public static SpriteColorPaletteIndex SpritePalette5 = new SpriteColorPaletteIndex(13, 208);
        public static SpriteColorPaletteIndex SpritePalette6 = new SpriteColorPaletteIndex(14, 224);
        public static SpriteColorPaletteIndex SpritePalette7 = new SpriteColorPaletteIndex(15, 240);
        /// <summary>
        /// Prevents a default instance of the <see cref="SpriteColorPaletteIndex"/> class from being created.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        private SpriteColorPaletteIndex(int value, int offset) : base(value, offset)
        {

        }
    }
}
