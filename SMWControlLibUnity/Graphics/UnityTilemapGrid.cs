using System;
using System.Collections.Generic;
using System.Text;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics;
using SMWControlLibRendering.Enumerator;

namespace SMWControlLibUnity.Graphics
{
    public class UnityTilemapGrid : TilemapGrid
    {
        public UnityTilemapGrid(int width, int height, Zoom z, params byte[] bgColor): base(width, height, z, BytesPerPixel.ARGB8888, bgColor)
        {
        }
    }
}
