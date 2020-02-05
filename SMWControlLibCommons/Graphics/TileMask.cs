using SMWControlLibCommons.DataStructs;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibRendering;
using System.Collections;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// The sprite tile mask.
    /// </summary>
    public class TileMask<T, U> : IComparer where T : struct
                                            where U : struct
    {
        /// <summary>
        /// Gets the tile.
        /// </summary>
        public Tile<T, U> Tile { get; private set; }
        /// <summary>
        /// Gets the properties.
        /// </summary>
        public TileProperties<U> Properties { get; private set; }
        private int x;
        /// <summary>
        /// Gets or sets the x.
        /// </summary>
        public int X { get => x; set { x = value; Border.X = x; } }
        private int y;
        /// <summary>
        /// Gets or sets the y.
        /// </summary>
        public int Y { get => y; set { y = value; Border.Y = y; } }
        /// <summary>
        /// Gets or sets the z.
        /// </summary>
        public uint Z { get; set; }
        /// <summary>
        /// Gets the border.
        /// </summary>
        public TileBorder Border { get; private set; }
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
        /// Initializes a new instance of the <see cref="TileMask"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="tile">The tile.</param>
        /// <param name="props">The props.</param>
        public TileMask(int x, int y, Tile<T, U> tile, TileProperties<U> props)
        {
            Border = new TileBorder(x, y, tile.Size);
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
        public BitmapBuffer<U> GetGraphics(Zoom z)
        {
            return Tile.RealObject.CreateBitmapBuffer(Properties.Flip, Properties.Palette, z);
        }
        /// <summary>
        /// Compares the.
        /// </summary>
        /// <param name="o1">The o1.</param>
        /// <param name="o2">The o2.</param>
        /// <returns>An int.</returns>
        public int Compare(object o1, object o2)
        {
            TileMask<T, U> x = (TileMask<T, U>)o1;
            TileMask<T, U> y = (TileMask<T, U>)o2;
            if (x.Z < y.Z) return -1;
            if (x.Z > y.Z) return 1;
            return 0;
        }
        /// <summary>
        /// Clones the.
        /// </summary>
        /// <returns>A SpriteTileMask.</returns>
        public TileMask<T, U> Clone()
        {
            return new TileMask<T, U>(X, Y, Tile, Properties);
        }
        
    }
}
