using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibUtils;

namespace SMWControlLibCommons.Graphics.DirtyClasses
{
    /// <summary>
    /// The dirty sprite tile.
    /// </summary>
    public class DirtyTile<T, U> : DirtyClass<Tile<T, U>> where T : struct
                                                          where U : struct
    {
        /// <summary>
        /// Gets the tile.
        /// </summary>
        public Tile<T, U> Tile
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
        public DirtyTile(TileSize size, TileIndex index) : base(new Tile<T, U>(size, index))
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
                Tile.RealObject.Dirty();
            }
        }
    }
}
