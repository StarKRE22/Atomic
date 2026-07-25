using System;

namespace Atomic.Elements
{
    public interface ICommand : IAction, ISignal
    {
        bool CanInvoke();

        ICommand AddCondition(Func<bool> condition);
        ICommand RemoveCondition(Func<bool> condition);

        ICommand AddAction(Action action);
        ICommand RemoveAction(Action action);
    }
    
    public interface ICommand<T> : IAction<T>, ISignal<T>
    {
        bool CanInvoke(T arg);

        ICommand<T> AddCondition(Func<T, bool> condition);
        ICommand<T> RemoveCondition(Func<T, bool> condition);

        ICommand<T> AddAction(Action<T> action);
        ICommand<T> RemoveAction(Action<T> action);
    }

    public interface ICommand<T1, T2> : IAction<T1, T2>, ISignal<T1, T2>
    {
        bool CanInvoke(T1 arg1, T2 arg2);

        ICommand<T1, T2> AddCondition(Func<T1, T2, bool> condition);
        ICommand<T1, T2> RemoveCondition(Func<T1, T2, bool> condition);

        ICommand<T1, T2> AddAction(Action<T1, T2> action);
        ICommand<T1, T2> RemoveAction(Action<T1, T2> action);
    }
    
    public interface ICommand<T1, T2, T3> : IAction<T1, T2, T3>, ISignal<T1, T2, T3>
    {
        bool CanInvoke(T1 arg1, T2 arg2, T3 arg3);

        ICommand<T1, T2, T3> AddCondition(Func<T1, T2, T3, bool> condition);
        ICommand<T1, T2, T3> RemoveCondition(Func<T1, T2, T3, bool> condition);

        ICommand<T1, T2, T3> AddAction(Action<T1, T2, T3> action);
        ICommand<T1, T2, T3> RemoveAction(Action<T1, T2, T3> action);
    }
    
    public interface ICommand<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>, ISignal<T1, T2, T3, T4>
    {
        bool CanInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4);

        ICommand<T1, T2, T3, T4> AddCondition(Func<T1, T2, T3, T4, bool> condition);
        ICommand<T1, T2, T3, T4> RemoveCondition(Func<T1, T2, T3, T4, bool> condition);

        ICommand<T1, T2, T3, T4> AddAction(Action<T1, T2, T3, T4> action);
        ICommand<T1, T2, T3, T4> RemoveAction(Action<T1, T2, T3, T4> action);
    }
}