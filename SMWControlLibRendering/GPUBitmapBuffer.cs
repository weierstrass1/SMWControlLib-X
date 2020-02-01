using System;
using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.Cuda;
using ILGPU.Runtime.OpenCL;
using SMWControlLibRendering.Interfaces;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The g p u bitmap buffer.
    /// </summary>
    public class GPUBitmapBuffer : BitmapBuffer
    {
        protected bool disposed = false;
        protected bool requireCopyTo = true;
        protected uint[] pixels;
        /// <summary>
        /// Gets or sets the pixels.
        /// </summary>
        public override uint[] Pixels
        {
            get
            {
                if (requireCopyTo)
                {
                    buffer.CopyTo(pixels, Index.Zero, 0, buffer.Extent);
                    requireCopyTo = false;
                }
                return pixels;
            }
            protected set => pixels = value;
        }
        protected MemoryBuffer<uint> buffer;
        protected static readonly Action<Index2, ArrayView<uint>, ArrayView<uint>, int, int, int> 
            _DrawKernel =
            HardwareAcceleratorManager.GPUAccelerator
            .LoadAutoGroupedStreamKernel<Index2, ArrayView<uint>, ArrayView<uint>, int, int, int>
                (drawKernel);
        protected static readonly Action<Index2, ArrayView<uint>, ArrayView<uint>, int, int, int, uint> 
            _DrawKernelWithBGColor =
            HardwareAcceleratorManager.GPUAccelerator
            .LoadAutoGroupedStreamKernel<Index2, ArrayView<uint>, ArrayView<uint>, int, int, int, uint>
                (drawKernel);
        protected static readonly Action<Index2, ArrayView<uint>, ArrayView<uint>, int, int, int, int> 
            _DrawKernelZoom =
            HardwareAcceleratorManager.GPUAccelerator
            .LoadAutoGroupedStreamKernel<Index2, ArrayView<uint>, ArrayView<uint>, int, int, int, int>
                (drawKernelZoom);
        protected static readonly Action<Index2, ArrayView<uint>, ArrayView<uint>, int, int, int, int, uint> 
            _DrawKernelZoomWithBGColor =
            HardwareAcceleratorManager.GPUAccelerator
            .LoadAutoGroupedStreamKernel<Index2, ArrayView<uint>, ArrayView<uint>, int, int, int, int, uint>
                (drawKernelZoom);
        protected static readonly Action<Index, ArrayView<uint>, uint> 
            _FillColor =
            HardwareAcceleratorManager.GPUAccelerator
            .LoadAutoGroupedStreamKernel<Index, ArrayView<uint>, uint>
                (fillColor);
        protected static readonly Action<Index2, ArrayView<uint>, int, int, uint> 
            _FillColorOffset =
            HardwareAcceleratorManager.GPUAccelerator
            .LoadAutoGroupedStreamKernel<Index2, ArrayView<uint>, int, int, uint>
                (fillColor);
        protected static readonly Action<Index, ArrayView<uint>, int, int, int, int, int, int, uint>
            _DrawRect =
            HardwareAcceleratorManager.GPUAccelerator
            .LoadAutoGroupedStreamKernel<Index, ArrayView<uint>, int, int, int, int, int, int, uint>
                (drawRectangle);
        /// <summary>
        /// draws the kernel.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        protected static void drawKernel
            (Index2 index, ArrayView<uint> destBuffer, ArrayView<uint> srcBuffer, int offset,
            int dstWidth, int srcWidth)
        {
            uint color = srcBuffer[(index.Y * srcWidth) + index.X];

            if ((color & 0XFF000000) == 0) return;

            destBuffer[(index.Y * dstWidth) + index.X + offset] = color;
        }
        /// <summary>
        /// draws the kernel.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="backgroundColor">The background color.</param>
        protected static void drawKernel
            (Index2 index, ArrayView<uint> destBuffer, ArrayView<uint> srcBuffer, int offset, 
            int dstWidth, int srcWidth, uint backgroundColor)
        {
            uint color = srcBuffer[(index.Y * srcWidth) + index.X];

            if ((color & 0XFF000000) == 0) 
                color = backgroundColor;

            destBuffer[(index.Y * dstWidth) + index.X + offset] = color;
        }
        /// <summary>
        /// draws the kernel zoom.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="width">The width.</param>
        /// <param name="zoom">The zoom.</param>
        protected static void drawKernelZoom
            (Index2 index, ArrayView<uint> destBuffer, ArrayView<uint> srcBuffer, int offset,
            int dstWidth, int srcWidth, int zoom)
        {
            uint color = srcBuffer[(index.Y * srcWidth) + index.X];

            if ((color & 0XFF000000) == 0) 
                return;

            int jw = offset;
            for (int j = 0; j < zoom; j++)
            {
                jw += dstWidth;
                for (int i = 0; i < zoom; i++)
                {
                    destBuffer[jw + i] = color;
                }
            }
        }
        /// <summary>
        /// draws the kernel zoom.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="width">The width.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        protected static void drawKernelZoom
            (Index2 index, ArrayView<uint> destBuffer, ArrayView<uint> srcBuffer, int offset,
            int dstWidth, int srcWidth, int zoom, uint backgroundColor)
        {
            uint color = srcBuffer[(index.Y * srcWidth) + index.X];

            if ((color & 0XFF000000) == 0) 
                color = backgroundColor;

            int jw = offset;
            for (int j = 0; j < zoom; j++)
            {
                jw += dstWidth;
                for (int i = 0; i < zoom; i++)
                {
                    destBuffer[jw + i] = color;
                }
            }
        }
        /// <summary>
        /// fills the color.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="backgroundColor">The background color.</param>
        protected static void fillColor(Index index, ArrayView<uint> destBuffer, uint backgroundColor)
        {
            destBuffer[index] = backgroundColor;
        }
        /// <summary>
        /// fills the color.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="backgroundColor">The background color.</param>
        protected static void fillColor(Index2 index, ArrayView<uint> destBuffer, int offset, int width, uint backgroundColor)
        {
            destBuffer[offset + (index.Y * width) + index.X] = backgroundColor;
        }
        /// <summary>
        /// draws the rectangle.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="dstWidth">The dst width.</param>
        /// <param name="xoffset">The xoffset.</param>
        /// <param name="yoffset">The yoffset.</param>
        /// <param name="rWidth">The r width.</param>
        /// <param name="rHeight">The r height.</param>
        /// <param name="rectangleColor">The rectangle color.</param>
        protected static void drawRectangle(Index index, ArrayView<uint> destBuffer, int offset, int dstWidth, int xoffset, int yoffset, int rWidth, int rHeight, uint rectangleColor)
        {
            if (index <= rWidth) 
            {
                destBuffer[offset + index] = rectangleColor;
                destBuffer[xoffset + index] = rectangleColor;
            }
            if (index <= rHeight)
            {
                int indw = index * dstWidth;
                destBuffer[offset + indw] = rectangleColor;
                destBuffer[yoffset + indw] = rectangleColor;
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GPUBitmapBuffer"/> class.
        /// </summary>
        public GPUBitmapBuffer() : base()
        {
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="GPUBitmapBuffer"/> class.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        public GPUBitmapBuffer(uint[] pixels, int width) : base(pixels, width)
        {
        }
        /// <summary>
        /// Sets the pixels.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        public override void SetPixels(uint[] pixls, int width)
        {
            if (buffer != null)
            {
                buffer.Dispose();
            }

            buffer = HardwareAcceleratorManager.CreateGPUBuffer<uint>(pixls.Length);
            base.SetPixels(pixls, width);
            buffer.CopyFrom(pixls, 0, Index.Zero, buffer.Extent);
        }
        /// <summary>
        /// Clones the.
        /// </summary>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer Clone()
        {
            GPUBitmapBuffer clone = new GPUBitmapBuffer(new uint[Length], Width);
            clone.DrawBitmap(this, 0, 0);
            return clone;
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public override void DrawBitmap(BitmapBuffer src, int x, int y)
        {
            GPUBitmapBuffer source = (GPUBitmapBuffer)src;

            _DrawKernel(new Index2(src.Width,src.Height), 
                buffer, source.buffer, x + (y * Width), Width, src.Width);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
            requireCopyTo = true;
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="zoom">The zoom.</param>
        public override void DrawBitmap(BitmapBuffer src, int x, int y, int zoom)
        {
            GPUBitmapBuffer source = (GPUBitmapBuffer)src;

            _DrawKernelZoom(new Index2(src.Width, src.Height),
                buffer, source.buffer, x + (y * Width), Width, src.Width, zoom);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
            requireCopyTo = true;
        }
        /// <summary>
        /// Draws the bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void DrawBitmap(BitmapBuffer src, int x, int y, uint backgroundColor)
        {
            GPUBitmapBuffer source = (GPUBitmapBuffer)src;

            _DrawKernelWithBGColor(new Index2(src.Width, src.Height),
                buffer, source.buffer, x + (y * Width), Width, src.Width, 
                backgroundColor);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
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
        public override void DrawBitmap(BitmapBuffer src, int x, int y, int zoom, uint backgroundColor)
        {
            GPUBitmapBuffer source = (GPUBitmapBuffer)src;

            _DrawKernelZoomWithBGColor(new Index2(src.Width, src.Height),
                buffer, source.buffer, x + (y * Width), Width, src.Width, zoom, 
                backgroundColor);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
            requireCopyTo = true;
        }
        /// <summary>
        /// Draws the grid.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="cellsize">The cellsize.</param>
        /// <param name="type">The type.</param>
        /// <param name="gridColor">The grid color.</param>
        public override void DrawGrid(int zoom, int cellsize, int type, uint gridColor)
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
        public override void DrawLine(int x1, int y1, int x2, int y2, uint lineColor)
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
        public override void DrawRectangle(int x, int y, int width, int height, uint rectangleColor)
        {
            int offset = (y * Width) + x;
            _DrawRect(new Index(Math.Max(width, height)), buffer, offset, Width,
                offset + (width - 1) * Width, offset + height - 1, width, height, rectangleColor);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
            requireCopyTo = true;
        }
        /// <summary>
        /// Fills the color.
        /// </summary>
        /// <param name="backgroundColor">The background color.</param>
        public override void FillColor(uint backgroundColor)
        {
            _FillColor(buffer.Extent, buffer, backgroundColor);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
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
        public override void FillColor(int x, int y, int width, int height, uint backgroundColor)
        {
            _FillColorOffset(new Index2(width, height), buffer, (y * Width) + x, Width, backgroundColor);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
            requireCopyTo = true;
        }
        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        public override void ZoomIn(int zoom)
        {
            GPUBitmapBuffer b = new GPUBitmapBuffer(Pixels, Width);

            int wz = Width * zoom;
            SetPixels(new uint[wz * Height * zoom], wz);

            DrawBitmap(b, 0, 0, zoom);
        }

        /// <summary>
        /// Zooms the in.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="backgroundColor">The background color.</param>
        public override void ZoomIn(int zoom, uint backgroundColor)
        {
            GPUBitmapBuffer b = new GPUBitmapBuffer(Pixels, Width);

            int wz = Width * zoom;
            SetPixels(new uint[wz * Height * zoom], wz);

            DrawBitmap(b, 0, 0, zoom, backgroundColor);
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="pixels">The pixels.</param>
        /// <param name="width">The width.</param>
        /// <returns>A BitmapBuffer.</returns>
        protected override BitmapBuffer Initialize(uint[] pixels, int width)
        {
            return new GPUBitmapBuffer(pixels, width);
        }
    }
}
