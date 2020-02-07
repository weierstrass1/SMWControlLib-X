using SMWControlLibRendering.DirtyClasses;
using SMWControlLibRendering.Enumerators;
using SMWControlLibRendering.Keys;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The indexed bitmap buffer.
    /// </summary>
    public abstract class IndexedBitmapBuffer :   DirtyCollection<ZoomFlipColorPaletteKey, uint, FlipColorPaletteKey, 
                                                        DirtyBitmap, BitmapBuffer>
    {
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width { get; protected set; }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedBitmapBuffer"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IndexedBitmapBuffer(int width, int height)
        {
            Width = width;
            Height = height;
        }
        /// <summary>
        /// Creates the bitmap buffer.
        /// </summary>
        /// <returns>A BitmapBuffer.</returns>
        public abstract BitmapBuffer CreateBitmapBuffer(Flip flip, ColorPalette palette);
        /// <summary>
        /// Creates the bitmap buffer.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <returns>A BitmapBuffer.</returns>
        public abstract BitmapBuffer CreateBitmapBuffer(Flip flip, ColorPalette palette, int zoom);
        /// <summary>
        /// Draws the indexed bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public abstract void DrawIndexedBitmap(IndexedBitmapBuffer src, int x, int y);
        /// <summary>
        /// Draws the indexed bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="dstX">The dst x.</param>
        /// <param name="dstY">The dst y.</param>
        /// <param name="srcX">The src x.</param>
        /// <param name="srcY">The src y.</param>
        public abstract void DrawIndexedBitmap(IndexedBitmapBuffer src, int dstX, int dstY, int srcX, int srcY);
    }
}
