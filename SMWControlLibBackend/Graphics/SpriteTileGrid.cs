using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Graphics.DirtyClasses;
using SMWControlLibBackend.Interfaces.Graphics;
using SMWControlLibBackend.Keys.Graphics;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The sprite tile grid.
    /// </summary>
    public class SpriteTileGrid
    {
        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        public IGridDrawable Target { get; set; }
        public uint backgroundColor;
        /// <summary>
        /// Gets or sets the background color.
        /// </summary>
        public uint BackgroundColor
        { 
            get
            {
                return backgroundColor;
            }
            set
            {
                backgroundColor = BackgroundColor;
                _ = Parallel.ForEach(background, kvp => kvp.Value.SetDirty(false));
            }
        }
        /// <summary>
        /// Gets or sets the grid color.
        /// </summary>
        public uint GridColor { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether draw grid.
        /// </summary>
        public bool DrawGrid { get; set; }
        /// <summary>
        /// Gets or sets the zoom.
        /// </summary>
        public Zoom Zoom { get; set; }
        /// <summary>
        /// Gets or sets the cell size.
        /// </summary>
        public GridCellSize CellSize { get; set; }
        /// <summary>
        /// Gets or sets the grid type.
        /// </summary>
        public GridType GridType { get; set; }
        private readonly ConcurrentDictionary<Zoom, DirtyBitmap> background;
        private readonly ConcurrentDictionary<Zoom, DirtyBitmap> selection;
        private uint[] lastBackground, lastTargetImage;
        private uint[] image;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileGrid"/> class.
        /// </summary>
        public SpriteTileGrid()
        {
            background = new ConcurrentDictionary<Zoom, DirtyBitmap>();
            selection = new ConcurrentDictionary<Zoom, DirtyBitmap>();
        }

        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <returns>An array of uint.</returns>
        public uint[] GetGraphics()
        {
            if (!background.ContainsKey(Zoom)) 
            {
                background.TryAdd(Zoom, new DirtyBitmap(256 * Zoom, 256 * Zoom));
            }

            if (background[Zoom].IsDirty) 
            {
                _ = Parallel.For(0, background[Zoom].Bitmap.Length, i =>
                  {
                      background[Zoom].Bitmap[i] = BackgroundColor;
                  });
                background[Zoom].SetDirty(true);
            }

            if(background[Zoom].Bitmap != lastBackground)
            {
                lastBackground = background[Zoom].Bitmap;
            }

            if(Target != null && Target.GetGraphics(Zoom) != lastTargetImage)
            {
                lastTargetImage = Target.GetGraphics(Zoom);
            }

            int sideZoom = 256 * Zoom;
            int sideZoom2 = sideZoom * sideZoom;
            if(image.Length != sideZoom)
            {
                image = new uint[sideZoom2];
            }

            lastBackground.CopyTo(image, 0);

            if (Target != null)
            {
                int x = Target.X * Zoom;
                int y = Target.Y * Zoom;

                _ = Parallel.For(0, Target.Height, j =>
                {
                    int jwz = (y + j) * sideZoom;
                    int tjw = j * Target.Width;
                    _ = Parallel.For(0, Target.Width, i =>
                    {
                        image[jwz + x + i] = lastTargetImage[tjw + i];
                    });
                });
            }

            return image;
        }
        /// <summary>
        /// Addings the at position.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="selection">The selection.</param>
        public void AddingAtPosition(int x, int y, SpriteTileMaskCollection selection)
        {
            if (Target == null || selection == null || selection.Count <= 0)
            {
                return;
            }

            int zcs = Zoom * CellSize;
            selection.MoveTo(x / zcs, y / zcs);
            Target.AddTiles(selection);
        }
        /// <summary>
        /// Removes the.
        /// </summary>
        public void Remove()
        {
            if (Target != null)
                Target.RemoveTiles();
        }
        /// <summary>
        /// Selects the.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public void Select(int x, int y, int width, int height)
        {
            if (Target != null)
            {
                int zcs = Zoom * CellSize;
                Target.Select(x / zcs, y / zcs, width, height);
            }
        }
        /// <summary>
        /// Moves the to.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void MoveTo(int x, int y)
        {
            if (Target != null)
            {
                int zcs = Zoom * CellSize;
                Target.MoveTiles(x / zcs, y / zcs);
            }
        }
        /// <summary>
        /// Increases the z index.
        /// </summary>
        public void IncreaseZIndex()
        {
            if (Target != null)
                Target.IncreaseZIndex();
        }
        /// <summary>
        /// Decreases the z index.
        /// </summary>
        public void DecreaseZIndex()
        {
            if (Target != null)
                Target.DecreaseZIndex();
        }
    }
}
