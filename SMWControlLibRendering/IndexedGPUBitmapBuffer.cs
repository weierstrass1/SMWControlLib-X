using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering.DirtyClasses;
using SMWControlLibRendering.Enumerators;
using SMWControlLibRendering.Exceptions;
using SMWControlLibRendering.KernelStrategies.IndexedBitmapBufferKernels;
using SMWControlLibRendering.Keys;
using System;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The g p u indexed bitmap buffer.
    /// </summary>
    public class IndexedGPUBitmapBuffer : IndexedBitmapBuffer
    {
        /// <summary>
        /// Gets or sets a value indicating whether require copy to.
        /// </summary>
        public bool RequireCopyTo { get; protected set; }
        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        public MemoryBuffer2D<byte> Buffer { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedGPUBitmapBuffer"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IndexedGPUBitmapBuffer(int width, int height) : base(width, height)
        {
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="args">The args.</param>
        public override void Initialize(params object[] args)
        {
            if (Buffer != null) Buffer.Dispose();
            base.Initialize(args);
            Buffer = HardwareAcceleratorManager.GPUAccelerator.Allocate<byte>((int)args[0], (int)args[1]);
            RequireCopyTo = true;
        }
        /// <summary>
        /// Creates the bitmap buffer.
        /// </summary>
        /// <param name="palette">The palette.</param>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer CreateBitmapBuffer(Flip flip, ColorPalette palette)
        {
            ZoomFlipColorPaletteKey key = new ZoomFlipColorPaletteKey(1, flip, palette);
            return DirtyAction(key,
                () =>
                    new DirtyBitmap(
                        new GPUBitmapBuffer(Width, Height)),
                (e) =>
                {
                    GPUColorPalette pal = (GPUColorPalette)palette;
                    CreateBitmapBufferByteRGB555Kernel.Execute(Buffer.Extent, Buffer, ((GPUBitmapBuffer)e.Bitmap).Buffer, pal.Buffer, flip);
                }).Bitmap;
        }
        /// <summary>
        /// Creates the bitmap buffer.
        /// </summary>
        /// <param name="palette">The palette.</param>
        /// <param name="zoom">The zoom.</param>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer CreateBitmapBuffer(Flip flip, ColorPalette palette, int zoom)
        {
            if (zoom < 2) zoom = 1;

            ZoomFlipColorPaletteKey key = new ZoomFlipColorPaletteKey((uint)zoom, flip, palette);
            if (zoom == 1)
                return CreateBitmapBuffer(flip, palette);

            return DirtyAction(key,
                () =>
                    new DirtyBitmap(
                        new GPUBitmapBuffer(Width * zoom, Height * zoom)),
                e =>
                {
                    GPUColorPalette pal = (GPUColorPalette)palette;
                    CreateBitmapBufferWithZoomByteRGB555Kernel.Execute(Buffer.Extent, Buffer, ((GPUBitmapBuffer)e.Bitmap).Buffer,
                        pal.Buffer, zoom, flip);
                }).Bitmap;
        }
        /// <summary>
        /// Draws the indexed bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public override void DrawIndexedBitmap(IndexedBitmapBuffer src, int x, int y)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if (src is IndexedGPUBitmapBuffer b)
            {
                if (x < 0) x = 0;
                if (y < 0) y = 0;
                if (x >= Width) x = Width - 1;
                if (y >= Height) y = Height - 1;

                int w = Math.Min(Width, src.Width + x) - x;
                if (w <= 0) return;
                int h = Math.Min(Height, src.Height + y) - y;
                if (h <= 0) return;

                DrawIndexedBitmapBufferByteKernel.Execute(new Index2(w, h), Buffer, b.Buffer, x, y);

                Dirty();

                RequireCopyTo = true;
            }
            else
            {
                throw new ArgumentNotIndexedGPUBitmapBufferException(nameof(src));
            }
        }
        /// <summary>
        /// Draws the indexed bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="dstX">The dst x.</param>
        /// <param name="dstY">The dst y.</param>
        /// <param name="srcX">The src x.</param>
        /// <param name="srcY">The src y.</param>
        public override void DrawIndexedBitmap(IndexedBitmapBuffer src, int dstX, int dstY, int srcX, int srcY)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if(src is IndexedGPUBitmapBuffer b)
            {
                if (dstX < 0) dstX = 0;
                if (dstY < 0) dstY = 0;
                if (dstX >= Width) dstX = Width - 1;
                if (dstY >= Height) dstY = Height - 1;

                if (srcX < 0) srcX = 0;
                if (srcY < 0) srcY = 0;
                if (srcX >= b.Width) srcX = b.Width - 1;
                if (srcY >= b.Height) srcY = b.Height - 1;

                int w = Math.Min(Width - dstX, b.Width - srcX);
                int h = Math.Min(Height - dstY, b.Height - srcY);

                DrawIndexedBitmapBufferWithOffsetByteKernel.Execute(new Index2(w, h), Buffer, b.Buffer,
                    dstX, dstY, srcX, srcY);

                Dirty();

                RequireCopyTo = true;
            }
            else
            {
                throw new ArgumentNotIndexedGPUBitmapBufferException(nameof(src));
            }
        }
    }
}
