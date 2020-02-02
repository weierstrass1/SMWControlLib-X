using SMWControlLibUtils;

namespace SMWControlLibFrontend.Enumerators
{
    /// <summary>
    /// The mouse state.
    /// </summary>
    public class MouseState : FakeEnumerator
    {
        public static MouseState Idle = new MouseState(0);
        public static MouseState Hover = new MouseState(1);
        public static MouseState Active = new MouseState(2);
        public static MouseState Selected = new MouseState(3);
        /// <summary>
        /// Prevents a default instance of the <see cref="MouseState"/> class from being created.
        /// </summary>
        /// <param name="value">The value.</param>
        private MouseState(int value) : base(value)
        {

        }
    }
}
