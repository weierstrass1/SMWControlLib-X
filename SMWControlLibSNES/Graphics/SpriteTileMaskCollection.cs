using SMWControlLibCommons.Graphics;
using SMWControlLibRendering.Enumerator;

namespace SMWControlLibSNES.Graphics
{
    /// <summary>
    /// The sprite tile mask collection.
    /// </summary>
    public class SpriteTileMaskCollection : TileMaskCollection
    {
        public SpriteTileMaskCollection() : base(BytesPerPixel.RGB555)
        {
        }
    }
}
