using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering.Enumerators.Graphics;
using System;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The g p u color palette.
    /// </summary>
    public class GPUColorPalette : ColorPalette
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
        public MemoryBuffer<byte> Buffer { get; protected set; }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="args">The args.</param>
        public override void Initialize(params object[] args)
        {
            if (Buffer != null) Buffer.Dispose();
            BytesPerColor = 3;
            base.Initialize(args);
            Buffer = HardwareAcceleratorManager.GPUAccelerator.Allocate<byte>((int)args[1] * BytesPerColor);
        }
        /// <summary>
        /// Gets the color.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <returns>A T.</returns>
        public override byte[] GetColor(int index)
        {
            byte[] color = new byte[BytesPerColor];
            Buffer.CopyTo(color, new Index(index*3), 0, new Index(BytesPerColor));
            return color;
        }
        /// <summary>
        /// Loads the.
        /// </summary>
        /// <param name="bin">The bin.</param>
        /// <param name="offset">The offset.</param>
        public override void Load(byte[] bin, int offset)
        {
            if (bin == null) throw new ArgumentNullException(nameof(bin));
            int w = Math.Min(bin.Length - (offset * BytesPerColor), Buffer.Length);
            if (w <= 0) return;

            Buffer.CopyFrom(bin, offset * BytesPerColor, new Index(0), Buffer.Extent);
        }

        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="c">The c.</param>
        public override void SetColor(int index, byte R, byte G, byte B)
        {
            byte[] color = { R, G, B };
            Buffer.CopyFrom(color, 0, new Index(index * BytesPerColor), new Index(3));
        }

        /// <summary>
        /// Sets the colors.
        /// </summary>
        /// <param name="newColors">The new colors.</param>
        /// <param name="srcOffset">The src offset.</param>
        /// <param name="dstOffset">The dst offset.</param>
        /// <param name="lenght">The lenght.</param>
        public override void SetColors(byte[] newColors, int srcOffset, int dstOffset, int lenght)
        {
            Buffer.CopyFrom(newColors, srcOffset, dstOffset, lenght);
        }
    }
}
