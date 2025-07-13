using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    public class ReactiveProxyVariable<T> : IReactiveVariable<T>
    {
        public event Action<T> OnValueChanged
        {
            add => this.subscribe.Invoke(value);
            remove => this.unsubscribe.Invoke(value);
        }

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        public T Value
        {
            get => this.getter != null ? this.getter.Invoke() : default;
            set => this.setter?.Invoke(value);
        }
        
        private Func<T> getter;
        private Action<T> setter;

        private Action<Action<T>> subscribe;
        private Action<Action<T>> unsubscribe;

        public ReactiveProxyVariable()
        {
        }

        public ReactiveProxyVariable(
            Func<T> getter,
            Action<T> setter,
            Action<Action<T>> subscribe,
            Action<Action<T>> unsubscribe
        )
        {
            this.getter = getter;
            this.setter = setter;
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }

        public void Construct(
            Func<T> getter,
            Action<T> setter,
            Action<Action<T>> subscribe,
            Action<Action<T>> unsubscribe
        )
        {
            this.getter = getter;
            this.setter = setter;
            this.subscribe = subscribe;
            this.unsubscribe = unsubscribe;
        }
        
        public void Subscribe(Action<T> action) => this.subscribe.Invoke(action);

        public void Unsubscribe(Action<T> action) => this.unsubscribe.Invoke(action);

        public override string ToString() => this.Value.ToString();
    }
}