using SMWControlLibRendering.Factory;
using SMWControlLibUtils;
using System;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The bitmap buffer.
    /// </summary>
    public abstract class BitmapBuffer<T> : CanFactory<T[], int> where T : struct
    {
        private static readonly BitmapBufferFactory<T> factory = new BitmapBufferFactory<T>();
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        /// <returns>A BitmapBuffer.</returns>
        public static BitmapBuffer<T> CreateInstance(T[] pixels, int width)
        {
            return factory.GenerateObject(pixels, width);
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
        /// Gets or sets the pixels.
        /// </summary>
        public abstract T[] Pixels { get; protected set; }
        /// <summary>
        /// Gets the length.
        /// </summary>
        public int Length => Pixels.Length;
        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapBuffer"/> class.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        public BitmapBuffer(T[] pixels, int width) : base(pixels, width)
        {
        }
        /// <summary>
        /// Sets the pixels.
        /// </summary>
        /// <param name="pixls">The pixels.</param>
        /// <param name="width">The width.</param>
        public override void Initialize(T[] pixls, int width)
        {
            if (pixls != null && pixls.Length > 0 && pixls.Length % width == 0)  
            {
                Pixels = pixls;
                Width = width;
                Height = pixls.Length / width;
            }
            else
            {
                if (pixls == null)
                {
                    throw new NullReferenceException("Pixels parameter is null.");
                }

                if (pixls.Length <= 0)
                {
                    throw new Exception("Pixels length is 0.");
                }

                if (pixls.Length % width != 0)
                {
                    throw new Exception("Pixels length is not divisible by width.");
                }
            }
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public abstract void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y);
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        public abstract void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y, int zoom);
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y, T backgroundColor);
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y, int zoom, T backgroundColor);
        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public abstract void DrawRectangleBorder(int x, int y, int width, int height, T rectangleColor);
        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        public abstract void DrawLine(int x1, int y1, int x2, int y2, T lineColor);
        /// <summary>
        /// Draws the grid.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="cellsize">The cellsize.</param>
        /// <param name="type">The type.</param>
        /// <param name="gridColor">The grid color.</param>
        public abstract void DrawGrid(int zoom, int cellsize, int type, T gridColor);
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void FillWithColor(T backgroundColor);
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void DrawRectangle(int x, int y, int width, int height, T backgroundColor);
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
        public abstract void ZoomIn(int zoom, T backgroundColor);
        /// <summary>
        /// Clones the.
        /// </summary>
        /// <returns>A BitmapBuffer.</returns>
        public abstract BitmapBuffer<T> Clone();
    }
}
