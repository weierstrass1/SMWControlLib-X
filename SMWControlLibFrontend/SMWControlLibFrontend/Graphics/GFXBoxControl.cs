using Eto.Drawing;
using Eto.Forms;
using SMWControlLibSNES.Enumerators.Graphics;
using SMWControlLibSNES.Graphics;
using SMWControlLibFrontend.Enumerators;
using SMWControlLibRendering.Enumerators;
using System;
using SMWControlLibRendering;
using SMWControlLibRendering.Colors;

namespace SMWControlLibFrontend.Graphics
{
    /// <summary>
    /// The g f x box control.
    /// </summary>
    public partial class GFXBoxControl : Drawable
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GFXBoxControl"/> class.
        /// </summary>
        public GFXBoxControl()
        {
            InitializeComponent();
            MouseDown += mouseDown;
            MouseUp += mouseUp;
            MouseMove += mouseMove;
            updateGraphics();
            Paint += paint;
        }

        /// <summary>
        /// paints the.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void paint(object sender, PaintEventArgs e)
        {
            var g = e.Graphics;
            g.DrawImage(image, 0, 0);
            g.DrawRectangle(Color.FromArgb(255, 0, 0), selectionRectangle);
        }

        /// <summary>
        /// mice the move.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mouseMove(object sender, MouseEventArgs e)
        {
            if (e.Buttons != MouseButtons.Primary)
                return;

            if (state == MouseState.Active)
            {
                updateRect(e);
            }
        }

        /// <summary>
        /// mice the up.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mouseUp(object sender, MouseEventArgs e)
        {
            if (e.Buttons != MouseButtons.Primary)
                return;

            if (state == MouseState.Active)
            {
                state = MouseState.Idle;
                updateRect(e);
                Selection = gfxBox.SelectTiles((selectionRectangle.X >> 3) / zoom,
                                                (selectionRectangle.Y >> 3) / zoom,
                                                ((selectionRectangle.Width + 1) >> 3) / zoom,
                                                ((selectionRectangle.Height + 1) >> 3) / zoom,
                                                SpriteTileSizeMode.ModeDefault,
                                                new SpriteTileProperties(Flip.NotFlipped,
                                                                        palette,
                                                                        SpritePage.SP12,
                                                                        SpritePriority.OverBG34WithPriority0));
            }
        }

        /// <summary>
        /// mice the down.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mouseDown(object sender, MouseEventArgs e)
        {
            if (e.Buttons != MouseButtons.Primary)
                return;

            if (state != MouseState.Active)
            {
                state = MouseState.Active;
                pointer = e.Location;
                int x = (int)pointer.X;
                int y = (int)pointer.Y;
                int Zoom8 = 8 * Zoom;
                selectionRectangle.Left = selectionRectangle.Right = x - x % Zoom8;
                selectionRectangle.Top = selectionRectangle.Bottom = y - y % Zoom8;
                updateRect(e);

            }
        }

        /// <summary>
        /// updates the rect.
        /// </summary>
        /// <param name="e">The e.</param>
        private void updateRect(MouseEventArgs e)
        {
            PointF p = e.Location;
            int minX = (int)(Math.Min(p.X, pointer.X));
            int minY = (int)(Math.Min(p.Y, pointer.Y));
            int maxX = (int)(Math.Max(p.X, pointer.X));
            int maxY = (int)(Math.Max(p.Y, pointer.Y));

            int Zoom8 = (int)(8 * Zoom);
            int x = minX - (minX % Zoom8);
            int y = minY - (minY % Zoom8);
            int r = maxX + (Zoom8 - (maxX % Zoom8));
            int b = maxY + (Zoom8 - (maxY % Zoom8));

            bool mustUpdate = false;

            if (x < 0) x = 0;
            if (y < 0) y = 0;

            if (x <= Width && selectionRectangle.Left != x)
            {
                selectionRectangle.Left = x;
                mustUpdate = true;
            }

            if (y <= Height && selectionRectangle.Top != y)
            {
                selectionRectangle.Top = y;
                mustUpdate = true;
            }

            if (r <= Width && selectionRectangle.Right != r - 1)
            {
                selectionRectangle.Right = r - 1;
                mustUpdate = true;
            }

            if (b <= Height && selectionRectangle.Bottom != b - 1)
            {
                selectionRectangle.Bottom = b - 1;
                mustUpdate = true;
            }

            if (mustUpdate)
                updateGraphics();
        }

        /// <summary>
        /// updates the graphics.
        /// </summary>
        private void updateGraphics()
        {
            BitmapBuffer<ColorR5G5B5> b = gfxBox.RealObject.CreateBitmapBuffer(Flip.NotFlipped, palette.RealObject, Zoom);

            if (b == previewsBitmap && lastRenderedSelection == selectionRectangle && previewsBitmap != null)
                return;

            lastRenderedSelection = selectionRectangle;

            if (previewsBitmap != b)
            {
                previewsBitmap = b;
                BitmapData bd = image.Lock();
                unsafe
                {
                    byte* bs = (byte*)bd.Data;
                    int l = (b.Length << 1) + b.Length;

                    fixed (ColorR5G5B5* bp = b.Pixels)
                    {
                        Buffer.MemoryCopy(bp, bs, l, l);
                    }
                }
                bd.Dispose();
            }
            Invalidate();
        }


    }
}
