using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    public static class DrawBitmapBufferKernel
    {
        private static readonly Action<Index2, ArrayView3D<byte>,
                ArrayView3D<byte>, Index2, Index2, int, ArrayView<byte>, int, ArrayView<byte>> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView3D<byte>,
                ArrayView3D<byte>, Index2, Index2, int, ArrayView<byte>, int, ArrayView<byte>>
            (strategy);
        private static readonly Action<Index2, ArrayView3D<byte>,
                ArrayView3D<byte>, Index2, Index2, int, ArrayView<byte>, int> kernelNoBG =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView3D<byte>,
                ArrayView3D<byte>, Index2, Index2, int, ArrayView<byte>, int>
            (strategyNoBG);
        public static void Execute(Index2 extent, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer,
            Index2 dstOffset, Index2 srcOffset, byte[] bitClearer, int Zoom, byte[] BackgroundColor)
        {
            if (bitClearer == null)
                throw new ArgumentNullException(nameof(bitClearer));

            if (extent.X + dstOffset.X > destBuffer.Extent.Y ||
                extent.Y + dstOffset.Y > destBuffer.Extent.Z)
                throw new ArgumentOutOfRangeException(nameof(extent));
            if (extent.X + srcOffset.X > srcBuffer.Extent.Y ||
                extent.Y + srcOffset.Y > srcBuffer.Extent.Z)
                throw new ArgumentOutOfRangeException(nameof(extent));

            using (MemoryBuffer<byte> b = HardwareAcceleratorManager.GPUAccelerator.Allocate<byte>(bitClearer.Length))
            {
                b.CopyFrom(bitClearer, 0, Index1.Zero, bitClearer.Length);
                if (BackgroundColor == null)
                {
                    kernelNoBG(extent, destBuffer, srcBuffer, dstOffset, srcOffset, destBuffer.Extent.X, b, Zoom);
                    HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                }
                else
                {
                    using (MemoryBuffer<byte> bgc = HardwareAcceleratorManager.GPUAccelerator.Allocate<byte>(BackgroundColor.Length))
                    {
                        bgc.CopyFrom(BackgroundColor, 0, 0, bgc.Extent);
                        kernel(extent, destBuffer, srcBuffer, dstOffset, srcOffset, destBuffer.Extent.X, b, Zoom, bgc);
                        HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                    }
                }
            }
        }
        private static void strategyNoBG(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer,
            Index2 dstOffset, Index2 srcOffset, int ColorSize, ArrayView<byte> BitChecker,
            int Zoom)
        {
            bool change = true;
            byte b;
            Index2 srcOff = index + srcOffset;
            if (ColorSize == 4 && srcBuffer[new Index3(0, srcOff)] == 0)
                change = false;
            for (int i = 0; change && i < ColorSize; i++)
            {
                b = BitChecker[i];
                if (change && b != 0 &&
                    (srcBuffer[new Index3(i, srcOff)] & b) != 0)
                {
                    change = false;
                    break;
                }
            }

            if (!change)
                return;

            Index2 off = dstOffset + (index * Zoom);

            for (int x = 0; x < Zoom; x++)
            {
                for (int y = 0; y < Zoom; y++)
                {
                    for (int i = 0; i < ColorSize; i++)
                    {

                        destBuffer[new Index3(i, off + new Index2(x, y))] = srcBuffer[new Index3(i, srcOff)];
                    }
                }
            }
        }
        private static void strategy(Index2 index, ArrayView3D<byte> destBuffer, ArrayView3D<byte> srcBuffer,
            Index2 dstOffset, Index2 srcOffset, int ColorSize, ArrayView<byte> BitChecker,
            int Zoom, ArrayView<byte> BackgroundColor)
        {
            bool change = true;
            byte b = 0;
            for (int i = 0; i < ColorSize; i++)
            {
                b = BitChecker[i];
                if (change && BitChecker[i] != 0 &&
                    (srcBuffer[new Index3(i, index + srcOffset)] & b) != 0)
                {
                    change = false;
                }
            }

            if (!change && BackgroundColor.Length != ColorSize)
                return;

            Index2 off = dstOffset + (index * Zoom);

            if (change)
            {
                Index2 srcOff = index + srcOffset;
                for (int x = 0; x < Zoom; x++)
                {
                    for (int y = 0; y < Zoom; y++)
                    {
                        for (int i = 0; i < ColorSize; i++)
                        {

                            destBuffer[new Index3(i, off + new Index2(x, y))] = srcBuffer[new Index3(i, srcOff)];
                        }
                    }
                }
            }
            else
            {
                for (int x = 0; x < Zoom; x++)
                {
                    for (int y = 0; y < Zoom; y++)
                    {
                        for (int i = 0; i < ColorSize; i++)
                        {

                            destBuffer[new Index3(i, off + new Index2(x, y))] = BackgroundColor[i];
                        }
                    }
                }
            }
        }
    }
}
