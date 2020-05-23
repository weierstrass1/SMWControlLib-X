using SMWControlLibUtils;

namespace SMWControlLibCommons.Keys
{
    /// <summary>
    /// The position key.
    /// </summary>
    public class PositionKey : DualKey<uint, uint>
    {
        /// <summary>
        /// Gets the x.
        /// </summary>
        public uint X { get => element1; }

        /// <summary>
        /// Gets the y.
        /// </summary>
        public uint Y { get => element2; }

        /// <summary>
        /// Initializes a new instance of the <see cref="PositionKey"/> class.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public PositionKey(uint x, uint y) : base(x, y)
        {
        }
    }
}