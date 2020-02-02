using SMWControlLibBackend.Enumerators.Graphics;
using System;
using System.IO;
using System.Threading.Tasks;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The color palette.
    /// </summary>
    public class SNESColorPalette
    {
        /// <summary>
        /// Bitplanes format of the palette.
        /// </summary>
        public BPP Bitsplanes { get; protected set; }
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public ColorPaletteIndex Index { get; protected set; }
        /// <summary>
        /// Array with all colors.
        /// </summary>
        protected uint[] colors { get; set; }
        /// <summary>
        /// Gets the colors.
        /// </summary>
        public uint[] Colors
        {
            get
            {
                return (uint[])colors.Clone();
            }
        }
        public event Action OnPaletteChange;
        public event Action<uint, uint> OnColorChange;
        /// <summary>
        /// Constructor of the Class.
        /// </summary>
        /// <param name="bitsplanes">BPP2 = 4 colors per palette.
        /// BPP4 = 16 colors per palette.
        /// BPP8 = 256 colors per palette.</param>
        public SNESColorPalette(BPP bitsplanes, ColorPaletteIndex index)
        {
            Bitsplanes = bitsplanes;
            Index = index;
            colors = new uint[Bitsplanes];
        }
        /// <summary>
        /// Get the color on the specific index.
        /// </summary>
        /// <param name="index">Index of the Color</param>
        /// <returns>Color on the index</returns>
        public uint GetColor(uint index)
        {
            if (index < Bitsplanes)
            {
                return colors[index];
            }
            else
            {
                throw new IndexOutOfRangeException("Get Color: This palette allows " + Bitsplanes + " colors. Index " + index + " is out of range.");
            }
        }
        /// <summary>
        /// Set the color on the specific index.
        /// </summary>
        /// <param name="index">Index of the Color.</param>
        /// <param name="NewColor">New color that will be changed on that index.</param>
        public void SetColor(uint index, uint c)
        {
            if (index < Bitsplanes)
            {
                colors[index] = c;
                OnColorChange?.Invoke(index, c);
            }
            else
            {
                throw new IndexOutOfRangeException("Set Color: This palette allows " + Bitsplanes + " colors. Index " + index + " is out of range.");
            }
        }

        /// <summary>
        /// Sets the colors.
        /// </summary>
        /// <param name="newColors">The new colors.</param>
        /// <param name="srcOffset">The src offset.</param>
        /// <param name="dstOffset">The dst offset.</param>
        /// <param name="lenght">The lenght.</param>
        public void SetColors(uint[] newColors, int srcOffset, int dstOffset, int lenght)
        {
            Buffer.BlockCopy(newColors, srcOffset, colors, dstOffset, lenght * 4);

            OnPaletteChange?.Invoke();
        }

        /// <summary>
        /// Loads the pal.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="srcOffset">The src offset.</param>
        /// <param name="dstOffset">The dst offset.</param>
        /// <param name="lenght">The lenght.</param>
        public void LoadPal(string path, int srcOffset, int dstOffset, int lenght)
        {
            byte[] b = File.ReadAllBytes(path);
            int srcoff = srcOffset * 3;

            _ = Parallel.For(0, lenght, i =>
              {
                  int i3 = srcoff + i * 3;
                  colors[dstOffset + i] = 0xFF000000 |
                                        (uint)(b[i3] << 16) |
                                        (uint)(b[i3 + 1] << 8) |
                                        (uint)(b[i3 + 2]);
              });

            OnPaletteChange?.Invoke();
        }
    }
}
