using ILGPU;
using ILGPU.Runtime;
using ILGPU.Runtime.Cuda;
using ILGPU.Runtime.OpenCL;
using System;
using System.Collections.Generic;
using System.Text;

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
        private static readonly List<MemoryBuffer> buffers = new List<MemoryBuffer>();
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
            return gpuAccelerator != null;
        }
        /// <summary>
        /// Creates the g p u buffer.
        /// </summary>
        /// <param name="size">The size.</param>
        /// <returns>A MemoryBuffer.</returns>
        public static MemoryBuffer<T> CreateGPUBuffer<T>(int size) where T : struct
        {
            if (GPUAccelerator != null)
            {
                MemoryBuffer<T> buffer = GPUAccelerator.Allocate<T>(size);
                buffers.Add(buffer);
                return buffer;
            }
            return null;
        }
        /// <summary>
        /// Disposes the.
        /// </summary>
        public static void Dispose()
        {
            foreach(MemoryBuffer buffer in buffers)
            {
                buffer.Dispose();
            }
            if(gpuAccelerator != null) gpuAccelerator.Dispose();
            if (context != null) context.Dispose();
        }
    }
}
