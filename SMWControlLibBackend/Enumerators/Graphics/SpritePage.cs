using SMWControlLibUtils;

namespace SMWControlLibBackend.Enumerators.Graphics
{
    /// <summary>
    /// The sprite page.
    /// </summary>
    public class SpritePage : FakeEnumerator
    {
        public static SpritePage SP12 = new SpritePage(0);
        public static SpritePage SP34 = new SpritePage(1);
        /// <summary>
        /// Prevents a default instance of the <see cref="SpritePage"/> class from being created.
        /// </summary>
        /// <param name="value">The value.</param>
        private SpritePage(int value) : base(value)
        {

        }
    }
}
