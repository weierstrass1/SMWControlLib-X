using ILGPU;
using ILGPU.Runtime;
using System;

namespace SMWControlLibRendering.KernelStrategies.BitmapBufferKernels
{
    public static class DrawSubBufferKernel
    {
        private static Action<Index3, ArrayView3D<byte>, ArrayView3D<byte>, Index2> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index3, ArrayView3D<byte>, ArrayView3D<byte>, Index2>
            (strategy);

        public static void Execute(ArrayView3D<byte> Buffer, ArrayView3D<byte> subBuffer, Index2 offset)
        {
            kernel(subBuffer.Extent, Buffer, subBuffer, offset);
            HardwareAcceleratorManager.GPUAccelerator.Synchronize();
        }

        private static void strategy(Index3 index, ArrayView3D<byte> Buffer, ArrayView3D<byte> subBuffer, Index2 offset)
        {
            subBuffer[index] = Buffer[new Index3(subBuffer.Extent.X - 1 - index.X, offset + index.YZ)];
        }
    }
}
