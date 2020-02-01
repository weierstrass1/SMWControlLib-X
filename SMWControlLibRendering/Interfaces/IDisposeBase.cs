using System;

namespace SMWControlLibRendering.Interfaces
{
    public interface IDisposeBase : IDisposable
    {
        /// <summary>
        /// Statics the dispose.
        /// </summary>
        void StaticDispose();
        /// <summary>
        /// Disposes the.
        /// </summary>
        /// <param name="">If true, .</param>
        void Dispose(bool disposing);
    }
}
