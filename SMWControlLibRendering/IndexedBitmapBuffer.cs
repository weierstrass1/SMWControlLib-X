using SMWControlLibRendering.DirtyClasses;
using SMWControlLibRendering.Enumerators;
using SMWControlLibRendering.Keys;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The indexed bitmap buffer.
    /// </summary>
    public abstract class IndexedBitmapBuffer<T, U> :   DirtyCollection<ZoomFlipColorPaletteKey<U>, uint, FlipColorPaletteKey<U>, 
                                                        DirtyBitmap<U>, BitmapBuffer<U>>    where T : struct
                                                                                            where U : struct
    {
        /// <summary>
        /// Gets or sets the indexes.
        /// </summary>
        public virtual T[,] Indexes { get; protected set; }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get
            {
                if (Indexes == null) return 0;
                return Indexes.GetLength(0);
            }
        }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get
            {
                if (Indexes == null) return 0;
                return Indexes.GetLength(1);
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedBitmapBuffer"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IndexedBitmapBuffer(int width, int height)
        {
            Indexes = new T[width, height];
        }
        /// <summary>
        /// Creates the bitmap buffer.
        /// </summary>
        /// <returns>A BitmapBuffer.</returns>
        public abstract BitmapBuffer<U> CreateBitmapBuffer(Flip flip, ColorPalette<U> palette);
        /// <summary>
        /// Creates the bitmap buffer.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <returns>A BitmapBuffer.</returns>
        public abstract BitmapBuffer<U> CreateBitmapBuffer(Flip flip, ColorPalette<U> palette, int zoom);
        /// <summary>
        /// Draws the indexed bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public abstract void DrawIndexedBitmap(IndexedBitmapBuffer<T, U> src, int x, int y);
        /// <summary>
        /// Draws the indexed bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="dstX">The dst x.</param>
        /// <param name="dstY">The dst y.</param>
        /// <param name="srcX">The src x.</param>
        /// <param name="srcY">The src y.</param>
        public abstract void DrawIndexedBitmap(IndexedBitmapBuffer<T, U> src, int dstX, int dstY, int srcX, int srcY);
    }
}
