using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    public class ReactiveProxyVariable<T> : IReactiveVariable<T>
    {
        public event System.Action<T> OnValueChanged
        {
            add { this.subscribe.Invoke(value); }
            remove { this.unsubscribe.Invoke(value); }
        }

        private Func<T> getter;
        private System.Action<T> setter;

        private System.Action<System.Action<T>> subscribe;
        private System.Action<System.Action<T>> unsubscribe;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        public T Value
        {
            get { return this.getter != null ? this.getter.Invoke() : default; }
            set { this.setter?.Invoke(value); }
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

        public ReactiveProxyVariable()
        {
        }

        public ReactiveProxyVariable(
            Func<T> getter,
            System.Action<T> setter,
            System.Action<System.Action<T>> subscribe,
            System.Action<System.Action<T>> unsubscribe
        )
        {
            this.getter = getter;
            this.setter = setter;
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Compose(
            Func<T> getter,
            System.Action<T> setter,
            System.Action<System.Action<T>> subscribe,
            System.Action<System.Action<T>> unsubscribe
        )
        {
            this.getter = getter;
            this.setter = setter;
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }
    }
}