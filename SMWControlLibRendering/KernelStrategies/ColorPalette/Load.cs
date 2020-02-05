using ILGPU;
using SMWControlLibRendering.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibRendering.KernelStrategies.ColorPalette
{
    /// <summary>
    /// The load.
    /// </summary>
    public class Load<T> : KernelStrategy<Index, ArrayView<T>, ArrayView<byte>, int, int, int> where T : struct, ICanBeMultibytes
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="dstBuffer">The dst buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="dstOffset">The dst offset.</param>
        /// <param name="srcOffset">The src offset.</param>
        /// <param name="sizeOfT">The size of t.</param>
        protected override void strategy(Index index, ArrayView<T> dstBuffer, ArrayView<byte> srcBuffer, int dstOffset, int srcOffset, int sizeOfT)
        {
            int offsrc = srcOffset + (index.X * 3);
            for (int i = 0; i < sizeOfT; i++)
                dstBuffer[dstOffset + index.X].SetByte(i, srcBuffer[offsrc + i]);
        }
    }
}
