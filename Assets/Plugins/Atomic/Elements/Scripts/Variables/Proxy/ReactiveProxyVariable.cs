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
        
        public Subscription<T> Subscribe(Action<T> action)
        {
            this.subscribe.Invoke(action);
            return new Subscription<T>(this, action);
        }

        public void Unsubscribe(Action<T> action) => this.unsubscribe.Invoke(action);

        public override string ToString() => this.Value.ToString();
        
        public static Builder StartBuild() => new();

        public struct Builder
        {
            private Func<T> _getter;
            private Action<T> _setter;
            private Action<Action<T>> _subscribe;
            private Action<Action<T>> _unsubscribe;

            public Builder WithGetter(Func<T> getter)
            {
                _getter = getter ?? throw new ArgumentNullException(nameof(getter));
                return this;
            }

            public Builder WithSetter(Action<T> setter)
            {
                _setter = setter ?? throw new ArgumentNullException(nameof(setter));
                return this;
            }
            
            public Builder WithSubscribe(Action<Action<T>> subscribe)
            {
                _subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
                return this;
            }

            public Builder WithUnsubscribe(Action<Action<T>> unsubscribe)
            {
                _unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));
                return this;
            }

            public ReactiveProxyVariable<T> Build()
            {
                if (_getter == null)
                    throw new InvalidOperationException("Getter must be provided.");
                
                if (_setter == null)
                    throw new InvalidOperationException("Setter must be provided.");

                if (_subscribe == null)
                    throw new InvalidOperationException("Subscribe must be provided.");

                if (_unsubscribe == null)
                    throw new InvalidOperationException("Unsubscribe must be provided.");

                return new ReactiveProxyVariable<T>(_getter, _setter, _subscribe, _unsubscribe);
            }
        }
    }
}