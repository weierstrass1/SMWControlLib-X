using ILGPU;

namespace SMWControlLibRendering.KernelStrategies.IndexedBitmapBufferKernels
{
    /// <summary>
    /// The create bitmap with zoom.
    /// </summary>
    public class CreateBitmapBufferWithZoomByteRGB555Kernel : KernelStrategy<Index2, ArrayView2D<byte>, ArrayView<byte>, ArrayView<byte>, int, int>
    {
        //private static readonly CreateBitmapBufferWithZoomByteRGB555Kernel instance = new CreateBitmapBufferWithZoomByteRGB555Kernel();
        /// <summary>
        /// Executes the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="indexedBitmapBuffer">The indexed bitmap buffer.</param>
        /// <param name="destBitmap">The dest bitmap.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="flip">The flip.</param>
        public void Execute(Index2 index, ArrayView2D<byte> indexedBitmapBuffer, ArrayView<byte> destBitmap, ArrayView<byte> colors, int zoom, int flip)
        {
            kernel(index, indexedBitmapBuffer, destBitmap, colors, zoom, flip);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="index">The index.</param>
        /// <param name="indexedBitmapBuffer">The indexed bitmap buffer.</param>
        /// <param name="destBitmap">The dest bitmap.</param>
        /// <param name="colors">The colors.</param>
        /// <param name="zoom">The zoom.</param>
        /// <param name="flip">The flip.</param>
        protected override void strategy(Index2 index, ArrayView2D<byte> indexedBitmapBuffer, ArrayView<byte> destBitmap, ArrayView<byte> colors, int zoom, int flip)
        {
            int colind = indexedBitmapBuffer[index] * 3;
            if (colind != 0)
            {
                
                int y = index.Y;
                if ((flip & 0x2) != 0)
                    y = indexedBitmapBuffer.Height - y - 1;
                int x = index.X;
                if ((flip & 0x1) != 0)
                    x = indexedBitmapBuffer.Width - x - 1;
                
                byte colorR = colors[colind];
                byte colorG = colors[colind + 1];
                byte colorB = colors[colind + 2];

                int wz = indexedBitmapBuffer.Width * zoom;
                int offset = ((y * indexedBitmapBuffer.Width) + x) * zoom;
                int wzz = wz * zoom;
                for (int j = 0; j < wzz; j += wz)
                {
                    for (int i = 0; i < zoom; i++)
                    {
                        int ind = (offset + j) * 3;
                        destBitmap[ind] = colorR;
                        destBitmap[ind + 1] = colorG;
                        destBitmap[ind + 2] = colorB;
                    }
                }
            }
        }
    }
}
