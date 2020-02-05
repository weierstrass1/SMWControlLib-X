using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibUtils;

namespace SMWControlLibCommons.Keys.Graphics
{
    /// <summary>
    /// The tile size index key.
    /// </summary>
    public class TileSizeIndexKey : DualKey<TileSize, TileIndex>
    {
        /// <summary>
        /// Gets the size.
        /// </summary>
        public TileSize Size => element1;
        /// <summary>
        /// Gets the index.
        /// </summary>
        public TileIndex Index => element2;
        /// <summary>
        /// Initializes a new instance of the <see cref="TileSizeIndexKey"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="index">The index.</param>
        public TileSizeIndexKey(TileSize size, TileIndex index) : base(size, index)
        {
        }
    }
}
