using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Graphics;

namespace SMWControlLibBackend.Interfaces.Graphics
{
    public interface IGridDrawable : ITileCollection
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        int X { get; set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        int Y { get; set; }
        /// <summary>
        /// Gets the width.
        /// </summary>
        int Width { get; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        int Height { get; }
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>An array of uint.</returns>
        uint[] GetGraphics(Zoom z);
        /// <summary>
        /// Selects the.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        void Select(int x, int y, int width, int height);
        /// <summary>
        /// Moves the tiles.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        void MoveTiles(int x, int y);
        /// <summary>
        /// Increases the z index.
        /// </summary>
        void IncreaseZIndex();
        /// <summary>
        /// Decreases the z index.
        /// </summary>
        void DecreaseZIndex();
    }
}
