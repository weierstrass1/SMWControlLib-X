using SMWControlLibUtils;

namespace SMWControlLibRendering.Enumerators.Graphics
{
    /// <summary>
    /// The color palette index.
    /// </summary>
    public class ColorPaletteIndex : FakeEnumerator
    {
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public int Offset { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPaletteIndex"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        protected ColorPaletteIndex(int value, int offset) : base(value)
        {
            Offset = offset;
        }
    }
}
