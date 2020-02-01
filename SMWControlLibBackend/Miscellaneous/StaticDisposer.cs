using SMWControlLibBackend.Interfaces;

namespace SMWControlLibBackend.Miscellaneous
{
    /// <summary>
    /// The static disposer.
    /// </summary>
    public static class StaticDisposer
    {
        /// <summary>
        /// Disposes the.
        /// </summary>
        public static void Dispose(IDisposeBase b)
        {
            b.StaticDispose();
        }
    }
}
