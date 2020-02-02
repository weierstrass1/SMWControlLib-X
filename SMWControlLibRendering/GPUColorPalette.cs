using ILGPU.Runtime;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The g p u color palette.
    /// </summary>
    public class GPUColorPalette : ColorPalette
    {
        /// <summary>
        /// Gets or sets the buffer.
        /// </summary>
        public MemoryBuffer<uint> Buffer { get; protected set; }
    }
}
