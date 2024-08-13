using System;

namespace Atomic.Elements
{
    public sealed class Subscription : IDisposable
    {
        private readonly IReactive observable;
        private readonly System.Action action;

        public Subscription(IReactive observable, System.Action action)
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
        private readonly System.Action<T> action;

        public Subscription(IReactive<T> observable, System.Action<T> action)
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
        private readonly System.Action<T1, T2> action;

        public Subscription(IReactive<T1, T2> observable, System.Action<T1, T2> action)
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
        private readonly System.Action<T1, T2, T3> action;

        public Subscription(IReactive<T1, T2, T3> observable, System.Action<T1, T2, T3> action)
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