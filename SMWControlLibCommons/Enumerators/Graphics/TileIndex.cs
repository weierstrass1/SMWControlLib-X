using SMWControlLibUtils;
using System.Globalization;

namespace SMWControlLibCommons.Enumerators.Graphics
{
    /// <summary>
    /// The sprite tile index.
    /// </summary>
    public class TileIndex : FakeEnumerator
    {
        /// <summary>
        /// Gets the x.
        /// </summary>
        public int X { get; protected set; }
        /// <summary>
        /// Gets the y.
        /// </summary>
        public int Y { get; protected set; }
        /// <summary>
        /// Gets the s n e s value.
        /// </summary>
        protected string StringValue { get; set; }
        /// <summary>
        /// Prevents a default instance of the <see cref="TileIndex"/> class from being created.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public TileIndex(int x, int y, int width) : base(x + (y * width))
        {
            X = x;
            Y = y;
            StringValue = Value.ToString("X2", CultureInfo.InvariantCulture);
        }
        /// <summary>
        /// Tos the string.
        /// </summary>
        /// <returns>A string.</returns>
        public override string ToString()
        {
            return StringValue;
        }
    }
}
