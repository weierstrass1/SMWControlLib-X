using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    public static class DrawRectangleKernel
    {
        private static readonly Action<Index2, ArrayView3D<byte>, Index2, ArrayView<byte>> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView3D<byte>, Index2, ArrayView<byte>>
            (strategy);
        public static void Execute(Index2 index, ArrayView3D<byte> destBuffer, Index2 offset, byte[] color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            using (MemoryBuffer<byte> c = HardwareAcceleratorManager.GPUAccelerator.Allocate<byte>(color.Length))
            {
                c.CopyFrom(color, 0, 0, color.Length);
                kernel(index, destBuffer, offset, c);
                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
            }
        }
        private static void strategy(Index2 index, ArrayView3D<byte> destBuffer, Index2 offset, ArrayView<byte> color)
        {
            Index2 ind = index + offset;
            for (int i = 0; i < color.Length; i++)
            {
                destBuffer[new Index3(i, ind)] = color[i];
            }
        }
    }
}
