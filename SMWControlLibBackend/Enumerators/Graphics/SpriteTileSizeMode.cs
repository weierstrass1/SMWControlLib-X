using SMWControlLibUtils;

namespace SMWControlLibSNES.Enumerators.Graphics
{
    /// <summary>
    /// The sprite tile size mode.
    /// </summary>
    public class SpriteTileSizeMode : FakeEnumerator
    {
        /// <summary>
        /// Gets the mode default.
        /// </summary>
        public static SpriteTileSizeMode ModeDefault => Mode0;
        public static SpriteTileSizeMode Mode0 =
            new SpriteTileSizeMode(SpriteTileSize.Size8x8, SpriteTileSize.Size16x16, 0);
        public static SpriteTileSizeMode Mode1 =
            new SpriteTileSizeMode(SpriteTileSize.Size8x8, SpriteTileSize.Size32x32, 1);
        public static SpriteTileSizeMode Mode2 =
            new SpriteTileSizeMode(SpriteTileSize.Size8x8, SpriteTileSize.Size64x64, 2);
        public static SpriteTileSizeMode Mode3 =
            new SpriteTileSizeMode(SpriteTileSize.Size16x16Small, SpriteTileSize.Size32x32, 3);
        public static SpriteTileSizeMode Mode4 =
            new SpriteTileSizeMode(SpriteTileSize.Size16x16Small, SpriteTileSize.Size64x64, 4);
        public static SpriteTileSizeMode Mode5 =
            new SpriteTileSizeMode(SpriteTileSize.Size32x32Small, SpriteTileSize.Size64x64, 5);
        public static SpriteTileSizeMode Mode6 =
            new SpriteTileSizeMode(SpriteTileSize.Size16x32, SpriteTileSize.Size32x64, 6);
        public static SpriteTileSizeMode Mode7 =
            new SpriteTileSizeMode(SpriteTileSize.Size16x32, SpriteTileSize.Size32x32, 7);
        /// <summary>
        /// Gets the small size.
        /// </summary>
        public SpriteTileSize SmallSize { get; private set; }
        /// <summary>
        /// Gets the big size.
        /// </summary>
        public SpriteTileSize BigSize { get; private set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileSizeMode"/> class.
        /// </summary>
        /// <param name="smallSize">The small size.</param>
        /// <param name="bigSize">The big size.</param>
        /// <param name="value">The value.</param>
        public SpriteTileSizeMode(SpriteTileSize smallSize, SpriteTileSize bigSize, int value) : base(value)
        {
            SmallSize = smallSize;
            BigSize = bigSize;
        }
    }
}
