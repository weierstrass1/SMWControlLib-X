using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Interfaces.Graphics;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The sprite tile frame.
    /// </summary>
    public class SpriteTileFrame : IGridDrawable
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
        /// Initializes a new instance of the <see cref="SpriteTileFrame"/> class.
        /// </summary>
        public SpriteTileFrame() : base()
        {

        }
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>An array of uint.</returns>
        public uint[] GetGraphics(Zoom z)
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
        public void Select(int x, int y, int width, int height)
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
        /// Adds the tiles.
        /// </summary>
        /// <param name="selection">The selection.</param>
        public void AddTiles(SpriteTileMaskCollection selection)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Moves the tiles.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public void MoveTiles(int x, int y)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Increases the z index.
        /// </summary>
        public void IncreaseZIndex()
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Decreases the z index.
        /// </summary>
        public void DecreaseZIndex()
        {
            throw new System.NotImplementedException();
        }
    }
}
