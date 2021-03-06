using Eto.Drawing;
using Eto.Forms;
using SMWControlLibFrontend.Enumerators;
using SMWControlLibSNES.Graphics;
using System;

namespace SMWControlLibFrontend.Graphics
{
    /// <summary>
    /// The o a m tile grid.
    /// </summary>
    public partial class OAMTileGrid : Drawable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OAMTileGrid"/> class.
        /// </summary>
        public OAMTileGrid()
        {
            InitializeComponent();
            CanFocus = true;
            Paint += paint;
            MouseDown += mouseDown;
            MouseMove += mouseMove;
            MouseUp += mouseUp;
            MouseEnter += mouseEnter;
            KeyDown += keyDown;

            Invalidate();
        }

        /// <summary>
        /// mice the enter.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mouseEnter(object sender, MouseEventArgs e)
        {
            Focus();
        }

        /// <summary>
        /// keys the down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void keyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Keys.Delete)
            {
                grid.Remove();
                state = MouseState.Idle;
                Invalidate();
            }
        }

        /// <summary>
        /// mice the up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.Buttons == MouseButtons.Primary)
            {
                if (state == MouseState.Selected)
                {
                    Point p = new Point((int)e.Location.X, (int)e.Location.Y);
                    int minX = Math.Min(select.X, p.X);
                    int maxX = Math.Max(select.X, p.X);
                    int minY = Math.Min(select.Y, p.Y);
                    int maxY = Math.Max(select.Y, p.Y);
                    grid.SelectTiles(minX, minY, maxX - minX, maxY - minY);
                    state = MouseState.Active;
                    Invalidate();
                }
                else if (state == MouseState.Active)
                {
                    Point p = new Point((int)e.Location.X, (int)e.Location.Y);
                    if (grid.MoveTo(p.X - action.X, p.Y - action.Y))
                        Invalidate();
                }
            }
        }

        /// <summary>
        /// mice the move.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mouseMove(object sender, MouseEventArgs e)
        {
            Focus();
            if (e.Buttons == MouseButtons.Primary)
            {
                if (state == MouseState.Selected)
                {
                    Point p = new Point((int)e.Location.X, (int)e.Location.Y);
                    int minX = Math.Min(select.X, p.X);
                    int maxX = Math.Max(select.X, p.X);
                    int minY = Math.Min(select.Y, p.Y);
                    int maxY = Math.Max(select.Y, p.Y);
                    grid.SelectTiles(minX, minY, maxX - minX, maxY - minY);
                    Invalidate();
                }
                else if (state == MouseState.Active)
                {
                    Point p = new Point((int)e.Location.X, (int)e.Location.Y);
                    if (grid.MoveTo(p.X - action.X, p.Y - action.Y))
                        Invalidate();
                }
            }
        }

        /// <summary>
        /// mice the down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mouseDown(object sender, MouseEventArgs e)
        {
            if (e.Buttons == MouseButtons.Alternate)
            {
                if (AddingTiles != null)
                {
                    SpriteTileMaskCollection s = (SpriteTileMaskCollection)AddingTiles?.Invoke();
                    grid.ClearTileSelection();
                    state = MouseState.Idle;
                    grid.AddingAtPosition((int)e.Location.X, (int)e.Location.Y, s);
                    Invalidate();
                }
            }
            else if (e.Buttons == MouseButtons.Primary)
            {
                if (state == MouseState.Idle)
                {
                    state = MouseState.Selected;
                    select = new Point((int)e.Location.X, (int)e.Location.Y);
                    grid.SelectTiles(select.X, select.Y, 1, 1);
                    Invalidate();
                }
                else if (state == MouseState.Active)
                {
                    int xoff = grid.GetXOffset((int)e.Location.X);
                    int yoff = grid.GetYOffset((int)e.Location.Y);

                    if (xoff < 0 || yoff < 0)
                    {
                        grid.ClearTileSelection();
                        state = MouseState.Idle;
                        Invalidate();
                    }
                    else
                    {
                        action = new Point(xoff, yoff);
                    }
                }
                else
                {
                    int xoff = grid.GetXOffset((int)e.Location.X);
                    int yoff = grid.GetYOffset((int)e.Location.Y);

                    if (xoff < 0 || yoff < 0)
                    {
                        grid.ClearTileSelection();
                        state = MouseState.Idle;
                        Invalidate();
                    }
                }
            }
        }

        /// <summary>
        /// paints the.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void paint(object sender, PaintEventArgs e)
        {
            if (VisibleRectangle != default)
            {
                int w = Math.Min(VisibleRectangle.Width, grid.WidthWithZoom);
                int h = Math.Min(VisibleRectangle.Height, grid.HeightWithZoom);
                if (image.Width * image.Height != w * h)
                    image = new Bitmap(w, h, PixelFormat.Format24bppRgb);
                using (BitmapData bd = image.Lock())
                {
                    unsafe
                    {
                        byte* bs = (byte*)bd.Data;
                        grid.CopyTo(bs, VisibleRectangle.X, image.Width + VisibleRectangle.X,
                            VisibleRectangle.Y, image.Height + VisibleRectangle.Y);
                    }
                }
                e.Graphics.DrawImage(image, VisibleRectangle.X, VisibleRectangle.Y);
            }
        }
    }
}