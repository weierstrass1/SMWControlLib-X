using SMWControlLibUtils;

namespace SMWControlLibCommons.Enumerators.Graphics
{
    /// <summary>
    /// The grid cell size.
    /// </summary>
    public class GridCellSize : FakeEnumerator
    {
        public static readonly GridCellSize Size1x1 = new GridCellSize(1);
        public static readonly GridCellSize Size2x2 = new GridCellSize(2);
        public static readonly GridCellSize Size4x4 = new GridCellSize(4);
        public static readonly GridCellSize Size8x8 = new GridCellSize(8);
        public static readonly GridCellSize Size16x16 = new GridCellSize(16);
        public static readonly GridCellSize Size32x32 = new GridCellSize(32);
        public static readonly GridCellSize Size64x64 = new GridCellSize(64);
        /// <summary>
        /// Initializes a new instance of the <see cref="GridCellSize"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public GridCellSize(int value) : base(value)
        {

        }
    }
}
