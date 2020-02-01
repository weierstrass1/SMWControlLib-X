using SMWControlLibBackend.Graphics;
using SMWControlLibRendering;

namespace SMWControlLibBackend.Delegates
{
    public delegate SpriteTileMaskCollection<T> SelectionHandler<T>() where T : BitmapBuffer, new();
}
