using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a subscription to a parameterless reactive source.
    /// Disposing the instance will unsubscribe the associated action.
    /// </summary>
    public readonly struct Subscription : IDisposable
    {
        private readonly IReactive reactive;
        private readonly Action action;

        /// <summary>
        /// Initializes a new subscription for a parameterless reactive source.
        /// </summary>
        /// <param name="reactive">The reactive source.</param>
        /// <param name="action">The action to unsubscribe when disposed.</param>
        public Subscription(in IReactive reactive, in Action action)
        {
            this.reactive = reactive;
            this.action = action;
        }

        /// <summary>
        /// Unsubscribes the associated action from the reactive source.
        /// </summary>
        public void Dispose()
        {
            this.reactive?.Unsubscribe(this.action);
        }
    }

    /// <summary>
    /// Represents a subscription to a reactive source with one value.
    /// Disposing the instance will unsubscribe the associated action.
    /// </summary>
    /// <typeparam name="T">The type of the value emitted by the reactive source.</typeparam>
    public readonly struct Subscription<T> : IDisposable
    {
        private readonly IReactive<T> reactive;
        private readonly Action<T> action;

        /// <summary>
        /// Initializes a new subscription for a reactive source that emits a single value.
        /// </summary>
        /// <param name="reactive">The reactive source.</param>
        /// <param name="action">The action to unsubscribe when disposed.</param>
        public Subscription(in IReactive<T> reactive, in Action<T> action)
        {
            this.reactive = reactive;
            this.action = action;
        }

        /// <summary>
        /// Unsubscribes the associated action from the reactive source.
        /// </summary>
        public void Dispose()
        {
            this.reactive?.Unsubscribe(this.action);
        }
    }

    /// <summary>
    /// Represents a subscription to a reactive source with two values.
    /// Disposing the instance will unsubscribe the associated action.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    public readonly struct Subscription<T1, T2> : IDisposable
    {
        private readonly IReactive<T1, T2> reactive;
        private readonly Action<T1, T2> action;

        /// <summary>
        /// Initializes a new subscription for a reactive source that emits two values.
        /// </summary>
        /// <param name="reactive">The reactive source.</param>
        /// <param name="action">The action to unsubscribe when disposed.</param>
        public Subscription(IReactive<T1, T2> reactive, Action<T1, T2> action)
        {
            this.reactive = reactive;
            this.action = action;
        }

        /// <summary>
        /// Unsubscribes the associated action from the reactive source.
        /// </summary>
        public void Dispose()
        {
            this.reactive?.Unsubscribe(this.action);
        }
    }

    /// <summary>
    /// Represents a subscription to a reactive source with three values.
    /// Disposing the instance will unsubscribe the associated action.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    /// <typeparam name="T3">The type of the third emitted value.</typeparam>
    public readonly struct Subscription<T1, T2, T3> : IDisposable
    {
        private readonly IReactive<T1, T2, T3> reactive;
        private readonly Action<T1, T2, T3> action;

        /// <summary>
        /// Initializes a new subscription for a reactive source that emits three values.
        /// </summary>
        /// <param name="reactive">The reactive source.</param>
        /// <param name="action">The action to unsubscribe when disposed.</param>
        public Subscription(IReactive<T1, T2, T3> reactive, Action<T1, T2, T3> action)
        {
            this.reactive = reactive;
            this.action = action;
        }

        /// <summary>
        /// Unsubscribes the associated action from the reactive source.
        /// </summary>
        public void Dispose()
        {
            this.reactive?.Unsubscribe(this.action);
        }
    }

    /// <summary>
    /// Represents a subscription to a reactive source with four values.
    /// Disposing the instance will unsubscribe the associated action.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    /// <typeparam name="T3">The type of the third emitted value.</typeparam>
    /// <typeparam name="T4">The type of the fourth emitted value.</typeparam>
    public readonly struct Subscription<T1, T2, T3, T4> : IDisposable
    {
        private readonly IReactive<T1, T2, T3, T4> reactive;
        private readonly Action<T1, T2, T3, T4> action;

        /// <summary>
        /// Initializes a new subscription for a reactive source that emits four values.
        /// </summary>
        /// <param name="reactive">The reactive source.</param>
        /// <param name="action">The action to unsubscribe when disposed.</param>
        public Subscription(IReactive<T1, T2, T3, T4> reactive, Action<T1, T2, T3, T4> action)
        {
            this.reactive = reactive;
            this.action = action;
        }

        /// <summary>
        /// Unsubscribes the associated action from the reactive source.
        /// </summary>
        public void Dispose()
        {
            this.reactive?.Unsubscribe(this.action);
        }
    }
}