namespace SMWControlLibUtils
{
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class ObjectFactory<S> where S : CanFactory
    {
    }
    /// <summary>
    /// The object factory without params.
    /// </summary>
    public abstract class ObjectFactoryWithoutParams<S> : ObjectFactory<S> where S : CanFactoryWithoutParams
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <returns>A S.</returns>
        public abstract S GenerateObject();
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class ObjectFactory<S, T> : ObjectFactory<S> where S : CanFactory<T>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <returns>A S.</returns>
        public abstract S GenerateObject(T param1);
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class ObjectFactory<S, T, U> : ObjectFactory<S> where S : CanFactory<T, U>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <returns>A S.</returns>
        public abstract S GenerateObject(T param1, U param2);
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class ObjectFactory<S, T, U, V> : ObjectFactory<S> where S : CanFactory<T, U, V>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <returns>A S.</returns>
        public abstract S GenerateObject(T param1, U param2, V param3);
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class ObjectFactory<S, T, U, V, W> : ObjectFactory<S> where S : CanFactory<T, U, V, W>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <returns>A S.</returns>
        public abstract S GenerateObject(T param1, U param2, V param3, W param4);
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class ObjectFactory<S, T, U, V, W, X> : ObjectFactory<S> where S : CanFactory<T, U, V, W, X>
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <returns>A S.</returns>
        public abstract S GenerateObject(T param1, U param2, V param3, W param4, X param5);
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class ObjectFactory<S, T, U, V, W, X, Y> : ObjectFactory<S> where S : CanFactory<T, U, V, W, X, Y>
    {
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
        public abstract S GenerateObject(T param1, U param2, V param3, W param4, X param5, Y param6);
    }
    /// <summary>
    /// The object factory.
    /// </summary>
    public abstract class ObjectFactory<S, T, U, V, W, X, Y, Z> : ObjectFactory<S> where S : CanFactory<T, U, V, W, X, Y, Z>
    {
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
        public abstract S GenerateObject(T param1, U param2, V param3, W param4, X param5, Y param6, Z param7);
    }
    /// <summary>
    /// The object factory with objs params.
    /// </summary>
    public abstract class ObjectFactoryWithObjsParams<S> : ObjectFactory<S> where S : CanFactoryWithObjsParams
    {
        /// <summary>
        /// Generates the object.
        /// </summary>
        /// <param name="args">The args.</param>
        /// <returns>A S.</returns>
        public abstract S GenerateObject(params object[] args);
    }
}
