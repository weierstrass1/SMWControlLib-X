using SMWControlLibRendering.DirtyClasses;
using SMWControlLibRendering.Enumerators;
using SMWControlLibRendering.Keys;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The indexed bitmap buffer.
    /// </summary>
    public abstract class IndexedBitmapBuffer<T, U> where T : struct, IComparable, IConvertible, IFormattable
                                                    where U : BitmapBuffer, new()
    {
        protected readonly ConcurrentDictionary<ZoomFlipColorPaletteKey, DirtyBitmap<U>> bitmapsbuffers;
        /// <summary>
        /// Gets or sets the indexes.
        /// </summary>
        public virtual T[,] Indexes { get; protected set; }
        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width
        {
            get
            {
                if (Indexes == null) return 0;
                return Indexes.GetLength(0);
            }
        }
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height
        {
            get
            {
                if (Indexes == null) return 0;
                return Indexes.GetLength(1);
            }
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="IndexedBitmapBuffer"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public IndexedBitmapBuffer(int width, int height)
        {
            Indexes = new T[width, height];
            bitmapsbuffers = new ConcurrentDictionary<ZoomFlipColorPaletteKey, DirtyBitmap<U>>();
        }
        /// <summary>
        /// Creates the bitmap buffer.
        /// </summary>
        /// <returns>A BitmapBuffer.</returns>
        public abstract BitmapBuffer CreateBitmapBuffer(Flip flip, ColorPalette palette);
        /// <summary>
        /// Creates the bitmap buffer.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <returns>A BitmapBuffer.</returns>
        public abstract BitmapBuffer CreateBitmapBuffer(Flip flip, ColorPalette palette, int zoom);
        /// <summary>
        /// Draws the indexed bitmap.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        public abstract void DrawIndexedBitmap(IndexedBitmapBuffer<T, U> src, int x, int y);
        /// <summary>
        /// Dirties the.
        /// </summary>
        public virtual void Dirty()
        {
            _ = Parallel.ForEach(bitmapsbuffers, kvp =>
            {
                kvp.Value.SetDirty(true);
            });
        }
    }
}
