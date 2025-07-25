using System;

namespace Atomic.Elements
{
    /// <summary>
    /// A base implementation of a non-generic reactive source.
    /// </summary>
    [Serializable]
    public class BasicSignal : ISignal
    {
        private Action<Action> subscribe;
        private Action<Action> unsubscribe;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicSignal"/> class.
        /// </summary>
        public BasicSignal()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicSignal"/> class with subscription delegates.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public BasicSignal(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        /// <summary>
        /// Sets the subscription and unsubscription actions after construction.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public void Construct(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        /// <summary>
        /// Subscribes to the reactive source.
        /// </summary>
        /// <param name="action">The action to invoke when the source changes.</param>
        /// <returns>A <see cref="Subscription"/> representing the subscription.</returns>
        public Subscription Subscribe(Action action)
        {
            this.subscribe?.Invoke(action);
            return new Subscription(this, action);
        }

        /// <summary>
        /// Unsubscribes the specified action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove.</param>
        public void Unsubscribe(Action action) => this.unsubscribe?.Invoke(action);
    }

    /// <summary>
    /// A base implementation of a generic reactive source with one value.
    /// </summary>
    /// <typeparam name="T">The type of value emitted by the reactive source.</typeparam>
    [Serializable]
    public class BasicSignal<T> : ISignal<T>
    {
        private Action<Action<T>> subscribe;
        private Action<Action<T>> unsubscribe;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicSignal{T}"/> class.
        /// </summary>
        public BasicSignal()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicSignal{T}"/> class with subscription delegates.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public BasicSignal(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        /// <summary>
        /// Sets the subscription and unsubscription actions after construction.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public void Сonstruct(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        /// <summary>
        /// Subscribes to the reactive source.
        /// </summary>
        /// <param name="action">The action to invoke with emitted values.</param>
        /// <returns>A <see cref="Subscription{T}"/> representing the subscription.</returns>
        public Subscription<T> Subscribe(Action<T> action)
        {
            this.subscribe.Invoke(action);
            return new Subscription<T>(this, action);
        }

        /// <summary>
        /// Unsubscribes the specified action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove.</param>
        public void Unsubscribe(Action<T> action)
        {
            this.unsubscribe.Invoke(action);
        }
    }

    /// <summary>
    /// A base implementation of a reactive source with two parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    [Serializable]
    public class BasicSignal<T1, T2> : ISignal<T1, T2>
    {
        private Action<Action<T1, T2>> subscribe;
        private Action<Action<T1, T2>> unsubscribe;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicSignal{T1,T2}"/> class.
        /// </summary>
        public BasicSignal()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicSignal{T1,T2}"/> class with subscription delegates.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public BasicSignal(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        /// <summary>
        /// Sets the subscription and unsubscription actions after construction.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public void Construct(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        /// <summary>
        /// Subscribes to the reactive source.
        /// </summary>
        /// <param name="action">The action to invoke with emitted values.</param>
        /// <returns>A <see cref="Subscription{T1, T2}"/> representing the subscription.</returns>
        public Subscription<T1, T2> Subscribe(Action<T1, T2> action)
        {
            this.subscribe.Invoke(action);
            return new Subscription<T1, T2>(this, action);
        }

        /// <summary>
        /// Unsubscribes the specified action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove.</param>
        public void Unsubscribe(Action<T1, T2> action) => this.unsubscribe.Invoke(action);
    }

    /// <summary>
    /// A base implementation of a reactive source with three parameters.
    /// </summary>
    /// <typeparam name="T1">The type of the first emitted value.</typeparam>
    /// <typeparam name="T2">The type of the second emitted value.</typeparam>
    /// <typeparam name="T3">The type of the third emitted value.</typeparam>
    [Serializable]
    public sealed class BasicSignal<T1, T2, T3> : ISignal<T1, T2, T3>
    {
        private Action<Action<T1, T2, T3>> subscribe;
        private Action<Action<T1, T2, T3>> unsubscribe;

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicSignal{T1,T2,T3}"/> class.
        /// </summary>
        public BasicSignal()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="BasicSignal{T1,T2,T3}"/> class with subscription delegates.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public BasicSignal(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        /// <summary>
        /// Sets the subscription and unsubscription actions after construction.
        /// </summary>
        /// <param name="subscribe">The action to handle subscription logic.</param>
        /// <param name="unsubscribe">The action to handle unsubscription logic.</param>
        public void Construct(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        /// <summary>
        /// Subscribes to the reactive source.
        /// </summary>
        /// <param name="action">The action to invoke with emitted values.</param>
        /// <returns>A <see cref="Subscription{T1, T2, T3}"/> representing the subscription.</returns>
        public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
        {
            this.subscribe.Invoke(action);
            return new Subscription<T1, T2, T3>(this, action);
        }

        /// <summary>
        /// Unsubscribes the specified action from the reactive source.
        /// </summary>
        /// <param name="action">The action to remove.</param>
        public void Unsubscribe(Action<T1, T2, T3> action) => this.unsubscribe.Invoke(action);
    }
}