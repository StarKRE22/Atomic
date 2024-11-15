namespace Atomic.Elements
{
    public interface IReactive
    {
        System.Action Subscribe(System.Action action);
        void Unsubscribe(System.Action action);
    }

    public interface IReactive<T>
    {
        System.Action<T> Subscribe(System.Action<T> action);
        void Unsubscribe(System.Action<T> action);
    }

    public interface IReactive<T1, T2>
    {
        System.Action<T1, T2> Subscribe(System.Action<T1, T2> action);
        void Unsubscribe(System.Action<T1, T2> action);
    }

    public interface IReactive<T1, T2, T3>
    {
        System.Action<T1, T2, T3> Subscribe(System.Action<T1, T2, T3> action);
        void Unsubscribe(System.Action<T1, T2, T3> action);
    }
}