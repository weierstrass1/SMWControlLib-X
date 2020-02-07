﻿using ILGPU;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    /// <summary>
    /// The draw bitmap buffer with zoom and b g.
    /// </summary>
    public class DrawBitmapBufferWithZoomAndBGRGB555Kernel : KernelStrategy<Index2, ArrayView<byte>, ArrayView<byte>, int, int, int, int, byte, byte, byte>
    {
        private static readonly DrawBitmapBufferWithZoomAndBGRGB555Kernel instance = new DrawBitmapBufferWithZoomAndBGRGB555Kernel();
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
        public static void Execute(Index2 index, ArrayView<byte> destBuffer, ArrayView<byte> srcBuffer, int offset,
            int dstWidth, int srcWidth, int zoom, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            instance.kernel(index, destBuffer, srcBuffer, offset, dstWidth, srcWidth, zoom, backgroundColorR, backgroundColorG, backgroundColorB);
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
        protected override void strategy(Index2 index, ArrayView<byte> destBuffer, ArrayView<byte> srcBuffer, int offset,
            int dstWidth, int srcWidth, int zoom, byte backgroundColorR, byte backgroundColorG, byte backgroundColorB)
        {
            int indsrc = ((index.Y * srcWidth) + index.X) * 3;
            byte colorR = srcBuffer[indsrc];
            byte colorG = srcBuffer[indsrc + 1];
            byte colorB = srcBuffer[indsrc + 2];

            if ((colorR & 0x7) != 0 || (colorG & 0x7) != 0 || (colorB & 0x7) != 0)
            {
                colorR = backgroundColorR;
                colorG = backgroundColorG;
                colorB = backgroundColorB;
            }

            int jw = offset;
            for (int j = 0; j < zoom; j++)
            {
                jw += dstWidth;
                for (int i = 0; i < zoom; i++)
                {
                    int ind = (jw + i) * 3;
                    destBuffer[ind] = colorR;
                    destBuffer[ind + 1] = colorG;
                    destBuffer[ind + 2] = colorB;
                }
            }
        }
    }
}