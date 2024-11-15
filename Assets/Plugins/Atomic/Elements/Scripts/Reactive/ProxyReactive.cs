using System;

namespace Atomic.Elements
{
    /// Provides observable interface to a specified source.
    
    [Serializable]
    public class ProxyReactive : IReactive
    {
        private Action<Action> subscribe;
        private Action<Action> unsubscribe;

        public ProxyReactive()
        {
        }

        public ProxyReactive(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(Action<Action> subscribe, Action<Action> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public Action Subscribe(Action action)
        {
            this.subscribe?.Invoke(action);
            return action;
        }

        public void Unsubscribe(Action action)
        {
            this.unsubscribe?.Invoke(action);
        }
    }

    [Serializable]
    public sealed class ProxyReactive<T> : IReactive<T>
    {
        private Action<Action<T>> subscribe;
        private Action<Action<T>> unsubscribe;

        public ProxyReactive()
        {
        }

        public ProxyReactive(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(Action<Action<T>> subscribe, Action<Action<T>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public Action<T> Subscribe(Action<T> action)
        {
            this.subscribe.Invoke(action);
            return action;
        }

        public void Unsubscribe(Action<T> action)
        {
            this.unsubscribe.Invoke(action);
        }
    }
    
    [Serializable]
    public sealed class ProxyReactive<T1, T2> : IReactive<T1, T2>
    {
        private Action<Action<T1, T2>> subscribe;
        private Action<Action<T1, T2>> unsubscribe;

        public ProxyReactive()
        {
        }

        public ProxyReactive(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(Action<Action<T1, T2>> subscribe, Action<Action<T1, T2>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public Action<T1, T2> Subscribe(Action<T1, T2> action)
        {
            this.subscribe.Invoke(action);
            return action;
        }

        public void Unsubscribe(Action<T1, T2> action)
        {
            this.unsubscribe.Invoke(action);
        }
    }
    
    [Serializable]
    public sealed class ProxyReactive<T1, T2, T3> : IReactive<T1, T2, T3>
    {
        private Action<Action<T1, T2, T3>> subscribe;
        private Action<Action<T1, T2, T3>> unsubscribe;

        public ProxyReactive()
        {
        }

        public ProxyReactive(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(Action<Action<T1, T2, T3>> subscribe, Action<Action<T1, T2, T3>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public Action<T1, T2, T3> Subscribe(Action<T1, T2, T3> action)
        {
            this.subscribe.Invoke(action);
            return action;
        }

        public void Unsubscribe(Action<T1, T2, T3> action)
        {
            this.unsubscribe.Invoke(action);
        }
    }
}
