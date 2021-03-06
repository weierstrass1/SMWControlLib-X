﻿using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering.Enumerator;
using SMWControlLibRendering.Enumerators.Graphics;
using System;
using System.Collections.Concurrent;

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
        public GPUColorPalette(ColorPaletteIndex index, int size, BytesPerPixel bpp) : base(index, size, bpp)
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
            Buffer.CopyTo(color, index * BytesPerColor, 0, BytesPerColor.Value);
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

            Buffer.CopyFrom(bin, offset * BytesPerColor, 0, Buffer.Extent);
        }

        /// <summary>
        /// Sets the color.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="c">The c.</param>
        public override void SetColor(int index, byte R, byte G, byte B)
        {
            byte[] color = { R, G, B };
            Buffer.CopyFrom(color, 0, index * BytesPerColor, BytesPerColor.Value);
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

        public override ConcurrentDictionary<Int32, int> ToColorDictionary()
        {
            ConcurrentDictionary<Int32, int> ret = new ConcurrentDictionary<int, int>();
            int off = 4 - BytesPerColor;
            for (int i = 0; i < Length; i++)
            {
                int[] c = new int[1];
                System.Buffer.BlockCopy(GetColor(i), 0, c, off, BytesPerColor);

                ret.TryAdd(ReverseBytes(c[0]), i);
            };
            return ret;
        }
    }
}
