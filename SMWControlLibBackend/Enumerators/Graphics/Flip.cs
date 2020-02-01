namespace SMWControlLibBackend.Enumerators.Graphics
{
    /// <summary>
    /// The flip.
    /// </summary>
    public class Flip : FakeEnumerator
    {
        /// <summary>
        /// Gets the flip x.
        /// </summary>
        public int FlipX { get { return Value; } }
        /// <summary>
        /// Gets the flip y.
        /// </summary>
        public int FlipY { get; private set; }

        public static Flip NoFlip = new Flip(0, 0);
        public static Flip HorizontalFlip = new Flip(1, 0);
        public static Flip VerticalFlip = new Flip(0, 1);
        public static Flip HorizontalAndVerticalFlip = new Flip(1, 1);

        /// <summary>
        /// Prevents a default instance of the <see cref="Flip"/> class from being created.
        /// </summary>
        /// <param name="fx">The fx.</param>
        /// <param name="fy">The fy.</param>
        private Flip(int fx, int fy) : base(fx)
        {
            FlipY = fy;
        }
    }
}
