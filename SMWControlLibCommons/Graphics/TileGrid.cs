﻿using SMWControlLibCommons.DataStructs;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Interfaces.Graphics;
using SMWControlLibRendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public IGridDrawable<TileMask> Target { get; set; }
        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public byte BackgroundColorR { get; private set; }
        /// <summary>
        /// Gets or sets the background color g.
        /// </summary>
        public byte BackgroundColorG { get; private set; }
        /// <summary>
        /// Gets or sets the background color b.
        /// </summary>
        public byte BackgroundColorB { get; private set; }
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
                if (zoom != value)
                {
                    zoom = value;
                    WidthWithZoom = Width * zoom;
                    HeightWithZoom = Height * zoom;
                    Lenght = WidthWithZoom * HeightWithZoom;
                    layer1 = BitmapBuffer.CreateInstance(WidthWithZoom, HeightWithZoom);
                    layer1.FillWithColor(BackgroundColorR, BackgroundColorG, BackgroundColorB);
                    UpdateLayer2();
                }
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
        private ITileCollection<TileMask> tileSelection;
        private bool moved = false;
        private bool selectionChanged = false;
        private int offset;
        private int copyLenght;
        /// <summary>
        /// Initializes a new instance of the <see cref="TileGrid"/> class.
        /// </summary>
        public TileGrid(int width, int height, Zoom z, byte bgR, byte bgG, byte bgB)
        {
            Width = width;
            Height = height;
            BackgroundColorR = bgR;
            BackgroundColorG = bgG;
            BackgroundColorB = bgB;
            Zoom = z;
            offset = 0;
            copyLenght = Lenght * layer1.BytesPerColor;
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
            drawTileMaskCollection(sel);
            UpdateLayer2();
            updateOffsetAndCopyLenght(sel.Left, sel.Top, sel.Right, sel.Bottom);
        }
        /// <summary>
        /// updates the offset and copy lenght.
        /// </summary>
        /// <param name="l">The l.</param>
        /// <param name="t">The t.</param>
        /// <param name="r">The r.</param>
        /// <param name="b">The b.</param>
        private void updateOffsetAndCopyLenght(int l, int t, int r, int b)
        {
            if (l >= 0 && t >= 0 && r > l && b > t)
            {
                offset = Math.Min(offset, ((t * Zoom * WidthWithZoom) + (l * Zoom)) * layer1.BytesPerColor);
                copyLenght = Math.Max(copyLenght, (((b * WidthWithZoom * Zoom) + (r * Zoom)) * layer1.BytesPerColor) - offset);
            }
        }
        /// <summary>
        /// Removes the.
        /// </summary>
        public void Remove()
        {
            if (Target != null && tileSelection != null)
            {
                int x = tileSelection.Left;
                int y = tileSelection.Top;
                int w = tileSelection.Right - x;
                int h = tileSelection.Bottom - y;

                Target.RemoveTiles();

                ITileCollection<TileMask> col = Target.TilesOnArea(x, y, w, h);

                if (col.Left >= 0)
                {
                    x = Math.Min(x, col.Left);
                    y = Math.Min(y, col.Top);
                    w = Math.Max(w, col.Right - x);
                    h = Math.Max(h, col.Bottom - y);
                }

                layer1.DrawRectangle(x * Zoom, y * Zoom, w * Zoom, h * Zoom, BackgroundColorR, BackgroundColorG, BackgroundColorB);
                if (col.Left >= 0) drawTileMaskCollection(col);
                updateOffsetAndCopyLenght(x, y, x + w, h + y);
                tileSelection = null;

                UpdateLayer2();
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
                int l = x;
                int t = y;
                int r = width + l;
                int b = height + t;
                if(tileSelection!=null)
                {
                    l = Math.Min(l, tileSelection.Left);
                    t = Math.Min(t, tileSelection.Top);
                    r = Math.Max(r, tileSelection.Right);
                    b = Math.Max(b, tileSelection.Bottom);
                }

                tileSelection = (TileMaskCollection)Target.SelectTiles(x / Zoom, y / Zoom, width / Zoom, height / Zoom);
                if (tileSelection != null && !tileSelection.IsEmpty())
                {
                    l = Math.Min(l, tileSelection.Left);
                    t = Math.Min(t, tileSelection.Top);
                    r = Math.Max(r, tileSelection.Right);
                    b = Math.Max(b, tileSelection.Bottom);
                    updateOffsetAndCopyLenght(l, t, r, b);
                }
                UpdateLayer2();
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

                int drx = Target.Left;
                int dry = Target.Top;
                int drr = drx + Target.Width;
                int drb = dry + Target.Height;

                bool b = Target.MoveTiles(x, y);
                if (b)
                {
                    drx = Math.Min(drx, Target.Left);
                    dry = Math.Min(dry, Target.Top);
                    drr = Math.Max(drr, Target.Left + Target.Width);
                    drb = Math.Max(drb, Target.Top + Target.Height);
                    ITileCollection<TileMask> col = Target.TilesOnArea(drx, dry, drr, drb);

                    drx = Math.Min(drx, col.Left);
                    dry = Math.Min(dry, col.Top);
                    drr = Math.Max(drr, col.Right);
                    drb = Math.Max(drb, col.Bottom);

                    layer1.DrawRectangle(drx * Zoom, dry * Zoom, (drr - drx) *Zoom, (drb - dry) * Zoom, 
                        BackgroundColorR, BackgroundColorG, BackgroundColorB);
                    drawTileMaskCollection(col);
                    UpdateLayer2();
                    updateOffsetAndCopyLenght(drx, dry, drr, drb);
                }

                moved = moved || b;
                return b;
            }
            return false;
        }
        /// <summary>
        /// draws the tile mask collection.
        /// </summary>
        /// <param name="col">The col.</param>
        private void drawTileMaskCollection(ITileCollection<TileMask> col)
        {
            foreach (TileMask tm in col.GetEnumerable())
            {
                layer1.DrawBitmapBuffer(tm.GetGraphics(Zoom), tm.X * Zoom, tm.Y * Zoom);
            }
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
                updateOffsetAndCopyLenght(tileSelection.Left, tileSelection.Top, tileSelection.Right, tileSelection.Bottom);
                tileSelection = null;
                selectionChanged = true;
                UpdateLayer2();
            }
        }
        /// <summary>
        /// Gets the x offset.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <returns>An int.</returns>
        public int GetXOffset(int x)
        {
            if (tileSelection == null) return -1;
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
            if (tileSelection == null) return -1;
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
            if (copyLenght < 0) return;
            layer2.CopyTo(b, offset, copyLenght);
            offset = int.MaxValue;
            copyLenght = int.MinValue;
        }
        /// <summary>
        /// Updates the layer2.
        /// </summary>
        private void UpdateLayer2()
        {
            if (layer2 == null)
            {
                layer2 = layer1.Clone();
            }
            else
            {
                layer2.DrawBitmapBuffer(layer1, 0, 0);
            }
            if (tileSelection != null && !tileSelection.IsEmpty())
            {
                foreach (TileBorder b in tileSelection.GetTileBorders())
                {
                    layer2.DrawRectangleBorder(b.X * Zoom, b.Y * Zoom, b.Width * Zoom, b.Height * Zoom,
                        SelectionColorR, SelectionColorG, SelectionColorB);
                }
            }
        }
    }
}
