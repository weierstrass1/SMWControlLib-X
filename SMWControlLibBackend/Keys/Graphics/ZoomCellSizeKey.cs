using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibUtils;

namespace SMWControlLibBackend.Keys.Graphics
{
    /// <summary>
    /// The zoom cell size key.
    /// </summary>
    public class ZoomCellSizeKey : DualKey<Zoom, GridCellSize>
    {
        /// <summary>
        /// Gets the zoom.
        /// </summary>
        public Zoom Zoom => element1;
        /// <summary>
        /// Gets the cell size.
        /// </summary>
        public GridCellSize CellSize => element2;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomCellSizeKey"/> class.
        /// </summary>
        /// <param name="z">The z.</param>
        /// <param name="cellsize">The cellsize.</param>
        public ZoomCellSizeKey(Zoom z, GridCellSize cellsize) : base(z, cellsize)
        {

        }
    }
}
