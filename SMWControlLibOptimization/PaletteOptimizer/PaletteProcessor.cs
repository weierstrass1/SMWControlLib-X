using SMWControlLibOptimization.Astar;
using SMWControlLibOptimization.Clustering;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using SMWControlLibRendering.Disguise;
using System.Runtime.InteropServices;
using System.Drawing;
using System.Drawing.Imaging;
using SMWControlLibRendering.Enumerators.Graphics;
using SMWControlLibUtils;
using SMWControlLibOptimization.Keys;

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
        public static T[] ExtractPalettesFromBitmap<T,K>(Int32[,] Bits, int tileWidth, int tileHeight, int maxPaletteSize)  where T: ColorPaletteDisguise, new()
                                                                                                                        where K : ColorPaletteIndex, new()
        {
            int width = Bits.GetLength(0);
            int height = Bits.GetLength(1);
            int wb = width / tileWidth;
            int hb = height / tileHeight;

            ConcurrentDictionary<TileKey, ConcurrentDictionary<Int32, int>> pals =
                new ConcurrentDictionary<TileKey, ConcurrentDictionary<int, int>>();

            Int32 val;
            TileKey k;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int x = i / tileWidth;
                    int y = j / tileHeight;
                    val = Bits[i, j];
                    k = new TileKey(x, y);

                    if (val != 0)
                    {
                        if (!pals.ContainsKey(k))
                            pals.TryAdd(k, new ConcurrentDictionary<int, int>());

                        if (!pals[k].TryAdd(val, 1))
                        {
                            pals[k][val]++;
                        }
                    }
                }
            }

            List<ConcurrentDictionary<Int32, int>> allPals = OptimizePalettes(pals, maxPaletteSize);

            return ColorDictionaryToPalette<T, K>(allPals, maxPaletteSize);
        }

        public static T[] 
            ColorDictionaryToPalette<T,K>(List<ConcurrentDictionary<Int32, int>> pals, int palSize) where T : ColorPaletteDisguise, new()
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

            List<ConcurrentDictionary<Int32, int>> res =
                HierarchicalClusteringSolver<ConcurrentDictionary<Int32, int>, PalettesClusterNode>.Solve(tilesperPal.Keys.ToList(), maxPal);

            return res;
        }

        public static int CountEquals(ConcurrentDictionary<Int32, int> p1, ConcurrentDictionary<Int32, int> p2)
        {
            ConcurrentDictionary<Int32, int> res = p1;
            ConcurrentDictionary<Int32, int> rev = p2;
            if (p2.Count > p1.Count)
            {
                res = p2;
                rev = p1;
            }

            int delta = 0;

            Parallel.ForEach(rev, kvp =>
            {
                if (res.ContainsKey(kvp.Key))
                    delta++;
            });

            return delta;
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

            int delta = 0;

            Parallel.ForEach(rev, kvp =>
            {
                if (!res.ContainsKey(kvp.Key))
                    delta++;
            });

            return delta;
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
