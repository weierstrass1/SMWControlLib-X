namespace SMWControlLibBackend.Enumerators.Graphics
{
    /// <summary>
    /// Enumerator used for Bitplanes (number of bits per color on the palette).
    /// </summary>
    public class BPP : FakeEnumerator
    {
        public static BPP BPP2 = new BPP(4);
        public static BPP BPP4 = new BPP(16);
        public static BPP BPP8 = new BPP(256);
        /// <summary>
        /// Prevents a default instance of the <see cref="BPP"/> class from being created.
        /// </summary>
        /// <param name="value">The value.</param>
        private BPP(int value) : base(value)
        {
        }
    }
}
