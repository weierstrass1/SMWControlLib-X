using Eto.Drawing;
using Eto.Forms;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibFrontend.Graphics;
using SMWControlLibRendering;
using SMWControlLibSNES.Graphics;
using SMWControlLibUnity.Enumerators.Graphics;
using SMWControlLibUnity.Graphics;
using System;
using System.ComponentModel;

namespace SMWControlLibFrontend
{
    /// <summary>
    /// The main form.
    /// </summary>
    public partial class MainForm : Form
    {
        private readonly GFXBoxControl gfx;
        private readonly OAMTileGrid grid;
        private readonly Scrollable scrolleablePanel;

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

            grid = new OAMTileGrid
            {
                AddingTiles = () => gfx.Selection
            };

            UnityGraphicBox gr = new UnityGraphicBox(new UnityTileSize(16, 16));
            gr.Load("image.png", 0);


            SpriteTileSection s = new SpriteTileSection();
            s.Add();
            SpriteTileSectionMask m = new SpriteTileSectionMask(s);
            grid.Target = m;

            scrolleablePanel = new Scrollable
            {
                Width = 512,
                Height = 512,
                Border = BorderType.None
            };
            PixelLayout scrollLayout = new PixelLayout();
            scrollLayout.Add(grid, 0, 0);
            scrolleablePanel.Content = scrollLayout;
            scrolleablePanel.Scroll += scroll;

            layout.Add(scrolleablePanel, 266, 5);
            grid.VisibleRectangle = scrolleablePanel.VisibleRect;

            Content = layout;
        }

        /// <summary>
        /// scrolls the.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void scroll(object sender, ScrollEventArgs e)
        {
            grid.VisibleRectangle = scrolleablePanel.VisibleRect;
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