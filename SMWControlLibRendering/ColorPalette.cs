using SMWControlLibRendering.Enumerators.Graphics;
using SMWControlLibRendering.Interfaces;
using SMWControlLibUtils;
using System;
using System.IO;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The color palette.
    /// </summary>
    public abstract class ColorPalette : CanFactoryWithObjsParams, ICanLoad
    {
        /// <summary>
        /// Gets or sets the bytes per color.
        /// </summary>
        public int BytesPerColor { get; protected set; }
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public ColorPaletteIndex Index { get; protected set; }
        /// <summary>
        /// Gets the length.
        /// </summary>
        public virtual int Length { get; protected set; }
        public event Action OnPaletteChange;
        public event Action<int, byte, byte, byte> OnColorChange;
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPalette"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        public ColorPalette(ColorPaletteIndex index, int size) : base(index, size)
        {
        }
        /// <summary>
        /// Get the color on the specific index.
        /// </summary>
        /// <param name="index">Index of the Color</param>
        /// <returns>Color on the index</returns>
        public abstract byte[] GetColor(int index);
        /// <summary>
        /// Set the color on the specific index.
        /// </summary>
        /// <param name="index">Index of the Color.</param>
        /// <param name="NewColor">New color that will be changed on that index.</param>
        public virtual void SetColor(int index, byte R, byte G, byte B)
        {
            OnColorChange?.Invoke(index, R, G, B);
        }
        /// <summary>
        /// Sets the colors.
        /// </summary>
        /// <param name="newColors">The new colors.</param>
        /// <param name="srcOffset">The src offset.</param>
        /// <param name="dstOffset">The dst offset.</param>
        /// <param name="lenght">The lenght.</param>
        public virtual void SetColors(byte[] newColors, int srcOffset, int dstOffset, int lenght)
        {
            OnPaletteChange?.Invoke();
        }

        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="path">The path.</param>
        public void Load(string path)
        {
            Load(path, 0);
        }

        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="bin">The bin.</param>
        public void Load(byte[] bin)
        {
            Load(bin, 0);
        }

        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="path">The path.</param>
        /// <param name="offset">The offset.</param>
        public void Load(string path, int offset)
        {
            byte[] bin = File.ReadAllBytes(path);
            Load(bin, offset);
        }

        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="bin">The bin.</param>
        /// <param name="offset">The offset.</param>
        public abstract void Load(byte[] bin, int offset);
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="args">The args.</param>
        public override void Initialize(params object[] args)
        {
            Index = (ColorPaletteIndex)args[0];
            Length = (int)args[1];
            OnPaletteChange?.Invoke();
        }
    }
}
