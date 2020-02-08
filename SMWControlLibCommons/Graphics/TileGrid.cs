using SMWControlLibCommons.DataStructs;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Interfaces.Graphics;
using SMWControlLibRendering;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// The sprite tile grid.
    /// </summary>
    public class TileGrid
    {
        /// <summary>
        /// Gets or sets the lenght.
        /// </summary>
        public int Lenght { get; protected set; }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int WidthWithZoom { get; protected set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int HeightWithZoom { get; protected set; }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width { get; protected set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int Height { get; protected set; }
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public IGridDrawable Target { get; set; }
        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public byte BackgroundColorR { get; set; }
        /// <summary>
        /// Gets or sets the background color g.
        /// </summary>
        public byte BackgroundColorG { get; set; }
        /// <summary>
        /// Gets or sets the background color b.
        /// </summary>
        public byte BackgroundColorB { get; set; }
        /// <summary>
        /// Gets or sets the grid color.
        /// </summary>
        public byte GridColorR { get; set; }
        /// <summary>
        /// Gets or sets the grid color g.
        /// </summary>
        public byte GridColorG { get; set; }
        /// <summary>
        /// Gets or sets the grid color b.
        /// </summary>
        public byte GridColorB { get; set; }
        /// <summary>
        /// Gets or sets the grid color.
        /// </summary>
        public byte SelectionColorR { get; set; }
        /// <summary>
        /// Gets or sets the selection color g.
        /// </summary>
        public byte SelectionColorG { get; set; }
        /// <summary>
        /// Gets or sets the selection color b.
        /// </summary>
        public byte SelectionColorB { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether draw grid.
        /// </summary>
        public bool DrawGrid { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether draw guidelines.
        /// </summary>
        public bool DrawGuidelines { get; set; }
        private Zoom zoom;
        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        public Zoom Zoom
        {
            get => zoom;
            set
            {
                zoom = value;
                WidthWithZoom = Width * zoom;
                HeightWithZoom = Height * zoom;
                Lenght = WidthWithZoom * HeightWithZoom;
            }
        }
        /// <summary>
        /// Gets or sets the cell size.
        /// </summary>
        public GridCellSize CellSize { get; set; }
        /// <summary>
        /// Gets or sets the grid type.
        /// </summary>
        public GridType GridType { get; set; }
        private BitmapBuffer layer1, layer2;
        private ITileCollection tileSelection;
        private bool moved = false;
        private bool selectionChanged = false;
        private bool requireRefresh = true;
        /// <summary>
        /// Initializes a new instance of the <see cref="TileGrid"/> class.
        /// </summary>
        public TileGrid(int width, int height)
        {
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <returns>An array of uint.</returns>
        public bool GetGraphics()
        {
            bool changed = false;
            if (layer1 == null)
            {
                layer1 = BitmapBuffer.CreateInstance(WidthWithZoom, HeightWithZoom);
                changed = true;
            }
            else if (layer1.Length != Lenght * layer1.BytesPerColor)  
            {
                layer1.Initialize(WidthWithZoom, HeightWithZoom);
                changed = true;
            }

            if (requireRefresh || moved)
            {
                if (Target != null)
                {
                    BitmapBuffer t = Target.GetGraphics(Zoom);
                    if (t != null)
                        layer1.DrawBitmapBuffer(t, Target.Left * Zoom, Target.Top * Zoom, BackgroundColorR, BackgroundColorG, BackgroundColorB);
                    else
                        layer1.FillWithColor(BackgroundColorR, BackgroundColorG, BackgroundColorB);
                }
                else
                {
                    layer1.FillWithColor(BackgroundColorR, BackgroundColorG, BackgroundColorB);
                }
                changed = true;
            }

            if (DrawGrid && (requireRefresh || moved)) 
            {
                layer1.DrawGrid(Zoom, CellSize, GridType, GridColorR, GridColorG, GridColorB);
                changed = true;
            }

            if (DrawGuidelines && (requireRefresh || moved))
            {
                int guideRectSize = 16 * Zoom;
                int guideRectOffset = 112 * Zoom;
                layer1.DrawRectangleBorder(guideRectOffset, guideRectOffset, guideRectSize, guideRectSize, SelectionColorR, SelectionColorG, SelectionColorB);
                int guideLineOffset = 120 * Zoom;
                layer1.DrawLine(guideLineOffset, 0, guideLineOffset, HeightWithZoom, SelectionColorR, SelectionColorG, SelectionColorB);
                layer1.DrawLine(0, guideLineOffset, WidthWithZoom, guideLineOffset, SelectionColorR, SelectionColorG, SelectionColorB);
                changed = true;
            }
            requireRefresh = false;

            layer2 = layer1;
            if (selectionChanged || moved)
            {
                if (tileSelection != null)
                {
                    List<TileBorder> rects = tileSelection.GetTileBorders();
                    if (rects.Count != 0)
                    {
                        layer2 = layer1.Clone();

                        Parallel.ForEach(rects, r =>
                        {
                            layer2.DrawRectangleBorder(r.X * Zoom, r.Y * Zoom, r.Width * Zoom, r.Height * Zoom, SelectionColorR, SelectionColorG, SelectionColorB);
                        });

                    }
                    selectionChanged = false;
                }
                changed = true;
            }
            moved = false;

            return changed;
        }
        /// <summary>
        /// Addings the at position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="selection">The selection.</param>
        public void AddingAtPosition(int x, int y, TileMaskCollection selection)
        {
            if (Target == null || selection == null || selection.Count <= 0)
            {
                return;
            }

            x /= Zoom;
            x -= x % CellSize;
            y /= Zoom;
            y -= y % CellSize;
            TileMaskCollection sel = selection.Clone();
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
                tileSelection = Target.SelectTiles(x / Zoom, y / Zoom, width / Zoom, height / Zoom);
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
                if (x + Target.Width > WidthWithZoom) x = WidthWithZoom - Target.Width;
                if (y + Target.Height > HeightWithZoom) y = HeightWithZoom - Target.Height;
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
                    layer1.DrawRectangle(drx, dry, drw, drh, BackgroundColorR, BackgroundColorG, BackgroundColorB);
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
        /// <summary>
        /// Copies the to.
        /// </summary>
        /// <param name="b">The b.</param>
        public unsafe void CopyTo(byte* b)
        {
            layer2.CopyTo(b);
        }
    }
}
