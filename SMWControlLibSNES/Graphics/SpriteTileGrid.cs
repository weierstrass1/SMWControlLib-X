using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics;

namespace SMWControlLibSNES.Graphics
{
    /// <summary>
    /// The sprite tile grid.
    /// </summary>
    public class SpriteTileGrid : TileGrid
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileGrid"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public SpriteTileGrid(int width, int height, Zoom zoom, byte bgR, byte bgG, byte bgB) : base(width, height, zoom, bgR, bgG, bgB)
        {
        }
    }
}
