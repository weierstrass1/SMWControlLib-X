using SMWControlLibRendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibBackend.Graphics
{
    /// <summary>
    /// The s n e s indexed g p u bitmap buffer.
    /// </summary>
    public class SNESIndexedGPUBitmapBuffer : IndexedGPUBitmapBuffer<byte>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SNESIndexedGPUBitmapBuffer"/> class.
        /// </summary>
        /// <param name="width">The width.</param>
        /// <param name="height">The height.</param>
        public SNESIndexedGPUBitmapBuffer(int width, int height) : base(width, height)
        {
        }

    }
}
