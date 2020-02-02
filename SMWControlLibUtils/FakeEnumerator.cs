namespace SMWControlLibUtils
{
    /// <summary>
    /// The fake enumerator.
    /// </summary>
    public abstract class FakeEnumerator
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public int Value { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="FakeEnumerator"/> class.
        /// </summary>
        /// <param name="value">The value.</param>
        protected FakeEnumerator(int value)
        {
            Value = value;
        }

        public static implicit operator int(FakeEnumerator ob)
        {
            return ob.Value;
        }
    }
}
