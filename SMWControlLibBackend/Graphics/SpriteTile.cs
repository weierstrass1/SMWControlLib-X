using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibRendering;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// Represent an OAM tile of the SNES.
    /// </summary>
    public class SpriteTile<T> : GraphicBox<T> where T: BitmapBuffer, new()
    {
        /// <summary>
        /// Size of the tile. Normally 8x8 or 16x16.
        /// </summary>
        public SpriteTileSize Size { get; private set; }
        /// <summary>
        /// Gets the index.
        /// </summary>
        public SpriteTileIndex Index { get; private set; }

        /// <summary>
        /// Constructor of the class.
        /// </summary>
        /// <param name="size">Size of the tile.</param>
        public SpriteTile(SpriteTileSize size, SpriteTileIndex index) : base(size.Width, size.Height)
        {
            Size = size;
            Index = index;
        }
    }
}
