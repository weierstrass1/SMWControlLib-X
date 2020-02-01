using SMWControlLibBackend.Enumerators.Graphics;

namespace SMWControlLibBackend.Keys.Graphics
{
    /// <summary>
    /// The grid type zoom cell size key.
    /// </summary>
    public class GridTypeZoomCellSizeKey : DualKey<GridType, ZoomCellSizeKey>
    {
        /// <summary>
        /// Gets the grid type.
        /// </summary>
        public GridType GridType => element1;
        /// <summary>
        /// Gets the zoom.
        /// </summary>
        public Zoom Zoom => element2.Zoom;
        /// <summary>
        /// Gets the grid size.
        /// </summary>
        public GridCellSize GridSize => element2.CellSize;
        /// <summary>
        /// Initializes a new instance of the <see cref="GridTypeZoomCellSizeKey"/> class.
        /// </summary>
        /// <param name="gt">The gt.</param>
        /// <param name="z">The z.</param>
        /// <param name="cs">The cs.</param>
        public GridTypeZoomCellSizeKey(GridType gt, Zoom z, GridCellSize cs) : base(gt, new ZoomCellSizeKey(z, cs))
        {

        }
    }
}
