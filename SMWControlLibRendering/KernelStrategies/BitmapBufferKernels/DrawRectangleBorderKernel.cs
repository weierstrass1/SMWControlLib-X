using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering.Exceptions;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    public static class DrawRectangleBorderKernel
    {
        private static readonly Action<Index1, ArrayView3D<byte>, Index2, int, int, ArrayView<byte>> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index1, ArrayView3D<byte>, Index2, int, int, ArrayView<byte>>
            (strategy);
        public static void Execute(Index1 index, ArrayView3D<byte> destBuffer,
            Index2 offset, int rWidth, int rHeight, byte[] color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));
            if (color.Length != destBuffer.Extent.X)
                throw new ArrayLengthNotValid(nameof(color), $"Must be {destBuffer.Extent.X}");

            using (MemoryBuffer<byte> c = HardwareAcceleratorManager.GPUAccelerator.Allocate<byte>(color.Length))
            {
                c.CopyFrom(color, 0, Index1.Zero, color.Length);
                kernel(index, destBuffer, offset, rWidth, rHeight, c);
                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
            }
        }
        private static void strategy(Index1 index, ArrayView3D<byte> destBuffer, Index2 offset,
            int rWidth, int rHeight, ArrayView<byte> color)
        {
            if (index <= rWidth)
            {
                Index2 indw0 = offset + new Index2(index, 0);
                Index2 indw1 = indw0 + new Index2(0, rHeight);

                for (int i = 0; i < color.Length; i++)
                {
                    destBuffer[new Index3(i, indw0)] = color[i];
                    destBuffer[new Index3(i, indw1)] = color[i];
                }
            }
            if (index <= rHeight)
            {
                Index2 indh0 = offset + new Index2(0, index);
                Index2 indh1 = indh0 + new Index2(rWidth, 0);

                for (int i = 0; i < color.Length; i++)
                {
                    destBuffer[new Index3(i, indh0)] = color[i];
                    destBuffer[new Index3(i, indh1)] = color[i];
                }
            }
        }
    }
}
