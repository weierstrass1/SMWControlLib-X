using System;
using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering.KernelStrategies.BitmapBufferKernels;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The g p u bitmap buffer.
    /// </summary>
    public class GPUBitmapBuffer : BitmapBuffer
    {
        protected bool requireCopyTo;
        protected byte[] pixels;
        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        public MemoryBuffer<byte> Buffer { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="GPUBitmapBuffer"/> class.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        public GPUBitmapBuffer(int width, int height) : base(width, height)
        {
        }
        /// <summary>
        /// Sets the pixels.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        public override void Initialize(int width, int height)
        {
            BytesPerColor = 3;
            if (Buffer != null)
            {
                Buffer.Dispose();
            }
            int l = width * height * BytesPerColor;
            Buffer = HardwareAcceleratorManager.GPUAccelerator.Allocate<byte>(l);
            pixels = new byte[l];
            requireCopyTo = true;
            base.Initialize(width, height);
        }
        /// <summary>
        /// Clones the.
        /// </summary>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer Clone()
        {
            GPUBitmapBuffer clone = new GPUBitmapBuffer(Width, Height);
            clone.DrawBitmapBuffer(this, 0, 0);
            return clone;
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public override void DrawBitmapBuffer(BitmapBuffer src, int x, int y)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            GPUBitmapBuffer source = (GPUBitmapBuffer)src;

            DrawBitmapBufferRGB555Kernel.Execute(new Index2(src.Width,src.Height), 
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
        public override void DrawBitmapBuffer(BitmapBuffer src, int x, int y, int zoom)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            GPUBitmapBuffer source = (GPUBitmapBuffer)src;

            DrawBitmapBufferWithZoomRGB555Kernel.Execute(new Index2(src.Width, src.Height),
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
        public override void DrawBitmapBuffer(BitmapBuffer src, int x, int y, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            GPUBitmapBuffer source = (GPUBitmapBuffer)src;

            DrawBitmapBufferWithBGRGB555Kernel.Execute(new Index2(src.Width, src.Height),
                Buffer, source.Buffer, x + (y * Width), Width, src.Width,
                backgroundColorR, backgroundColorG, backgroundColorB);
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
        public override void DrawBitmapBuffer(BitmapBuffer src, int x, int y, int zoom, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            GPUBitmapBuffer source = (GPUBitmapBuffer)src;

            DrawBitmapBufferWithZoomAndBGRGB555Kernel.Execute(new Index2(src.Width, src.Height),
                Buffer, source.Buffer, x + (y * Width), Width, src.Width, zoom,
                backgroundColorR, backgroundColorG, backgroundColorB);
            requireCopyTo = true;
        }
        /// <summary>
        /// Draws the grid.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="cellsize">The cellsize.</param>
        /// <param name="type">The type.</param>
        /// <param name="gridColor">The grid color.</param>
        public override void DrawGrid(int zoom, int cellsize, int type, byte colorR, byte colorG, byte colorB)
        {
            requireCopyTo = true;
        }
        /// <summary>
        /// Draws the line.
        /// </summary>
        /// <param name="x1">The x1.</param>
        /// <param name="y1">The y1.</param>
        /// <param name="x2">The x2.</param>
        /// <param name="y2">The y2.</param>
        /// <param name="lineColor">The line color.</param>
        public override void DrawLine(int x1, int y1, int x2, int y2, byte colorR, byte colorG, byte colorB)
        {
            requireCopyTo = true;
        }
        /// <summary>
        /// Draws the rectangle.
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        /// <param name="rectangleColor">The rectangle color.</param>
        public override void DrawRectangleBorder(int x, int y, int width, int height, byte colorR, byte colorG, byte colorB)
        {
            int offset = (y * Width) + x;
            DrawRectangleBorderRGBKernel.Execute(new Index(Math.Max(width, height)), Buffer, offset, Width,
                offset + (width - 1) * Width, offset + height - 1, width, height, colorR, colorG, colorB);
            requireCopyTo = true;
        }
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        public override void FillWithColor(byte colorR, byte colorG, byte colorB)
        {
            FillWithColorRGBKernel.Execute(Buffer.Length / BytesPerColor, Buffer, colorR, colorG, colorB);
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
        public override void DrawRectangle(int x, int y, int width, int height, byte colorR, byte colorG, byte colorB)
        {
            DrawRectangleRGBKernel.Execute(new Index2(width, height), Buffer, (y * Width) + x, Width, colorR, colorG, colorB);
            requireCopyTo = true;
        }
        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        public override void ZoomIn(int zoom)
        {
            GPUBitmapBuffer b = new GPUBitmapBuffer(Width, Height)
            {
                Buffer = Buffer
            };

            Initialize(Width * zoom, Height * zoom);

            DrawBitmapBuffer(b, 0, 0, zoom);
            b.Buffer.Dispose();
            requireCopyTo = true;
        }

        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void ZoomIn(int zoom, byte colorR, byte colorG, byte colorB)
        {
            GPUBitmapBuffer b = new GPUBitmapBuffer(Width, Height)
            {
                Buffer = Buffer
            };

            Initialize(Width * zoom, Height * zoom);

            DrawBitmapBuffer(b, 0, 0, zoom, colorR, colorG, colorB);
            b.Buffer.Dispose();
            requireCopyTo = true;
        }

        /// <summary>
        /// Copies the to.
        /// </summary>
        /// <param name="target">The target.</param>
        public override unsafe void CopyTo(byte* target)
        {
            if(requireCopyTo)
            {
                Buffer.CopyTo(pixels, 0, 0, Buffer.Extent);
                requireCopyTo = false;
            }
            fixed (byte* bp = pixels)
            {
                System.Buffer.MemoryCopy(bp, target, Length, Length);
            }
        }
    }
}
