namespace SMWControlLibUtils
{
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy
    {
    }
    /// <summary>
    /// The strategy with objs params.
    /// </summary>
    public abstract class StrategyWithObjsParams : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="args">The args.</param>
        protected abstract void strategy(params object[] args);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param">The param.</param>
        protected abstract void strategy(T param);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T, U> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param">The param.</param>
        /// <param name="param2">The param2.</param>
        protected abstract void strategy(T param, U param2);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T, U, V> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        protected abstract void strategy(T param1, U param2, V param3);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T, U, V, W> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        protected abstract void strategy(T param1, U param2, V param3, W param4);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T, U, V, W, X> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        protected abstract void strategy(T param1, U param2, V param3, W param4, X param5);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T, U, V, W, X, Y> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        protected abstract void strategy(T param1, U param2, V param3, W param4, X param5, Y param6);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T, U, V, W, X, Y, Z> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        protected abstract void strategy(T param1, U param2, V param3, W param4, X param5, Y param6, Z param7);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T1, T2, T3, T4, T5, T6, T7, T8> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        /// <param name="param8">The param8.</param>
        protected abstract void strategy(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        /// <param name="param8">The param8.</param>
        /// <param name="param9">The param9.</param>
        protected abstract void strategy(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        /// <param name="param8">The param8.</param>
        /// <param name="param9">The param9.</param>
        /// <param name="param10">The param10.</param>
        protected abstract void strategy(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        /// <param name="param8">The param8.</param>
        /// <param name="param9">The param9.</param>
        /// <param name="param10">The param10.</param>
        /// <param name="param11">The param11.</param>
        protected abstract void strategy(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        /// <param name="param8">The param8.</param>
        /// <param name="param9">The param9.</param>
        /// <param name="param10">The param10.</param>
        /// <param name="param11">The param11.</param>
        /// <param name="param12">The param12.</param>
        protected abstract void strategy(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, T12 param12);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        /// <param name="param8">The param8.</param>
        /// <param name="param9">The param9.</param>
        /// <param name="param10">The param10.</param>
        /// <param name="param11">The param11.</param>
        /// <param name="param12">The param12.</param>
        /// <param name="param13">The param13.</param>
        protected abstract void strategy(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, T12 param12, T13 param13);
    }
    /// <summary>
    /// The strategy.
    /// </summary>
    public abstract class Strategy<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14> : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        /// <param name="param1">The param1.</param>
        /// <param name="param2">The param2.</param>
        /// <param name="param3">The param3.</param>
        /// <param name="param4">The param4.</param>
        /// <param name="param5">The param5.</param>
        /// <param name="param6">The param6.</param>
        /// <param name="param7">The param7.</param>
        /// <param name="param8">The param8.</param>
        /// <param name="param9">The param9.</param>
        /// <param name="param10">The param10.</param>
        /// <param name="param11">The param11.</param>
        /// <param name="param12">The param12.</param>
        /// <param name="param13">The param13.</param>
        /// <param name="param14">The param14.</param>
        protected abstract void strategy(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, T12 param12, T13 param13, T14 param14);
    }
    /// <summary>
    /// The strategy without params.
    /// </summary>
    public abstract class StrategyWithoutParams : Strategy
    {
        /// <summary>
        /// strategies the.
        /// </summary>
        protected abstract void strategy();
    }
}
