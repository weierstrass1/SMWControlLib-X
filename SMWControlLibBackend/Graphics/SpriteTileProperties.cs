using SMWControlLibBackend.Enumerators.Graphics;
using SMWControlLibRendering.Enumerators;

namespace SMWControlLibBackend.Graphics
{
    public struct SpriteTileProperties
    {
        /// <summary>
        /// Gets the flip.
        /// </summary>
        public Flip Flip { get; internal set; }
        /// <summary>
        /// Gets the palette.
        /// </summary>
        public SNESColorPalette Palette { get; internal set; }
        /// <summary>
        /// Gets the s p.
        /// </summary>
        public SpritePage SP { get; internal set; }
        /// <summary>
        /// Gets the priority.
        /// </summary>
        public SpritePriority Priority { get; internal set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SpriteTileProperties"/> class.
        /// </summary>
        /// <param name="flip">The flip.</param>
        /// <param name="pal">The pal.</param>
        /// <param name="sp">The sp.</param>
        /// <param name="prior">The prior.</param>
        public SpriteTileProperties(Flip flip, SNESColorPalette pal, SpritePage sp, SpritePriority prior)
        {
            Flip = flip;
            Palette = pal;
            SP = sp;
            Priority = prior;
        }

        public static implicit operator string(SpriteTileProperties ob)
        {
            int val = (ob.Flip.Value << 6) |
                (ob.Priority << 4) |
                (ob.Palette.Index << 1) |
                ob.SP;

            return val.ToString("X2");
        }
    }
}
