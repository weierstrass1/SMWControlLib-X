using SMWControlLibRendering.Factory;
using SMWControlLibUtils;

namespace SMWControlLibRendering.Disguise
{
    /// <summary>
    /// The color palette disguise.
    /// </summary>
    public class ColorPaletteDisguise<T> : DisguiseWithObjsParams<ColorPaletteFactory<T>, ColorPalette<T>> where T : struct
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
