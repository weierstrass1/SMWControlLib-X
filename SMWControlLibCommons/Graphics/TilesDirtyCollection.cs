using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics.DirtyClasses;
using SMWControlLibCommons.Keys.Graphics;
using SMWControlLibRendering;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// The tiles dirty collection.
    /// </summary>
    public class TilesDirtyCollection : DirtyCollection<TileSizeIndexKey, TileSize, TileIndex, DirtyTile, Tile>
    {
    }
}
