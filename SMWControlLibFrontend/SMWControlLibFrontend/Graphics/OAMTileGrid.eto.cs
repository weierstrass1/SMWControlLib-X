using Eto.Drawing;
using Eto.Forms;
using SMWControlLibSNES.Graphics;
using SMWControlLibFrontend.Enumerators;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Delegates;
using SMWControlLibCommons.Interfaces.Graphics;
using SMWControlLibCommons.Graphics;

namespace SMWControlLibFrontend.Graphics
{
	/// <summary>
	/// The o a m tile grid.
	/// </summary>
	public partial class OAMTileGrid : Drawable
	{
		private SpriteTileGrid grid;
		private Bitmap image;
		/// <summary>
		/// Gets or sets the target.
		/// </summary>
		public IGridDrawable<TileMask> Target
		{
			get => grid.Target;
			set
			{
				if (grid.Target != value)
				{
					grid.Target = value;
					Invalidate();
				}
			}
		}
		public SelectionHandler AddingTiles;
		/// <summary>
		/// Gets the zoom.
		/// </summary>
		public Zoom Zoom
		{
			get => grid.Zoom; set
			{
				if (grid.Zoom != value)
				{
					grid.Zoom = value;
					Width = grid.WidthWithZoom;
					Height = grid.HeightWithZoom;
					image = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
					Invalidate();
				}
			}
		}
		/// <summary>
		/// Gets or sets the cell size.
		/// </summary>
		public GridCellSize CellSize { get => grid.CellSize; set { grid.CellSize = value; Invalidate(); } }
		/// <summary>
		/// Gets or sets the grid type.
		/// </summary>
		public GridType GridType { get => grid.GridType; set { grid.GridType = value; Invalidate(); } }
		private MouseState state;
		private Point select, action;
		/// <summary>
		/// Initializes the component.
		/// </summary>
		void InitializeComponent()
		{
			grid = new SpriteTileGrid(255, 255, Zoom.X8, 0xB0, 0xC0, 0xD0)
			{
				CellSize = GridCellSize.Size4x4,
				SelectionColorR = 0xE0,
				SelectionColorG = 0x80,
				SelectionColorB = 0x40,
				GridType = GridType.DottedLine,
				DrawGrid = false,
				DrawGuidelines = false
			};
			state = MouseState.Idle;
			image = new Bitmap(grid.WidthWithZoom, grid.HeightWithZoom, PixelFormat.Format24bppRgb);
			Width = grid.WidthWithZoom;
			Height = grid.HeightWithZoom;
		}
	}
}
