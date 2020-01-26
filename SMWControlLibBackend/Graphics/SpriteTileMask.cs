using SMWControlLibBackend.Enumerators.Graphics;
using System.Collections;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The sprite tile mask.
    /// </summary>
    public class SpriteTileMask : IComparer
    {
        /// <summary>
        /// Gets the tile.
        /// </summary>
        public SpriteTile Tile { get; private set; }
        /// <summary>
        /// Gets the properties.
        /// </summary>
        public SpriteTileProperties Properties { get; private set; }
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public int X { get; set; }
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public int Y { get; set; }
        /// <summary>
        /// Gets or sets the z.
        /// </summary>
        public uint Z { get; set; }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get
            {
                return Tile.Size.Width;
            }
        }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get
            {
                return Tile.Size.Height;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileMask"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="tile">The tile.</param>
        /// <param name="props">The props.</param>
        public SpriteTileMask(int x, int y, SpriteTile tile, SpriteTileProperties props)
        {
            Tile = tile;
            X = x;
            Y = y;
            Z = 0;
            Properties = props;
        }
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <returns>An array of uint.</returns>
        public uint[] GetGraphics(Zoom z)
        {
            return Tile.GetGraphics(Properties.Palette, z);
        }
        /// <summary>
        /// Compares the.
        /// </summary>
        /// <param name="o1">The o1.</param>
        /// <param name="o2">The o2.</param>
        /// <returns>An int.</returns>
        public int Compare(object o1, object o2)
        {
            SpriteTileMask x = (SpriteTileMask)o1;
            SpriteTileMask y = (SpriteTileMask)o2;
            if (x.Z < y.Z) return -1;
            if (x.Z > y.Z) return 1;
            return 0;
        }

        /// <summary>
        /// Clones the.
        /// </summary>
        /// <returns>A SpriteTileMask.</returns>
        public SpriteTileMask Clone()
        {
            return new SpriteTileMask(X, Y, Tile, Properties);
        }
    }
}
