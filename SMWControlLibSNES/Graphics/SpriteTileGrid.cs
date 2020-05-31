using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics;
using SMWControlLibRendering.Enumerator;

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
        public SpriteTileGrid(int width, int height, Zoom zoom, params byte[] bgColors) : base(width, height, zoom, BytesPerPixel.RGB555, bgColors)
        {
        }
    }
}
