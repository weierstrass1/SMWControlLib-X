using SMWControlLibCommons.DataStructs;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Interfaces.Graphics;
using SMWControlLibRendering;
using System.Collections.Generic;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// The sprite tile frame.
    /// </summary>
    public class TileFrame<T> : IGridDrawable<T> where T: struct
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public int X { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public int Y { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int Height { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        /// <summary>
        /// Gets the left.
        /// </summary>
        public int Left => throw new System.NotImplementedException();

        /// <summary>
        /// Gets the top.
        /// </summary>
        public int Top => throw new System.NotImplementedException();

        /// <summary>
        /// Gets the right.
        /// </summary>
        public int Right => throw new System.NotImplementedException();

        /// <summary>
        /// Gets the bottom.
        /// </summary>
        public int Bottom => throw new System.NotImplementedException();

        /// <summary>
        /// Initializes a new instance of the <see cref="TileFrame"/> class.
        /// </summary>
        public TileFrame() : base()
        {

        }
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>An array of uint.</returns>
        public BitmapBuffer<T> GetGraphics(Zoom z)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Selects the.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public ITileCollection Select(int x, int y, int width, int height)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Adds the tiles.
        /// </summary>
        /// <param name="tiles">The tiles.</param>
        public void AddTiles(ITileCollection tiles)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Removes the tiles.
        /// </summary>
        public void RemoveTiles()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Moves the tiles.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public bool MoveTiles(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Increases the z index.
        /// </summary>
        public bool IncreaseZIndex()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Decreases the z index.
        /// </summary>
        public bool DecreaseZIndex()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Gets the tile borders.
        /// </summary>
        /// <returns>A list of TileBorders.</returns>
        public List<TileBorder> GetTileBorders()
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Are the empty.
        /// </summary>
        /// <returns>A bool.</returns>
        public bool IsEmpty()
        {
            throw new System.NotImplementedException();
        }
    }
}
