using Eto.Forms;
using Eto.Drawing;
using SMWControlLibBackend.Graphics;
using System;
using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Delegates;
using SMWControlLibBackend.Interfaces.Graphics;

namespace SMWControlLibFrontend.Graphics
{
	/// <summary>
	/// The o a m tile grid.
	/// </summary>
	public partial class OAMTileGrid : Drawable
	{
		private readonly SpriteTileGrid grid;
		private Bitmap image;
		/// <summary>
		/// Gets or sets the target.
		/// </summary>
		public IGridDrawable Target 
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
		public Zoom Zoom { get => grid.Zoom; set 
			{
				if (grid.Zoom != value)
				{
					grid.Zoom = value;
					Width = grid.Side;
					Height = grid.Side;
					image = new Bitmap(Width, Height, PixelFormat.Format32bppRgba);
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
		/// <summary>
		/// Initializes a new instance of the <see cref="OAMTileGrid"/> class.
		/// </summary>
		public OAMTileGrid()
		{
			InitializeComponent();
			grid = new SpriteTileGrid
			{
				Zoom = Zoom.X1,
				CellSize = GridCellSize.Size8x8,
				BackgroundColor = 0xFF8090A0,
				GridType = GridType.Line
			};
			image = new Bitmap(grid.Side, grid.Side, PixelFormat.Format32bppRgba);
			Width = grid.Side;
			Height = Width;
			Paint += paint;
			MouseDown += mouseDown;
			Invalidate();
		}

		/// <summary>
		/// mice the down.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void mouseDown(object sender, MouseEventArgs e)
		{
			if (e.Buttons == MouseButtons.Alternate)
			{
				if(AddingTiles!=null)
				{
					SpriteTileMaskCollection s = AddingTiles?.Invoke();

					grid.AddingAtPosition((int)e.Location.X, (int)e.Location.Y, s);

					Invalidate();
				}
			}
		}

		/// <summary>
		/// paints the.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void paint(object sender, PaintEventArgs e)
		{
			BitmapData bd = image.Lock();
			uint[] b = grid.GetGraphics();
			unsafe
			{
				byte* bs = (byte*)bd.Data;
				int l = b.Length << 2;

				fixed (uint* bp = b)
				{
					Buffer.MemoryCopy(bp, bs, l, l);
				}
			}
			bd.Dispose();
			e.Graphics.DrawImage(image, 0, 0);
		}
	}
}
