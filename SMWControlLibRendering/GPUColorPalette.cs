using ILGPU.Runtime;
using SMWControlLibRendering.Enumerators.Graphics;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The g p u color palette.
    /// </summary>
    public class GPUColorPalette<T> : ColorPalette<T> where T : struct
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="GPUColorPalette"/> class.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="size">The size.</param>
        public GPUColorPalette(ColorPaletteIndex index, int size) : base(index, size)
        {
        }
        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        public MemoryBuffer<T> Buffer { get; protected set; }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="args">The args.</param>
        public override void Initialize(params object[] args)
        {
            base.Initialize(args);
            Buffer = HardwareAcceleratorManager.GPUAccelerator.Allocate<T>((int)args[1]);
        }
        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>A T.</returns>
        public override T GetColor(int index)
        {
            return Buffer.ToArrayView()[index];
        }
        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="bin">The bin.</param>
        /// <param name="offset">The offset.</param>
        public override void Load(byte[] bin, int offset)
        {
            throw new System.NotImplementedException();
        }

        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="c">The c.</param>
        public override void SetColor(int index, T c)
        {
            Buffer.ToArrayView()[index] = c;
        }

        /// <summary>
        /// Sets the colors.
        /// </summary>
        /// <param name="newColors">The new colors.</param>
        /// <param name="srcOffset">The src offset.</param>
        /// <param name="dstOffset">The dst offset.</param>
        /// <param name="lenght">The lenght.</param>
        public override void SetColors(T[] newColors, int srcOffset, int dstOffset, int lenght)
        {
            Buffer.CopyFrom(newColors, srcOffset, dstOffset, lenght);
        }
    }
}
