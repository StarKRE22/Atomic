using System;

namespace Atomic.Elements
{
    public readonly struct Subscription
    {
        private readonly IReactive reactive;
        private readonly Action action;

        public static Subscription Create(in IReactive reactive, in Action action)
        {
            if (reactive == null)
                throw new ArgumentNullException(nameof(reactive));

            if (action == null)
                throw new ArgumentNullException(nameof(action));

            reactive.Subscribe(action);
            return new Subscription(reactive, action);
        }

        private Subscription(in IReactive reactive, in Action action)
        {
            this.reactive = reactive;
            this.action = action;
        }

        public void Dispose()
        {
            this.reactive.Unsubscribe(this.action);
        }
    }

    public readonly struct Subscription<T> : IDisposable
    {
        private readonly IReactive<T> reactive;
        private readonly Action<T> action;

        public Subscription(IReactive<T> reactive, Action<T> action)
        {
            this.reactive = reactive;
            this.action = action;
            this.reactive.Subscribe(action);
        }

        public void Dispose()
        {
            this.reactive.Unsubscribe(this.action);
        }
    }

    public readonly struct Subscription<T1, T2> : IDisposable
    {
        private readonly IReactive<T1, T2> reactive;
        private readonly Action<T1, T2> action;

        public Subscription(IReactive<T1, T2> reactive, Action<T1, T2> action)
        {
            this.reactive = reactive;
            this.action = action;
            this.reactive.Subscribe(action);
        }

        public void Dispose()
        {
            this.reactive.Unsubscribe(this.action);
        }
    }

    public readonly struct Subscription<T1, T2, T3> : IDisposable
    {
        private readonly IReactive<T1, T2, T3> reactive;
        private readonly Action<T1, T2, T3> action;

        public Subscription(IReactive<T1, T2, T3> reactive, Action<T1, T2, T3> action)
        {
            this.reactive = reactive;
            this.action = action;
            this.reactive.Subscribe(action);
        }

        public void Dispose()
        {
            this.reactive.Unsubscribe(this.action);
        }
    }
}