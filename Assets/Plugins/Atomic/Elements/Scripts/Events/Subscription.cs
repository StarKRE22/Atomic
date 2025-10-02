using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a subscription to a parameterless signal source.
    /// Disposing the instance will unsubscribe the associated action.
    /// </summary>
    public readonly struct Subscription : IDisposable
    {
        private readonly ISignal signal;
        private readonly Action action;

        /// <summary>
        /// Initializes a new subscription for a parameterless signal source.
        /// </summary>
        /// <param name="signal">The signal source.</param>
        /// <param name="action">The action to unsubscribe when disposed.</param>
        public Subscription(ISignal signal, Action action)
        {
            this.signal = signal ?? throw new ArgumentNullException(nameof(signal));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.signal.OnEvent += this.action;
        }

        /// <summary>
        /// Unsubscribes the associated action from the signal source.
        /// </summary>
        public void Dispose() => this.signal.OnEvent -= this.action;
    }

    /// <summary>
    /// Represents a subscription to a signal source with one value.
    /// Disposing the instance will unsubscribe the associated action.
    /// </summary>
    /// <typeparam name="T">The type of the value emitted by the signal source.</typeparam>
    public readonly struct Subscription<T> : IDisposable
    {
        private readonly ISignal<T> signal;
        private readonly Action<T> action;

        /// <summary>
        /// Initializes a new subscription for a signal source that emits a single value.
        /// </summary>
        /// <param name="signal">The signal source.</param>
        /// <param name="action">The action to unsubscribe when disposed.</param>
        public Subscription(ISignal<T> signal, Action<T> action)
        {
            this.signal = signal ?? throw new ArgumentNullException(nameof(signal));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.signal.OnEvent += this.action;
        }

        /// <summary>
        /// Unsubscribes the associated action from the signal source.
        /// </summary>
        public void Dispose() => this.signal.OnEvent -= this.action;
    }

    /// <summary>
    /// Represents a subscription to a signal source with two values.
    /// Disposing the instance will unsubscribe the associated action.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    public readonly struct Subscription<T1, T2> : IDisposable
    {
        private readonly ISignal<T1, T2> signal;
        private readonly Action<T1, T2> action;

        /// <summary>
        /// Initializes a new subscription for a signal source that emits two values.
        /// </summary>
        /// <param name="signal">The signal source.</param>
        /// <param name="action">The action to unsubscribe when disposed.</param>
        public Subscription(ISignal<T1, T2> signal, Action<T1, T2> action)
        {
            this.signal = signal ?? throw new ArgumentNullException(nameof(signal));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.signal.OnEvent += this.action;
        }

        /// <summary>
        /// Unsubscribes the associated action from the signal source.
        /// </summary>
        public void Dispose() => this.signal.OnEvent -= this.action;
    }

    /// <summary>
    /// Represents a subscription to a signal source with three values.
    /// Disposing the instance will unsubscribe the associated action.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    /// <typeparam name="T3">The type of the third emitted value.</typeparam>
    public readonly struct Subscription<T1, T2, T3> : IDisposable
    {
        private readonly ISignal<T1, T2, T3> signal;
        private readonly Action<T1, T2, T3> action;

        /// <summary>
        /// Initializes a new subscription for a signal source that emits three values.
        /// </summary>
        /// <param name="signal">The signal source.</param>
        /// <param name="action">The action to unsubscribe when disposed.</param>
        public Subscription(ISignal<T1, T2, T3> signal, Action<T1, T2, T3> action)
        {
            this.signal = signal ?? throw new ArgumentNullException(nameof(signal));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.signal.OnEvent += this.action;
        }

        /// <summary>
        /// Unsubscribes the associated action from the signal source.
        /// </summary>
        public void Dispose() => this.signal.OnEvent -= this.action;
    }

    /// <summary>
    /// Represents a subscription to a signal source with four values.
    /// Disposing the instance will unsubscribe the associated action.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    /// <typeparam name="T3">The type of the third emitted value.</typeparam>
    /// <typeparam name="T4">The type of the fourth emitted value.</typeparam>
    public readonly struct Subscription<T1, T2, T3, T4> : IDisposable
    {
        private readonly ISignal<T1, T2, T3, T4> signal;
        private readonly Action<T1, T2, T3, T4> action;

        /// <summary>
        /// Initializes a new subscription for a signal source that emits four values.
        /// </summary>
        /// <param name="signal">The signal source.</param>
        /// <param name="action">The action to unsubscribe when disposed.</param>
        public Subscription(ISignal<T1, T2, T3, T4> signal, Action<T1, T2, T3, T4> action)
        {
            this.signal = signal ?? throw new ArgumentNullException(nameof(signal));
            this.action = action ?? throw new ArgumentNullException(nameof(action));
            this.signal.OnEvent += this.action;
        }

        /// <summary>
        /// Unsubscribes the associated action from the signal source.
        /// </summary>
        public void Dispose() => this.signal.OnEvent -= this.action;
    }
}