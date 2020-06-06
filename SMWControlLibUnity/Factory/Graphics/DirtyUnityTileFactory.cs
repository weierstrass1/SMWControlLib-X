using SMWControlLibCommons.Enumerators.Graphics;
using SMWControlLibCommons.Factory;
using SMWControlLibCommons.Graphics.DirtyClasses;
using SMWControlLibUnity.Graphics.DirtyClasses;

namespace SMWControlLibUnity.Factory.Graphics
{
    public class DirtyUnityTileFactory : DirtyTileFactory
    {
        public override DirtyTile GenerateObject(params object[] args)
        {
            return new DirtyUnityTile((TileSize)args[0], (TileIndex)args[1]);
        }
    }
}
