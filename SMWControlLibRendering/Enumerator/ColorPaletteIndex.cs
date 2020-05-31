using SMWControlLibUtils;
using System;

namespace SMWControlLibRendering.Enumerators.Graphics
{
    /// <summary>
    /// The color palette index.
    /// </summary>
    public abstract class ColorPaletteIndex : FakeEnumerator
    {
        protected ColorPaletteIndex() : base(0)
        {

        }
        /// <summary>
        /// Gets or sets the offset.
        /// </summary>
        public int Offset { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="ColorPaletteIndex"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        /// <param name="offset">The offset.</param>
        protected ColorPaletteIndex(int value, int offset) : base(value)
        {
            Offset = offset;
        }
        public static T Generate<T>(int value, int offset) where T : ColorPaletteIndex, new()
        {
            T t = new T();
            t.Value = value;
            t.Offset = offset;
            return t;
        }
        public override int GetHashCode()
        {
            return Value;
        }
        public override bool Equals(object obj)
        {
            if (GetType() != obj.GetType()) return false;
            ColorPaletteIndex p = (ColorPaletteIndex)obj;
            return Value == p.Value && Offset == p.Offset;
        }
        public override string ToString()
        {
            return "Index: " + Value + ", Offset: " + Offset;
        }
    }
}
