using SMWControlLibCommons.Graphics;
using SMWControlLibUtils;

namespace SMWControlLibCommons.Factory
{
    /// <summary>
    /// The tile dirty collection factory.
    /// </summary>
    public class TileDirtyCollectionFactory : ObjectFactoryWithObjsParams<TilesDirtyCollection>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>A TilesDirtyCollection.</returns>
        public override TilesDirtyCollection GenerateObject(params object[] args)
        {
            return new TilesDirtyCollection();
        }
    }
}
