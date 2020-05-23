using System;
using System.Collections.Generic;
using System.Text;
using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics;

namespace SMWControlLibUnity.Graphics
{
    public class UnityTilemapGrid : TilemapGrid
    {
        public UnityTilemapGrid(int width, int height, Zoom z, byte bgR, byte bgG, byte bgB): base(width, height, z, bgR, bgG, bgB)
        {
        }
    }
}
