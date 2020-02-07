using SMWControlLibRendering.Factory;
using SMWControlLibUtils;

namespace SMWControlLibRendering.Disguise
{
    /// <summary>
    /// The color palette disguise.
    /// </summary>
    public class ColorPaletteDisguise : DisguiseWithObjsParams<ColorPaletteFactory, ColorPalette>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPaletteDisguise"/> class.
        /// </summary>
        /// <param name="args">The args.</param>
        public ColorPaletteDisguise(params object[] args) : base(args)
        {
        }
    }
}
