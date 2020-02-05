namespace SMWControlLibUtils
{
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class Disguise<R, S> where R : ObjectFactory<S>
                                         where S : CanFactory
    {
    }
    /// <summary>
    /// The object factory without params.
    /// </summary>
    public abstract class DisguiseWithoutParams<R, S> : Disguise<R, S> where R : ObjectFactoryWithoutParams<S>, new()
                                                                        where S : CanFactoryWithoutParams
    {
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        protected R factory { get; set; }
        /// <summary>
        /// Gets or sets the real object.
        /// </summary>
        public S RealObject { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DisguiseWithoutParams"/> class.
        /// </summary>
        public DisguiseWithoutParams()
        {
            factory = new R();
            RealObject = DressUp();
        }
        /// <summary>
        /// Dresses the up.
        /// </summary>
        /// <returns>A S.</returns>
        protected virtual S DressUp()
        {
            return factory.GenerateObject();
        }
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class Disguise<R, S, T> : Disguise<R, S>   where R : ObjectFactory<S, T>, new()
                                                                where S : CanFactory<T>
    {
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        protected R factory { get; set; }
        /// <summary>
        /// Gets or sets the real object.
        /// </summary>
        public S RealObject { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Disguise"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        public Disguise(T param1)
        {
            factory = new R();
            RealObject = DressUp(param1);
        }
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <returns>A S.</returns>
        protected virtual S DressUp(T param1)
        {
            return factory.GenerateObject(param1);
        }
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class Disguise<R, S, T, U> : Disguise<R, S> where R : ObjectFactory<S,T,U>, new()
                                                                where S : CanFactory<T, U>
    {
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        protected R factory { get; set; }
        /// <summary>
        /// Gets or sets the real object.
        /// </summary>
        public S RealObject { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Disguise"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        public Disguise(T param1, U param2)
        {
            factory = new R();
            RealObject = DressUp(param1, param2);
        }
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <returns>A S.</returns>
        protected virtual S DressUp(T param1, U param2)
        {
            return factory.GenerateObject(param1, param2);
        }
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class Disguise<R, S, T, U, V> : Disguise<R, S>  where R : ObjectFactory<S,T,U,V>, new()
                                                                    where S : CanFactory<T, U, V>
    {
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        protected R factory { get; set; }
        /// <summary>
        /// Gets or sets the real object.
        /// </summary>
        public S RealObject { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Disguise"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        public Disguise(T param1, U param2, V param3)
        {
            factory = new R();
            RealObject = DressUp(param1, param2, param3);
        }
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <returns>A S.</returns>
        protected virtual S DressUp(T param1, U param2, V param3)
        {
            return factory.GenerateObject(param1, param2, param3);
        }
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class Disguise<R, S, T, U, V, W> : Disguise<R, S>   where R : ObjectFactory<S, T, U, V, W>, new()
                                                                        where S : CanFactory<T, U, V, W>
    {
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        protected R factory { get; set; }
        /// <summary>
        /// Gets or sets the real object.
        /// </summary>
        public S RealObject { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Disguise"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        public Disguise(T param1, U param2, V param3, W param4)
        {
            factory = new R();
            RealObject = DressUp(param1, param2, param3, param4);
        }
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <returns>A S.</returns>
        protected virtual S DressUp(T param1, U param2, V param3, W param4)
        {
            return factory.GenerateObject(param1, param2, param3, param4);
        }
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class Disguise<R, S, T, U, V, W, X> : Disguise<R, S>    where R : ObjectFactory<S, T, U, V, W, X>, new()
                                                                            where S : CanFactory<T, U, V, W, X>
    {
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        protected R factory { get; set; }
        /// <summary>
        /// Gets or sets the real object.
        /// </summary>
        public S RealObject { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Disguise"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        public Disguise(T param1, U param2, V param3, W param4, X param5)
        {
            factory = new R();
            RealObject = DressUp(param1, param2, param3, param4, param5);
        }
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <returns>A S.</returns>
        protected virtual S DressUp(T param1, U param2, V param3, W param4, X param5)
        {
            return factory.GenerateObject(param1, param2, param3, param4, param5);
        }
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class Disguise<R, S, T, U, V, W, X, Y> : Disguise<R, S> where R : ObjectFactory<S, T, U, V, W, X, Y>, new()
                                                                            where S : CanFactory<T, U, V, W, X, Y>
    {
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        protected R factory { get; set; }
        /// <summary>
        /// Gets or sets the real object.
        /// </summary>
        public S RealObject { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Disguise"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        public Disguise(T param1, U param2, V param3, W param4, X param5, Y param6)
        {
            factory = new R();
            RealObject = DressUp(param1, param2, param3, param4, param5, param6);
        }
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <returns>A S.</returns>
        protected virtual S DressUp(T param1, U param2, V param3, W param4, X param5, Y param6)
        {
            return factory.GenerateObject(param1, param2, param3, param4, param5, param6);
        }
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class Disguise<R, S, T, U, V, W, X, Y, Z> : Disguise<R, S>  where R : ObjectFactory<S, T, U, V, W, X, Y, Z>, new()
                                                                                where S : CanFactory<T, U, V, W, X, Y, Z>
    {
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        protected R factory { get; set; }
        /// <summary>
        /// Gets or sets the real object.
        /// </summary>
        public S RealObject { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="Disguise"/> class.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        public Disguise(T param1, U param2, V param3, W param4, X param5, Y param6, Z param7)
        {
            factory = new R();
            RealObject = DressUp(param1, param2, param3, param4, param5, param6, param7);
        }
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        /// <returns>A S.</returns>
        protected virtual S DressUp(T param1, U param2, V param3, W param4, X param5, Y param6, Z param7)
        {
            return factory.GenerateObject(param1, param2, param3, param4, param5, param6, param7);
        }
    }
    /// <summary>
    /// The object factory with objs params.
    /// </summary>
    public abstract class DisguiseWithObjsParams<R, S> : Disguise<R, S> where R : ObjectFactoryWithObjsParams<S>, new()
                                                                        where S : CanFactoryWithObjsParams
    {
        /// <summary>
        /// Gets or sets the factory.
        /// </summary>
        protected R factory { get; set; }
        /// <summary>
        /// Gets or sets the real object.
        /// </summary>
        public S RealObject { get; protected set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="DisguiseWithObjsParams"/> class.
        /// </summary>
        /// <param name="args">The args.</param>
        public DisguiseWithObjsParams(params object[] args)
        {
            factory = new R();
            RealObject = DressUp(args);
        }
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>A S.</returns>
        protected virtual S DressUp(params object[] args)
        {
            return factory.GenerateObject(args);
        }
    }
}
