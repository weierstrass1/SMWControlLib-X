﻿using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibBackend.Graphics.DirtyClasses;
using SMWControlLibBackend.Keys.Graphics;
using SMWControlLibBackend.Utils.Graphics;
using SMWControlLibRendering;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The graphic box.
    /// </summary>
    public class GraphicBox<T> where T: BitmapBuffer, new()
    {
        protected ConcurrentDictionary<ZoomPaletteKey, DirtyBitmap<T>> graphicsCollection;

        /// <summary>
        /// Gets the width.
        /// </summary>
        public int Width => graphicsMap.GetLength(0);
        /// <summary>
        /// Gets the height.
        /// </summary>
        public int Height => graphicsMap.GetLength(1);

        /// <summary>
        /// Graphics but using color palette.
        /// </summary>
        protected byte[,] graphicsMap { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GraphicBox"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public GraphicBox(int width, int height)
        {
            graphicsCollection = new ConcurrentDictionary<ZoomPaletteKey, DirtyBitmap<T>>();
            graphicsMap = new byte[width, height];
        }
        /// <summary>
        /// Gets the graphics.
        /// </summary>
        /// <param name="cp">The cp.</param>
        /// <param name="z">The z.</param>
        /// <returns>An array of uint.</returns>
        public virtual BitmapBuffer GetGraphics(ColorPalette cp, Zoom z)
        {
            ZoomPaletteKey nd = new ZoomPaletteKey(z, cp);

            if (graphicsCollection.ContainsKey(nd))
            {
                DirtyBitmap<T> d = graphicsCollection[nd];

                if (d.IsDirty)
                {
                    if (nd.Zoom > 1)
                        d = new DirtyBitmap<T>((T)BitmapBuffer.CreateInstance<T>(SnesGraphics.GetCreateBitmapIntPointer(cp, graphicsMap, z), Width * z));
                    else
                        d = new DirtyBitmap<T>((T)BitmapBuffer.CreateInstance<T>(SnesGraphics.GetCreateBitmapIntPointer(cp, graphicsMap), Width));
                }

                d.SetDirty(false);
                return d.Bitmap;
            }
            else
            {
                DirtyBitmap<T> d;

                if (nd.Zoom > 1)
                    d = new DirtyBitmap<T>((T)BitmapBuffer.CreateInstance<T>(SnesGraphics.GetCreateBitmapIntPointer(cp, graphicsMap, z), Width * z));
                else
                    d = new DirtyBitmap<T>((T)BitmapBuffer.CreateInstance<T>(SnesGraphics.GetCreateBitmapIntPointer(cp, graphicsMap), Width));

                d.SetDirty(false);

                cp.OnColorChange += (i, c) =>
                {
                    d.SetDirty(true);
                };

                cp.OnPaletteChange += () =>
                {
                    d.SetDirty(true);
                };

                graphicsCollection.AddOrUpdate(nd, d, (k, v) => { return null; });
                return d.Bitmap;
            }
        }

        /// <summary>
        /// Dirties the.
        /// </summary>
        public virtual void Dirty()
        {
            Parallel.ForEach(graphicsCollection, kvp =>
            {
                kvp.Value.SetDirty(true);
            });
        }
        /// <summary>
        /// Loads the g f x.
        /// </summary>
        /// <param name="path">The path.</param>
        public virtual void LoadGFX(string path)
        {
            LoadGFX(path, 0, 0, 0, 0);
        }
        /// <summary>
        /// Loads the g f x.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="dstHOffset">The dst h offset.</param>
        /// <param name="dstVOffset">The dst v offset.</param>
        public virtual void LoadGFX(string path, int dstHOffset, int dstVOffset)
        {
            LoadGFX(path, 0, 0, dstHOffset, dstVOffset);
        }
        /// <summary>
        /// Loads the g f x.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="srcHOffset">The src h offset.</param>
        /// <param name="srcVOffset">The src v offset.</param>
        /// <param name="dstHOffset">The dst h offset.</param>
        /// <param name="dstVOffset">The dst v offset.</param>
        public virtual void LoadGFX(string path, int srcHOffset, int srcVOffset, int dstHOffset, int dstVOffset)
        {
            byte[,] b = SnesGraphics.GenerateGFX(path);

            CopyFrom(b, srcHOffset, srcVOffset, dstHOffset, dstVOffset);
        }
        /// <summary>
        /// Copies the from.
        /// </summary>
        /// <param name="src">The src.</param>
        public virtual void CopyFrom(GraphicBox<T> src)
        {
            CopyFrom(src.graphicsMap, 0, 0, 0, 0);
        }
        /// <summary>
        /// Copies the from.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="srcHOffset">The src h offset.</param>
        /// <param name="srcVOffset">The src v offset.</param>
        public virtual void CopyFrom(GraphicBox<T> src, int srcHOffset, int srcVOffset)
        {
            CopyFrom(src.graphicsMap, srcHOffset, srcVOffset, 0, 0);
        }
        /// <summary>
        /// Copies the from.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="srcHOffset">The src h offset.</param>
        /// <param name="srcVOffset">The src v offset.</param>
        /// <param name="dstHOffset">The dst h offset.</param>
        /// <param name="dstVOffset">The dst v offset.</param>
        public virtual void CopyFrom(GraphicBox<T> src, int srcHOffset, int srcVOffset, int dstHOffset, int dstVOffset)
        {
            CopyFrom(src.graphicsMap, srcHOffset, srcVOffset, dstHOffset, dstVOffset);
        }
        /// <summary>
        /// Copies the from.
        /// </summary>
        /// <param name="src">The src.</param>
        public virtual void CopyFrom(byte[,] src)
        {
            CopyFrom(src, 0, 0, 0, 0);
        }
        /// <summary>
        /// Copies the from.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="srcHOffset">The src h offset.</param>
        /// <param name="srcVOffset">The src v offset.</param>
        public virtual void CopyFrom(byte[,] src, int srcHOffset, int srcVOffset)
        {
            CopyFrom(src, srcHOffset, srcVOffset, 0, 0);
        }
        /// <summary>
        /// Copies the from.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="srcHOffset">The src h offset.</param>
        /// <param name="srcVOffset">The src v offset.</param>
        /// <param name="dstHOffset">The dst h offset.</param>
        /// <param name="dstVOffset">The dst v offset.</param>
        public virtual void CopyFrom(byte[,] src, int srcHOffset, int srcVOffset, int dstHOffset, int dstVOffset)
        {
            Parallel.For(0, Math.Min(src.GetLength(0) - srcHOffset, graphicsMap.GetLength(0) - dstHOffset), i =>
            {
                Parallel.For(0, Math.Min(src.GetLength(1) - srcVOffset, graphicsMap.GetLength(1) - dstVOffset), j =>
                {
                    graphicsMap[i + dstHOffset, j + dstVOffset] = src[i + srcHOffset, j + srcVOffset];
                });
            });
        }
        /// <summary>
        /// Copies the not zero from.
        /// </summary>
        /// <param name="tile">The tile.</param>
        /// <param name="srcHOffset">The src h offset.</param>
        /// <param name="srcVOffset">The src v offset.</param>
        /// <param name="dstHOffset">The dst h offset.</param>
        /// <param name="dstVOffset">The dst v offset.</param>
        public virtual void CopyNotZeroFrom(SpriteTile<T> tile, int srcHOffset, int srcVOffset, int dstHOffset, int dstVOffset)
        {
            CopyNotZeroFrom(tile.graphicsMap, srcHOffset, srcVOffset, dstHOffset, dstVOffset);
        }
        /// <summary>
        /// Copies the not zero from.
        /// </summary>
        /// <param name="src">The src.</param>
        /// <param name="srcHOffset">The src h offset.</param>
        /// <param name="srcVOffset">The src v offset.</param>
        /// <param name="dstHOffset">The dst h offset.</param>
        /// <param name="dstVOffset">The dst v offset.</param>
        public virtual void CopyNotZeroFrom(byte[,] src, int srcHOffset, int srcVOffset, int dstHOffset, int dstVOffset)
        {
            Parallel.For(0, Math.Min(src.GetLength(0) - srcHOffset, graphicsMap.GetLength(0) - dstHOffset), i =>
            {
                Parallel.For(0, Math.Min(src.GetLength(1) - srcVOffset, graphicsMap.GetLength(1) - dstVOffset), j =>
                {
                    if (src[i + srcHOffset, j + srcVOffset] != 0)
                        graphicsMap[i + dstHOffset, j + dstVOffset] = src[i + srcHOffset, j + srcVOffset];
                });
            });
        }
    }
}
