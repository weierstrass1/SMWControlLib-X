using SMWControlLibBackend.Graphics.DirtyClasses;
using SMWControlLibBackend.Enumerators.Graphics;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;
using System.Linq;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The sprite tile g f x box.
    /// </summary>
    public class SpriteTileGFXBox : GraphicBox
    {
        /// <summary>
        /// Gets the size.
        /// </summary>
        public GFXBoxSize Size { get; private set; }
        private readonly ConcurrentDictionary<SpriteTileSize, DirtySpriteTile[,]> tiles;
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileGFXBox"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        public SpriteTileGFXBox(GFXBoxSize size) : base(size.Width, size.Height)
        {
            Size = size;
            tiles = new ConcurrentDictionary<SpriteTileSize, DirtySpriteTile[,]>();
        }

        /// <summary>
        /// Selects the tiles.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="mode">The mode.</param>
        /// <param name="props">The props.</param>
        /// <returns>A SpriteTileMaskCollection.</returns>
        public SpriteTileMaskCollection SelectTiles(int x, 
                                                    int y, 
                                                    int width, 
                                                    int height, 
                                                    SpriteTileSizeMode mode,
                                                    SpriteTileProperties props)
        {
            SpriteTileMaskCollection Selection = new SpriteTileMaskCollection();

            bool onlySmall = width <= (mode.SmallSize.Width >> 3) && 
                                height <= (mode.SmallSize.Height >> 3);

            SpriteTileSize selected = mode.BigSize;

            if (onlySmall)
                selected = mode.SmallSize;

            int upi = selected.Width >> 3;
            int upj = selected.Height >> 3;
            int ilim = width - (width % upi);
            int jlim = height - (height % upj);
            bool extraColumn = (height % upj) != 0;
            int extraColumnI = width - upi;
            int extraColumnI3 = extraColumnI << 3;
            int j3;

            for (int j = 0; j < jlim; j += upj)
            {
                j3 = j << 3;
                for (int i = 0; i < ilim; i += upi)
                {
                    Selection.Add(new SpriteTileMask(i << 3, j3,
                            GetTile(selected, x + i, y + j), props));
                }
                if(extraColumn)
                    Selection.Add(new SpriteTileMask(extraColumnI3, j3,
                        GetTile(selected, x + extraColumnI, y + j), props));
            }

            bool extraRow = (width % upi) != 0;
            int extraRowJ = height - upj;
            int extraRowJ3 = extraRowJ << 3;
            if (extraRow)
            {
                for (int i = 0; i < ilim; i += upi)
                {
                    Selection.Add(new SpriteTileMask(i << 3, extraRowJ3,
                            GetTile(selected, x + i, y + extraRowJ), props));
                }
            }

            if (extraRow && extraColumn) 
            {
                if (!onlySmall && width - ilim <= mode.SmallSize.Width && height - jlim <= mode.SmallSize.Height)
                {
                    selected = mode.SmallSize;
                    upi = selected.Width >> 3;
                    upj = selected.Height >> 3;
                    extraColumnI = width - upi;
                    extraColumnI3 = extraColumnI << 3;
                    extraRowJ = height - upj;
                    extraRowJ3 = extraRowJ << 3;
                }

                Selection.Add(new SpriteTileMask(extraColumnI3, extraRowJ3,
                            GetTile(selected, x + extraColumnI, y + extraRowJ), props));
            }

            return Selection;
        }
        /// <summary>
        /// Gets the tile.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="hIndex">The h index.</param>
        /// <param name="vIndex">The v index.</param>
        /// <returns>A SpriteTile.</returns>
        public SpriteTile GetTile(SpriteTileSize size, int hIndex, int vIndex)
        {
            if (!tiles.ContainsKey(size))
            {
                tiles.AddOrUpdate(size, new DirtySpriteTile[(Size.Width >> 3) + 1 - (size.Width >> 3),
                                            (Size.Height >> 3) + 1 - (size.Height >> 3)],
                                            (a, b) => { return null; });

                tiles[size][hIndex, vIndex] = new DirtySpriteTile(size, SpriteTileIndex.GetIndex(hIndex, vIndex));
                tiles[size][hIndex, vIndex].Tile.CopyFrom(graphicsMap, hIndex << 3, vIndex << 3);
                tiles[size][hIndex, vIndex].SetDirty(false);
            }
            else 
            {
                if (tiles[size][hIndex, vIndex] == null)
                    tiles[size][hIndex, vIndex] = new DirtySpriteTile(size, SpriteTileIndex.GetIndex(hIndex, vIndex));
                if (tiles[size][hIndex, vIndex].IsDirty)
                {
                    tiles[size][hIndex, vIndex].Tile.CopyFrom(graphicsMap, hIndex << 3, vIndex << 3);
                    tiles[size][hIndex, vIndex].SetDirty(false);
                }
            }
            return tiles[size][hIndex, vIndex].Tile;
        }

        /// <summary>
        /// Copies the from.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="srcHOffset">The src h offset.</param>
        /// <param name="srcVOffset">The src v offset.</param>
        /// <param name="dstHOffset">The dst h offset.</param>
        /// <param name="dstVOffset">The dst v offset.</param>
        public override void CopyFrom(byte[,] src, int srcHOffset, int srcVOffset, int dstHOffset, int dstVOffset)
        {
            base.CopyFrom(src, srcHOffset, srcVOffset, dstHOffset, dstVOffset);

            int maxw = Math.Min((src.GetLength(0) - srcHOffset) >> 3, (Size.Width - dstHOffset) >> 3);
            int maxh = Math.Min((src.GetLength(1) - srcVOffset) >> 3, (Size.Height - dstVOffset) >> 3);
            int dstHOff = dstHOffset >> 3;
            int dstVOff = dstVOffset >> 3;

            Parallel.ForEach(tiles, kvp =>
            {
                Parallel.For(0, Math.Min(maxw,kvp.Value.GetLength(0)), i =>
                {
                    Parallel.For(0, Math.Min(maxh, kvp.Value.GetLength(1)), j =>
                    {
                        int x = dstHOff + i;
                        int y = dstVOff + j;
                        if (kvp.Value[x, y] != null)
                            kvp.Value[x, y].SetDirty(true);
                    });
                });
            });

        }
    }
}
