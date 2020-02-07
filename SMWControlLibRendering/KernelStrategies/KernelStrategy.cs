using ILGPU;
using ILGPU.Runtime;
using SMWControlLibUtils;
using System;

namespace SMWControlLibRendering.KernelStrategies
{
    /// <summary>
    /// The kernel strategy without params.
    /// </summary>
    public abstract class KernelStrategy<T1, T2> : Strategy<T1, T2> where T1 : struct, IIndex
                                                                    where T2 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3> : Strategy<T1, T2, T3> where T1 : struct, IIndex
                                                                            where T2 : struct
                                                                            where T3 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3, T4> : Strategy<T1, T2, T3, T4> where T1 : struct, IIndex
                                                                                    where T2 : struct
                                                                                    where T3 : struct
                                                                                    where T4 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3, T4> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3, T4>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3, T4, T5> : Strategy<T1, T2, T3, T4, T5> where T1 : struct, IIndex
                                                                                            where T2 : struct
                                                                                            where T3 : struct
                                                                                            where T4 : struct
                                                                                            where T5 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3, T4, T5> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3, T4, T5>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3, T4, T5, T6> : Strategy<T1, T2, T3, T4, T5, T6> where T1 : struct, IIndex
                                                                                                    where T2 : struct
                                                                                                    where T3 : struct
                                                                                                    where T4 : struct
                                                                                                    where T5 : struct
                                                                                                    where T6 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3, T4, T5, T6> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3, T4, T5, T6>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3, T4, T5, T6, T7> : Strategy<T1, T2, T3, T4, T5, T6, T7> where T1 : struct, IIndex
                                                                                                            where T2 : struct 
                                                                                                            where T3 : struct
                                                                                                            where T4 : struct
                                                                                                            where T5 : struct
                                                                                                            where T6 : struct 
                                                                                                            where T7 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3, T4, T5, T6, T7> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3, T4, T5, T6, T7>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3, T4, T5, T6, T7, T8> : Strategy<T1, T2, T3, T4, T5, T6, T7, T8> where T1 : struct, IIndex
                                                                                                                    where T2 : struct
                                                                                                                    where T3 : struct
                                                                                                                    where T4 : struct
                                                                                                                    where T5 : struct
                                                                                                                    where T6 : struct
                                                                                                                    where T7 : struct
                                                                                                                    where T8 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3, T4, T5, T6, T7, T8> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3, T4, T5, T6, T7, T8>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3, T4, T5, T6, T7, T8, T9> : Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9> where T1 : struct, IIndex
                                                                                                                            where T2 : struct
                                                                                                                            where T3 : struct
                                                                                                                            where T4 : struct
                                                                                                                            where T5 : struct
                                                                                                                            where T6 : struct
                                                                                                                            where T7 : struct
                                                                                                                            where T8 : struct
                                                                                                                            where T9 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3, T4, T5, T6, T7, T8, T9> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3, T4, T5, T6, T7, T8, T9>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> where T1 : struct, IIndex
                                                                                                                        where T2 : struct
                                                                                                                        where T3 : struct
                                                                                                                        where T4 : struct
                                                                                                                        where T5 : struct
                                                                                                                        where T6 : struct
                                                                                                                        where T7 : struct
                                                                                                                        where T8 : struct
                                                                                                                        where T9 : struct
                                                                                                                        where T10 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> where T1 : struct, IIndex
                                                                                                                        where T2 : struct
                                                                                                                        where T3 : struct
                                                                                                                        where T4 : struct
                                                                                                                        where T5 : struct
                                                                                                                        where T6 : struct
                                                                                                                        where T7 : struct
                                                                                                                        where T8 : struct
                                                                                                                        where T9 : struct
                                                                                                                        where T10 : struct
                                                                                                                        where T11 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> where T1 : struct, IIndex
                                                                                                                        where T2 : struct
                                                                                                                        where T3 : struct
                                                                                                                        where T4 : struct
                                                                                                                        where T5 : struct
                                                                                                                        where T6 : struct
                                                                                                                        where T7 : struct
                                                                                                                        where T8 : struct
                                                                                                                        where T9 : struct
                                                                                                                        where T10 : struct
                                                                                                                        where T11 : struct
                                                                                                                        where T12 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> where T1 : struct, IIndex
                                                                                                                        where T2 : struct
                                                                                                                        where T3 : struct
                                                                                                                        where T4 : struct
                                                                                                                        where T5 : struct
                                                                                                                        where T6 : struct
                                                                                                                        where T7 : struct
                                                                                                                        where T8 : struct
                                                                                                                        where T9 : struct
                                                                                                                        where T10 : struct
                                                                                                                        where T11 : struct
                                                                                                                        where T12 : struct
                                                                                                                        where T13 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> where T1 : struct, IIndex
                                                                                                                    where T2 : struct
                                                                                                                    where T3 : struct
                                                                                                                    where T4 : struct
                                                                                                                    where T5 : struct
                                                                                                                    where T6 : struct
                                                                                                                    where T7 : struct
                                                                                                                    where T8 : struct
                                                                                                                    where T9 : struct
                                                                                                                    where T10 : struct
                                                                                                                    where T11 : struct
                                                                                                                    where T12 : struct
                                                                                                                    where T13 : struct
                                                                                                                    where T14 : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14>(strategy);
        }
    }
}
