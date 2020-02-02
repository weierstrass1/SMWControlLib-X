using Eto.Drawing;
using Eto.Forms;
using SMWControlLibBackend.Graphics;
using SMWControlLibFrontend.Graphics;
using SMWControlLibRendering;
using System.ComponentModel;

namespace SMWControlLibFrontend
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm<T> : Form where T : BitmapBuffer, new()
    {
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <returns>A Form.</returns>
        public static Form CreateInstance()
        {
            if (HardwareAcceleratorManager.IsGPUAvailable())
                return new MainForm<GPUBitmapBuffer>();
            return new MainForm<CPUBitmapBuffer>();
        }

        public GFXBoxControl<T> gfx;
        public OAMTileGrid<T> grid;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        private MainForm()
        {
            Title = "My Eto Form";
            ClientSize = new Size(800, 600);

            gfx = new GFXBoxControl<T>();

            PixelLayout layout = new PixelLayout();
            layout.Add(gfx, 5, 5);

            grid = new OAMTileGrid<T>();
            layout.Add(grid, 266, 5);

            grid.AddingTiles = () => gfx.Selection;

            SpriteTileSection<T> s = new SpriteTileSection<T>();
            s.Add();
            SpriteTileSectionMask<T> m = new SpriteTileSectionMask<T>(s);
            grid.Target = m;

            Content = layout;
        }

        /// <summary>
        /// Disposes the.
        /// </summary>
        /// <param name="disposing">If true, disposing.</param>
        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            HardwareAcceleratorManager.Dispose();
        }

        /// <summary>
        /// Ons the closing.
        /// </summary>
        /// <param name="e">The e.</param>
        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            HardwareAcceleratorManager.Dispose();
        }
    }
}
