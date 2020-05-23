using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    /// <summary>
    /// The draw bitmap buffer with zoom.
    /// </summary>
    public static class DrawBitmapBufferWithZoomRGB555Kernel
    {
        private static readonly Action<Index2, ArrayView3D<byte>, ArrayView3D<byte>, Index2, int> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView3D<byte>, ArrayView3D<byte>, Index2, int>
            (strategy);
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="dstWidth">The dst width.</param>
        /// <param name="srcWidth">The src width.</param>
        /// <param name="zoom">The zoom.</param>
        public static void Execute(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer,
            Index2 offset, int zoom)
        {
            kernel(index, destBuffer, srcBuffer, offset, zoom);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="destBuffer">The dest buffer.</param>
        /// <param name="srcBuffer">The src buffer.</param>
        /// <param name="offset">The offset.</param>
        /// <param name="dstWidth">The dst width.</param>
        /// <param name="srcWidth">The src width.</param>
        /// <param name="zoom">The zoom.</param>
        private static void strategy(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer,
            Index2 offset, int zoom)
        {
            byte colorR = srcBuffer[new Index3(0, index)];
            byte colorG = srcBuffer[new Index3(1, index)];
            byte colorB = srcBuffer[new Index3(2, index)];

            if ((colorR & 0x7) != 0 || (colorG & 0x7) != 0 || (colorB & 0x7) != 0)
                return;

            Index2 ind = index + offset;
            ind = new Index2(ind.X * zoom, ind.Y * zoom);
            Index2 curOf;
            for (int j = 0; j < zoom; j++)
            {
                for (int i = 0; i < zoom; i++)
                {
                    curOf = ind + new Index2(i, j);
                    destBuffer[new Index3(0, curOf)] = colorR;
                    destBuffer[new Index3(1, curOf)] = colorG;
                    destBuffer[new Index3(2, curOf)] = colorB;
                }
            }
        }
    }
}
