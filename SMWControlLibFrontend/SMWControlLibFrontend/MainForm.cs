using Eto.Drawing;
using Eto.Forms;
using SMWControlLibSNES.Graphics;
using SMWControlLibFrontend.Graphics;
using SMWControlLibRendering;
using System.ComponentModel;

namespace SMWControlLibFrontend
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        public GFXBoxControl gfx;
        public OAMTileGrid grid;
        /// <summary>
        /// Initializes a new instance of the <see cref="MainForm"/> class.
        /// </summary>
        public MainForm()
        {
            Title = "My Eto Form";
            ClientSize = new Size(800, 600);

            gfx = new GFXBoxControl();

            PixelLayout layout = new PixelLayout();
            layout.Add(gfx, 5, 5);

            grid = new OAMTileGrid();
            layout.Add(grid, 266, 5);

            grid.AddingTiles = () => gfx.Selection;

            SpriteTileSection s = new SpriteTileSection();
            s.Add();
            SpriteTileSectionMask m = new SpriteTileSectionMask(s);
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
