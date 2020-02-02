using Eto.Drawing;
using Eto.Forms;
using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Graphics;
using SMWControlLibFrontend.Enumerators;
using SMWControlLibRendering;
using SMWControlLibRendering.Enumerators;
using System;

namespace SMWControlLibFrontend.Graphics
{
    /// <summary>
    /// The g f x box control.
    /// </summary>
    public partial class GFXBoxControl<T> : Drawable where T: BitmapBuffer, new()
    {
        private readonly SNESColorPalette palette;
        /// <summary>
        /// Gets or sets the g f x size.
        /// </summary>
        public GFXBoxSize GFXSize
        {
            get
            {
                return size;
            }
            set
            {
                size = value;
                gfxBox = new SpriteTileGFXBox<T>(size);
                Width = size.Width * Zoom;
                Height = size.Height * Zoom;
                image = new Bitmap(Width, Height, PixelFormat.Format32bppRgba);
            }
        }

        private Zoom zoom = Zoom.X2;
        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        public Zoom Zoom
        {
            get { return zoom; }
            set
            {
                zoom = value;
                Width = size.Width * value;
                Height = size.Height * value;
                image = new Bitmap(Width, Height, PixelFormat.Format32bppRgba);
            }
        }

        /// <summary>
        /// Gets the selection.
        /// </summary>
        public SpriteTileMaskCollection<T> Selection { get; private set; }
        private SpriteTileGFXBox<T> gfxBox;
        private Rectangle selectionRectangle, lastRenderedSelection;
        private MouseState state = MouseState.Idle;
        private GFXBoxSize size = GFXBoxSize.Size128x128;
        private T previewsBitmap;
        private PointF pointer;
        private Bitmap image;

        /// <summary>
        /// Initializes a new instance of the <see cref="GFXBoxControl"/> class.
        /// </summary>
        public GFXBoxControl()
        {
            gfxBox = new SpriteTileGFXBox<T>(size);
            Width = size.Width * Zoom;
            Height = size.Height * Zoom;
            image = new Bitmap(Width, Height, PixelFormat.Format32bppRgba);
            ClientSize = new Size(Width, Height);
            selectionRectangle = new Rectangle();
            palette = new SNESColorPalette(BPP.BPP4, SpriteColorPaletteIndex.SpritePalette0);
            gfxBox.LoadGFX("gfx.bin");
            palette.LoadPal("pal.pal", SpriteColorPaletteIndex.SpritePalette0.Offset, 0, 16);
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
            T b = (T)gfxBox.GetGraphics(palette, Zoom);

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
                    int l = b.Length << 2;

                    fixed (uint* bp = b.Pixels)
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
