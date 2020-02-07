using SMWControlLibCommons.Graphics;

namespace SMWControlLibSNES.Graphics
{
    /// <summary>
    /// The sprite tile mask.
    /// </summary>
    public class SpriteTileMask : TileMask
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileMask"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="tile">The tile.</param>
        /// <param name="props">The props.</param>
        public SpriteTileMask(int x, int y, Tile tile, TileProperties props) : base(x, y, tile, props)
        {
        }
    }
}
