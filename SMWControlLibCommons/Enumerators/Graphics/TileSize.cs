using SMWControlLibUtils;

namespace SMWControlLibCommons.Enumerators.Graphics
{
    /// <summary>
    /// Enumerator for possible OAM tile sizes.
    /// </summary>
    public class TileSize : FakeEnumerator
    {
        /// <summary>
        /// Value used on SNES to represent that size
        /// </summary>
        protected string StringValue { get; private set; }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get
            {
                return Value;
            }
        }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; private set; }
        /// <summary>
        /// Prevents a default instance of the <see cref="TileSize"/> class from being created.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="snesValue">The snes value.</param>
        protected TileSize(int width, int height, string stringValue) : base(width)
        {
            StringValue = stringValue;
            Height = height;
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
