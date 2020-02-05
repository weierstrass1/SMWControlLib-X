using Eto.Drawing;
using Eto.Forms;
using SMWControlLibSNES.Graphics;
using SMWControlLibFrontend.Enumerators;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibRendering.Colors;

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
				BackgroundColor = new ColorR5G5B5((byte)0xB0, (byte)0xC0, (byte)0xD0),
				SelectionColor = new ColorR5G5B5((byte)0xE0, (byte)0x80, (byte)0x40),
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
