namespace SMWControlLibRendering
{
    /// <summary>
    /// The color palette.
    /// </summary>
    public class ColorPalette
    {
        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        public uint[] Colors { get; protected set; }
        /// <summary>
        /// Gets the length.
        /// </summary>
        public int Length
        {
            get
            {
                if (Colors == null) return 0;
                return Colors.Length;
            }
        }
    }
}
