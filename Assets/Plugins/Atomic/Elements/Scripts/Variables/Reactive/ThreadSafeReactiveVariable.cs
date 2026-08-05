using System;
using System.Collections.Generic;
using System.Threading;
// ReSharper disable RedundantAssignment
// ReSharper disable ConditionIsAlwaysTrueOrFalse

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a thread-safe reactive variable whose change notifications
    /// are dispatched on the main thread.
    /// </summary>
    /// <typeparam name="T">The type of the stored value.</typeparam>
    public class ThreadSafeReactiveVariable<T> :
        IReactiveVariable<T>,
        IDisposable,
        MainThreadDispatcher.IFlushable
    {
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer<T>.Default;
        private readonly object _lock = new();

        /// <summary>
        /// Occurs when the value has changed and the notification is dispatched
        /// on the main thread.
        /// </summary>
        public event Action<T> OnEvent;

        private T _value;

        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        /// <value>
        /// The stored value. Reading and writing are synchronized to ensure
        /// thread safety. Setting a new value schedules a notification on the
        /// main thread if the value has changed.
        /// </value>
        public T Value
        {
            get
            {
                lock (_lock)
                {
                    return _value;
                }
            }
            set
            {
                bool changed = false;

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

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ThreadSafeReactiveVariable{T}"/> class with the default
        /// value of <typeparamref name="T"/>.
        /// </summary>
        public ThreadSafeReactiveVariable() => _value = default;

        /// <summary>
        /// Initializes a new instance of the
        /// <see cref="ThreadSafeReactiveVariable{T}"/> class with the specified
        /// initial value.
        /// </summary>
        /// <param name="value">The initial value.</param>
        public ThreadSafeReactiveVariable(T value) => _value = value;

        /// <summary>
        /// Dispatches the current value to all subscribed listeners.
        /// </summary>
        /// <remarks>
        /// This method is intended to be called by
        /// <see cref="MainThreadDispatcher"/> on the main thread.
        /// </remarks>
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

        /// <summary>
        /// Releases all event subscriptions associated with this variable.
        /// </summary>
        public void Dispose()
        {
            Interlocked.Exchange(ref OnEvent, null);
        }

        /// <summary>
        /// Returns the string representation of the current value.
        /// </summary>
        /// <returns>
        /// The string representation of the stored value, or
        /// <see langword="null"/> if the value is <see langword="null"/>.
        /// </returns>
        public override string ToString()
        {
            lock (_lock)
                return _value?.ToString();
        }
    }
}
