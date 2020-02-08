using SMWControlLibUtils;

namespace SMWControlLibCommons.Enumerators.Graphics
{
    /// <summary>
    /// The grid type.
    /// </summary>
    public class GridType : FakeEnumerator
    {
        public static readonly GridType Line = new GridType(0);
        public static readonly GridType DashedLine = new GridType(1);
        public static readonly GridType DottedLine = new GridType(2);
        /// <summary>
        /// Initializes a new instance of the <see cref="GridType"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        public GridType(int value) : base(value)
        {

        }
    }
}
