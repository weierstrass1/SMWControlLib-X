using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Graphics.DirtyClasses;

namespace SMWControlLibUnity.Graphics.DirtyClasses
{
    public class DirtyUnityTile : DirtyTile
    {
        public DirtyUnityTile(TileSize size, TileIndex index) : base(new UnityTile(size, index))
        {
        }
    }
}
