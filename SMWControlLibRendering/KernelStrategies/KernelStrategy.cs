using ILGPU;
using ILGPU.Runtime;
using SMWControlLibUtils;
using System;

namespace SMWControlLibRendering.KernelStrategies
{
    /// <summary>
    /// The kernel strategy without params.
    /// </summary>
    public abstract class KernelStrategy<T, U> : Strategy<T, U> where T : struct, IIndex
                                                                where U : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T, U> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T, U> (strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T, U, V> : Strategy<T, U, V>   where T : struct, IIndex
                                                                        where U : struct
                                                                        where V : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T, U, V> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T, U, V>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T, U, V, W> : Strategy<T, U, V, W> where T : struct, IIndex
                                                                            where U : struct
                                                                            where V : struct
                                                                            where W : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T, U, V, W> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T, U, V, W>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T, U, V, W, X> : Strategy<T, U, V, W, X> where T : struct, IIndex
                                                                                    where U : struct
                                                                                    where V : struct
                                                                                    where W : struct
                                                                                    where X : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T, U, V, W, X> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T, U, V, W, X>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T, U, V, W, X, Y> : Strategy<T, U, V, W, X, Y> where T : struct, IIndex
                                                                                        where U : struct
                                                                                        where V : struct
                                                                                        where W : struct
                                                                                        where X : struct
                                                                                        where Y : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T, U, V, W, X, Y> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T, U, V, W, X, Y>(strategy);
        }
    }
    /// <summary>
    /// The kernel strategy.
    /// </summary>
    public abstract class KernelStrategy<T, U, V, W, X, Y, Z> : Strategy<T, U, V, W, X, Y, Z> where T : struct, IIndex
                                                                                        where U : struct
                                                                                        where V : struct
                                                                                        where W : struct
                                                                                        where X : struct
                                                                                        where Y : struct
                                                                                        where Z : struct
    {
        /// <summary>
        /// Gets or sets the kernel.
        /// </summary>
        protected Action<T, U, V, W, X, Y, Z> kernel { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="KernelStrategy"/> class.
        /// </summary>
        public KernelStrategy()
        {
            kernel = HardwareAcceleratorManager.GPUAccelerator.LoadAutoGroupedStreamKernel<T, U, V, W, X, Y, Z>(strategy);
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
