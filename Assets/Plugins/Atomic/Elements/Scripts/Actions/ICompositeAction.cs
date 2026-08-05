using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a composite parameterless action that can contain multiple handlers.
    /// Invoking the action executes all registered handlers in the collection.
    /// </summary>
    /// <remarks>
    /// Combines the behavior of <see cref="IAction"/> and a reactive list of
    /// <see cref="Action"/> delegates, allowing handlers to be added, removed,
    /// and observed dynamically.
    /// </remarks>
    public interface ICompositeAction : IAction, IReactiveList<Action>
    {
    }
    
    /// <summary>
    /// Represents a composite action that takes one argument of type <typeparamref name="T"/>
    /// and can contain multiple handlers.
    /// Invoking the action executes all registered handlers with the specified argument.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <remarks>
    /// Combines the behavior of <see cref="IAction{T}"/> and a reactive list of
    /// <see cref="Action{T}"/> delegates, allowing handlers to be added, removed,
    /// and observed dynamically.
    /// </remarks>
    public interface ICompositeAction<T> : IAction<T>, IReactiveList<Action<T>>
    {
    }
    
    /// <summary>
    /// Represents a composite action that takes two arguments and can contain
    /// multiple handlers.
    /// Invoking the action executes all registered handlers with the specified arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <remarks>
    /// Combines the behavior of <see cref="IAction{T1, T2}"/> and a reactive list of
    /// <see cref="Action{T1, T2}"/> delegates, allowing handlers to be added, removed,
    /// and observed dynamically.
    /// </remarks>
    public interface ICompositeAction<T1, T2> : IAction<T1, T2>, IReactiveList<Action<T1, T2>>
    {
    }
    
    /// <summary>
    /// Represents a composite action that takes three arguments and can contain
    /// multiple handlers.
    /// Invoking the action executes all registered handlers with the specified arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <remarks>
    /// Combines the behavior of <see cref="IAction{T1, T2, T3}"/> and a reactive list of
    /// <see cref="Action{T1, T2, T3}"/> delegates, allowing handlers to be added, removed,
    /// and observed dynamically.
    /// </remarks>
    public interface ICompositeAction<T1, T2, T3> : IAction<T1, T2, T3>, IReactiveList<Action<T1, T2, T3>>
    {
    }
    
    /// <summary>
    /// Represents a composite action that takes four arguments and can contain
    /// multiple handlers.
    /// Invoking the action executes all registered handlers with the specified arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    /// <remarks>
    /// Combines the behavior of <see cref="IAction{T1, T2, T3, T4}"/> and a reactive list of
    /// <see cref="Action{T1, T2, T3, T4}"/> delegates, allowing handlers to be added, removed,
    /// and observed dynamically.
    /// </remarks>
    public interface ICompositeAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>, IReactiveList<Action<T1, T2, T3, T4>>
    {
    }
}
