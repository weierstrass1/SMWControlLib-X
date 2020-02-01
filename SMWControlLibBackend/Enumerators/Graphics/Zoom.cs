namespace SMWControlLibBackend.Enumerators.Graphics
{
    /// <summary>
    /// The zoom.
    /// </summary>
    public class Zoom : FakeEnumerator
    {
        public static Zoom X1 = new Zoom(1);
        public static Zoom X2 = new Zoom(2);
        public static Zoom X3 = new Zoom(3);
        public static Zoom X4 = new Zoom(4);
        public static Zoom X5 = new Zoom(5);
        public static Zoom X6 = new Zoom(6);
        public static Zoom X7 = new Zoom(7);
        public static Zoom X8 = new Zoom(8);
        public static Zoom X16 = new Zoom(16);

        /// <summary>
        /// Prevents a default instance of the <see cref="Zoom"/> class from being created.
        /// </summary>
        /// <param name="value">The value.</param>
        private Zoom(int value) : base(value)
        {

        }
    }
}
