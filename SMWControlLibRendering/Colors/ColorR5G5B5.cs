using SMWControlLibRendering.Interfaces;

namespace SMWControlLibRendering.Colors
{
    public struct ColorR5G5B5 : ICanBeMultibytes
    {
        /// <summary>
        /// Gets or sets the r.
        /// </summary>
        public byte R { get; set; }
        /// <summary>
        /// Gets or sets the g.
        /// </summary>
        public byte G { get; set; }
        /// <summary>
        /// Gets or sets the b.
        /// </summary>
        public byte B { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorR5G5B5"/> class.
        /// </summary>
        /// <param name="r">The r.</param>
        /// <param name="g">The g.</param>
        /// <param name="b">The b.</param>
        public ColorR5G5B5(byte r, byte g, byte b)
        {
            R = r;
            G = g;
            B = b;
        }
        public static implicit operator int(ColorR5G5B5 col)
        {
            return (col.R << 16) | (col.G << 8) | col.B;
        }
        public static implicit operator bool(ColorR5G5B5 col)
        {
            return ((col.R & 0x07) | (col.G & 0x07) | (col.B & 0x07)) != 0;
        }

        /// <summary>
        /// Sets the byte.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="b">The b.</param>
        public void SetByte(int index, byte b)
        {
            switch(index)
            {
                case 0:
                    R = b;
                    break;
                case 1:
                    G = b;
                    break;
                default:
                    B = b;
                    break;
            }
        }
    }
}
