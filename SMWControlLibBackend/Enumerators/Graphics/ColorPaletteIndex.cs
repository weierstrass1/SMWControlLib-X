namespace SMWControlLibBackend.Enumerators.Graphics
{
    /// <summary>
    /// The color palette index.
    /// </summary>
    public class ColorPaletteIndex : FakeEnumerator
    {
        /// <summary>
        /// Gets the palette0.
        /// </summary>
        public static ColorPaletteIndex Palette0 { get { return BGColorPaletteIndex.BGPalette0; } }
        /// <summary>
        /// Gets the palette1.
        /// </summary>
        public static ColorPaletteIndex Palette1 { get { return BGColorPaletteIndex.BGPalette1; } }
        /// <summary>
        /// Gets the palette2.
        /// </summary>
        public static ColorPaletteIndex Palette2 { get { return BGColorPaletteIndex.BGPalette2; } }
        /// <summary>
        /// Gets the palette3.
        /// </summary>
        public static ColorPaletteIndex Palette3 { get { return BGColorPaletteIndex.BGPalette3; } }
        /// <summary>
        /// Gets the palette4.
        /// </summary>
        public static ColorPaletteIndex Palette4 { get { return BGColorPaletteIndex.BGPalette4; } }
        /// <summary>
        /// Gets the palette5.
        /// </summary>
        public static ColorPaletteIndex Palette5 { get { return BGColorPaletteIndex.BGPalette5; } }
        /// <summary>
        /// Gets the palette6.
        /// </summary>
        public static ColorPaletteIndex Palette6 { get { return BGColorPaletteIndex.BGPalette6; } }
        /// <summary>
        /// Gets the palette7.
        /// </summary>
        public static ColorPaletteIndex Palette7 { get { return BGColorPaletteIndex.BGPalette7; } }
        /// <summary>
        /// Gets the palette8.
        /// </summary>
        public static ColorPaletteIndex Palette8 { get { return SpriteColorPaletteIndex.SpritePalette0; } }
        /// <summary>
        /// Gets the palette9.
        /// </summary>
        public static ColorPaletteIndex Palette9 { get { return SpriteColorPaletteIndex.SpritePalette1; } }
        /// <summary>
        /// Gets the palette a.
        /// </summary>
        public static ColorPaletteIndex PaletteA { get { return SpriteColorPaletteIndex.SpritePalette2; } }
        /// <summary>
        /// Gets the palette b.
        /// </summary>
        public static ColorPaletteIndex PaletteB { get { return SpriteColorPaletteIndex.SpritePalette3; } }
        /// <summary>
        /// Gets the palette c.
        /// </summary>
        public static ColorPaletteIndex PaletteC { get { return SpriteColorPaletteIndex.SpritePalette4; } }
        /// <summary>
        /// Gets the palette d.
        /// </summary>
        public static ColorPaletteIndex PaletteD { get { return SpriteColorPaletteIndex.SpritePalette5; } }
        /// <summary>
        /// Gets the palette e.
        /// </summary>
        public static ColorPaletteIndex PaletteE { get { return SpriteColorPaletteIndex.SpritePalette6; } }
        /// <summary>
        /// Gets the palette f.
        /// </summary>
        public static ColorPaletteIndex PaletteF { get { return SpriteColorPaletteIndex.SpritePalette7; } }
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public int Offset { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPaletteIndex"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        protected ColorPaletteIndex(int value, int offset) : base(value)
        {
            Offset = offset;
        }
    }
}
