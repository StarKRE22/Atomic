using System;

namespace Atomic.Elements
{
    /// <summary>
    /// A base implementation of a non-generic reactive source.
    /// </summary>
    [Serializable]
    public class InlineSignal : ISignal
    {
        public event Action OnEvent
        {
            add => this.subscribe.Invoke(value);
            remove => this.unsubscribe.Invoke(value);
        }

        private readonly Action<Action> subscribe;
        private readonly Action<Action> unsubscribe;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineSignal"/> class with subscription delegates.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public InlineSignal(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            this.subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
            this.unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));
        }
    }

    /// <summary>
    /// A base implementation of a generic reactive source with one value.
    /// </summary>
    /// <typeparam name="T">The type of value emitted by the reactive source.</typeparam>
    [Serializable]
    public class InlineSignal<T> : ISignal<T>
    {
        public event Action<T> OnEvent
        {
            add => this.subscribe.Invoke(value);
            remove => this.unsubscribe.Invoke(value);
        }

        private readonly Action<Action<T>> subscribe;
        private readonly Action<Action<T>> unsubscribe;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineSignal{T}"/> class with subscription delegates.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public InlineSignal(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            this.subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
            this.unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));
        }
    }

    /// <summary>
    /// A base implementation of a reactive source with two parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    [Serializable]
    public class InlineSignal<T1, T2> : ISignal<T1, T2>
    {
        public event Action<T1, T2> OnEvent
        {
            add => this.subscribe.Invoke(value);
            remove => this.unsubscribe.Invoke(value);
        }

        private readonly Action<Action<T1, T2>> subscribe;
        private readonly Action<Action<T1, T2>> unsubscribe;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineSignal{T1,T2}"/> class with subscription delegates.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public InlineSignal(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
        {
            this.subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
            this.unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));
        }
    }

    /// <summary>
    /// A base implementation of a reactive source with three parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    /// <typeparam name="T3">The type of the third emitted value.</typeparam>
    [Serializable]
    public class InlineSignal<T1, T2, T3> : ISignal<T1, T2, T3>
    {
        public event Action<T1, T2, T3> OnEvent
        {
            add => this.subscribe.Invoke(value);
            remove => this.unsubscribe.Invoke(value);
        }

        private readonly Action<Action<T1, T2, T3>> subscribe;
        private readonly Action<Action<T1, T2, T3>> unsubscribe;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineSignal{T1,T2,T3}"/> class with subscription delegates.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public InlineSignal(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
        {
            this.subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
            this.unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));
        }
    }

    /// <summary>
    /// A base implementation of a reactive source with four parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    /// <typeparam name="T3">The type of the third emitted value.</typeparam>
    /// <typeparam name="T4">The type of the fourth emitted value.</typeparam>
    [Serializable]
    public class InlineSignal<T1, T2, T3, T4> : ISignal<T1, T2, T3, T4>
    {
        public event Action<T1, T2, T3, T4> OnEvent
        {
            add => this.subscribe.Invoke(value);
            remove => this.unsubscribe.Invoke(value);
        }

        private readonly Action<Action<T1, T2, T3, T4>> subscribe;
        private readonly Action<Action<T1, T2, T3, T4>> unsubscribe;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineSignal{T1,T2,T3,T4}"/> class with subscription delegates.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public InlineSignal(Action<Action<T1, T2, T3, T4>> subscribe, Action<Action<T1, T2, T3, T4>> unsubscribe)
        {
            this.subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
            this.unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));
        }
    }
}