using System;
using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering.KernelStrategies.BitmapBuffer;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The g p u bitmap buffer.
    /// </summary>
    public class GPUBitmapBuffer<T> : BitmapBuffer<T> where T : struct
    {
        protected bool disposed = false;
        protected bool requireCopyTo = true;
        protected T[] pixels;
        /// <summary>
        /// Gets or sets the pixels.
        /// </summary>
        public override T[] Pixels
        {
            get
            {
                if (requireCopyTo)
                {
                    Buffer.CopyTo(pixels, Index.Zero, 0, Buffer.Extent);
                    requireCopyTo = false;
                }
                return pixels;
            }
            protected set => pixels = value;
        }
        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        public MemoryBuffer<T> Buffer { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GPUBitmapBuffer"/> class.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        public GPUBitmapBuffer(T[] pixels, int width) : base(pixels, width)
        {
        }
        /// <summary>
        /// Sets the pixels.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        public override void Initialize(T[] pixls, int width)
        {
            if (Buffer != null)
            {
                Buffer.Dispose();
            }

            Buffer = HardwareAcceleratorManager.GPUAccelerator.Allocate<T>(pixls.Length);
            base.Initialize(pixls, width);
            Buffer.CopyFrom(pixls, 0, Index.Zero, Buffer.Extent);
        }
        /// <summary>
        /// Clones the.
        /// </summary>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer<T> Clone()
        {
            GPUBitmapBuffer<T> clone = new GPUBitmapBuffer<T>(new T[Length], Width);
            clone.DrawBitmapBuffer(this, 0, 0);
            return clone;
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public override void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y)
        {
            GPUBitmapBuffer<T> source = (GPUBitmapBuffer<T>)src;

            DrawBitmapBuffer<T>.Execute(new Index2(src.Width,src.Height), 
                Buffer, source.Buffer, x + (y * Width), Width, src.Width);
            requireCopyTo = true;
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        public override void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y, int zoom)
        {
            GPUBitmapBuffer<T> source = (GPUBitmapBuffer<T>)src;

            DrawBitmapBufferWithZoom<T>.Execute(new Index2(src.Width, src.Height),
                Buffer, source.Buffer, x + (y * Width), Width, src.Width, zoom);
            requireCopyTo = true;
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y, T backgroundColor)
        {
            GPUBitmapBuffer<T> source = (GPUBitmapBuffer<T>)src;

            DrawBitmapBufferWithBG<T>.Execute(new Index2(src.Width, src.Height),
                Buffer, source.Buffer, x + (y * Width), Width, src.Width, 
                backgroundColor);
            requireCopyTo = true;
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void DrawBitmapBuffer(BitmapBuffer<T> src, int x, int y, int zoom, T backgroundColor)
        {
            GPUBitmapBuffer<T> source = (GPUBitmapBuffer<T>)src;

            DrawBitmapBufferWithZoomAndBG<T>.Execute(new Index2(src.Width, src.Height),
                Buffer, source.Buffer, x + (y * Width), Width, src.Width, zoom, 
                backgroundColor);
            requireCopyTo = true;
        }
        /// <summary>
        /// Draws the grid.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="cellsize">The cellsize.</param>
        /// <param name="type">The type.</param>
        /// <param name="gridColor">The grid color.</param>
        public override void DrawGrid(int zoom, int cellsize, int type, T gridColor)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="lineColor">The line color.</param>
        public override void DrawLine(int x1, int y1, int x2, int y2, T lineColor)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="rectangleColor">The rectangle color.</param>
        public override void DrawRectangleBorder(int x, int y, int width, int height, T rectangleColor)
        {
            int offset = (y * Width) + x;
            DrawRectangleBorder<T>.Execute(new Index(Math.Max(width, height)), Buffer, offset, Width,
                offset + (width - 1) * Width, offset + height - 1, width, height, rectangleColor);
            requireCopyTo = true;
        }
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        public override void FillWithColor(T backgroundColor)
        {
            FillWithColor<T>.Execute(Buffer.Extent, Buffer, backgroundColor);
            requireCopyTo = true;
        }
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void DrawRectangle(int x, int y, int width, int height, T backgroundColor)
        {
            DrawRectangle<T>.Execute(new Index2(width, height), Buffer, (y * Width) + x, Width, backgroundColor);
            requireCopyTo = true;
        }
        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        public override void ZoomIn(int zoom)
        {
            GPUBitmapBuffer<T> b = new GPUBitmapBuffer<T>(Pixels, Width);

            int wz = Width * zoom;
            Initialize(new T[wz * Height * zoom], wz);

            DrawBitmapBuffer(b, 0, 0, zoom);
        }

        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void ZoomIn(int zoom, T backgroundColor)
        {
            GPUBitmapBuffer<T> b = new GPUBitmapBuffer<T>(Pixels, Width);

            int wz = Width * zoom;
            Initialize(new T[wz * Height * zoom], wz);

            DrawBitmapBuffer(b, 0, 0, zoom, backgroundColor);
        }
    }
}
