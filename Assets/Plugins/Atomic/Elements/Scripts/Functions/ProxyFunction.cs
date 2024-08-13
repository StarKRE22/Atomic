using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// Represents a function object.
    
    [Serializable]
    public class ProxyFunction<T> : IFunction<T>
    {
        private Func<T> func;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly]
#endif
        public T Value
        {
            get { return this.func != null ? this.func.Invoke() : default; }
        }

        public ProxyFunction()
        {
        }

        public ProxyFunction(Func<T> func)
        {
            this.func = func;
        }
        
        public static implicit operator ProxyFunction<T>(Func<T> value)
        {
            return new ProxyFunction<T>(value);
        }
        
        public void Compose(Func<T> func)
        {
            this.func = func;
        }

        public T Invoke()
        {
            return this.func != null ? this.func.Invoke() : default;
        }
    }

    [Serializable]
    public sealed class Function<T, R> : IFunction<T, R>
    {
        private Func<T, R> func;

        public Function()
        {
        }

        public Function(Func<T, R> func)
        {
            this.func = func;
        }
        
        public static implicit operator Function<T, R>(Func<T, R> value)
        {
            return new Function<T, R>(value);
        }
        
        public void Compose(Func<T, R> func)
        {
            this.func = func;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T args)
        {
            return this.func.Invoke(args);
        }
    }
    
    [Serializable]
    public sealed class Function<T1, T2, R> : IFunction<T1, T2, R>
    {
        private Func<T1, T2, R> func;

        public Function()
        {
        }

        public Function(Func<T1, T2, R> func)
        {
            this.func = func;
        }
        
        public static implicit operator Function<T1, T2, R>(Func<T1, T2, R> value)
        {
            return new Function<T1, T2, R>(value);
        }

        public void Compose(Func<T1, T2, R> func)
        {
            this.func = func;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T1 arg1, T2 arg2)
        {
            return this.func.Invoke(arg1, arg2);
        }
    }
}
