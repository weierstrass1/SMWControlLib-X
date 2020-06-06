using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering.Enumerator;
using SMWControlLibRendering.KernelStrategies.BitmapBufferKernels;
using System;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The g p u bitmap buffer.
    /// </summary>
    public class GPUBitmapBuffer : BitmapBuffer
    {
        protected byte[] pixels;
        public MemoryBuffer3D<byte> Buffer { get; protected set; }
        protected GPUBitmapBuffer subBuffer { get; set; }
        public GPUBitmapBuffer(int width, int height, BytesPerPixel bpp) : base(width, height, bpp)
        {
        }
        public override void Initialize(int width, int height, BytesPerPixel bpp)
        {
            if (Buffer != null)
            {
                Buffer.Dispose();
            }

            Buffer = HardwareAcceleratorManager.GPUAccelerator.Allocate<byte>(bpp, width, height);
            requireCopyTo = true;
            base.Initialize(width, height, bpp);
        }
        public override void DrawBitmapBuffer(BitmapBuffer src, int dstXOffset, int dstYOffset, int srcXOffset, int srcYOffset, int zoom, byte[] backgroundColor)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            GPUBitmapBuffer source = (GPUBitmapBuffer)src;

            DrawBitmapBufferKernel.Execute(new Index2(src.Width, src.Height),
                Buffer, source.Buffer, new Index2(dstXOffset, dstYOffset), new Index2(srcXOffset, srcYOffset),
                BytesPerColor.BitClearer, zoom, backgroundColor);
            requireCopyTo = true;

            if (dstXOffset == 0 && dstYOffset == 0)
            {
                int a = 0;
            }
        }
        public override void DrawGrid(int zoom, int cellsize, int type, byte[] color)
        {
            throw new NotImplementedException();
        }
        public override void DrawLine(int x1, int y1, int x2, int y2, byte[] color)
        {
            throw new NotImplementedException();
        }
        public override void DrawRectangleBorder(int x, int y, int width, int height, byte[] color)
        {
            int offset = (y * Width) + x;
            DrawRectangleBorderKernel.Execute(Math.Max(width, height), Buffer,
                new Index2(x, y), width, height, color);
            requireCopyTo = true;
        }
        public override void FillWithColor(byte[] color)
        {
            FillWithColorKernel.Execute(new Index3(BytesPerColor, Width, Height), Buffer, color);
            requireCopyTo = true;
        }
        public override void DrawRectangle(int x, int y, int width, int height, byte[] color)
        {
            DrawRectangleKernel.Execute(new Index2(width, height), Buffer, new Index2(x, y), color);
            requireCopyTo = true;
        }
        public override void Dispose()
        {
            Buffer.Dispose();
        }
        public override unsafe void CopyTo(byte* target, int subImageLeft, int subImageRight, int subImageTop, int subImageBottom, int dirtyLeft, int dirtyRight, int dirtyTop, int dirtyBottom)
        {
            if (subImageLeft < 0) subImageLeft = 0;
            if (subImageTop < 0) subImageTop = 0;
            if (subImageLeft >= Width) subImageLeft = Width - 1;
            if (subImageTop >= Height) subImageTop = Height - 1;

            if (subImageRight >= Width) subImageRight = Width - 1;
            if (subImageBottom >= Height) subImageBottom = Height - 1;
            if (subImageRight <= subImageLeft) subImageRight = subImageLeft;
            if (subImageBottom <= subImageTop) subImageBottom = subImageTop;

            int w = 1 + subImageRight - subImageLeft;
            int h = 1 + subImageBottom - subImageTop;

            int l = w * h * BytesPerColor;

            if (subBuffer == null)
            {
                subBuffer = (GPUBitmapBuffer)CreateInstance(w, h, BytesPerColor);
                pixels = new byte[l];
                requireCopyTo = true;
            }
            else if (subBuffer.Buffer.Width != BytesPerColor || subBuffer.Buffer.Height != w || subBuffer.Buffer.Depth != h)
            {
                subBuffer.Initialize(w, h, BytesPerColor);
                pixels = new byte[l];
                requireCopyTo = true;
            }

            //if(requireCopyTo)
            //{
            DrawSubBufferKernel.Execute(Buffer, subBuffer.Buffer, new Index2(subImageLeft, subImageTop));
            subBuffer.requireCopyTo = false;
            subBuffer.Buffer.CopyTo(pixels, Index3.Zero, 0, subBuffer.Buffer.Extent);
            requireCopyTo = false;
            //}

            fixed (byte* bs = pixels)
            {
                System.Buffer.MemoryCopy(bs, target, l, l);
            }
        }
    }
}