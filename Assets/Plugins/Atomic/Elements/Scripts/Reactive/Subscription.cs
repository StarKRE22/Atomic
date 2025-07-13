using System;

namespace Atomic.Elements
{
    public sealed class Subscription : IDisposable
    {
        private readonly IReactive observable;
        private readonly Action action;

        public Subscription(IReactive observable, Action action)
        {
            this.observable = observable;
            this.action = action;
            this.observable.Subscribe(action);
        }

        public void Dispose()
        {
            this.observable.Unsubscribe(this.action);
        }
    }

    public sealed class Subscription<T> : IDisposable
    {
        private readonly IReactive<T> observable;
        private readonly Action<T> action;

        public Subscription(IReactive<T> observable, Action<T> action)
        {
            this.observable = observable;
            this.action = action;
            this.observable.Subscribe(action);
        }

        public void Dispose()
        {
            this.observable.Unsubscribe(this.action);
        }
    }
    
    public sealed class Subscription<T1, T2> : IDisposable
    {
        private readonly IReactive<T1, T2> observable;
        private readonly Action<T1, T2> action;

        public Subscription(IReactive<T1, T2> observable, Action<T1, T2> action)
        {
            this.observable = observable;
            this.action = action;
            this.observable.Subscribe(action);
        }

        public void Dispose()
        {
            this.observable.Unsubscribe(this.action);
        }
    }
    
    public sealed class Subscription<T1, T2, T3> : IDisposable
    {
        private readonly IReactive<T1, T2, T3> observable;
        private readonly Action<T1, T2, T3> action;

        public Subscription(IReactive<T1, T2, T3> observable, Action<T1, T2, T3> action)
        {
            this.observable = observable;
            this.action = action;
            this.observable.Subscribe(action);
        }

        public void Dispose()
        {
            this.observable.Unsubscribe(this.action);
        }
    }
}