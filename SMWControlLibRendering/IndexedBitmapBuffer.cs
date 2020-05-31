using SMWControlLibRendering.DirtyClasses;
using SMWControlLibRendering.Enumerators;
using SMWControlLibRendering.Keys;
using System;
using System.Collections.Concurrent;
using System.Drawing;

namespace SMWControlLibRendering
{
    public abstract class IndexedBitmapBuffer : DirtyCollection<ZoomFlipColorPaletteKey, uint, FlipColorPaletteKey,
                                                        DirtyBitmap, BitmapBuffer>
    {
        public int Width { get; protected set; }
        public int Height { get; protected set; }
        public IndexedBitmapBuffer(int width, int height) : base(width, height)
        {
        }
        public override void Initialize(params object[] args)
        {
            base.Initialize(args);
            Width = (int)args[0];
            Height = (int)args[1];
        }
        public virtual BitmapBuffer CreateBitmapBuffer(ColorPalette palette)
        {
            return CreateBitmapBuffer(Flip.NotFlipped, palette, 1);
        }
        public virtual BitmapBuffer CreateBitmapBuffer(Flip flip, ColorPalette palette)
        {
            return CreateBitmapBuffer(flip, palette, 1);
        }
        public abstract BitmapBuffer CreateBitmapBuffer(Flip flip, ColorPalette palette, int zoom);
        public virtual void DrawIndexedBitmap(IndexedBitmapBuffer src)
        {
            DrawIndexedBitmap(src, 0, 0, 0, 0);
        }
        public virtual void DrawIndexedBitmap(IndexedBitmapBuffer src, int dstX, int dstY)
        {
            DrawIndexedBitmap(src, dstX, dstY, 0, 0);
        }
        public abstract void DrawIndexedBitmap(IndexedBitmapBuffer src, int dstX, int dstY, int srcX, int srcY);
        public abstract void BuildFromBitmap(Int32[,] bp, ConcurrentDictionary<Int32, int> coldic, int xOffset, int yOffset);
    }
}
