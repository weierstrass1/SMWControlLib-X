using ILGPU.Runtime;
using SMWControlLibCommons.Graphics;
using SMWControlLibOptimization.Clustering;
using SMWControlLibOptimization.Keys;
using SMWControlLibOptimization.PaletteOptimizer.KernelStrategies;
using SMWControlLibRendering.Disguise;
using SMWControlLibRendering.Enumerators.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.PaletteOptimizer
{
    public class PaletteProcessor
    {
        public static int reverseBytes(int c)
        {
            int res = (int)(((c >> 24) & 0x000000FF) | ((c >> 8) & 0x0000FF00) |
                ((c << 8) & 0x00FF0000) | ((c << 24) & 0xFF000000));
            return res;
        }
        public static Int32[,] BitmapToIntArray(Bitmap bp)
        {
            Int32[] Bits = new Int32[bp.Width * bp.Height];
            GCHandle BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap bp2 = new Bitmap(bp.Width, bp.Height, bp.Width * 4, PixelFormat.Format32bppArgb,
                BitsHandle.AddrOfPinnedObject());

            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bp2))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(bp, 0, 0, bp.Width, bp.Height);
            }
            Int32[,] Bits2 = new Int32[bp.Width, bp.Height];
            int width = bp.Width;
            int height = bp.Height;

            Parallel.For(0, width, i =>
            {
                Parallel.For(0, height, j =>
                {
                    int index = i + (j * width);
                    Bits2[i, j] = Bits[index];
                });
            });
            return Bits2;
        }
        public static void RoundColor5BitsPerChannel(Int32[,] bp)
        {
            Parallel.For(0, bp.GetLength(0), i =>
            {
                Parallel.For(0, bp.GetLength(1), j =>
                {
                    bp[i, j] = (Int32)((bp[i, j] & 0xFFF8F8F8) | ((bp[i, j] & 0x00040404) << 1));
                });
            });
        }
        public static T[] ExtractPalettesFromBitmap<T, K>(int[,] Bit, ConcurrentDictionary<TileKey, int> tiles, int tileWidth, int tileHeight, int maxPaletteSize) where T : ColorPaletteDisguise, new()
                                                                                                                        where K : ColorPaletteIndex, new()
        {
            int width = Bit.GetLength(0);
            int height = Bit.GetLength(1);
            int wb = width / tileWidth;
            int hb = height / tileHeight;

            ConcurrentDictionary<TileKey, ConcurrentDictionary<Int32, int>> pals =
                new ConcurrentDictionary<TileKey, ConcurrentDictionary<int, int>>();

            Parallel.ForEach(tiles, kvp =>
            {
                int offx = kvp.Key.X * tileWidth;
                int offy = kvp.Key.Y * tileHeight;
                int x, y, c;
                ConcurrentDictionary<Int32, int> pal = new ConcurrentDictionary<int, int>();
                for (int i = 0; i < tileWidth; i++)
                {
                    x = offx + i;
                    for (int j = 0; j < tileHeight; j++)
                    {
                        y = offy + j;
                        c = Bit[x, y];

                        if (c != 0)
                        {
                            if (!pal.TryAdd(c, 1))
                                pal[c]++;
                        }
                    }
                }
                pals.TryAdd(kvp.Key, pal);
            });
            /*string s = "";
            foreach(var kvp in pals)
            {
                if (kvp.Value.Count > maxPaletteSize)
                    s += $"({kvp.Key.X * tileWidth},{kvp.Key.Y * tileHeight}),";
            }*/

            List<ConcurrentDictionary<Int32, int>> allPals = OptimizePalettes(pals, maxPaletteSize);

            return ColorDictionaryToPalette<T, K>(allPals, maxPaletteSize);
        }

        public static T[]
            ColorDictionaryToPalette<T, K>(List<ConcurrentDictionary<Int32, int>> pals, int palSize) where T : ColorPaletteDisguise, new()
                                                                                                     where K : ColorPaletteIndex, new()
        {
            T[] Palettes = new T[pals.Count];
            byte[] cols;
            int it = 0;
            int ind = 0;
            foreach (var p in pals)
            {
                Palettes[it] = ColorPaletteDisguise.Generate<T>(ColorPaletteIndex.Generate<K>(it, (palSize + 1) * it), (palSize + 1));

                cols = new byte[(palSize + 1) * 4];

                cols[0] = 0;
                cols[1] = 0;
                cols[2] = 0;
                cols[3] = 0;
                ind = 4;
                foreach (var kvp in p)
                {
                    cols[ind] = (byte)((kvp.Key >> 24) & 0x000000FF);
                    cols[ind + 1] = (byte)((kvp.Key >> 16) & 0x000000FF);
                    cols[ind + 2] = (byte)((kvp.Key >> 8) & 0x000000FF);
                    cols[ind + 3] = (byte)(kvp.Key & 0x000000FF);
                    ind += 4;
                }

                Palettes[it].RealObject.Load(cols);

                it++;
            }

            return Palettes;
        }
        public static List<ConcurrentDictionary<Int32, int>>
            OptimizePalettes(ConcurrentDictionary<TileKey, ConcurrentDictionary<Int32, int>> palMatrix,
            int maxPal)
        {
            ConcurrentDictionary<ConcurrentDictionary<Int32, int>, ConcurrentDictionary<TileKey, int>> tilesperPal =
                new ConcurrentDictionary<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>>();
            bool found;
            ConcurrentDictionary<TileKey, int> remlist;
            ConcurrentDictionary<TileKey, ConcurrentDictionary<Int32, int>> clone = new ConcurrentDictionary<TileKey, ConcurrentDictionary<int, int>>();

            Parallel.ForEach(palMatrix, kv =>
            {
                clone.TryAdd(kv.Key, kv.Value);
            });
            ConcurrentDictionary<Int32, int> p;

            KeyValuePair<TileKey, ConcurrentDictionary<Int32, int>> kvp1;
            
            while (clone.Count > 0)
            {
                kvp1 = clone.First();
                clone.TryRemove(kvp1.Key, out p);
                remlist = new ConcurrentDictionary<TileKey, int>();
                found = false;
                Parallel.ForEach(palMatrix, kvp2 =>
                {
                    if (kvp1.Key != kvp2.Key && CountDiffs(kvp1.Value, kvp2.Value) == 0)
                    {
                        if (kvp1.Value.Count >= kvp2.Value.Count)
                            remlist.TryAdd(kvp2.Key, 0);
                        else
                        {
                            found = true;
                        }
                    }
                });
                if (!found)
                {
                    remlist.TryAdd(kvp1.Key, 0);
                    tilesperPal.TryAdd(kvp1.Value, remlist);

                    foreach (var k in remlist)
                    {
                        clone.TryRemove(k.Key, out p);
                    }
                }
            }

            int size = 0;

            while (size != tilesperPal.Count)
            {
                size = tilesperPal.Count;
                ReducePaletteColors.Execute(tilesperPal, palMatrix);

                var ret = GPUHierarchicalClusteringSolver.Solve(tilesperPal.Keys.ToList(), maxPal);

                tilesperPal = new ConcurrentDictionary<ConcurrentDictionary<int, int>, ConcurrentDictionary<TileKey, int>>();
                ConcurrentDictionary<TileKey, int> tk;

                foreach (var pal in ret)
                {
                    tk = new ConcurrentDictionary<TileKey, int>();
                    Parallel.ForEach(palMatrix, kvp =>
                    {
                        bool notFound = false;
                        foreach (var col in kvp.Value)
                        {
                            if (!pal.ContainsKey(col.Key))
                            {
                                notFound = true;
                                break;
                            }
                        }
                        if (!notFound)
                            tk.TryAdd(kvp.Key, 0);
                    });
                    tilesperPal.TryAdd(pal, tk);
                }
            }

            ReducePaletteColors.Execute(tilesperPal, palMatrix);
            return tilesperPal.Keys.ToList();
        }

        public static int CountEquals(ConcurrentDictionary<Int32, int> p1, ConcurrentDictionary<Int32, int> p2)
        {
            return Math.Min(p1.Count, p2.Count) - CountDiffs(p1, p2);
        }

        public static int CountDiffs(ConcurrentDictionary<Int32, int> p1, ConcurrentDictionary<Int32, int> p2)
        {
            ConcurrentDictionary<Int32, int> res = p1;
            ConcurrentDictionary<Int32, int> rev = p2;
            if (p2.Count > p1.Count)
            {
                res = p2;
                rev = p1;
            }

            ConcurrentDictionary<Int32, int> delta = new ConcurrentDictionary<Int32, int>();

            Parallel.ForEach(res, kvp =>
            {
                delta.TryAdd(kvp.Key, kvp.Value);
            });

            Parallel.ForEach(rev, kvp =>
            {
                delta.TryAdd(kvp.Key, kvp.Value);
            });

            return delta.Count - res.Count;
        }

        public static ConcurrentDictionary<Int32, int> GetDiffs(ConcurrentDictionary<Int32, int> p1, ConcurrentDictionary<Int32, int> p2)
        {
            ConcurrentDictionary<Int32, int> res = p1;
            ConcurrentDictionary<Int32, int> rev = p2;
            if (p2.Count > p1.Count)
            {
                res = p2;
                rev = p1;
            }

            ConcurrentDictionary<Int32, int> ret = new ConcurrentDictionary<int, int>();

            Parallel.ForEach(rev, kvp =>
            {
                ret.TryAdd(kvp.Key, kvp.Value);
            });

            Parallel.ForEach(res, kvp =>
            {
                if (ret.ContainsKey(kvp.Key))
                    ret[kvp.Key] += kvp.Value;
                else
                    ret.TryAdd(kvp.Key, kvp.Value);
            });

            return ret;
        }
    }
}
