using Eto.Drawing;
using Eto.Forms;
using SMWControlLibSNES.Graphics;
using SMWControlLibFrontend.Enumerators;
using System;

namespace SMWControlLibFrontend.Graphics
{
	/// <summary>
	/// The o a m tile grid.
	/// </summary>
	public partial class OAMTileGrid : Drawable
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OAMTileGrid"/> class.
		/// </summary>
		public OAMTileGrid()
		{
			InitializeComponent();
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
					SpriteTileMaskCollection s = (SpriteTileMaskCollection)AddingTiles?.Invoke();

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
		/// <summary>
		/// paints the.
		/// </summary>
		/// <param name="sender">The sender.</param>
		/// <param name="e">The e.</param>
		private void paint(object sender, PaintEventArgs e)
		{
			if (grid.GetGraphics())
			{
				if (image.Width * image.Height != grid.Lenght)
					image = new Bitmap(grid.WidthWithZoom, grid.HeightWithZoom, PixelFormat.Format24bppRgb);

				BitmapData bd = image.Lock();
				unsafe
				{
					byte* bs = (byte*)bd.Data;

					grid.CopyTo(bs);
				}
				bd.Dispose();
			}
			e.Graphics.DrawImage(image, 0, 0);
		}
	}
}
