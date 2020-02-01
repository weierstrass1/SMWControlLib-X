using SMWControlLibBackend.DataStructs;
using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Interfaces.Graphics;
using SMWControlLibRendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The sprite tile grid.
    /// </summary>
    public class SpriteTileGrid<T> where T: BitmapBuffer, new()
    {
        /// <summary>
        /// Gets the side.
        /// </summary>
        public int Side => Zoom * 255;
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public IGridDrawable Target { get; set; }
        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public uint BackgroundColor { get; set; }
        /// <summary>
        /// Gets or sets the grid color.
        /// </summary>
        public uint GridColor { get; set; }
        /// <summary>
        /// Gets or sets the grid color.
        /// </summary>
        public uint SelectionColor { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether draw grid.
        /// </summary>
        public bool DrawGrid { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether draw guidelines.
        /// </summary>
        public bool DrawGuidelines { get; set; }
        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        public Zoom Zoom { get; set; }
        /// <summary>
        /// Gets or sets the cell size.
        /// </summary>
        public GridCellSize CellSize { get; set; }
        /// <summary>
        /// Gets or sets the grid type.
        /// </summary>
        public GridType GridType { get; set; }
        private T image;
        private ITileCollection tileSelection;
        private bool moved = false;
        private bool selectionChanged = false;
        private bool requireRefresh = true;
        /// <summary>
        /// Gets a value indicating whether changed.
        /// </summary>
        public bool Changed { get; private set; } = true;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileGrid"/> class.
        /// </summary>
        public SpriteTileGrid()
        {
        }

        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <returns>An array of uint.</returns>
        public uint[] GetGraphics()
        {
            Changed = false;
            int s = Side;
            int s2 = s * s;
            if (image == null)
            {
                image = BitmapBuffer.CreateInstance<T>(new uint[s2], s);
                Changed = true;
            }
            else if(image.Length != s2)
            {
                image.SetPixels(new uint[s2], s);
                Changed = true;
            }

            if (requireRefresh || moved)
            {
                if (Target != null)
                {
                    T t = (T)Target.GetGraphics(Zoom);
                    if (t != null)
                        image.DrawBitmap(t, Target.Left * Zoom, Target.Top * Zoom, BackgroundColor);
                    else
                        image.FillColor(BackgroundColor);
                }
                else
                {
                    image.FillColor(BackgroundColor);
                }
                Changed = true;
            }

            if (DrawGrid && (requireRefresh || moved)) 
            {
                image.DrawGrid(Zoom, CellSize, GridType, GridColor);
                Changed = true;
            }

            if (DrawGuidelines && (requireRefresh || moved))
            {
                int guideRectSize = 16 * Zoom;
                int guideRectOffset = 112 * Zoom;
                image.DrawRectangle(guideRectOffset, guideRectOffset, guideRectSize, guideRectSize, SelectionColor);
                int guideLineOffset = 120 * Zoom;
                image.DrawLine(guideLineOffset, 0, guideLineOffset, Side, SelectionColor);
                image.DrawLine(0, guideLineOffset, Side, guideLineOffset, SelectionColor);
                Changed = true;
            }
            requireRefresh = false;

            T im = image;
            if (selectionChanged || moved)
            {
                if (tileSelection != null)
                {
                    List<TileBorder> rects = tileSelection.GetTileBorders();
                    if (rects.Count != 0)
                    {
                        im = (T)image.Clone();

                        Parallel.ForEach(rects, r =>
                        {
                            im.DrawRectangle(r.X * Zoom, r.Y * Zoom, r.Width * Zoom, r.Height * Zoom, SelectionColor);
                        });

                    }
                    selectionChanged = false;
                }
                Changed = true;
            }
            moved = false;

            return im.Pixels;
        }
        /// <summary>
        /// Addings the at position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="selection">The selection.</param>
        public void AddingAtPosition(int x, int y, SpriteTileMaskCollection<T> selection)
        {
            if (Target == null || selection == null || selection.Count <= 0)
            {
                return;
            }

            x /= Zoom;
            x -= x % CellSize;
            y /= Zoom;
            y -= y % CellSize;
            SpriteTileMaskCollection<T> sel = selection.Clone();
            sel.MoveTo(x, y);
            Target.AddTiles(sel);
            requireRefresh = true;
        }
        /// <summary>
        /// Removes the.
        /// </summary>
        public void Remove()
        {
            if (Target != null)
            {
                Target.RemoveTiles();
                requireRefresh = true;
            }
        }
        /// <summary>
        /// Selects the.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void Select(int x, int y, int width, int height)
        {
            if (Target != null)
            {
                tileSelection = Target.Select(x / Zoom, y / Zoom, width / Zoom, height / Zoom);
                selectionChanged = selectionChanged || tileSelection.IsEmpty();
            }
        }
        /// <summary>
        /// Moves the to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public bool MoveTo(int x, int y)
        {
            if (Target != null)
            {
                int s = Side;
                if (x + Target.Width > s) x = s - Target.Width;
                if (y + Target.Height > s) y = s - Target.Height;
                if (x < 0) x = 0;
                if (y < 0) y = 0;

                x /= Zoom;
                x -= x % CellSize;
                y /= Zoom;
                y -= y % CellSize;

                int drx = Target.Left * Zoom;
                int dry = Target.Top * Zoom;
                int drw = Target.Width * Zoom;
                int drh = Target.Height * Zoom;

                bool b = Target.MoveTiles(x, y);
                if (b)
                {
                    image.FillColor(drx, dry, drw, drh, BackgroundColor);
                }

                moved = moved || b;
                return b;
            }
            return false;
        }
        /// <summary>
        /// Increases the z index.
        /// </summary>
        public void IncreaseZIndex()
        {
            if (Target != null)
                selectionChanged = selectionChanged || Target.IncreaseZIndex();
        }
        /// <summary>
        /// Decreases the z index.
        /// </summary>
        public void DecreaseZIndex()
        {
            if (Target != null)
                selectionChanged = selectionChanged || Target.DecreaseZIndex();
        }
        /// <summary>
        /// Clears the tile selection.
        /// </summary>
        public void ClearTileSelection()
        {
            if (tileSelection != null)
            {
                tileSelection = null;
                selectionChanged = true;
            }
        }
        /// <summary>
        /// Gets the x offset.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>An int.</returns>
        public int GetXOffset(int x)
        {
            int xdivz = x / Zoom;
            if (xdivz > tileSelection.Right) return -1;
            return xdivz - tileSelection.Left;
        }
        /// <summary>
        /// Gets the x offset.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>An int.</returns>
        public int GetYOffset(int y)
        {
            int ydivz = y / Zoom;
            if (ydivz > tileSelection.Bottom) return -1;
            return ydivz - tileSelection.Top;
        }
    }
}
