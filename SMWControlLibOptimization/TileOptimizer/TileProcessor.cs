using ILGPU;
using ILGPU.Runtime;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibOptimization.Keys;
using SMWControlLibOptimization.TileOptimizer.KernelStrategies;
using SMWControlLibRendering;
using SMWControlLibRendering.Disguise;
using SMWControlLibRendering.Enumerator;
using SMWControlLibRendering.Enumerators.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.TileOptimizer
{
    public class TileProcessor
    {
        public static Tuple<ConcurrentDictionary<TileKey, int>, ConcurrentDictionary<TileKey, TileKey>>
            GetUniqueTilesPositions(int[,] bitmap, int tilewidth, int tileheight)
        {
            int w = bitmap.GetLength(0);
            int h = bitmap.GetLength(1);
            int l;
            Index2 extent = new Index2(tilewidth, tileheight);
            ConcurrentQueue<TileKey> ts = new ConcurrentQueue<TileKey>();
            ConcurrentDictionary<TileKey, int> remainingVals = new ConcurrentDictionary<TileKey, int>();
            Tuple<ConcurrentDictionary<TileKey, int>, ConcurrentDictionary<TileKey, TileKey>> ret = null;

            using (MemoryBuffer2D<int> bpbuffer = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(w, h))
            {
                bpbuffer.CopyFrom(bitmap, Index2.Zero, Index2.Zero, bpbuffer.Extent);
                w /= tilewidth;
                h /= tileheight;
                l = w * h;
                Parallel.For(0, l, ind =>
                  {
                      int i1 = ind % w;
                      int j1 = ind / w;
                      TileKey k1 = new TileKey(i1, j1, w);
                      if (!TileIsBlank.Execute(extent, bpbuffer, new Index2(i1 * tilewidth, j1 * tileheight)))
                          remainingVals.TryAdd(k1, ind);
                  });

                ret = CheckRepeatedKernel.Execute(remainingVals, bpbuffer, extent.X, extent.Y);
            }

            return ret;
        }

        public static ConcurrentDictionary<ColorPaletteIndex, List<Tuple<TileKey, bool, bool, K>>>
            GetTiles<T, K>(T[] palettes, ConcurrentDictionary<TileKey, int> tiles, ConcurrentDictionary<TileKey,TileKey> alltiles, Int32[,] bp, int tileWidth, int tileHeight) where T : ColorPaletteDisguise, new()
                                                                                                                            where K : IndexedBitmapBufferDisguise, new()
        {
            BytesPerPixel bpp = palettes[0].RealObject.BytesPerColor;
            ConcurrentDictionary<Int32, int> colDic;
            ConcurrentDictionary<TileKey, K> curtileQ;
            int rm;
            int flip;
            ConcurrentDictionary<TileKey, int> cloneTiles = new ConcurrentDictionary<TileKey, int>();
            ConcurrentQueue<Tuple<TileKey, bool, bool, K>> results;

            Parallel.ForEach(tiles, kvp =>
            {
                cloneTiles.TryAdd(kvp.Key, 0);
            });

            ConcurrentDictionary<ColorPaletteIndex, List<Tuple<TileKey, bool, bool, K>>> ret = new ConcurrentDictionary<ColorPaletteIndex, List<Tuple<TileKey, bool, bool, K>>>();

            using (MemoryBuffer2D<int> bpbuff = HardwareAcceleratorManager.GPUAccelerator.Allocate<int>(bp.GetLength(0), bp.GetLength(1)))
            {
                bpbuff.CopyFrom(bp, Index2.Zero, Index2.Zero, bpbuff.Extent);
                for (int x = 0; x < palettes.Length; x++)
                {
                    colDic = palettes[x].RealObject.ToColorDictionary();
                    curtileQ = new ConcurrentDictionary<TileKey, K>();
                    results = new ConcurrentQueue<Tuple<TileKey, bool, bool, K>>();
                    Parallel.ForEach(cloneTiles, kvp =>
                    {
                        int curpX = 0;
                        int curpY = 0;
                        int offX = kvp.Key.X * tileWidth;
                        int offY = kvp.Key.Y * tileHeight;
                        int c;
                        bool add = true;
                        K curTile;
                        for (int i = 0; i < tileWidth && add; i++)
                        {
                            curpX = i + offX;
                            for (int j = 0; j < tileHeight; j++)
                            {
                                curpY = j + offY;
                                c = bp[curpX, curpY];
                                c = bpp.ShortColor(c);
                                if (!colDic.ContainsKey(c))
                                {
                                    add = false;
                                    break;
                                }
                            }
                        }
                        if (add)
                        {
                            curTile = IndexedBitmapBufferDisguise.Generate<K>(tileWidth, tileHeight);
                            curTile.RealObject.BuildFromBitmap(bp, colDic, offX, offY);
                            curtileQ.TryAdd(kvp.Key, curTile);
                        }
                    });

                    Parallel.ForEach(curtileQ, t =>
                    {
                        cloneTiles.TryRemove(t.Key, out rm);
                    });

                    foreach(var kvp in alltiles)
                    {
                        if (kvp.Value.X == -1 && curtileQ.ContainsKey(kvp.Key))
                        {
                            results.Enqueue(new Tuple<TileKey, bool, bool, K>(kvp.Key, false, false, curtileQ[kvp.Key]));
                        }
                        else if (curtileQ.ContainsKey(kvp.Value)) 
                        {
                            flip = TileCheckerKernel.Execute(new Index2(tileWidth, tileHeight), bpbuff, 
                                new Index2(kvp.Value.X * tileWidth, kvp.Value.Y * tileHeight), new Index2(kvp.Key.X * tileWidth, kvp.Key.Y * tileHeight));
                            if (flip >= 0)
                            {
                                results.Enqueue(new Tuple<TileKey, bool, bool, K>(kvp.Key, (flip & 1) == 1, (flip & 2) == 2, curtileQ[kvp.Value]));
                            }
                        }
                    }

                    ret.TryAdd(palettes[x].RealObject.Index, results.ToList());
                }
            }

            return ret;
        }
    }
}
