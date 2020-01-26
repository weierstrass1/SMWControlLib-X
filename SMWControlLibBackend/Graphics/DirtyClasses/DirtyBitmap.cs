namespace SMWControlLibBackend.Graphics.DirtyClasses
{
    /// <summary>
    /// The dirty bitmap.
    /// </summary>
    public class DirtyBitmap : DirtyClass<uint[]>
    {
        /// <summary>
        /// Gets or sets the bitmap.
        /// </summary>
        public uint[] Bitmap
        {
            get
            {
                return Object;
            }
            set
            {
                Object = value;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DirtyBitmap"/> class.
        /// </summary>
        public DirtyBitmap() : base(null)
        {

        }
        /// <summary>
        /// Initializes a new instance of the <see cref="DirtyBitmap"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public DirtyBitmap(int width, int height) : base(new uint[width * height])
        {
            
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirtyBitmap"/> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        public DirtyBitmap(uint[] bitmap) : base(bitmap)
        {

        }
    }
}
