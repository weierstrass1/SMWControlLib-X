using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    /// <summary>
    /// The draw bitmap buffer with zoom and b g.
    /// </summary>
    public static class DrawBitmapBufferWithZoomAndBGRGB555Kernel
    {
        private static readonly Action<Index2, ArrayView3D<byte>, ArrayView3D<byte>, Index2, int, byte, byte, byte> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView3D<byte>, ArrayView3D<byte>, Index2, int, byte, byte, byte>
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
        /// <param name="backgroundColor">The background color.</param>
        public static void Execute(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer,
            Index2 offset, int zoom, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            kernel(index, destBuffer, srcBuffer, offset, zoom, backgroundColorR, backgroundColorG, backgroundColorB);
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
        /// <param name="backgroundColor">The background color.</param>
        private static void strategy(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer,
            Index2 offset, int zoom, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            byte colorR = srcBuffer[new Index3(0, index)];
            byte colorG = srcBuffer[new Index3(1, index)];
            byte colorB = srcBuffer[new Index3(2, index)];

            if ((colorR & 0x7) != 0 || (colorG & 0x7) != 0 || (colorB & 0x7) != 0)
            {
                colorR = backgroundColorB;
                colorG = backgroundColorG;
                colorB = backgroundColorR;
            }

            Index2 ind = index + offset;
            ind = new Index2(ind.X * zoom, ind.Y * zoom);
            for (int j = 0; j < zoom; j++)
            {
                for (int i = 0; i < zoom; i++)
                {
                    Index2 curoff = ind + new Index2(i, j);
                    destBuffer[new Index3(0, curoff)] = colorR;
                    destBuffer[new Index3(1, curoff)] = colorG;
                    destBuffer[new Index3(2, curoff)] = colorB;
                }
            }
        }
    }
}
