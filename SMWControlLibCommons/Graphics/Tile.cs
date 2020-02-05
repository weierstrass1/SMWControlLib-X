using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibRendering.Disguise;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// Represent an OAM tile of the SNES.
    /// </summary>
    public class Tile<T, U> : IndexedBitmapBufferDisguise<T, U> where T : struct
                                                                where U : struct
    {
        /// <summary>
        /// Size of the tile. Normally 8x8 or 16x16.
        /// </summary>
        public TileSize Size { get; private set; }
        /// <summary>
        /// Gets the index.
        /// </summary>
        public TileIndex Index { get; private set; }

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="size">Size of the tile.</param>
        public Tile(TileSize size, TileIndex index) : base(size.Width, size.Height)
        {
            Size = size;
            Index = index;
        }
    }
}
