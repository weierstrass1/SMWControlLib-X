﻿using SMWControlLibRendering.Enumerators;
using SMWControlLibRendering;

namespace SMWControlLibCommons.Graphics
{
    /// <summary>
    /// The tile properties.
    /// </summary>
    public class TileProperties<T> where T : struct
    {
        /// <summary>
        /// Gets the flip.
        /// </summary>
        public Flip Flip { get; internal set; }
        /// <summary>
        /// Gets the palette.
        /// </summary>
        public ColorPalette<T> Palette { get; internal set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="TileProperties"/> class.
        /// </summary>
        /// <param name="flip">The flip.</param>
        /// <param name="pal">The pal.</param>
        /// <param name="sp">The sp.</param>
        /// <param name="prior">The prior.</param>
        public TileProperties(Flip flip, ColorPalette<T> pal)
        {
            Flip = flip;
            Palette = pal;
        }
    }
}
