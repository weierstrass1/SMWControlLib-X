using SMWControlLibRendering;
using SMWControlLibRendering.Disguise;
using SMWControlLibUnity.Enumerators.Graphics;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace SMWControlLibUnity.Graphics
{
    public class UnityColorPalette : ColorPaletteDisguise
    {
        public UnityColorPalette(UnityColorPaletteIndex index, int size) : base(index, size)
        {
        }
    }
}
