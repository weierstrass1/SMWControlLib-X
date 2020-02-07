﻿using SMWControlLibRendering;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Keys.Graphics;
using SMWControlLibCommons.Graphics.DirtyClasses;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// The tiles dirty collection.
    /// </summary>
    public class TilesDirtyCollection : DirtyCollection<TileSizeIndexKey, TileSize, TileIndex, DirtyTile, Tile>
    {
    }
}