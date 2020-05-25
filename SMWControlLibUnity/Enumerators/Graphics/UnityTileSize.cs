using SMWControlLibCommons.Enumerators.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibUnity.Enumerators.Graphics
{
    public class UnityTileSize : TileSize
    {
        public UnityTileSize(int width, int height) : base(width, height, width + "x" + height)
        {

        }
    }
}
