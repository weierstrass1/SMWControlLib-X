using SMWControlLibRendering.Colors;
using SMWControlLibCommons.Graphics;
using SMWControlLibCommons.Enumerators.Graphics;

namespace SMWControlLibSNES.Graphics
{
    /// <summary>
    /// Represent an OAM tile of the SNES.
    /// </summary>
    public class SNESSpriteTile : Tile<byte, ColorA1R5G5B5>
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
