using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// Represents a function object.
    [Serializable]
    public class BaseFunction<T> : IValue<T>
    {
        private Func<T> func;

        public BaseFunction()
        {
        }

        public BaseFunction(Func<T> func) => this.func = func;

        public static implicit operator BaseFunction<T>(Func<T> value) => new(value);

        public T Value => this.func != null ? this.func.Invoke() : default;

        public void Construct(Func<T> func) => this.func = func;

#if ODIN_INSPECTOR
        [Button]
#endif
        public T Invoke() => this.func != null ? this.func.Invoke() : default;
    }

    [Serializable]
    public sealed class BaseFunction<T, R> : IFunction<T, R>
    {
        private Func<T, R> func;

        public BaseFunction()
        {
        }

        public BaseFunction(Func<T, R> func) => this.func = func;

        public static implicit operator BaseFunction<T, R>(Func<T, R> value) => new(value);

        public void Compose(Func<T, R> func) => this.func = func;

#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T args) => this.func.Invoke(args);
    }

    [Serializable]
    public sealed class BaseFunction<T1, T2, R> : IFunction<T1, T2, R>
    {
        private Func<T1, T2, R> func;

        public BaseFunction()
        {
        }

        public BaseFunction(Func<T1, T2, R> func) => this.func = func;

        public static implicit operator BaseFunction<T1, T2, R>(Func<T1, T2, R> value) => new(value);

        public void Construct(Func<T1, T2, R> func) => this.func = func;

#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T1 arg1, T2 arg2) => this.func.Invoke(arg1, arg2);
    }
}