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
    public abstract class ColorPalette<T> : CanFactoryWithObjsParams, ICanLoad where T : struct
    {
        /// <summary>
        /// Gets or sets the colors.
        /// </summary>
        public T[] Colors { get; protected set; }
        /// <summary>
        /// Gets or sets the index.
        /// </summary>
        public ColorPaletteIndex Index { get; protected set; }
        /// <summary>
        /// Gets the length.
        /// </summary>
        public int Length
        {
            get
            {
                if (Colors == null) return 0;
                return Colors.Length;
            }
        }
        public event Action OnPaletteChange;
        public event Action<int, T> OnColorChange;
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
        public abstract T GetColor(int index);
        /// <summary>
        /// Set the color on the specific index.
        /// </summary>
        /// <param name="index">Index of the Color.</param>
        /// <param name="NewColor">New color that will be changed on that index.</param>
        public abstract void SetColor(int index, T c);
        /// <summary>
        /// Sets the colors.
        /// </summary>
        /// <param name="newColors">The new colors.</param>
        /// <param name="srcOffset">The src offset.</param>
        /// <param name="dstOffset">The dst offset.</param>
        /// <param name="lenght">The lenght.</param>
        public abstract void SetColors(T[] newColors, int srcOffset, int dstOffset, int lenght);

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
            Colors = new T[(int)args[1]];
        }
    }
}
