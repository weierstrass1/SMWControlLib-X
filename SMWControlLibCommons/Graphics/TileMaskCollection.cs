using SMWControlLibCommons.DataStructs;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibRendering;
using SMWControlLibRendering.DirtyClasses;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SMWControlLibCommons.Interfaces.Graphics;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// The sprite tile mask collection.
    /// </summary>
    public class TileMaskCollection<T, U> : ITileCollection where T : struct
                                                            where U : struct
    {
        private List<TileMask<T, U>> tiles;
        /// <summary>
        /// Gets the count.
        /// </summary>
        public int Count => tiles.Count;
        public event Action<TileMaskCollection<T, U>, TileMaskCollection<T, U>> OnCollectionAdded, OnSelectionChanged, OnSelectionZIndexChanged;
        public event Action<TileMaskCollection<T, U>> OnSelectionClear;
        public event Action<TileMaskCollection<T, U>, TileMask<T, U>> OnTileAdded;
        public event Action<TileMaskCollection<T, U>, int, int> OnMoveTo;
        /// <summary>
        /// Gets a value indicating whether require refresh.
        /// </summary>
        public bool RequireRefresh { get; private set; }
        private bool sorted = true;
        private TileMaskCollection<T, U> selection;
        /// <summary>
        /// Gets the left.
        /// </summary>
        public int Left { get; private set; }
        /// <summary>
        /// Gets the top.
        /// </summary>
        public int Top { get; private set; }
        /// <summary>
        /// Gets the right.
        /// </summary>
        public int Right { get; private set; }
        /// <summary>
        /// Gets the bottom.
        /// </summary>
        public int Bottom { get; private set; }
        public ConcurrentDictionary<Zoom, DirtyBitmap<U>> bitmaps;
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width => Right - Left;
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height => Bottom - Top;
        /// <summary>
        /// Initializes a new instance of the <see cref="TileMaskCollection"/> class.
        /// </summary>
        public TileMaskCollection()
        {
            tiles = new List<TileMask<T, U>>();
            bitmaps = new ConcurrentDictionary<Zoom, DirtyBitmap<U>>();
            Left = -1;
            Top = -1;
            Right = -1;
            Bottom = -1;
            RequireRefresh = true;
        }
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>An array of uint.</returns>
        public BitmapBuffer<U> GetGraphics(Zoom z)
        {
            if (Width == 0 || Height == 0) 
                return null;

            if (RequireRefresh)
            {
                RequireRefresh = false;
                _ = Parallel.ForEach(bitmaps, b =>
                  {
                      b.Value.SetDirty(true);
                  });
            }

            int lenght = Width * Height * z * z;

            if (!bitmaps.ContainsKey(z))
            {
                bitmaps.TryAdd(z, new DirtyBitmap<U>(BitmapBuffer<U>.CreateInstance(new U[lenght], Width * z)));
            }

            DirtyBitmap<U> d = bitmaps[z];
            if (d.IsDirty)
            {
                if (d.Bitmap.Length < lenght)
                    d.Bitmap = BitmapBuffer<U>.CreateInstance(new U[lenght], Width * z);

                foreach (TileMask<T, U> t in tiles)
                {
                    d.Bitmap.DrawBitmapBuffer(t.GetGraphics(z), (t.X - Left) * z, (t.Y - Top) * z);
                }
                d.SetDirty(false);
            }

            return bitmaps[z].Bitmap;
        }
        /// <summary>
        /// Decreases the selection z index.
        /// </summary>
        public bool DecreaseSelectionZIndex()
        {
            selection.Sort();

            uint selMinZ = selection.tiles.First().Z;
            uint selMaxZ = selection.tiles.Last().Z;
            TileMask<T, U> MinZTile = tiles.FirstOrDefault(t =>
            {
                return t.Z < selMinZ && !selection.tiles.Contains(t);
            });

            if (MinZTile != default(TileMask<T, U>))
            {
                var upperTiles = tiles.Where(t =>
                {
                    return t.Z < MinZTile.Z && !selection.tiles.Contains(t);
                });

                uint i = MinZTile.Z - 1;

                foreach (TileMask<T, U> t in selection.tiles)
                {
                    t.Z = i;
                    i--;
                }

                foreach (TileMask<T, U> t in upperTiles)
                {
                    t.Z = i;
                    i--;
                }
                sorted = false;
                Sort();
                OnSelectionZIndexChanged?.Invoke(this, selection);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Increases the selection z index.
        /// </summary>
        public bool IncreaseSelectionZIndex()
        {
            selection.Sort();

            uint selMinZ = selection.tiles.First().Z;
            uint selMaxZ = selection.tiles.Last().Z;
            TileMask<T, U> MaxZTile = tiles.FirstOrDefault(t =>
            {
                return t.Z > selMinZ && !selection.tiles.Contains(t);
            });

            if (MaxZTile != default(TileMask<T, U>))
            {
                var upperTiles = tiles.Where(t =>
                {
                    return t.Z > MaxZTile.Z && !selection.tiles.Contains(t);
                });

                uint i = MaxZTile.Z + 1;

                foreach (TileMask<T, U> t in selection.tiles)
                {
                    t.Z = i;
                    i++;
                }

                foreach (TileMask<T, U> t in upperTiles)
                {
                    t.Z = i;
                    i++;
                }

                sorted = false;
                Sort();
                OnSelectionZIndexChanged?.Invoke(this, selection);
                return true;
            }
            return false;
        }
        /// <summary>
        /// Moves the selection.
        /// </summary>
        /// <param name="newX">The new x.</param>
        /// <param name="newY">The new y.</param>
        public bool MoveSelection(int newX, int newY)
        {
            if (selection == null) selection = new TileMaskCollection<T, U>();
            if (selection.Left == newX && selection.Top == newY) return false;
            bool b = selection.MoveTo(newX, newY);
            UpdateContainer();
            RequireRefresh = true;
            OnSelectionChanged?.Invoke(this, selection);
            return b;
        }
        /// <summary>
        /// Updates the container.
        /// </summary>
        public void UpdateContainer()
        {
            UpdateLeft();
            UpdateRight();
            UpdateTop();
            UpdateBottom();
        }
        /// <summary>
        /// Updates the left.
        /// </summary>
        public void UpdateLeft()
        {
            if (tiles.Count > 0)
                Left = tiles.Min(ti => ti.X);
        }
        /// <summary>
        /// Updates the right.
        /// </summary>
        public void UpdateRight()
        {
            if (tiles.Count > 0)
                Right = tiles.Max(ti => ti.X + ti.Width);
        }
        /// <summary>
        /// Updates the top.
        /// </summary>
        public void UpdateTop()
        {
            if (tiles.Count > 0)
                Top = tiles.Min(ti => ti.Y);
        }
        /// <summary>
        /// Updates the bottom.
        /// </summary>
        public void UpdateBottom()
        {
            if (tiles.Count > 0)
                Bottom = tiles.Max(ti => ti.Y + ti.Height);
        }
        /// <summary>
        /// Clears the selection.
        /// </summary>
        public void ClearSelection()
        {
            if (selection == null) selection = new TileMaskCollection<T, U>();
            selection.tiles.Clear();
            selection.Left = -1;
            selection.Top = -1;
            selection.Right = -1;
            selection.Bottom = -1;
            OnSelectionClear?.Invoke(this);
        }

        /// <summary>
        /// Moves the to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public bool MoveTo(int x, int y)
        {
            int minX = Left;

            int minY = Top;

            var query = tiles.AsParallel();
            query.ForAll(t =>
            {
                int delta = Math.Abs(t.X - minX);
                t.X = x + delta;
                delta = Math.Abs(t.Y - minY);
                t.Y = y + delta;
            });
            int d = Right - Left;
            Left = x;
            Right = Left + d;
            d = Bottom - Top;
            Top = y;
            Bottom = Top + d;
            OnMoveTo?.Invoke(this, x, y);
            return true;
        }
        /// <summary>
        /// Finds the by area.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="width">The width.</param>
        /// <param name="y">The y.</param>
        /// <param name="height">The height.</param>
        public TileMaskCollection<T, U> FindByArea(int x, int y, int width, int height)
        {
            if (selection == null) selection = new TileMaskCollection<T, U>();
            selection.tiles = tiles.Where((t) =>
            {
                if (t.X + t.Width < x) return false;
                if (x + width < t.X) return false;
                if (t.Y + t.Height < y) return false;
                if (y + height < t.Y) return false;

                return true;
            }).ToList();
            selection.sorted = false;
            selection.UpdateContainer();
            OnSelectionChanged?.Invoke(this, selection);
            return selection;
        }
        /// <summary>
        /// Finds the by position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public TileMaskCollection<T, U> FindByPosition(int x, int y)
        {
            ClearSelection();

            TileMask<T, U> tm = tiles.FirstOrDefault((t) =>
            {
                if (t.X <= x && t.X + t.Width >= x
                    && t.Y <= y && t.Y + t.Height >= y)
                    return true;

                return false;
            });

            if (tm == default) return selection;

            selection.Add(tm);
            OnSelectionChanged?.Invoke(this, selection);
            return selection;
        }
        /// <summary>
        /// Removes the selection.
        /// </summary>
        public void RemoveSelection()
        {
            if (selection == null) selection = new TileMaskCollection<T, U>();
            if (selection.Left > 0)
            {
                foreach (TileMask<T, U> t in selection.tiles)
                {
                    tiles.Remove(t);
                }

                UpdateContainer();

                RequireRefresh = true;
                ClearSelection();
            }
        }
        /// <summary>
        /// Adds the collection.
        /// </summary>
        /// <param name="col">The col.</param>
        public void AddCollection(TileMaskCollection<T, U> col)
        {
            ClearSelection();
            col.Sort();
            foreach (TileMask<T, U> t in col.tiles)
            {
                Add(t);
            }
            RequireRefresh = true;
            OnCollectionAdded?.Invoke(this, col);
        }
        /// <summary>
        /// Adds the.
        /// </summary>
        /// <param name="tile">The tile.</param>
        public void Add(TileMask<T, U> tile)
        {
            if (Count > 0)
                tile.Z = tiles.Last().Z + 1;
            else
                tile.Z = 0;

            tiles.Add(tile);

            if (tile.X < Left || Left < 0)
                Left = tile.X;
            if (tile.X + tile.Width > Right)
                Right = tile.X + tile.Width;
            if (tile.Y < Top || Top < 0)
                Top = tile.Y;
            if (tile.Y + tile.Height > Bottom)
                Bottom = tile.Y + tile.Height;

            RequireRefresh = true;
            OnTileAdded?.Invoke(this, tile);
        }
        /// <summary>
        /// Sorts the.
        /// </summary>
        public void Sort()
        {
            if (!sorted)
            {
                tiles.Sort();
                sorted = true;
                RequireRefresh = true;
            }
        }
        public TileMask<T, U> this[int index]
        {
            get
            {
                return tiles[index];
            }
        }
        /// <summary>
        /// Fusions the.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>A SpriteTileMaskCollection.</returns>
        public static TileMaskCollection<T, U> Fusion(params TileMaskCollection<T, U>[] args)
        {
            if (args.Length == 1)
                return args[0];
            if (args.Length == 2)
                return Fusion(args[0], args[1]);

            Parallel.ForEach(args, col =>
            {
                col.Sort();
            });

            TileMaskCollection<T, U>[] objs = new TileMaskCollection<T, U>[(args.Length >> 1) + (args.Length % 2)];
            TileMaskCollection<T, U>[] res = args;
            TileMaskCollection<T, U>[] aux = null;
            while (objs.Length > 1)
            {
                Parallel.For(0, res.Length >> 1, i =>
                {
                    int j = i << 1;
                    objs[i] = Fusion(res[j], res[j + 1]);
                });
                if (res.Length % 2 == 1) objs[objs.Length - 1] = res[res.Length - 1];

                aux = objs;
                objs = new TileMaskCollection<T, U>[(res.Length >> 1) + (res.Length % 2)];
                res = aux;
            }

            return objs[0];
        }
        /// <summary>
        /// Fusions the.
        /// </summary>
        /// <param name="c1">The c1.</param>
        /// <param name="c2">The c2.</param>
        /// <returns>A SpriteTileMaskCollection.</returns>
        public static TileMaskCollection<T, U> Fusion(TileMaskCollection<T, U> c1, TileMaskCollection<T, U> c2)
        {
            c1.Sort();
            c2.Sort();

            var vals1 = c1.tiles;
            var vals2 = c2.tiles;

            TileMaskCollection<T, U> ret = new TileMaskCollection<T, U>();

            int total = vals1.Count + vals2.Count;
            var enum1 = vals1.GetEnumerator();
            var enum2 = vals2.GetEnumerator();
            enum1.MoveNext();
            enum2.MoveNext();

            for (int i = 0; i < total; i++)
            {
                if (enum2.Current == null ||
                    (enum1.Current != null && enum1.Current.Z <= enum2.Current.Z))
                {
                    ret.tiles.Add(enum1.Current);
                    enum1.MoveNext();
                }
                else if (enum2.Current != null)
                {
                    ret.tiles.Add(enum2.Current);
                    enum2.MoveNext();
                }
            }

            ret.sorted = true;
            ret.Left = Math.Min(c1.Left, c2.Left);
            if (ret.Left < 0) ret.Left = Math.Max(c1.Left, c2.Left);
            ret.Right = Math.Max(c1.Right, c2.Right);
            ret.Top = Math.Min(c1.Top, c2.Top);
            if (ret.Top < 0) ret.Top = Math.Max(c1.Top, c2.Top);
            ret.Bottom = Math.Max(c1.Bottom, c2.Bottom);

            return ret;
        }

        /// <summary>
        /// Adds the tiles.
        /// </summary>
        /// <param name="tiles">The tiles.</param>
        public void AddTiles(ITileCollection tiles)
        {
            AddCollection((TileMaskCollection<T, U>)tiles);
        }

        /// <summary>
        /// Removes the tiles.
        /// </summary>
        public void RemoveTiles()
        {
            RemoveSelection();
        }

        public static explicit operator TileMask<T, U>[](TileMaskCollection<T, U> c)
        {
            return c.tiles.ToArray();
        }

        /// <summary>
        /// Clones the.
        /// </summary>
        /// <returns>A SpriteTileMaskCollection.</returns>
        public TileMaskCollection<T, U> Clone()
        {
            TileMaskCollection<T, U> col = new TileMaskCollection<T, U>();
            foreach (TileMask<T, U> t in tiles)
            {
                col.Add(t.Clone());
            }
            return col;
        }

        /// <summary>
        /// Gets the tile borders.
        /// </summary>
        /// <returns>A list of TileBorders.</returns>
        public List<TileBorder> GetTileBorders()
        {
            List<TileBorder> ret = new List<TileBorder>();

            if (tiles != null && tiles.Count > 0) 
            {
                foreach (TileMask<T, U> t in tiles)
                {
                    ret.Add(t.Border);
                }
            }

            return ret;
        }

        /// <summary>
        /// Are the empty.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool IsEmpty()
        {
            return tiles != null && tiles.Count > 0;
;        }
    }
}
