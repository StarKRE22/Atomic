using System;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    public sealed class ProxyValue<T> : IValue<T>
    {
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        public T Value
        {
            get { return this.func != null ? this.func.Invoke() : default; }
        }

        private Func<T> func;

        public ProxyValue()
        {
        }

        public ProxyValue(Func<T> func)
        {
            this.func = func;
        }

        public static implicit operator ProxyValue<T>(Func<T> value)
        {
            return new ProxyValue<T>(value);
        }

        public void Compose(Func<T> func)
        {
            this.func = func;
        }
    }
}