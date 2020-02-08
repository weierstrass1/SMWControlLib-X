using SMWControlLibUtils;

namespace SMWControlLibCommons.Enumerators.Graphics
{
    /// <summary>
    /// The zoom.
    /// </summary>
    public class Zoom : FakeEnumerator
    {
        public static readonly Zoom X1 = new Zoom(1);
        public static readonly Zoom X2 = new Zoom(2);
        public static readonly Zoom X3 = new Zoom(3);
        public static readonly Zoom X4 = new Zoom(4);
        public static readonly Zoom X5 = new Zoom(5);
        public static readonly Zoom X6 = new Zoom(6);
        public static readonly Zoom X7 = new Zoom(7);
        public static readonly Zoom X8 = new Zoom(8);
        public static readonly Zoom X16 = new Zoom(16);

        /// <summary>
        /// Prevents a default instance of the <see cref="Zoom"/> class from being created.
        /// </summary>
        /// <param name="value">The value.</param>
        private Zoom(int value) : base(value)
        {

        }
    }
}
