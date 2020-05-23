using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics.DirtyClasses;

namespace SMWControlLibSNES.Graphics.DirtyClasses
{
    /// <summary>
    /// The dirty sprite tile.
    /// </summary>
    public class DirtySNESSpriteTile : DirtyTile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DirtySNESSpriteTile"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="index">The index.</param>
        public DirtySNESSpriteTile(TileSize size, TileIndex index) : base(new SNESSpriteTile(size, index))
        {
        }
    }
}
