using Eto.Forms;
using Eto.Drawing;
using SMWControlLibBackend.Graphics;
using System;
using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Delegates;

namespace SMWControlLibFrontend.Graphics
{
	/// <summary>
	/// The o a m tile grid.
	/// </summary>
	public partial class OAMTileGrid : Drawable
	{
		private readonly SpriteTileSectionMask section;
		private Bitmap image;
		public SelectionHandler AddingTiles;
		/// <summary>
		/// Gets the zoom.
		/// </summary>
		public Zoom Zoom { get; private set; }
		/// <summary>
		/// Initializes a new instance of the <see cref="OAMTileGrid"/> class.
		/// </summary>
		public OAMTileGrid()
		{
			InitializeComponent();
			SpriteTileSection s = new SpriteTileSection();
			section = new SpriteTileSectionMask(s);
			section.Section.Add();
			section.Index = 0;
			Width = 512;
			Height = 512;
			Paint += paint;
			MouseDown += mouseDown;
			Zoom = Zoom.X2;
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

					if (s != null && s.Count > 0)
					{
						int x = ((int)e.Location.X) / Zoom;
						int y = ((int)e.Location.Y) / Zoom;
						if (section.X < 0) section.X = x;
						if (section.Y < 0) section.Y = y;
						s.MoveTo(x, y);
						section.GetCollection().AddCollection(s);
					}

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
			if (section.Width == 0 || section.Height == 0)
			{
				e.Graphics.Clear();
				return;
			}

			image = new Bitmap(section.Width * Zoom,
								section.Height * Zoom,
								PixelFormat.Format32bppRgba);
			uint[] b = section.GetGraphics(Zoom);
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
			var g = e.Graphics;
			g.DrawImage(image, 0, 0);
		}
	}
}
