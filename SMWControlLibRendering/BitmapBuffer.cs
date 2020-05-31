using SMWControlLibRendering.Enumerator;
using SMWControlLibRendering.Factory;
using SMWControlLibUtils;
using System;

namespace SMWControlLibRendering
{
    public abstract class BitmapBuffer : CanFactory<int, int, BytesPerPixel>, IDisposable
    {
        protected bool requireCopyTo;
        private static readonly BitmapBufferFactory factory = new BitmapBufferFactory();
        public static BitmapBuffer CreateInstance(int width, int height, BytesPerPixel bpp)
        {
            return factory.GenerateObject(width, height, bpp);
        }
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public int Length { get; protected set; }
        public BytesPerPixel BytesPerColor { get; protected set; }
        public BitmapBuffer(int width, int height, BytesPerPixel bpp) : base(width, height, bpp)
        {
        }
        public override void Initialize(int width, int height, BytesPerPixel bpp)
        {
            BytesPerColor = bpp;
            Width = width;
            Height = height;
            Length = width * height * BytesPerColor;
            requireCopyTo = true;
        }
        public virtual void DrawBitmapBuffer(BitmapBuffer src)
        {
            DrawBitmapBuffer(src, 0, 0, 0, 0, 1, null);
        }
        public virtual void DrawBitmapBuffer(BitmapBuffer src, int zoom)
        {
            DrawBitmapBuffer(src, 0, 0, 0, 0, zoom, null);
        }
        public virtual void DrawBitmapBuffer(BitmapBuffer src, byte[] backgroundColor)
        {
            DrawBitmapBuffer(src, 0, 0, 0, 0, 1, backgroundColor);
        }
        public virtual void DrawBitmapBuffer(BitmapBuffer src, int zoom, byte[] backgroundColor)
        {
            DrawBitmapBuffer(src, 0, 0, 0, 0, zoom, backgroundColor);
        }
        public virtual void DrawBitmapBuffer(BitmapBuffer src, int dstXOffset, int dstYOffset)
        {
            DrawBitmapBuffer(src, dstXOffset, dstYOffset, 0, 0, 1, null);
        }
        public virtual void DrawBitmapBuffer(BitmapBuffer src, int dstXOffset, int dstYOffset, int zoom)
        {
            DrawBitmapBuffer(src, dstXOffset, dstYOffset, 0, 0, zoom, null);
        }
        public virtual void DrawBitmapBuffer(BitmapBuffer src, int dstXOffset, int dstYOffset, byte[] backgroundColor)
        {
            DrawBitmapBuffer(src, dstXOffset, dstYOffset, 0, 0, 1, backgroundColor);
        }
        public virtual void DrawBitmapBuffer(BitmapBuffer src, int dstXOffset, int dstYOffset, int zoom, byte[] backgroundColor)
        {
            DrawBitmapBuffer(src, dstXOffset, dstYOffset, 0, 0, zoom, backgroundColor);
        }
        public virtual void DrawBitmapBuffer(BitmapBuffer src, int dstXOffset, int dstYOffset, int srcXOffset, int srcYOffset)
        {
            DrawBitmapBuffer(src, dstXOffset, dstYOffset, srcXOffset, srcYOffset, 1, null);
        }
        public virtual void DrawBitmapBuffer(BitmapBuffer src, int dstXOffset, int dstYOffset, int srcXOffset, int srcYOffset, byte[] backgroundColor)
        {
            DrawBitmapBuffer(src, dstXOffset, dstYOffset, srcXOffset, srcYOffset, 1, backgroundColor);
        }
        public virtual void DrawBitmapBuffer(BitmapBuffer src, int dstXOffset, int dstYOffset, int srcXOffset, int srcYOffset, int zoom)
        {
            DrawBitmapBuffer(src, dstXOffset, dstYOffset, srcXOffset, srcYOffset, zoom, null);
        }
        public abstract void DrawBitmapBuffer(BitmapBuffer src, int dstXOffset, int dstYOffset, int srcXOffset, int srcYOffset, int zoom, byte[] backgroundColor);
        public abstract void DrawRectangleBorder(int x, int y, int width, int height, byte[] color);
        public abstract void DrawLine(int x1, int y1, int x2, int y2, byte[] color);
        public abstract void DrawGrid(int zoom, int cellsize, int type, byte[] color);
        public abstract void FillWithColor(byte[] color);
        public abstract void DrawRectangle(int x, int y, int width, int height, byte[] color);
        public virtual void ZoomIn(int zoom)
        {
            ZoomIn(zoom, null);
        }
        public virtual void ZoomIn(int zoom, byte[] color)
        {
            BitmapBuffer b = Clone();

            Initialize(Width * zoom, Height * zoom, BytesPerColor);

            DrawBitmapBuffer(b, 0, 0, zoom, color);
            requireCopyTo = true;
            b.Dispose();
        }
        public BitmapBuffer Clone()
        {
            BitmapBuffer clone = CreateInstance(Width, Height, BytesPerColor);
            clone.DrawBitmapBuffer(this, 0, 0);
            return clone;
        }
        public abstract unsafe void CopyTo(byte* target,
            int subImageLeft, int subImageRight, int subImageTop, int subImageBottom,
            int dirtyLeft, int dirtyRight, int dirtyTop, int dirtyBottom);
        public virtual void Dispose()
        {

        }
    }
}