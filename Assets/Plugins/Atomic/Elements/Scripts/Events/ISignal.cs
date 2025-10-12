using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a signal that can notify subscribers of events without passing any data.
    /// </summary>
    public interface ISignal
    {
        /// <summary>
        /// Occurs when the signal is emitted.
        /// </summary>
        event Action OnEvent;
    }

    /// <summary>
    /// Represents a reactive source that notifies subscribers with a single value.
    /// </summary>
    /// <typeparam name="T">The type of the value emitted to subscribers.</typeparam>
    public interface ISignal<out T>
    {
        /// <summary>
        /// Occurs when the signal is emitted with single argument.
        /// </summary>
        event Action<T> OnEvent;
    }

    /// <summary>
    /// Represents a reactive source that notifies subscribers with two values.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    public interface ISignal<out T1, out T2>
    {
        /// <summary>
        /// Occurs when the signal is emitted with two arguments.
        /// </summary>
        event Action<T1, T2> OnEvent;
    }

    /// <summary>
    /// Represents a reactive source that notifies subscribers with three values.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    /// <typeparam name="T3">The type of the third emitted value.</typeparam>
    public interface ISignal<out T1, out T2, out T3>
    {
        /// <summary>
        /// Occurs when the signal is emitted with three arguments.
        /// </summary>
        event Action<T1, T2, T3> OnEvent;
    }

    /// <summary>
    /// Represents a reactive source that notifies subscribers with four values.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    /// <typeparam name="T3">The type of the third emitted value.</typeparam>
    /// <typeparam name="T4">The type of the fourth emitted value.</typeparam>
    public interface ISignal<out T1, out T2, out T3, out T4>
    {
        /// <summary>
        /// Occurs when the signal is emitted with four arguments.
        /// </summary>
        event Action<T1, T2, T3, T4> OnEvent;
    }
}