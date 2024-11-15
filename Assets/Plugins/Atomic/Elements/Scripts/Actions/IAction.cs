namespace Atomic.Elements
{
    public interface IAction
    {
        void Invoke();
    }

    public interface IAction<in T>
    {
        void Invoke(T arg);
    }

    public interface IAction<in T1, in T2>
    {
        void Invoke(T1 args1, T2 args2);
    }
    
    public interface IAction<in T1, in T2, in T3>
    {
        void Invoke(T1 args1, T2 args2, T3 args3);
    }
}


