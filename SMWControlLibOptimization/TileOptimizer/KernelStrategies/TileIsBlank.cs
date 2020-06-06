using ILGPU;
using ILGPU.Runtime;
using SMWControlLibRendering;
using System;

namespace SMWControlLibOptimization.TileOptimizer.KernelStrategies
{
    public class TileIsBlank
    {
        private static readonly Action<Index2, ArrayView2D<int>, Index2, ArrayView<byte>> kernel =
            HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<Index2, ArrayView2D<int>, Index2, ArrayView<byte>>
            (strategy);

        public static bool Execute(Index2 extent, ArrayView2D<int> bpbuffer, Index2 offset)
        {
            byte res;
            using (MemoryBuffer<byte> r = HardwareAcceleratorManager.GPUAccelerator.Allocate<byte>(1))
            {
                r.CopyFrom(1, 0);
                kernel(extent, bpbuffer, offset, r);
                HardwareAcceleratorManager.GPUAccelerator.Synchronize();
                r.CopyTo(out res, 0);
            }
            return res == 1;
        }

        private static void strategy(Index2 index, ArrayView2D<int> bpbuffer, Index2 offset, ArrayView<byte> s)
        {
            if (bpbuffer[offset + index] < 0)
                s[0] = 0;
        }
    }
}
