using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics;
using SMWControlLibOptimization.ColorReduction;
using SMWControlLibOptimization.PaletteOptimizer;
using SMWControlLibOptimization.TileOptimizer;
using SMWControlLibRendering;
using SMWControlLibRendering.Disguise;
using SMWControlLibRendering.Enumerators;
using SMWControlLibUnity.Enumerators.Graphics;
using SMWControlLibUnity.Factory.Graphics;
using SMWControlLibUnity.Graphics.DirtyClasses;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace SMWControlLibUnity.Graphics
{
    public class UnityGraphicBox : GraphicBox<DirtyUnityTile, UnityTile, DirtyUnityTileFactory>
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

            PaletteProcessor.RoundColor5BitsPerChannel(Bits);
            ColorReductor.ReduceColorsFromBitmap<UnityColorPalette, UnityColorPaletteIndex>(15, Bits);
            var ts = TileProcessor.GetUniqueTilesPositions(Bits, tileSize.Width, tileSize.Height);
            UnityColorPalette[] pals = PaletteProcessor.ExtractPalettesFromBitmap<UnityColorPalette, UnityColorPaletteIndex>(Bits, ts.Item1, tileSize.Width, tileSize.Height, 15);
            var tiles = TileProcessor.GetTiles<UnityColorPalette, IndexedBitmapBufferDisguise>(pals, ts.Item1, ts.Item2, Bits, tileSize.Width, tileSize.Height);

            int w = bp.Width;
            int h = bp.Height;

            foreach(var kvp in tiles)
            {
                BitmapBuffer bf = BitmapBuffer.CreateInstance(w, h, pals[0].RealObject.BytesPerColor);
                Int32[] retbp = new Int32[w * h];
                GCHandle BitsHandle = GCHandle.Alloc(retbp, GCHandleType.Pinned);
                Bitmap finishedBP = new Bitmap(w, h, w * 4, PixelFormat.Format32bppArgb, BitsHandle.AddrOfPinnedObject());
                byte[] ret = new byte[retbp.Length * 4];

                foreach (var t in kvp.Value)
                {
                    BitmapBuffer bpb = t.Item4.RealObject.CreateBitmapBuffer(Flip.GetFlip(t.Item2, t.Item3), pals[kvp.Key].RealObject);
                    bf.DrawBitmapBuffer(bpb, t.Item1.X * tileSize.Width, t.Item1.Y * tileSize.Height);
                }
                unsafe
                {
                    fixed (byte* bs = ret)
                    {
                        bf.CopyTo(bs, 0, w - 1, 0, h - 1, 0, 0, 0, 0);
                    }
                }
                Buffer.BlockCopy(ret, 0, retbp, 0, ret.Length);
                finishedBP.Save(Path.GetFileNameWithoutExtension(path) + kvp.Key.Value + ".png",
                    ImageFormat.Png);
            }
        }

        public override void Load(byte[] bin, int offset)
        {
        }
    }
}
