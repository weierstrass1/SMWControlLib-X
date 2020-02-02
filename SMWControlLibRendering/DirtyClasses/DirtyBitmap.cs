
using SMWControlLibUtils;

namespace SMWControlLibRendering.DirtyClasses
{
    /// <summary>
    /// The dirty bitmap.
    /// </summary>
    public class DirtyBitmap<T>: DirtyClass<T> where T: BitmapBuffer, new()
    {
        /// <summary>
        /// Gets or sets the bitmap.
        /// </summary>
        public T Bitmap { get => Object; set => Object = value; }
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
        public DirtyBitmap(int width, int height) : base((T)BitmapBuffer.CreateInstance<T>(new uint[width * height], width))
        {

        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DirtyBitmap"/> class.
        /// </summary>
        /// <param name="bitmap">The bitmap.</param>
        public DirtyBitmap(T bitmap) : base(bitmap)
        {

        }
    }
}
