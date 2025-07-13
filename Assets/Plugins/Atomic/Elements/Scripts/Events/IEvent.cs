using System;

namespace Atomic.Elements
{
    public interface IEvent : IReactive, IAction
    {
        event Action OnEvent;
    }

    public interface IEvent<T> : IReactive<T>, IAction<T>
    {
        event Action<T> OnEvent;
    }

    public interface IEvent<T1, T2> : IReactive<T1, T2>, IAction<T1, T2>
    {
        event Action<T1, T2> OnEvent;
    }

    public interface IEvent<T1, T2, T3> : IReactive<T1, T2, T3>, IAction<T1, T2, T3>
    {
        event Action<T1, T2, T3> OnEvent;
    }
}