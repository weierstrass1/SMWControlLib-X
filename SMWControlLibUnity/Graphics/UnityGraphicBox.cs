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
using SMWControlLibOptimization.TileOptimizer;
using SMWControlLibRendering.Enumerators;
using System.IO;

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

        public unsafe override void Load(string path, int offset)
        {
            Bitmap bp = new Bitmap(path);
            Int32[,] Bits = PaletteProcessor.BitmapToIntArray(bp);
            //PaletteProcessor.RoundColor5BitsPerChannel(Bits);
            UnityColorPalette[] pals = PaletteProcessor.ExtractPalettesFromBitmap<UnityColorPalette, UnityColorPaletteIndex>(Bits, tileSize.Width, tileSize.Height, 19);
            var tiles = TileProcessor.GetTiles<UnityColorPalette, IndexedBitmapBufferDisguise>(pals, Bits, tileSize.Width, tileSize.Height);

            int w = bp.Width;
            int h = bp.Height;
            
            Parallel.ForEach(tiles, kvp =>
            {
                BitmapBuffer bf = BitmapBuffer.CreateInstance(w, h, pals[0].RealObject.BytesPerColor);
                BitmapBuffer bpb;
                Int32[] retbp = new Int32[w * h];
                GCHandle BitsHandle = GCHandle.Alloc(retbp, GCHandleType.Pinned);
                Bitmap finishedBP = new Bitmap(w, h, w * 4, PixelFormat.Format32bppArgb, BitsHandle.AddrOfPinnedObject());
                byte[] ret = new byte[retbp.Length * 4];
                
                foreach (var t in kvp.Value)
                {
                    bpb = t.Item3.RealObject.CreateBitmapBuffer(Flip.NotFlipped, pals[kvp.Key].RealObject);
                    bf.DrawBitmapBuffer(bpb, t.Item1 * tileSize.Width, t.Item2 * tileSize.Height);
                }
                unsafe
                {
                    fixed(byte* bs = ret)
                    {
                        bf.CopyTo(bs, 0, w - 1, 0, h - 1, 0, 0, 0, 0);
                    }
                }
                Buffer.BlockCopy(ret, 0, retbp, 0, ret.Length);
                finishedBP.Save(Path.GetFileNameWithoutExtension(path) + kvp.Key.Value + ".png", 
                    ImageFormat.Png);
            });
        }

        public override void Load(byte[] bin, int offset)
        {
        }
    }
}
