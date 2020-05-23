using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.Cuda;
using ILGPU.Runtime.OpenCL;

namespace SMWControlLibRendering
{
    /// <summary>
    /// The hardware accelerator manager.
    /// </summary>
    public static class HardwareAcceleratorManager
    {
        /// <summary>
        /// Gets the context.
        /// </summary>
        private static Context context { get; set; }

        private static Accelerator gpuAccelerator;

        /// <summary>
        /// Gets the g p u accelerator.
        /// </summary>
        public static Accelerator GPUAccelerator
        {
            get
            {
                getGPUAccelerator();
                return gpuAccelerator;
            }
        }

        /// <summary>
        /// gets the g p u accelerator.
        /// </summary>
        /// <returns>An Accelerator.</returns>
        private static void getGPUAccelerator()
        {
            if (gpuAccelerator != null)
                return;

            if (CudaAccelerator.CudaAccelerators.Length > 0)
            {
                if (context == null)
                    context = new Context();
                gpuAccelerator = Accelerator.Create(context, CudaAccelerator.CudaAccelerators[0]);
                return;
            }
            foreach (CLAcceleratorId aid in CLAccelerator.CLAccelerators)
            {
                if (aid.DeviceType == ILGPU.Runtime.OpenCL.API.CLDeviceType.CL_DEVICE_TYPE_GPU)
                {
                    if (context == null)
                        context = new Context();
                    gpuAccelerator = Accelerator.Create(context, aid);
                }
            }
        }

        /// <summary>
        /// Are the available.
        /// </summary>
        /// <returns>A bool.</returns>
        public static bool IsGPUAvailable()
        {
            if (gpuAccelerator == null)
                getGPUAccelerator();
            return gpuAccelerator != null;
        }

        /// <summary>
        /// Disposes the.
        /// </summary>
        public static void Dispose()
        {
            if (context != null) context.Dispose();
            if (GPUAccelerator != null) GPUAccelerator.Dispose();
        }
    }
}