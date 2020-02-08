using SMWControlLibRendering.Factory;
using SMWControlLibUtils;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The bitmap buffer.
    /// </summary>
    public abstract class BitmapBuffer: CanFactory<int, int>
    {
        private static readonly BitmapBufferFactory factory = new BitmapBufferFactory();
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        /// <returns>A BitmapBuffer.</returns>
        public static BitmapBuffer CreateInstance(int width, int height)
        {
            return factory.GenerateObject(width, height);
        }
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        public int Width { get; protected set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int Height { get; protected set; }
        /// <summary>
        /// Gets the length.
        /// </summary>
        public int Length { get; protected set; }
        /// <summary>
        /// Gets or sets the bytes per color.
        /// </summary>
        public int BytesPerColor { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapBuffer"/> class.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        public BitmapBuffer(int width, int height) : base(width, height)
        {
        }
        /// <summary>
        /// Sets the pixels.
        /// </summary>
        /// <param name="pixls">The pixels.</param>
        /// <param name="width">The width.</param>
        public override void Initialize(int width, int height)
        {
            Width = width;
            Height = height;
            Length = width * height * BytesPerColor;
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public abstract void DrawBitmapBuffer(BitmapBuffer src, int x, int y);
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        public abstract void DrawBitmapBuffer(BitmapBuffer src, int x, int y, int zoom);
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void DrawBitmapBuffer(BitmapBuffer src, int x, int y, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB);
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void DrawBitmapBuffer(BitmapBuffer src, int x, int y, int zoom, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB);
        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public abstract void DrawRectangleBorder(int x, int y, int width, int height, byte colorR, byte colorG, byte colorB);
        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        public abstract void DrawLine(int x1, int y1, int x2, int y2, byte colorR, byte colorG, byte colorB);
        /// <summary>
        /// Draws the grid.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="cellsize">The cellsize.</param>
        /// <param name="type">The type.</param>
        /// <param name="gridColor">The grid color.</param>
        public abstract void DrawGrid(int zoom, int cellsize, int type, byte colorR, byte colorG, byte colorB);
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void FillWithColor(byte backgroundColorR, byte backgroundColorG, byte backgroundColorB);
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void DrawRectangle(int x, int y, int width, int height, byte colorR, byte colorG, byte colorB);
        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        public abstract void ZoomIn(int zoom);
        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void ZoomIn(int zoom, byte colorR, byte colorG, byte colorB);
        /// <summary>
        /// Clones the.
        /// </summary>
        /// <returns>A BitmapBuffer.</returns>
        public abstract BitmapBuffer Clone();
        /// <summary>
        /// Copies the to.
        /// </summary>
        /// <param name="target">The target.</param>
        public abstract unsafe void CopyTo(byte* target);
    }
}
