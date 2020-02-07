using Eto.Drawing;
using Eto.Forms;
using SMWControlLibSNES.Enumerators.Graphics;
using SMWControlLibSNES.Graphics;
using SMWControlLibFrontend.Enumerators;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibRendering;

namespace SMWControlLibFrontend.Graphics
{
    /// <summary>
    /// The g f x box control.
    /// </summary>
    public partial class GFXBoxControl : Drawable
    {
        private SNESColorPalette palette;
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
                gfxBox = new SpriteTileGFXBox(size);
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
        public SpriteTileMaskCollection Selection { get; private set; }
        private SpriteTileGFXBox gfxBox;
        private Rectangle selectionRectangle, lastRenderedSelection;
        private MouseState state = MouseState.Idle;
        private GFXBoxSize size = GFXBoxSize.Size128x128;
        private BitmapBuffer previewsBitmap;
        private PointF pointer;
        private Bitmap image;
        /// <summary>
        /// Initializes the component.
        /// </summary>
        void InitializeComponent()
        {
            gfxBox = new SpriteTileGFXBox(size);
            Width = size.Width * Zoom;
            Height = size.Height * Zoom;
            image = new Bitmap(Width, Height, PixelFormat.Format32bppRgba);
            ClientSize = new Size(Width, Height);
            selectionRectangle = new Rectangle();
            palette = new SNESColorPalette(SpriteColorPaletteIndex.SpritePalette0, 16);
        }
    }
}
