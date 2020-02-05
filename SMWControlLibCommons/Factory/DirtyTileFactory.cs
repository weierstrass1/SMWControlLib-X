using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics.DirtyClasses;
using SMWControlLibUtils;

namespace SMWControlLibCommons.Factory
{
    /// <summary>
    /// The dirty tile factory.
    /// </summary>
    public class DirtyTileFactory<T, U> : ObjectFactoryWithObjsParams<DirtyTile<T, U>>  where T : struct
                                                                                        where U : struct
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>A DirtyTile.</returns>
        public override DirtyTile<T, U> GenerateObject(params object[] args)
        {
            return new DirtyTile<T, U>((TileSize)args[0], (TileIndex)args[1]);
        }
    }
}
