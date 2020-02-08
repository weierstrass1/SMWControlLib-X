using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibRendering.Disguise;
using System;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// Represent an OAM tile of the SNES.
    /// </summary>
    public class Tile : IndexedBitmapBufferDisguise
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
        public Tile(TileSize size, TileIndex index) : base(size != null ? size.Width : 0,
                                                            size != null ? size.Height : 0)
        {
            Size = size ?? throw new ArgumentNullException(nameof(size));
            Index = index ?? throw new ArgumentNullException(nameof(index));
        }
    }
}
