using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics.DirtyClasses;
using SMWControlLibUtils;

namespace SMWControlLibCommons.Factory
{
    /// <summary>
    /// The dirty tile factory.
    /// </summary>
    public class DirtyTileFactory : ObjectFactoryWithObjsParams<DirtyTile>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>A DirtyTile.</returns>
        public override DirtyTile GenerateObject(params object[] args)
        {
            return new DirtyTile((TileSize)args[0], (TileIndex)args[1]);
        }
    }
}
