using Eto.Drawing;
using Eto.Forms;
using SMWControlLibBackend.Delegates;
using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Graphics;
using SMWControlLibBackend.Interfaces.Graphics;
using SMWControlLibFrontend.Enumerators;
using SMWControlLibRendering;
using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace SMWControlLibFrontend.Graphics
{
	/// <summary>
	/// The o a m tile grid.
	/// </summary>
	public partial class OAMTileGrid<T> : Drawable where T: BitmapBuffer, new()
	{
		private readonly SpriteTileGrid<T> grid;
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
		public SelectionHandler<T> AddingTiles;
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
		private MouseState state;
		private Point select, action;
		/// <summary>
		/// Initializes a new instance of the <see cref="OAMTileGrid"/> class.
		/// </summary>
		public OAMTileGrid()
		{
			grid = new SpriteTileGrid<T>
			{
				Zoom = Zoom.X8,
				CellSize = GridCellSize.Size16x16,
				BackgroundColor = 0xFFB0C0D0,
				SelectionColor = 0xFFE08040,
				GridType = GridType.DottedLine,
				DrawGrid = false,
				DrawGuidelines = false
			};
			state = MouseState.Idle;
			image = new Bitmap(grid.Side, grid.Side, PixelFormat.Format32bppRgba);
			Width = grid.Side;
			Height = Width;
			Paint += paint;
			MouseDown += mouseDown;
			MouseMove += mouseMove;
			MouseUp += mouseUp;
			KeyDown += keyDown;
			Invalidate();
		}

		/// <summary>
		/// keys the down.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void keyDown(object sender, KeyEventArgs e)
		{
			if(e.Key == Keys.Delete)
			{
				grid.Remove();
				Invalidate();
			}
		}

		/// <summary>
		/// mice the up.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void mouseUp(object sender, MouseEventArgs e)
		{
			if (e.Buttons == MouseButtons.Primary)
			{
				if (state == MouseState.Selected)
				{
					Point p = new Point((int)e.Location.X, (int)e.Location.Y);
					int minX = Math.Min(select.X, p.X);
					int maxX = Math.Max(select.X, p.X);
					int minY = Math.Min(select.Y, p.Y);
					int maxY = Math.Max(select.Y, p.Y);
					grid.Select(minX, minY, maxX - minX, maxY - minY);
					state = MouseState.Active;
					Invalidate();
				}
				else if (state == MouseState.Active)
				{
					Point p = new Point((int)e.Location.X, (int)e.Location.Y);
					if (grid.MoveTo(p.X - action.X, p.Y - action.Y))
						Invalidate();
				}
			}
		}

		/// <summary>
		/// mice the move.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void mouseMove(object sender, MouseEventArgs e)
		{
			if (e.Buttons == MouseButtons.Primary)
			{
				if (state == MouseState.Selected)
				{
					Point p = new Point((int)e.Location.X, (int)e.Location.Y);
					int minX = Math.Min(select.X, p.X);
					int maxX = Math.Max(select.X, p.X);
					int minY = Math.Min(select.Y, p.Y);
					int maxY = Math.Max(select.Y, p.Y);
					grid.Select(minX, minY, maxX - minX, maxY - minY);
					Invalidate();
				}
				else if (state == MouseState.Active)
				{
					Point p = new Point((int)e.Location.X, (int)e.Location.Y);
					if (grid.MoveTo(p.X - action.X, p.Y - action.Y)) 
						Invalidate();
				}
			}
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
				if (AddingTiles != null)
				{
					SpriteTileMaskCollection<T> s = AddingTiles?.Invoke();

					grid.AddingAtPosition((int)e.Location.X, (int)e.Location.Y, s);

					Invalidate();
				}
			}
			else if(e.Buttons == MouseButtons.Primary)
			{
				if(state == MouseState.Idle)
				{
					state = MouseState.Selected;
					select = new Point((int)e.Location.X, (int)e.Location.Y);
					grid.Select(select.X, select.Y, 1, 1);
					Invalidate();
				}
				if (state == MouseState.Active) 
				{
					int xoff = grid.GetXOffset((int)e.Location.X);
					int yoff = grid.GetYOffset((int)e.Location.Y);

					if (xoff < 0 || yoff < 0)
					{
						grid.ClearTileSelection();
						state = MouseState.Idle;
						Invalidate();
					}
					else
					{
						action = new Point(xoff * Zoom, yoff * Zoom);
					}
				}
			}
		}

		readonly List<long> times = new List<long>();
		/// <summary>
		/// paints the.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void paint(object sender, PaintEventArgs e)
		{
			Stopwatch watch = new Stopwatch();

			watch.Start();
			uint[] b = grid.GetGraphics();
			watch.Stop();

			if (grid.Changed)
			{
				times.Add(watch.ElapsedMilliseconds);
				if (image.Width * image.Height != b.Length)
					image = new Bitmap((int)Math.Sqrt(b.Length), (int)Math.Sqrt(b.Length), PixelFormat.Format32bppRgba);

				BitmapData bd = image.Lock();
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
			}
			e.Graphics.DrawImage(image, 0, 0);
		}
	}
}
