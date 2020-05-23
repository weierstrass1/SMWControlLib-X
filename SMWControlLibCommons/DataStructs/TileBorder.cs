using SMWControlLibCommons.Enumerators.Graphics;
using System;

namespace SMWControlLibCommons.DataStructs
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
        public int Width { get; protected set; }

        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; protected set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileBorder"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="size">The size.</param>
        public TileBorder(int x, int y, int width, int heigth)
        {
            X = x;
            Y = y;
            Width = width;
            Height = heigth;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="TileBorder"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="size">The size.</param>
        public TileBorder(int x, int y, TileSize size)
        {
            if (size == null) throw new ArgumentNullException(nameof(size));
            X = x;
            Y = y;
            Width = size.Width;
            Height = size.Height;
        }
    }
}