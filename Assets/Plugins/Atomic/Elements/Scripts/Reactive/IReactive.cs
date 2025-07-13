using System;

namespace Atomic.Elements
{
    public interface IReactive
    {
        void Subscribe(Action action);
        void Unsubscribe(Action action);
    }

    public interface IReactive<out T>
    {
        void Subscribe(Action<T> action);
        void Unsubscribe(Action<T> action);
    }

    public interface IReactive<out T1, out T2>
    {
        void Subscribe(Action<T1, T2> action);
        void Unsubscribe(Action<T1, T2> action);
    }

    public interface IReactive<out T1, out T2, out T3>
    {
        void Subscribe(Action<T1, T2, T3> action);
        void Unsubscribe(Action<T1, T2, T3> action);
    }
    
    public interface IReactive<out T1, out T2, out T3, out T4>
    {
        void Subscribe(Action<T1, T2, T3, T4> action);
        void Unsubscribe(Action<T1, T2, T3, T4> action);
    }
}