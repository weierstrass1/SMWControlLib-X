using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Factory;
using SMWControlLibCommons.Graphics.DirtyClasses;
using SMWControlLibRendering.Colors;
using SMWControlLibSNES.Graphics.DirtyClasses;

namespace SMWControlLibSNES.Factory.Graphics
{
    /// <summary>
    /// The dirty s n e s sprite tile factory.
    /// </summary>
    public class DirtySNESSpriteTileFactory : DirtyTileFactory<byte, ColorR5G5B5>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>A DirtyTile.</returns>
        public override DirtyTile<byte, ColorR5G5B5> GenerateObject(params object[] args)
        {
            return new DirtySNESSpriteTile((TileSize)args[0], (TileIndex)args[1]);
        }
    }
}
