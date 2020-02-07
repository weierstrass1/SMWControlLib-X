using SMWControlLibRendering.Enumerators.Graphics;
using SMWControlLibUtils;

namespace SMWControlLibRendering.Factory
{
    /// <summary>
    /// The color palette factory.
    /// </summary>
    public class ColorPaletteFactory : ObjectFactoryWithObjsParams<ColorPalette>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>A ColorPalette.</returns>
        public override ColorPalette GenerateObject(params object[] args)
        {
            if (HardwareAcceleratorManager.IsGPUAvailable())
                return new GPUColorPalette((ColorPaletteIndex)args[0], (int)args[1]);
            return null;
        }
    }
}
