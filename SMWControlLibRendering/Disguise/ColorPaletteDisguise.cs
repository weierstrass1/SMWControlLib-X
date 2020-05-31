using SMWControlLibRendering.Enumerator;
using SMWControlLibRendering.Enumerators.Graphics;
using SMWControlLibRendering.Factory;
using SMWControlLibUtils;

namespace SMWControlLibRendering.Disguise
{
    /// <summary>
    /// The color palette disguise.
    /// </summary>
    public abstract class ColorPaletteDisguise : DisguiseWithObjsParams<ColorPaletteFactory, ColorPalette>, IMustInitialize<ColorPaletteIndex, int, BytesPerPixel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPaletteDisguise"/> class.
        /// </summary>
        /// <param name="args">The args.</param>
        protected ColorPaletteDisguise(ColorPaletteIndex index, int size, BytesPerPixel bpp) : base(index, size, bpp)
        {
        }

        public static T Generate<T>(ColorPaletteIndex index, int size) where T : ColorPaletteDisguise, new()
        {
            T t = new T();
            t.Initialize(index, size, t.RealObject.BytesPerColor);
            return t;
        }
        public void Initialize(ColorPaletteIndex index, int size, BytesPerPixel bpp)
        {
            RealObject = DressUp(index, size, bpp);
        }
    }
}
