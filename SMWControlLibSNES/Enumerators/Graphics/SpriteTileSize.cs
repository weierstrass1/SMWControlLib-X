using SMWControlLibCommons.Enumerators.Graphics;

namespace SMWControlLibSNES.Enumerators.Graphics
{
    /// <summary>
    /// Enumerator for possible OAM tile sizes.
    /// </summary>
    public class SpriteTileSize : TileSize
    {
        public static SpriteTileSize Size8x8 = new SpriteTileSize(8, 8, "00");
        public static SpriteTileSize Size16x16 = new SpriteTileSize(16, 16, "02");
        public static SpriteTileSize Size32x32 = new SpriteTileSize(32, 32, "02");
        public static SpriteTileSize Size64x64 = new SpriteTileSize(64, 64, "02");
        public static SpriteTileSize Size16x16Small = new SpriteTileSize(16, 16, "00");
        public static SpriteTileSize Size32x32Small = new SpriteTileSize(32, 32, "00");
        public static SpriteTileSize Size16x32 = new SpriteTileSize(16, 32, "00");
        public static SpriteTileSize Size32x64 = new SpriteTileSize(32, 64, "02");

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileSize"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="stringValue">The string value.</param>
        protected SpriteTileSize(int width, int height, string stringValue) : base(width, height, stringValue)
        {
        }
    }
}
