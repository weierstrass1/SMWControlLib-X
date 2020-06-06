using SMWControlLibRendering.Disguise;
using SMWControlLibRendering.Enumerator;
using SMWControlLibUnity.Enumerators.Graphics;

namespace SMWControlLibUnity.Graphics
{
    public class UnityColorPalette : ColorPaletteDisguise
    {
        public UnityColorPalette() : base(null, 1, BytesPerPixel.ARGB8888)
        {

        }

        public UnityColorPalette(UnityColorPaletteIndex index, int size) : base(index, size, BytesPerPixel.ARGB8888)
        {
        }
    }
}
