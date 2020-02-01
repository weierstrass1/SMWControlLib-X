using System;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The bitmap buffer.
    /// </summary>
    public abstract class BitmapBuffer
    {
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
        public abstract uint[] Pixels { get; protected set; }
        /// <summary>
        /// Gets the length.
        /// </summary>
        public int Length => Pixels.Length;
        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapBuffer"/> class.
        /// </summary>
        public BitmapBuffer()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="BitmapBuffer"/> class.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        public BitmapBuffer(uint[] pixels, int width)
        {
            SetPixels(pixels, width);
        }
        /// <summary>
        /// Sets the pixels.
        /// </summary>
        /// <param name="pixls">The pixels.</param>
        /// <param name="width">The width.</param>
        public virtual void SetPixels(uint[] pixls, int width)
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
        public abstract void DrawBitmap(BitmapBuffer src, int x, int y);
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        public abstract void DrawBitmap(BitmapBuffer src, int x, int y, int zoom);
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void DrawBitmap(BitmapBuffer src, int x, int y, uint backgroundColor);
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void DrawBitmap(BitmapBuffer src, int x, int y, int zoom, uint backgroundColor);
        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public abstract void DrawRectangle(int x, int y, int width, int height, uint rectangleColor);
        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        public abstract void DrawLine(int x1, int y1, int x2, int y2, uint lineColor);
        /// <summary>
        /// Draws the grid.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="cellsize">The cellsize.</param>
        /// <param name="type">The type.</param>
        /// <param name="gridColor">The grid color.</param>
        public abstract void DrawGrid(int zoom, int cellsize, int type, uint gridColor);
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void FillColor(uint backgroundColor);
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        public abstract void FillColor(int x, int y, int width, int height, uint backgroundColor);
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
        public abstract void ZoomIn(int zoom, uint backgroundColor);
        /// <summary>
        /// Clones the.
        /// </summary>
        /// <returns>A BitmapBuffer.</returns>
        public abstract BitmapBuffer Clone();
        /// <summary>
        /// Creates the instance.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        /// <returns>A BitmapBuffer.</returns>
        public static T CreateInstance<T>(uint[] pixels, int width) where T : BitmapBuffer, new()
        {
            T t = new T();
            return (T)t.Initialize(pixels, width);
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        /// <returns>A BitmapBuffer.</returns>
        protected abstract BitmapBuffer Initialize(uint[] pixels, int width);
    }
}
