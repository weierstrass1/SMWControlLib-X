using SMWControlLibCommons.Graphics;
using SMWControlLibRendering.Colors;

namespace SMWControlLibSNES.Graphics
{
    /// <summary>
    /// The sprite tile section mask.
    /// </summary>
    public class SpriteTileSectionMask : TileSectionMask<byte, ColorA1R5G5B5>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileSectionMask"/> class.
        /// </summary>
        /// <param name="s">The s.</param>
        public SpriteTileSectionMask(TileSection<byte, ColorA1R5G5B5> s) : base(s)
        {
        }
    }
}
