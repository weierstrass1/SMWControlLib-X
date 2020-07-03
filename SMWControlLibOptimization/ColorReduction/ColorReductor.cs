using C5;
using SMWControlLibOptimization.Clustering;
using SMWControlLibRendering.Disguise;
using SMWControlLibRendering.Enumerators.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.ColorReduction
{
    public static class ColorReductor
    {
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

        public static ConcurrentDictionary<int, int> ExtractColors(Int32[,] bp)
        {
            ConcurrentDictionary<int, int> cs = new ConcurrentDictionary<int, int>();

            Parallel.For(0, bp.GetLength(0), i =>
            {
                Parallel.For(0, bp.GetLength(1), j =>
                {
                    if ((bp[i, j] & 0xFF000000) != 0) 
                    {
                        if (!cs.TryAdd(bp[i, j], 1))
                        {
                            cs[bp[i, j]]++;
                        }
                    }
                });
            });

            return cs;
        }

        public static T ReduceColorsFromBitmap<T, K>(int maxSize, Int32[,] bp) where T : ColorPaletteDisguise, new()
                                                                                where K : ColorPaletteIndex, new()
        {
            ConcurrentDictionary<int, int> cols = ExtractColors(bp);

            List<ColorGroup> clsgrs = new List<ColorGroup>();
            foreach (var kvp in cols) 
            {
                clsgrs.Add(new ColorGroup(kvp.Key, kvp.Value));
            }

            clsgrs = HierarchicalClusteringSolver<ColorGroup, ColorReductionClusterNode>.Solve(clsgrs, 100);

            IntervalHeap<ColorGroup> ih = new IntervalHeap<ColorGroup>();

            ih.Add(clsgrs[0]);

            ColorGroup aux = null;
            List<ColorGroup> ret = new List<ColorGroup>();

            while (ih.Count + ret.Count < maxSize)
            {
                aux = ih.DeleteMax();

                if (aux.LeftSon != null && aux.RightSon != null)
                {
                    ih.Add(aux.LeftSon);
                    ih.Add(aux.RightSon);
                }
                else
                {
                    ret.Add(aux);
                }
            }

            foreach (var cg in ih)
            {
                ret.Add(cg);
            }
            Int32[] Bits = new Int32[bp.GetLength(0)* bp.GetLength(1)];
            GCHandle BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap bitm = new Bitmap(bp.GetLength(0), bp.GetLength(1), bp.GetLength(0) * 4, PixelFormat.Format32bppArgb,
                BitsHandle.AddrOfPinnedObject());

            Parallel.For(0, bp.GetLength(0), x =>
            {
                Parallel.For(0, bp.GetLength(1), y =>
                {
                    ColorGroup cg = new ColorGroup(bp[x, y],1);
                    int mind = int.MaxValue;
                    ColorGroup minval = null;
                    int curd = 0;
                    foreach (var c in ret)
                    {
                        curd = c.Distance(cg);
                        if (curd < mind) 
                        {
                            mind = curd;
                            minval = c;
                        }
                    }

                    Bits[x + (y * bp.GetLength(1))] = (minval.A << 24) + (minval.R << 16) + (minval.G << 8) + (minval.B);
                });
            });

            bitm.Save("Try.png");

            T res = ColorPaletteDisguise.Generate<T>(ColorPaletteIndex.Generate<K>(0, 0), maxSize + 1);

            byte[] newcols = new byte[(maxSize + 1) * 4];

            int i = 4;

            foreach (var c in ret)
            {
                newcols[i] = (byte)c.A;
                newcols[i + 1] = (byte)c.R;
                newcols[i + 2] = (byte)c.G;
                newcols[i + 3] = (byte)c.B;
                i += 4;
            }

            res.RealObject.Load(newcols);

            return res;
        }
    }
}
