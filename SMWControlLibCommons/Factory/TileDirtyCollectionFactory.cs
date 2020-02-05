using SMWControlLibCommons.Graphics;
using SMWControlLibUtils;

namespace SMWControlLibCommons.Factory
{
    /// <summary>
    /// The tile dirty collection factory.
    /// </summary>
    public class TileDirtyCollectionFactory<T, U> : ObjectFactoryWithObjsParams<TilesDirtyCollection<T, U>> where T : struct
                                                                                                            where U : struct
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>A TilesDirtyCollection.</returns>
        public override TilesDirtyCollection<T, U> GenerateObject(params object[] args)
        {
            return new TilesDirtyCollection<T, U>();
        }
    }
}
