using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering.DirtyClasses;
using SMWControlLibRendering.Enumerators;
using SMWControlLibRendering.Keys;
using System;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The g p u indexed bitmap buffer.
    /// </summary>
    public class IndexedGPUBitmapBuffer<T> : IndexedBitmapBuffer<T,GPUBitmapBuffer> where T : struct, IComparable, IConvertible, IFormattable
    {
        private static readonly Action<Index2, ArrayView2D<T>, ArrayView<uint>, ArrayView<uint>> _CreateBitmap = 
            HardwareAcceleratorManager.GPUAccelerator
            .LoadAutoGroupedStreamKernel<Index2,ArrayView2D<T>,ArrayView<uint>,ArrayView<uint>>
            (createBitmap);
        private static readonly Action<Index2, ArrayView2D<T>, ArrayView<uint>, ArrayView<uint>, int> _CreateBitmapZoom =
            HardwareAcceleratorManager.GPUAccelerator
            .LoadAutoGroupedStreamKernel<Index2, ArrayView2D<T>, ArrayView<uint>, ArrayView<uint>, int>
            (createBitmap);
        private static readonly Action<Index2, ArrayView2D<T>, ArrayView2D<T>, int, int> _DrawIndexedBitmap =
            HardwareAcceleratorManager.GPUAccelerator
            .LoadAutoGroupedStreamKernel<Index2, ArrayView2D<T>, ArrayView2D<T>, int, int>
            (drawIndexedBitmap);
        /// <summary>
        /// draws the bitmap.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="indexedBitmapBuffer">The indexed bitmap buffer.</param>
        /// <param name="destBitmap">The dest bitmap.</param>
        /// <param name="colors">The colors.</param>
        private static void createBitmap(Index2 index, ArrayView2D<T> indexedBitmapBuffer, ArrayView<uint> destBitmap, ArrayView<uint> colors)
        {
            destBitmap[(index.Y * indexedBitmapBuffer.Width) + index.X] =
                colors[Convert.ToInt32(indexedBitmapBuffer[index])];
        }
        /// <summary>
        /// draws the bitmap.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="indexedBitmapBuffer">The indexed bitmap buffer.</param>
        /// <param name="destBitmap">The dest bitmap.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="zoom">The zoom.</param>
        private static void createBitmap(Index2 index, ArrayView2D<T> indexedBitmapBuffer, ArrayView<uint> destBitmap, ArrayView<uint> colors, int zoom)
        {
            int ind = Convert.ToInt32(indexedBitmapBuffer[index]);
            if (ind == 0) return;

            uint color = colors[ind];

            int wz = indexedBitmapBuffer.Width * zoom;
            int offset = ((index.Y * indexedBitmapBuffer.Width) + index.X) * zoom;
            int wzz = wz * zoom;
            for (int j = 0; j < wzz; j+= wz)
            {
                for (int i = 0; i < zoom; i++)
                {
                    destBitmap[offset + j] = color;
                }
            }
        }
        /// <summary>
        /// draws the indexed bitmap.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        private static void drawIndexedBitmap(Index2 index, ArrayView2D<T> destBuffer, ArrayView2D<T> srcBuffer, int x, int y)
        {
            destBuffer[index.X + x, index.Y + y] = srcBuffer[index];
        }
        /// <summary>
        /// Gets or sets a value indicating whether require copy to.
        /// </summary>
        public bool RequireCopyTo { get; protected set; }
        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        public MemoryBuffer2D<T> buffer { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedGPUBitmapBuffer"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IndexedGPUBitmapBuffer(int width, int height) : base(width, height)
        {
            buffer = HardwareAcceleratorManager.CreateGPUBuffer<T>(width, height);
            RequireCopyTo = false;
        }
        /// <summary>
        /// Creates the bitmap buffer.
        /// </summary>
        /// <param name="palette">The palette.</param>
        /// <returns>A BitmapBuffer.</returns>
        public override BitmapBuffer CreateBitmapBuffer(Flip flip, ColorPalette palette)
        {
            ZoomFlipColorPaletteKey key = new ZoomFlipColorPaletteKey(1, flip, palette);
            if(!bitmapsbuffers.ContainsKey(key))
            {
                GPUBitmapBuffer bitmap =
                    BitmapBuffer.CreateInstance<GPUBitmapBuffer>(new uint[Width * Height], Width);
                bitmapsbuffers.TryAdd(key, new DirtyBitmap<GPUBitmapBuffer>(bitmap));
            }

            DirtyBitmap<GPUBitmapBuffer> dbm = bitmapsbuffers[key];

            if (dbm.IsDirty)
            {
                GPUColorPalette pal = (GPUColorPalette)palette;
                _CreateBitmap(buffer.Extent, buffer, dbm.Bitmap.Buffer, pal.Buffer);
                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                dbm.SetDirty(false);
            }
            return dbm.Bitmap;
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
            if (!bitmapsbuffers.ContainsKey(key))
            {
                GPUBitmapBuffer bitmap =
                    BitmapBuffer.CreateInstance<GPUBitmapBuffer>(new uint[Width * Height], Width);
                bitmapsbuffers.TryAdd(key, new DirtyBitmap<GPUBitmapBuffer>(bitmap));
            }

            DirtyBitmap<GPUBitmapBuffer> dbm = bitmapsbuffers[key];

            if (dbm.IsDirty)
            {
                GPUColorPalette pal = (GPUColorPalette)palette;
                _CreateBitmapZoom(buffer.Extent, buffer, dbm.Bitmap.Buffer, pal.Buffer, zoom);
                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                dbm.SetDirty(false);
            }
            return dbm.Bitmap;
        }
        /// <summary>
        /// Draws the indexed bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public override void DrawIndexedBitmap(IndexedBitmapBuffer<T, GPUBitmapBuffer> src, int x, int y)
        {
            if (x < 0) x = 0;
            if (y < 0) y = 0;
            if (x >= Width) x = Width - 1;
            if (y >= Height) y = Height - 1;

            int w = Math.Min(Width, src.Width + x) - x;
            if (w <= 0) return;
            int h = Math.Min(Height, src.Height + y) - y;
            if (h <= 0) return;

            IndexedGPUBitmapBuffer<T> srcibb = (IndexedGPUBitmapBuffer<T>)src;
            _DrawIndexedBitmap(new Index2(w, h), buffer, srcibb.buffer, x, y);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();

            Dirty();

            RequireCopyTo = true;
        }
    }
}
