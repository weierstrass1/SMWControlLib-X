using System;
using System.Diagnostics;
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
        /// Gets or sets the sub buffer.
        /// </summary>
        protected GPUBitmapBuffer subBuffer { get; set; }
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
        /// <param name="subImageLeft">The sub image left.</param>
        /// <param name="subImageRight">The sub image right.</param>
        /// <param name="subImageTop">The sub image top.</param>
        /// <param name="subImageBottom">The sub image bottom.</param>
        /// <param name="dirtyLeft">The dirty left.</param>
        /// <param name="dirtyRight">The dirty right.</param>
        /// <param name="dirtyTop">The dirty top.</param>
        /// <param name="dirtyBottom">The dirty bottom.</param>
        public override unsafe void CopyTo(byte* target, int subImageLeft, int subImageRight, int subImageTop, int subImageBottom, int dirtyLeft, int dirtyRight, int dirtyTop, int dirtyBottom)
        {
            if (subImageLeft < 0) subImageLeft = 0;
            if (subImageTop < 0) subImageTop = 0;
            if (subImageLeft >= Width) subImageLeft = Width - 1;
            if (subImageTop >= Height) subImageTop = Height - 1;

            if (subImageRight <= subImageLeft) subImageRight = subImageLeft + 1;
            if (subImageBottom <= subImageTop) subImageBottom = subImageTop + 1;
            if (subImageRight >= Width) subImageRight = Width - 1;
            if (subImageBottom >= Height) subImageBottom = Height - 1;

            int w = subImageRight - subImageLeft;
            int h = subImageBottom - subImageTop;

            if (subImageRight == Width - 1) w++;
            if (subImageBottom == Height - 1) h++;

            int l = w * h * BytesPerColor;

            if (subBuffer == null)
            {
                subBuffer = (GPUBitmapBuffer)CreateInstance(w, h);
                pixels = new byte[l];
                requireCopyTo = true;
            }
            else if(subBuffer.Buffer.Extent != l)
            {
                subBuffer.Initialize(w, h);
                pixels = new byte[l];
                requireCopyTo = true;
            }

            //if(requireCopyTo)
            //{
            subBuffer.DrawBitmapBuffer(this, 0, 0, subImageLeft, subImageTop);
            subBuffer.Buffer.CopyTo(pixels, 0, 0, subBuffer.Buffer.Extent);
            requireCopyTo = false;
            //}

            fixed(byte* bs = pixels)
            {
                System.Buffer.MemoryCopy(bs, target, pixels.Length, pixels.Length);
            }
        }

        /// <summary>
        /// Draws the bitmap buffer.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="dstX">The dst x.</param>
        /// <param name="dstY">The dst y.</param>
        /// <param name="srcX">The src x.</param>
        /// <param name="srcY">The src y.</param>
        public override void DrawBitmapBuffer(BitmapBuffer src, int dstX, int dstY, int srcX, int srcY)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));

            GPUBitmapBuffer b = (GPUBitmapBuffer)src;

            int w = Math.Min(b.Width - srcX, Width - dstX);
            int h = Math.Min(b.Height - srcY, Height - dstY);

            DrawBitmapBufferRGB555WithOffsetKernel.Execute(new Index2(w, h), Buffer, b.Buffer, dstX, dstY, Width, srcX, srcY, src.Width);
            requireCopyTo = true;
        }

        /// <summary>
        /// Draws the bitmap buffer.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="dstX">The dst x.</param>
        /// <param name="dstY">The dst y.</param>
        /// <param name="srcX">The src x.</param>
        /// <param name="srcY">The src y.</param>
        /// <param name="zoom">The zoom.</param>
        public override void DrawBitmapBuffer(BitmapBuffer src, int dstX, int dstY, int srcX, int srcY, int zoom)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the bitmap buffer.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="dstX">The dst x.</param>
        /// <param name="dstY">The dst y.</param>
        /// <param name="srcX">The src x.</param>
        /// <param name="srcY">The src y.</param>
        /// <param name="backgroundColorR">The background color r.</param>
        /// <param name="backgroundColorG">The background color g.</param>
        /// <param name="backgroundColorB">The background color b.</param>
        public override void DrawBitmapBuffer(BitmapBuffer src, int dstX, int dstY, int srcX, int srcY, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Draws the bitmap buffer.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="dstX">The dst x.</param>
        /// <param name="dstY">The dst y.</param>
        /// <param name="srcX">The src x.</param>
        /// <param name="srcY">The src y.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColorR">The background color r.</param>
        /// <param name="backgroundColorG">The background color g.</param>
        /// <param name="backgroundColorB">The background color b.</param>
        public override void DrawBitmapBuffer(BitmapBuffer src, int dstX, int dstY, int srcX, int srcY, int zoom, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            throw new NotImplementedException();
        }
    }
}
