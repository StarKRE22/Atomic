using System;

namespace Atomic.Elements
{
    public interface ICompositeAction : IAction, IReactiveList<Action>
    {
    }
    
    public interface ICompositeAction<T> : IAction<T>, IReactiveList<Action<T>>
    {
    }
    
    public interface ICompositeAction<T1, T2> : IAction<T1, T2>, IReactiveList<Action<T1, T2>>
    {
    }
    
    public interface ICompositeAction<T1, T2, T3> : IAction<T1, T2, T3>, IReactiveList<Action<T1, T2, T3>>
    {
    }
    
    public interface ICompositeAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>, IReactiveList<Action<T1, T2, T3, T4>>
    {
    }
}