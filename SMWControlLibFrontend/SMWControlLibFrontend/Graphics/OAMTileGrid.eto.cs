using Eto.Drawing;
using Eto.Forms;
using SMWControlLibSNES.Graphics;
using SMWControlLibFrontend.Enumerators;
using SMWControlLibCommons.Enumerators.Graphics;

namespace SMWControlLibFrontend.Graphics
{
    /// <summary>
    /// The o a m tile grid.
    /// </summary>
    public partial class OAMTileGrid : Drawable
    {
        /// <summary>
        /// Initializes the component.
        /// </summary>
        void InitializeComponent()
        {
			grid = new SpriteTileGrid
			{
				Zoom = Zoom.X8,
				CellSize = GridCellSize.Size16x16,
				BackgroundColorR = 0xB0, 
				BackgroundColorG = 0xC0, 
				BackgroundColorB = 0xD0,
				SelectionColorR = 0xE0, 
				SelectionColorG = 0x80, 
				SelectionColorB = 0x40,
				GridType = GridType.DottedLine,
				DrawGrid = false,
				DrawGuidelines = false
			};
			state = MouseState.Idle;
			image = new Bitmap(grid.Side, grid.Side, PixelFormat.Format32bppRgba);
			Width = grid.Side;
			Height = Width;
		}
    }
}
