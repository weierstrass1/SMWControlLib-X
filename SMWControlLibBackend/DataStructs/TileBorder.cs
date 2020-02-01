using SMWControlLibBackend.Enumerators.Graphics;

namespace SMWControlLibBackend.DataStructs
{
    /// <summary>
    /// The tile border.
    /// </summary>
    public class TileBorder
    {
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width => Size.Width;
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height => Size.Height;
        /// <summary>
        /// Gets or sets the size.
        /// </summary>
        public SpriteTileSize Size { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TileBorder"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="size">The size.</param>
        public TileBorder(int x, int y, SpriteTileSize size)
        {
            X = x;
            Y = y;
            Size = size;
        }
    }
}
