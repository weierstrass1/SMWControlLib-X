using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering.DirtyClasses;
using SMWControlLibRendering.Disguise;
using SMWControlLibRendering.Enumerators;
using SMWControlLibRendering.Exceptions;
using SMWControlLibRendering.KernelStrategies.IndexedBitmapBufferKernels;
using SMWControlLibRendering.Keys;
using System;
using System.Collections.Concurrent;
using System.Drawing;
using System.Threading.Tasks;

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
        /// <param name="zoom">The zoom.</param>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer CreateBitmapBuffer(Flip flip, ColorPalette palette, int zoom)
        {
            if (zoom < 1) zoom = 1;

            ZoomFlipColorPaletteKey key = new ZoomFlipColorPaletteKey((uint)zoom, flip, palette);

            return DirtyAction(key,
                () =>
                    new DirtyBitmap(
                        (GPUBitmapBuffer)BitmapBuffer.CreateInstance(Width * zoom, Height * zoom, palette.BytesPerColor)),
                e =>
                {
                    GPUColorPalette pal = (GPUColorPalette)palette;
                    CreateBitmapBufferKernel.Execute(new Index3(palette.BytesPerColor, Buffer.Extent), Buffer, ((GPUBitmapBuffer)e.Bitmap).Buffer,
                        pal.Buffer, zoom, flip);
                }).Bitmap;
        }
        public override void DrawIndexedBitmap(IndexedBitmapBuffer src, int dstX, int dstY, int srcX, int srcY)
        {
            if (src == null) throw new ArgumentNullException(nameof(src));
            if (src is IndexedGPUBitmapBuffer b)
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

        public override void BuildFromBitmap(Int32[,] bp, ConcurrentDictionary<Int32, int> coldic, int xOffset, int yOffset)
        {
            if (bp == null)
                throw new ArgumentNullException(nameof(bp));
            if (coldic == null)
                throw new ArgumentNullException(nameof(coldic));

            int sizew = Math.Min(Width, bp.GetLength(0) - xOffset);
            int sizeh = Math.Min(Height, bp.GetLength(1) - yOffset);

            int[,] til = new int[sizew, sizeh];

            Parallel.For(0, sizew, i =>
            {
                Parallel.For(0, sizeh, j =>
                {
                    til[i, j] = bp[i + xOffset, j + yOffset];
                });
            });

            MemoryBuffer2D<Int32> buff = HardwareAcceleratorManager.GPUAccelerator.Allocate<Int32>(sizew, sizeh);

            buff.CopyFrom(til, Index2.Zero, Index2.Zero, buff.Extent);

            int[] kvps = new int[coldic.Count];

            Parallel.ForEach(coldic, kvp =>
            {
                kvps[kvp.Value] = kvp.Key;
            });

            MemoryBuffer<int> pal = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(kvps.Length);
            pal.CopyFrom(kvps, 0, 0, kvps.Length);

            BuildFromBitmapKernel.Execute(buff.Extent, Buffer, buff, pal);

            pal.Dispose();
            buff.Dispose();
        }
    }
}
