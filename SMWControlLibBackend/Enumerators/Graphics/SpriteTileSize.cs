using SMWControlLibUtils;

namespace SMWControlLibBackend.Enumerators.Graphics
{
    /// <summary>
    /// Enumerator for possible OAM tile sizes.
    /// </summary>
    public class SpriteTileSize : FakeEnumerator
    {
        /// <summary>
        /// Value used on SNES to represent that size
        /// </summary>
        public string SnesValue { get; private set; }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get
            {
                return Value;
            }
        }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        public static SpriteTileSize Size8x8 = new SpriteTileSize(8, 8, "00");
        public static SpriteTileSize Size16x16 = new SpriteTileSize(16, 16, "02");
        public static SpriteTileSize Size32x32 = new SpriteTileSize(32, 32, "02");
        public static SpriteTileSize Size64x64 = new SpriteTileSize(64, 64, "02");
        public static SpriteTileSize Size16x16Small = new SpriteTileSize(16, 16, "00");
        public static SpriteTileSize Size32x32Small = new SpriteTileSize(32, 32, "00");
        public static SpriteTileSize Size16x32 = new SpriteTileSize(16, 32, "00");
        public static SpriteTileSize Size32x64 = new SpriteTileSize(32, 64, "02");
        /// <summary>
        /// Prevents a default instance of the <see cref="SpriteTileSize"/> class from being created.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="snesValue">The snes value.</param>
        private SpriteTileSize(int width, int height, string snesValue) : base(width)
        {
            SnesValue = snesValue;
            Height = height;
        }
    }
}
