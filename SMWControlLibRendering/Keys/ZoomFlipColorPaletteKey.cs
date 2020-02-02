using SMWControlLibRendering.Enumerators;
using SMWControlLibUtils;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibRendering.Keys
{
    /// <summary>
    /// The zoom flip color palette key.
    /// </summary>
    public class ZoomFlipColorPaletteKey : DualKey<uint, FlipColorPaletteKey>
    {
        /// <summary>
        /// Gets the zoom.
        /// </summary>
        public uint Zoom => element1;
        /// <summary>
        /// Gets the flip.
        /// </summary>
        public Flip Flip => element2.element1;
        /// <summary>
        /// Gets the palette.
        /// </summary>
        public ColorPalette Palette => element2.element2;
        /// <summary>
        /// Initializes a new instance of the <see cref="ZoomFlipColorPaletteKey"/> class.
        /// </summary>
        /// <param name="zoom">The zoom.</param>
        /// <param name="flip">The flip.</param>
        /// <param name="cp">The cp.</param>
        public ZoomFlipColorPaletteKey(uint zoom, Flip flip, ColorPalette cp) : base(zoom, new FlipColorPaletteKey(flip, cp))
        {
        }
    }
}
