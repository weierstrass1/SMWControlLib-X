using SMWControlLibBackend.Enumerators.Graphics;

namespace SMWControlLibBackend.Graphics.DirtyClasses
{
    /// <summary>
    /// The dirty sprite tile.
    /// </summary>
    public class DirtySpriteTile : DirtyClass<SpriteTile>
    {
        /// <summary>
        /// Gets the tile.
        /// </summary>
        public SpriteTile Tile
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
        public DirtySpriteTile(SpriteTileSize size, SpriteTileIndex index) : base(new SpriteTile(size, index))
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
