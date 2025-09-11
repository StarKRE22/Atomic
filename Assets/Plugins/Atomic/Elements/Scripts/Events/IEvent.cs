
namespace Atomic.Elements
{
    /// <summary>
    /// Represents a parameterless reactive event that can be invoked and observed.
    /// </summary>
    /// <remarks>
    /// Inherits from <see cref="ISignal"/> and <see cref="IAction"/> to support both reactive tracking and action-based invocation.
    /// </remarks>
    public interface IEvent : ISignal, IAction
    {
    }

    /// <summary>
    /// Represents a reactive event with a single parameter that can be invoked and observed.
    /// </summary>
    /// <typeparam name="T">The type of the event parameter.</typeparam>
    /// <remarks>
    /// Inherits from <see cref="ISignal{T}"/> and <see cref="IAction{T}"/> to support reactive updates and invocation logic.
    /// </remarks>
    public interface IEvent<T> : ISignal<T>, IAction<T>
    {
    }

    /// <summary>
    /// Represents a reactive event with two parameters that can be invoked and observed.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <remarks>
    /// Inherits from <see cref="ISignal{T1,T2}"/> and <see cref="IAction{T1, T2}"/> to support reactive tracking and action invocation.
    /// </remarks>
    public interface IEvent<T1, T2> : ISignal<T1, T2>, IAction<T1, T2>
    {
    }

    /// <summary>
    /// Represents a reactive event with three parameters that can be invoked and observed.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <remarks>
    /// Inherits from <see cref="ISignal{T1,T2,T3}"/> and <see cref="IAction{T1, T2, T3}"/> to support reactive state and behavior.
    /// </remarks>
    public interface IEvent<T1, T2, T3> : ISignal<T1, T2, T3>, IAction<T1, T2, T3>
    {
    }

    /// <summary>
    /// Represents a reactive event source that allows subscription, invocation, and direct event access.
    /// Combines reactive observation and active triggering of events with four parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first event argument.</typeparam>
    /// <typeparam name="T2">The type of the second event argument.</typeparam>
    /// <typeparam name="T3">The type of the third event argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth event argument.</typeparam>
    public interface IEvent<T1, T2, T3, T4> : ISignal<T1, T2, T3, T4>, IAction<T1, T2, T3, T4>
    {
    }
}