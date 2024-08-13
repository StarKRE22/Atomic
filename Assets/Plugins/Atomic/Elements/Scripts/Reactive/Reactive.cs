using System;

namespace Atomic.Elements
{
    /// Provides observable interface to a specified source.
    
    [Serializable]
    public class Reactive : IReactive
    {
        private System.Action<System.Action> subscribe;
        private System.Action<System.Action> unsubscribe;

        public Reactive()
        {
        }

        public Reactive(System.Action<System.Action> subscribe, System.Action<System.Action> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(System.Action<System.Action> subscribe, System.Action<System.Action> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public System.Action Subscribe(System.Action action)
        {
            this.subscribe?.Invoke(action);
            return action;
        }

        public void Unsubscribe(System.Action action)
        {
            this.unsubscribe?.Invoke(action);
        }
    }

    [Serializable]
    public sealed class Reactive<T> : IReactive<T>
    {
        private System.Action<System.Action<T>> subscribe;
        private System.Action<System.Action<T>> unsubscribe;

        public Reactive()
        {
        }

        public Reactive(System.Action<System.Action<T>> subscribe, System.Action<System.Action<T>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(System.Action<System.Action<T>> subscribe, System.Action<System.Action<T>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public System.Action<T> Subscribe(System.Action<T> action)
        {
            this.subscribe.Invoke(action);
            return action;
        }

        public void Unsubscribe(System.Action<T> action)
        {
            this.unsubscribe.Invoke(action);
        }
    }
    
    [Serializable]
    public sealed class Reactive<T1, T2> : IReactive<T1, T2>
    {
        private System.Action<System.Action<T1, T2>> subscribe;
        private System.Action<System.Action<T1, T2>> unsubscribe;

        public Reactive()
        {
        }

        public Reactive(System.Action<System.Action<T1, T2>> subscribe, System.Action<System.Action<T1, T2>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(System.Action<System.Action<T1, T2>> subscribe, System.Action<System.Action<T1, T2>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public System.Action<T1, T2> Subscribe(System.Action<T1, T2> action)
        {
            this.subscribe.Invoke(action);
            return action;
        }

        public void Unsubscribe(System.Action<T1, T2> action)
        {
            this.unsubscribe.Invoke(action);
        }
    }
    
    [Serializable]
    public sealed class Reactive<T1, T2, T3> : IReactive<T1, T2, T3>
    {
        private System.Action<System.Action<T1, T2, T3>> subscribe;
        private System.Action<System.Action<T1, T2, T3>> unsubscribe;

        public Reactive()
        {
        }

        public Reactive(System.Action<System.Action<T1, T2, T3>> subscribe, System.Action<System.Action<T1, T2, T3>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(System.Action<System.Action<T1, T2, T3>> subscribe, System.Action<System.Action<T1, T2, T3>> unsubscribe)
        {
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public System.Action<T1, T2, T3> Subscribe(System.Action<T1, T2, T3> action)
        {
            this.subscribe.Invoke(action);
            return action;
        }

        public void Unsubscribe(System.Action<T1, T2, T3> action)
        {
            this.unsubscribe.Invoke(action);
        }
    }
}
