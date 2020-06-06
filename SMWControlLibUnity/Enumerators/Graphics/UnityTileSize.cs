using SMWControlLibCommons.Enumerators.Graphics;

namespace SMWControlLibUnity.Enumerators.Graphics
{
    public class UnityTileSize : TileSize
    {
        public UnityTileSize(int width, int height) : base(width, height, width + "x" + height)
        {

        }
    }
}
