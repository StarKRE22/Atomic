using System.Collections.Generic;

namespace Atomic.Elements
{
    public class ThreadSafeVariable<T> : IVariable<T>
    {
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer<T>.Default;
        private readonly object _lock = new();

        private T _value;

        public T Value
        {
            get
            {
                lock (_lock)
                    return _value;
            }
            set
            {
                lock (_lock)
                {
                    if (s_comparer.Equals(_value, value))
                        return;

                    _value = value;
                }
            }
        }

        public ThreadSafeVariable() => _value = default;

        public ThreadSafeVariable(T value) => _value = value;

        public override string ToString()
        {
            lock (_lock)
                return _value?.ToString();
        }
    }
}