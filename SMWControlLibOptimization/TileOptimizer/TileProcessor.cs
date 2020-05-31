using SMWControlLibRendering.Disguise;
using SMWControlLibRendering.Enumerator;
using SMWControlLibRendering.Enumerators.Graphics;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Threading.Tasks;

namespace SMWControlLibOptimization.TileOptimizer
{
    public class TileProcessor
    {
        public static ConcurrentDictionary<ColorPaletteIndex, List<Tuple<int,int,K>>> 
            GetTiles<T,K>(T[] palettes, Int32[,] bp, int tileWidth, int tileHeight) where T : ColorPaletteDisguise, new()
                                                                                                                            where K : IndexedBitmapBufferDisguise, new()
        {
            int width = bp.GetLength(0);
            int height = bp.GetLength(1);
            ConcurrentDictionary<ColorPaletteIndex, List<Tuple<int, int, K>>> result = new ConcurrentDictionary<ColorPaletteIndex, List<Tuple<int, int, K>>>();
            int wb = width / tileWidth;
            int hb = height / tileHeight;
            BytesPerPixel bpp = palettes[0].RealObject.BytesPerColor;

            for (int i = 0; i < palettes.Length; i++) 
            {
                ConcurrentDictionary<Int32, int> colDic = palettes[i].RealObject.ToColorDictionary();
                List<Tuple<int, int, K>> curTileList = new List<Tuple<int, int, K>>();
                int curpx;
                int curpy;
                int c;
                int ty;
                int tx;
                K curTile;
                bool add;
                bool blank;
                int index;

                for (int x = 0; x < wb; x++)
                {
                    tx = x * tileWidth;

                    for (int y = 0; y < hb; y++)
                    {
                        ty = y * tileHeight;
                        add = true;
                        blank = true;
                        for (int px = 0; px < tileWidth && add; px++)
                        {
                            curpx = px + tx;
                            for (int py = 0; py < tileHeight; py++)
                            {
                                curpy = py + ty;
                                c = bp[curpx, curpy];
                                c = bpp.ShortColor(c);
                                if (!colDic.ContainsKey(c))
                                {
                                    add = false;
                                    break;
                                }
                                if (c != 0) blank = false;
                            }
                        }
                        if (add && !blank) 
                        {
                            curTile = IndexedBitmapBufferDisguise.Generate<K>(tileWidth, tileHeight);
                            curTile.RealObject.BuildFromBitmap(bp, colDic, x * tileWidth, y * tileHeight);
                            curTileList.Add(new Tuple<int, int, K>(x, y, curTile));
                        }
                    }
                }
                result.TryAdd(palettes[i].RealObject.Index, curTileList);
            }
            return result;
        }
    }
}
