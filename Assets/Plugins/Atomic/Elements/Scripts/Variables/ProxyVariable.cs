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

        public void Compose(Func<T> getter, Action<T> setter)
        {
            this.getter = getter;
            this.setter = setter;
        }
    }
}
