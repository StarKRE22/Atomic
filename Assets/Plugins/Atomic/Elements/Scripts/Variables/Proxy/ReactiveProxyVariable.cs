using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive proxy variable that delegates get, set, subscribe, and unsubscribe operations
    /// to external functions. Useful for binding external data sources or event systems.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public class ReactiveProxyVariable<T> : IReactiveVariable<T>
    {
        /// <summary>
        /// Subscribes or unsubscribes to value change notifications using delegated handlers.
        /// </summary>
        public event Action<T> OnValueChanged
        {
            add => this.subscribe.Invoke(value);
            remove => this.unsubscribe.Invoke(value);
        }

        /// <summary>
        /// Gets or sets the current value using delegated getter and setter.
        /// Returns default if getter is not assigned.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public T Value
        {
            get => this.getter.Invoke();
            set => this.setter?.Invoke(value);
        }

        private readonly Func<T> getter;
        private readonly Action<T> setter;
        private readonly Action<Action<T>> subscribe;
        private readonly Action<Action<T>> unsubscribe;

        /// <summary>
        /// Initializes an empty instance of <see cref="ReactiveProxyVariable{T}"/>.
        /// </summary>
        public ReactiveProxyVariable()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="ReactiveProxyVariable{T}"/> with delegates.
        /// </summary>
        /// <param name="getter">Function to retrieve the current value.</param>
        /// <param name="setter">Action to assign a new value.</param>
        /// <param name="subscribe">Action to handle subscription.</param>
        /// <param name="unsubscribe">Action to handle unsubscription.</param>
        public ReactiveProxyVariable(
            Func<T> getter,
            Action<T> setter,
            Action<Action<T>> subscribe,
            Action<Action<T>> unsubscribe
        )
        {
            this.getter = getter ?? throw new ArgumentNullException(nameof(getter));
            this.setter = setter ?? throw new ArgumentNullException(nameof(setter));
            this.subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
            this.unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));
        }

        /// <summary>
        /// Subscribes to value changes and returns a subscription handle.
        /// </summary>
        /// <param name="action">Callback to invoke when value changes.</param>
        public Subscription<T> Subscribe(Action<T> action)
        {
            this.subscribe.Invoke(action);
            return new Subscription<T>(this, action);
        }

        /// <summary>
        /// Unsubscribes a listener from value changes.
        /// </summary>
        /// <param name="action">Callback to remove.</param>
        public void Unsubscribe(Action<T> action) => this.unsubscribe.Invoke(action);

        /// <summary>
        /// Returns the string representation of the current value.
        /// </summary>
        public override string ToString() => this.Value?.ToString();

        /// <summary>
        /// Begins a fluent builder for constructing a <see cref="ReactiveProxyVariable{T}"/>.
        /// </summary>
        public static Builder StartBuild() => new();

        /// <summary>
        /// Fluent builder for creating <see cref="ReactiveProxyVariable{T}"/> instances.
        /// </summary>
        public struct Builder
        {
            private Func<T> _getter;
            private Action<T> _setter;
            private Action<Action<T>> _subscribe;
            private Action<Action<T>> _unsubscribe;

            /// <summary>
            /// Assigns a getter function.
            /// </summary>
            public Builder WithGetter(Func<T> getter)
            {
                _getter = getter ?? throw new ArgumentNullException(nameof(getter));
                return this;
            }

            /// <summary>
            /// Assigns a setter action.
            /// </summary>
            public Builder WithSetter(Action<T> setter)
            {
                _setter = setter ?? throw new ArgumentNullException(nameof(setter));
                return this;
            }

            /// <summary>
            /// Assigns a subscription handler.
            /// </summary>
            public Builder WithSubscribe(Action<Action<T>> subscribe)
            {
                _subscribe = subscribe ?? throw new ArgumentNullException(nameof(subscribe));
                return this;
            }

            /// <summary>
            /// Assigns an unsubscription handler.
            /// </summary>
            public Builder WithUnsubscribe(Action<Action<T>> unsubscribe)
            {
                _unsubscribe = unsubscribe ?? throw new ArgumentNullException(nameof(unsubscribe));
                return this;
            }

            /// <summary>
            /// Builds and returns the configured <see cref="ReactiveProxyVariable{T}"/>.
            /// </summary>
            public ReactiveProxyVariable<T> Build()
            {
                if (_getter == null) throw new InvalidOperationException("Getter must be provided.");
                if (_setter == null) throw new InvalidOperationException("Setter must be provided.");
                if (_subscribe == null) throw new InvalidOperationException("Subscribe must be provided.");
                if (_unsubscribe == null) throw new InvalidOperationException("Unsubscribe must be provided.");

                return new ReactiveProxyVariable<T>(_getter, _setter, _subscribe, _unsubscribe);
            }
        }
    }
}