using SMWControlLibCommons.Graphics.DirtyClasses;
using SMWControlLibRendering.Colors;
using SMWControlLibCommons.Enumerators.Graphics;

namespace SMWControlLibSNES.Graphics.DirtyClasses
{
    /// <summary>
    /// The dirty sprite tile.
    /// </summary>
    public class DirtySNESSpriteTile : DirtyTile<byte, ColorR5G5B5>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirtySNESSpriteTile"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="index">The index.</param>
        public DirtySNESSpriteTile(TileSize size, TileIndex index) : base(size, index)
        {
        }
    }
}
