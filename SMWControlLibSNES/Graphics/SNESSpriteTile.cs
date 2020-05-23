using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics;

namespace SMWControlLibSNES.Graphics
{
    /// <summary>
    /// Represent an OAM tile of the SNES.
    /// </summary>
    public class SNESSpriteTile : Tile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SNESSpriteTile"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="index">The index.</param>
        public SNESSpriteTile(TileSize size, TileIndex index) : base(size, index)
        {
        }
    }
}
