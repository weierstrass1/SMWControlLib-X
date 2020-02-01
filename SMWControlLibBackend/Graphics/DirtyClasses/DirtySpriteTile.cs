using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibRendering;

namespace SMWControlLibBackend.Graphics.DirtyClasses
{
    /// <summary>
    /// The dirty sprite tile.
    /// </summary>
    public class DirtySpriteTile<T> : DirtyClass<SpriteTile<T>> where T: BitmapBuffer, new()
    {
        /// <summary>
        /// Gets the tile.
        /// </summary>
        public SpriteTile<T> Tile
        {
            get
            {
                return Object;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DirtySpriteTile"/> class.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <param name="index">The index.</param>
        public DirtySpriteTile(SpriteTileSize size, SpriteTileIndex index) : base(new SpriteTile<T>(size, index))
        {
        }

        /// <summary>
        /// Sets the dirty.
        /// </summary>
        /// <param name="d">If true, d.</param>
        public override void SetDirty(bool d)
        {
            base.SetDirty(d);
            if (d)
            {
                Tile.Dirty();
            }
        }
    }
}
