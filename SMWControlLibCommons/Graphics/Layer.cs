using SMWControlLibCommons.DataStructs;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Interfaces.Graphics;
using SMWControlLibCommons.Keys;
using SMWControlLibRendering;
using SMWControlLibRendering.Enumerator;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// The layer.
    /// </summary>
    public class Layer : IGridDrawable
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public int X { get; protected set; }

        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public int Y { get; protected set; }

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width { get; protected set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        /// Gets or sets the left.
        /// </summary>
        public int Left { get; protected set; }

        /// <summary>
        /// Gets or sets the top.
        /// </summary>
        public int Top { get; protected set; }

        /// <summary>
        /// Gets or sets the right.
        /// </summary>
        public int Right { get; protected set; }

        /// <summary>
        /// Gets or sets the bottom.
        /// </summary>
        public int Bottom { get; protected set; }

        /// <summary>
        /// Gets or sets a value indicating that in each position, the layer can have more than one tile.
        /// </summary>
        public bool MultiTilePerPosition { get; protected set; }

        /// <summary>
        /// Gets or sets the cell size.
        /// </summary>
        public GridCellSize CellSize { get; protected set; }
        public BytesPerPixel BytesPerColor { get; protected set; }

        private readonly Dictionary<PositionKey, List<TileMask>> tiles;
        public TileMaskCollection selection;

        /// <summary>
        /// Initializes a new instance of the <see cref="Layer"/> class.
        /// </summary>
        public Layer(BytesPerPixel bytesPerColor)
        {
            BytesPerColor = bytesPerColor;
            tiles = new Dictionary<PositionKey, List<TileMask>>();
        }

        /// <summary>
        /// Adds the tiles.
        /// </summary>
        /// <param name="tls">The tls.</param>
        public void AddTiles(ITileCollection tls)
        {
            if (tls == null) throw new ArgumentNullException(nameof(tls));
            List<TileMask> ts = (List<TileMask>)tls.GetEnumerable();
            int px;
            int py;
            PositionKey pk;
            foreach (TileMask t in ts)
            {
                px = t.X / CellSize;
                py = t.Y / CellSize;
                if (px >= 0 && py >= 0 && px < Width && py < Height)
                {
                    pk = new PositionKey((uint)px, (uint)py);
                    if (!tiles.ContainsKey(pk))
                    {
                        tiles.Add(pk, new List<TileMask>());
                    }
                    tiles[pk].Add(t);
                }
            }
        }

        /// <summary>
        /// Decreases the z index.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool DecreaseZIndex()
        {
            return false;
        }

        /// <summary>
        /// Gets the enumerable.
        /// </summary>
        /// <returns>An IEnumerable.</returns>
        public IEnumerable<ITile> GetEnumerable()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>A BitmapBuffer.</returns>
        public BitmapBuffer GetGraphics(Zoom z)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets the tile borders.
        /// </summary>
        /// <returns>A list of TileBorders.</returns>
        public List<TileBorder> GetTileBorders()
        {
            List<TileBorder> ret = new List<TileBorder>();
            if (selection == null) return ret;

            TileBorder tb = new TileBorder(selection.Left, selection.Top, selection.Width, selection.Height);
            ret.Add(tb);
            return ret;
        }

        /// <summary>
        /// Increases the z index.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool IncreaseZIndex()
        {
            return false;
        }

        /// <summary>
        /// Are the empty.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool IsEmpty()
        {
            return tiles.Count <= 0;
        }

        /// <summary>
        /// Moves the tiles.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <returns>A bool.</returns>
        public bool MoveTiles(int x, int y)
        {
            if (selection == null) return false;
            TileMaskCollection sel = selection;
            RemoveTiles();
            sel.MoveTo(x, y);
            AddTiles(sel);
            selection = sel;
            return true;
        }

        /// <summary>
        /// Removes the tiles.
        /// </summary>
        public void RemoveTiles()
        {
            if (selection == null) return;
            uint px;
            uint py;
            PositionKey pk;
            List<TileMask> tml;
            foreach (TileMask t in selection.GetEnumerable())
            {
                px = (uint)t.X;
                py = (uint)t.Y;

                pk = new PositionKey(px, py);

                tml = tiles[pk];
                tml.Remove(t);
            }
            selection = null;
        }

        /// <summary>
        /// Selects the tiles.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>An ITileCollection.</returns>
        public ITileCollection SelectTiles(int x, int y, int width, int height)
        {
            selection = (TileMaskCollection)TilesOnArea(x, y, width, height);
            return selection;
        }

        /// <summary>
        /// Tiles the on area.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <returns>An ITileCollection.</returns>
        public ITileCollection TilesOnArea(int x, int y, int width, int height)
        {
            uint px = (uint)(x / CellSize);
            uint py = (uint)(y / CellSize);
            int w = width / CellSize;
            int h = height / CellSize;

            if (width < 2 && height < 2)
            {
                PositionKey pk = new PositionKey(px, py);
                if (tiles.ContainsKey(pk))
                {
                    TileMaskCollection ret = new TileMaskCollection(BytesPerColor);
                    foreach (TileMask selT in tiles[pk])
                    {
                        ret.Add(selT);
                    }
                    return ret;
                }
                else
                {
                    return null;
                }
            }

            IEnumerable filtered = tiles.Where(kvp =>
            {
                if (kvp.Key.X < px) return false;
                if (kvp.Key.Y < py) return false;
                if (kvp.Key.X >= w + px) return false;
                if (kvp.Key.Y >= h + py) return false;
                return true;
            });

            TileMaskCollection t = new TileMaskCollection(BytesPerColor);
            foreach (KeyValuePair<PositionKey, List<TileMask>> kvp in filtered)
            {
                foreach (TileMask selT in kvp.Value)
                {
                    t.Add(selT);
                }
            }

            return t;
        }
    }
}