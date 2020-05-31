namespace SMWControlLibUtils
{

    /// <summary>
    /// The can factory.
    /// </summary>
    public abstract class CanFactory
    {
    }
    /// <summary>
    /// The can factory without params.
    /// </summary>
    public abstract class CanFactoryWithoutParams : CanFactory, IMustInitialize
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanFactoryWithoutParams"/> class.
        /// </summary>
        public CanFactoryWithoutParams()
        {
            Initialize();
        }

        /// <summary>
        /// Initializes the.
        /// </summary>
        public abstract void Initialize();
    }
    /// <summary>
    /// The can factory.
    /// </summary>
    public abstract class CanFactory<T> : CanFactory, IMustInitialize<T>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanFactory"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        public CanFactory(T param1)
        {
            Initialize(param1);
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        public abstract void Initialize(T param1);
    }
    /// <summary>
    /// The can factory.
    /// </summary>
    public abstract class CanFactory<T, U> : CanFactory, IMustInitialize<T, U>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanFactory"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        public CanFactory(T param1, U param2)
        {
            Initialize(param1, param2);
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        public abstract void Initialize(T param1, U param2);
    }
    /// <summary>
    /// The can factory.
    /// </summary>
    public abstract class CanFactory<T, U, V> : CanFactory, IMustInitialize<T, U, V>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanFactory"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        public CanFactory(T param1, U param2, V param3)
        {
            Initialize(param1, param2, param3);
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        public abstract void Initialize(T param1, U param2, V param3);
    }
    /// <summary>
    /// The can factory.
    /// </summary>
    public abstract class CanFactory<T, U, V, W> : CanFactory, IMustInitialize<T, U, V, W>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanFactory"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        public CanFactory(T param1, U param2, V param3, W param4)
        {
            Initialize(param1, param2, param3, param4);
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        public abstract void Initialize(T param1, U param2, V param3, W param4);
    }
    /// <summary>
    /// The can factory.
    /// </summary>
    public abstract class CanFactory<T, U, V, W, X> : CanFactory, IMustInitialize<T, U, V, W, X>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanFactory"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        public CanFactory(T param1, U param2, V param3, W param4, X param5)
        {
            Initialize(param1, param2, param3, param4, param5);
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        public abstract void Initialize(T param1, U param2, V param3, W param4, X param5);
    }
    /// <summary>
    /// The can factory.
    /// </summary>
    public abstract class CanFactory<T, U, V, W, X, Y> : CanFactory, IMustInitialize<T, U, V, W, X, Y>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanFactory"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        public CanFactory(T param1, U param2, V param3, W param4, X param5, Y param6)
        {
            Initialize(param1, param2, param3, param4, param5, param6);
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        public abstract void Initialize(T param1, U param2, V param3, W param4, X param5, Y param6);
    }
    /// <summary>
    /// The can factory.
    /// </summary>
    public abstract class CanFactory<T, U, V, W, X, Y, Z> : CanFactory, IMustInitialize<T, U, V, W, X, Y, Z>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanFactory"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        public CanFactory(T param1, U param2, V param3, W param4, X param5, Y param6, Z param7)
        {
            Initialize(param1, param2, param3, param4, param5, param6, param7);
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        public abstract void Initialize(T param1, U param2, V param3, W param4, X param5, Y param6, Z param7);
    }
    /// <summary>
    /// The can factory with objs params.
    /// </summary>
    public abstract class CanFactoryWithObjsParams : CanFactory, IMustInitializeWithObjsParams
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CanFactoryWithObjsParams"/> class.
        /// </summary>
        /// <param name="args">The args.</param>
        public CanFactoryWithObjsParams(params object[] args)
        {
            Initialize(args);
        }
        /// <summary>
        /// Initializes the.
        /// </summary>
        /// <param name="args">The args.</param>
        public abstract void Initialize(params object[] args);
    }
}
