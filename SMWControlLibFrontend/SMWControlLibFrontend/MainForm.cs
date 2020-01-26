using Eto.Drawing;
using Eto.Forms;
using SMWControlLibFrontend.Graphics;

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

            Content = layout;
        }
    }
}
