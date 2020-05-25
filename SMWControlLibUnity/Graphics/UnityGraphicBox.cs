using System;
using System.Collections.Generic;
using System.Text;
using SMWControlLibCommons.Graphics;
using SMWControlLibUnity.Factory.Graphics;
using SMWControlLibUnity.Graphics.DirtyClasses;
using SMWControlLibRendering.Disguise;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Collections.Concurrent;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibUnity.Enumerators.Graphics;
using System.Linq;
using SMWControlLibOptimization.PaletteOptimizer;
using SMWControlLibRendering;
using SMWControlLibRendering.Enumerators.Graphics;

namespace SMWControlLibUnity.Graphics
{
    public class UnityGraphicBox : GraphicBox<DirtyUnityTile,UnityTile,DirtyUnityTileFactory>
    {
        protected TileSize tileSize;
        public UnityColorPalette[] Palettes { get; private set; }
        public UnityGraphicBox(UnityTileSize tsz) : base(1, 1)
        {
            tileSize = tsz;
        }

        public override void Load(string path, int offset)
        {
            Bitmap bp = new Bitmap(path);
            Int32[] Bits = new Int32[bp.Width * bp.Height];
            GCHandle BitsHandle = GCHandle.Alloc(Bits, GCHandleType.Pinned);
            Bitmap bp2 = new Bitmap(bp.Width, bp.Height, bp.Width * 4, PixelFormat.Format32bppArgb,
                BitsHandle.AddrOfPinnedObject());

            using (System.Drawing.Graphics g = System.Drawing.Graphics.FromImage(bp2))
            {
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.NearestNeighbor;
                g.DrawImage(bp, 0, 0);
            }

            int wb = bp.Width / tileSize.Width;
            int hb = bp.Height / tileSize.Height;

            ConcurrentDictionary<Int32, int>[,] pals = new ConcurrentDictionary<Int32, int>[wb, hb];

            Parallel.For(0, wb, i =>
            {
                Parallel.For(0, hb, j =>
                {
                    pals[i, j] = new ConcurrentDictionary<int, int>();
                });
            });

            int width = bp.Width;
            int height = bp.Height;
            Int32 val;

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    int index = i + (j * width);
                    int x = i / tileSize.Width;
                    int y = j / tileSize.Height;
                    val = (Int32)((Bits[index] & 0xF8F8F8F8) | ((Bits[index] & 0x04040404) << 1));

                    if (val != 0 && !pals[x, y].TryAdd(val, 1))
                    {
                        pals[x, y][val]++;
                    }
                }
            }

            List<ConcurrentDictionary<Int32, int>> resumedPals = new List<ConcurrentDictionary<int, int>>();
            ConcurrentDictionary<Int32, int> p1, p2;
            bool found = false;
            int it = 0;

            for (int i = 0; i < wb; i++)
            {
                for (int j = 0; j < hb; j++)
                {
                    found = false;
                    it = 0;
                    p1 = pals[i, j];
                    foreach (var t in resumedPals)
                    {
                        p2 = t;
                        int eee = PaletteProcessor.CountEquals(p1, p2);

                        if (eee == Math.Min(p1.Count, p2.Count))
                        {
                            found = true;

                            if (Math.Min(p1.Count, p2.Count) == p2.Count && p1.Count > p2.Count) 
                            {
                                resumedPals[it] = p1;
                            }
                            break;
                        }
                        it++;
                    }
                    if (!found)
                    {
                        resumedPals.Add(p1);
                    }
                }
            }

            resumedPals.Sort((t1, t2) =>
            {
                if (t1.Count > t2.Count) return 1;
                else if (t1.Count < t2.Count) return -1;
                return 0;
            });

            resumedPals = PaletteProcessor.OptimizePalettes(resumedPals,15);
            Palettes = new UnityColorPalette[resumedPals.Count + 1];
            byte[] cols;
            it = 0;
            int ind = 0;
            foreach (var p in resumedPals)
            {
                Palettes[it] = new UnityColorPalette(new UnityColorPaletteIndex(16, 16 * (it + 1)), 16);

                cols = new byte[16 * 4];

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
        }

        public override void Load(byte[] bin, int offset)
        {
        }
    }
}
