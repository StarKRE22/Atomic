using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a signal that can notify subscribers of events without passing any data.
    /// </summary>
    public interface ISignal
    {
        /// <summary>
        /// Subscribes an action to be invoked when the signal is triggered.
        /// </summary>
        /// <param name="action">The action to invoke when the signal is triggered.</param>
        Subscription Subscribe(Action action);

        /// <summary>
        /// Unsubscribes a previously registered action from the signal.
        /// </summary>
        /// <param name="action">The action to remove from the list of subscribers.</param>
        void Unsubscribe(Action action);
    }

    /// <summary>
    /// Represents a reactive source that notifies subscribers with a single value.
    /// </summary>
    /// <typeparam name="T">The type of the value emitted to subscribers.</typeparam>
    public interface ISignal<T>
    {
        /// <summary>
        /// Subscribes an action to be invoked when the reactive source emits a value.
        /// </summary>
        /// <param name="action">The action to invoke with the emitted value.</param>
        Subscription<T> Subscribe(Action<T> action);

        /// <summary>
        /// Unsubscribes a previously registered action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove from the subscription list.</param>
        void Unsubscribe(Action<T> action);
    }

    /// <summary>
    /// Represents a reactive source that notifies subscribers with two values.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    public interface ISignal<T1, T2>
    {
        /// <summary>
        /// Subscribes an action to be invoked when the reactive source emits two values.
        /// </summary>
        /// <param name="action">The action to invoke with the emitted values.</param>
        Subscription<T1, T2> Subscribe(Action<T1, T2> action);

        /// <summary>
        /// Unsubscribes a previously registered action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove from the subscription list.</param>
        void Unsubscribe(Action<T1, T2> action);
    }

    /// <summary>
    /// Represents a reactive source that notifies subscribers with three values.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    /// <typeparam name="T3">The type of the third emitted value.</typeparam>
    public interface ISignal<T1, T2, T3>
    {
        /// <summary>
        /// Subscribes an action to be invoked when the reactive source emits three values.
        /// </summary>
        /// <param name="action">The action to invoke with the emitted values.</param>
        Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action);

        /// <summary>
        /// Unsubscribes a previously registered action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove from the subscription list.</param>
        void Unsubscribe(Action<T1, T2, T3> action);
    }

    /// <summary>
    /// Represents a reactive source that notifies subscribers with four values.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    /// <typeparam name="T3">The type of the third emitted value.</typeparam>
    /// <typeparam name="T4">The type of the fourth emitted value.</typeparam>
    public interface ISignal<T1, T2, T3, T4>
    {
        /// <summary>
        /// Subscribes an action to be invoked when the reactive source emits four values.
        /// </summary>
        /// <param name="action">The action to invoke with the emitted values.</param>
        Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action);

        /// <summary>
        /// Unsubscribes a previously registered action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove from the subscription list.</param>
        void Unsubscribe(Action<T1, T2, T3, T4> action);
    }
}