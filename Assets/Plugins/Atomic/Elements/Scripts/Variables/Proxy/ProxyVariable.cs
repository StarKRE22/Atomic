using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    ///Provides read-write interface to a specified source
    [Serializable]
    public class ProxyVariable<T> : IVariable<T>
    {
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public T Value
        {
            get { return this.getter != null ? this.getter.Invoke() : default; }
            set { this.setter?.Invoke(value); }
        }

        private Func<T> getter;
        private Action<T> setter;

        public ProxyVariable()
        {
        }

        public ProxyVariable(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public void Construct(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        public override string ToString() => this.Value.ToString();

        public static Builder StartBuild() => new();

        public struct Builder
        {
            private Func<T> _getter;
            private Action<T> _setter;

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

            public ProxyVariable<T> Build()
            {
                if (_getter == null)
                    throw new InvalidOperationException("Getter must be provided.");
                if (_setter == null)
                    throw new InvalidOperationException("Setter must be provided.");

                return new ProxyVariable<T>(_getter, _setter);
            }
        }
    }
}