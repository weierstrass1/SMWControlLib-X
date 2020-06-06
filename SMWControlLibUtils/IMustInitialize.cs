namespace SMWControlLibUtils
{
    public interface IMustInitialize
    {
        void Initialize();
    }
    public interface IMustInitialize<T1>
    {
        void Initialize(T1 param1);
    }
    public interface IMustInitialize<T1, T2>
    {
        void Initialize(T1 param1, T2 param2);
    }
    public interface IMustInitialize<T1, T2, T3>
    {
        void Initialize(T1 param1, T2 param2, T3 param3);
    }
    public interface IMustInitialize<T1, T2, T3, T4>
    {
        void Initialize(T1 param1, T2 param2, T3 param3, T4 param4);
    }
    public interface IMustInitialize<T1, T2, T3, T4, T5>
    {
        void Initialize(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5);
    }
    public interface IMustInitialize<T1, T2, T3, T4, T5, T6>
    {
        void Initialize(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6);
    }
    public interface IMustInitialize<T1, T2, T3, T4, T5, T6, T7>
    {
        void Initialize(T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7);
    }
    public interface IMustInitializeWithObjsParams
    {
        void Initialize(params object[] args);
    }
}
