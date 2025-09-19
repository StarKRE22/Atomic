using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a parameterless event that supports subscription, invocation, and disposal.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class BaseEvent : IEvent, IDisposable
    {
        public event Action OnEvent;

        /// <summary>
        /// Subscribes a handler to the event.
        /// </summary>
        public Subscription Subscribe(Action action)
        {
            this.OnEvent += action;
            return new Subscription(this, action);
        }

        /// <summary>
        /// Unsubscribes a handler from the event.
        /// </summary>
        public void Unsubscribe(Action action) => this.OnEvent -= action;

        /// <summary>
        /// Invokes the event, notifying all subscribers.
        /// </summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke() => this.OnEvent?.Invoke();

        /// <summary>
        /// Disposes of all event subscriptions.
        /// </summary>
        public void Dispose() => this.OnEvent = null;
    }

    /// <summary>
    /// Represents an event with one argument that supports subscription, invocation, and disposal.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class BaseEvent<T> : IEvent<T>, IDisposable
    {
        public event Action<T> OnEvent;

        /// <summary>
        /// Subscribes a handler to the event.
        /// </summary>
        public Subscription<T> Subscribe(Action<T> action)
        {
            this.OnEvent += action;
            return new Subscription<T>(this, action);
        }

        /// <summary>
        /// Unsubscribes a handler from the event.
        /// </summary>
        public void Unsubscribe(Action<T> action) => this.OnEvent -= action;

        /// <summary>
        /// Invokes the event with the specified argument.
        /// </summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T arg) => this.OnEvent?.Invoke(arg);

        /// <summary>
        /// Disposes of all event subscriptions.
        /// </summary>
        public void Dispose() => this.OnEvent = null;
    }

    /// <summary>
    /// Represents an event with two arguments that supports subscription, invocation, and disposal.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class BaseEvent<T1, T2> : IEvent<T1, T2>, IDisposable
    {
        public event Action<T1, T2> OnEvent;

        /// <summary>
        /// Subscribes a handler to the event.
        /// </summary>
        public Subscription<T1, T2> Subscribe(Action<T1, T2> action)
        {
            this.OnEvent += action;
            return new Subscription<T1, T2>(this, action);
        }

        /// <summary>
        /// Unsubscribes a handler from the event.
        /// </summary>
        public void Unsubscribe(Action<T1, T2> action) => this.OnEvent -= action;

        /// <summary>
        /// Invokes the event with the specified arguments.
        /// </summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 arg1, T2 arg2) => this.OnEvent?.Invoke(arg1, arg2);

        /// <summary>
        /// Disposes of all event subscriptions.
        /// </summary>
        public void Dispose() => this.OnEvent = null;
    }

    /// <summary>
    /// Represents an event with three arguments that supports subscription, invocation, and disposal.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class BaseEvent<T1, T2, T3> : IEvent<T1, T2, T3>, IDisposable
    {
        public event Action<T1, T2, T3> OnEvent;

        /// <summary>
        /// Subscribes a handler to the event.
        /// </summary>
        public Subscription<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
        {
            this.OnEvent += action;
            return new Subscription<T1, T2, T3>(this, action);
        }

        /// <summary>
        /// Unsubscribes a handler from the event.
        /// </summary>
        public void Unsubscribe(Action<T1, T2, T3> action) => this.OnEvent -= action;

        /// <summary>
        /// Invokes the event with the specified arguments.
        /// </summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 arg1, T2 arg2, T3 arg3) => this.OnEvent?.Invoke(arg1, arg2, arg3);

        /// <summary>
        /// Disposes of all event subscriptions.
        /// </summary>
        public void Dispose() => this.OnEvent = null;
    }


    /// <summary>
    /// Represents an event with three arguments that supports subscription, invocation, and disposal.
    /// </summary>
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class BaseEvent<T1, T2, T3, T4> : IEvent<T1, T2, T3, T4>, IDisposable
    {
        public event Action<T1, T2, T3, T4> OnEvent;

        /// <summary>
        /// Subscribes a handler to the event.
        /// </summary>
        public Subscription<T1, T2, T3, T4> Subscribe(Action<T1, T2, T3, T4> action)
        {
            this.OnEvent += action;
            return new Subscription<T1, T2, T3, T4>(this, action);
        }

        /// <summary>
        /// Unsubscribes a handler from the event.
        /// </summary>
        public void Unsubscribe(Action<T1, T2, T3, T4> action) => this.OnEvent -= action;

        /// <summary>
        /// Invokes the event with the specified arguments.
        /// </summary>
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => this.OnEvent?.Invoke(arg1, arg2, arg3, arg4);

        /// <summary>
        /// Disposes of all event subscriptions.
        /// </summary>
        public void Dispose() => this.OnEvent = null;
    }
}