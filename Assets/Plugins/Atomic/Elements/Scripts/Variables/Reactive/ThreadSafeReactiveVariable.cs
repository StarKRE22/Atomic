using System;
using System.Collections.Generic;
using System.Threading;
// ReSharper disable RedundantAssignment
// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace Atomic.Elements
{
    public class ThreadSafeReactiveVariable<T> : IReactiveVariable<T>, IDisposable, MainThreadDispatcher.IFlushable
    {
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer<T>.Default;
        private readonly object _lock = new();

        public event Action<T> OnEvent;

        private T _value;

        public T Value
        {
            get
            {
                
                lock (_lock)
                {
                    
                    return _value; // Single thread
                }
            }
            set
            {
                bool changed = false;

                // Multi threads
                // Thread 1
                // Thread 2
                // Thread 3

                lock (_lock)
                {
                    if (s_comparer.Equals(_value, value))
                        return;

                    _value = value;
                    changed = true;
                }

                if (changed)
                    MainThreadDispatcher.MarkDirty(this);
            }
        }

        public ThreadSafeReactiveVariable() => _value = default;

        public ThreadSafeReactiveVariable(T value) => _value = value;

        void MainThreadDispatcher.IFlushable.Flush()
        {
            T value;
            lock (_lock)
            {
                value = _value;
            }

            Action<T> handler = this.OnEvent;
            handler?.Invoke(value);
        }

        public void Dispose()
        {
            Interlocked.Exchange(ref OnEvent, null);
        }

        public override string ToString()
        {
            lock (_lock)
                return _value?.ToString();
        }
    }
}