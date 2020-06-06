using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    public static class FillWithColorKernel
    {
        private static readonly Action<Index3, ArrayView3D<byte>, ArrayView<byte>> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index3, ArrayView3D<byte>, ArrayView<byte>>
            (strategy);
        public static void Execute(Index3 index, ArrayView3D<byte> destBuffer, byte[] color)
        {
            if (color == null)
                throw new ArgumentNullException(nameof(color));

            using (MemoryBuffer<byte> c = HardwareAcceleratorManager.GPUAccelerator.Allocate<byte>(color.Length))
            {
                c.CopyFrom(color, 0, 0, color.Length);
                kernel(index, destBuffer, c);
                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
            }
        }
        private static void strategy(Index3 index, ArrayView3D<byte> destBuffer, ArrayView<byte> color)
        {
            destBuffer[index] = color[index.X];
        }
    }
}
