namespace SMWControlLibBackend.Enumerators.Graphics
{
    /// <summary>
    /// The sprite priority.
    /// </summary>
    public class SpritePriority : FakeEnumerator
    {
        public static SpritePriority OverBG34WithPriority0 = new SpritePriority(0);
        public static SpritePriority OverBG34WithPriority1 = new SpritePriority(1);
        public static SpritePriority OverBG12WithPriority0 = new SpritePriority(2);
        public static SpritePriority OverBG12WithPriority1 = new SpritePriority(3);
        /// <summary>
        /// Prevents a default instance of the <see cref="SpritePriority"/> class from being created.
        /// </summary>
        /// <param name="value">The value.</param>
        private SpritePriority(int value) : base(value)
        {

        }
    }
}
