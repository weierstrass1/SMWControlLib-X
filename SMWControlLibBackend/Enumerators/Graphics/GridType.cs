using SMWControlLibUtils;

namespace SMWControlLibBackend.Enumerators.Graphics
{
    /// <summary>
    /// The grid type.
    /// </summary>
    public class GridType : FakeEnumerator
    {
        public static GridType Line = new GridType(0);
        public static GridType DashedLine = new GridType(1);
        public static GridType DottedLine = new GridType(2);
        /// <summary>
        /// Initializes a new instance of the <see cref="GridType"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public GridType(int value) : base(value)
        {

        }
    }
}
