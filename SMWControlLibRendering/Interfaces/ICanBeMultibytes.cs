using System;
using System.Collections.Generic;
using System.Text;

namespace SMWControlLibRendering.Interfaces
{
    public interface ICanBeMultibytes
    {
        /// <summary>
        /// Sets the byte.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="b">The b.</param>
        void SetByte(int index, byte b);
    }
}
