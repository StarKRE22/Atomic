using System;
using System.Threading;

namespace Atomic.Elements
{
    /// <summary>
    /// Thread-safe implementation of <see cref="IEvent"/>.
    /// </summary>
    /// <remarks>
    /// Allows the event to be raised safely from any thread. Calls to
    /// <see cref="Invoke"/> are deferred, and subscribers are notified on the
    /// main thread through <see cref="MainThreadDispatcher"/>.
    /// </remarks>
    public sealed class ThreadSafeEvent : IEvent, IDisposable, MainThreadDispatcher.IFlushable
    {
        /// <inheritdoc/>
        public event Action OnEvent;
    
        /// <inheritdoc/>
        public void Invoke()
        {
            MainThreadDispatcher.MarkDirty(this);
        }
    
        /// <summary>
        /// Releases all event subscribers.
        /// </summary>
        public void Dispose()
        {
            Interlocked.Exchange(ref OnEvent, null);
        }
    
        void MainThreadDispatcher.IFlushable.Flush()
        {
            Action handler = OnEvent;
            handler?.Invoke();
        }
    }
    
    /// <summary>
    /// Thread-safe implementation of <see cref="IEvent{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the event argument.</typeparam>
    /// <remarks>
    /// Allows the event to be raised safely from any thread. Calls to
    /// <see cref="Invoke(T)"/> are deferred, and subscribers are notified on the
    /// main thread through <see cref="MainThreadDispatcher"/>.
    /// </remarks>
    public sealed class ThreadSafeEvent<T> : IEvent<T>, IDisposable, MainThreadDispatcher.IFlushable
    {
        /// <inheritdoc/>
        public event Action<T> OnEvent;
    
        private readonly object _lock = new();
        private bool _hasValue;
        private T _value;
    
        /// <inheritdoc/>
        public void Invoke(T value)
        {
            lock (_lock)
            {
                _value = value;
                _hasValue = true;
            }
    
            MainThreadDispatcher.MarkDirty(this);
        }
    
        /// <summary>
        /// Releases all event subscribers.
        /// </summary>
        public void Dispose()
        {
            Interlocked.Exchange(ref OnEvent, null);
        }
    
        void MainThreadDispatcher.IFlushable.Flush()
        {
            T value;
    
            lock (_lock)
            {
                if (!_hasValue)
                    return;
    
                value = _value;
                _hasValue = false;
            }
    
            Action<T> handler = OnEvent;
            handler?.Invoke(value);
        }
    }
    
    /// <summary>
    /// Thread-safe implementation of <see cref="IEvent{T1, T2}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first event argument.</typeparam>
    /// <typeparam name="T2">The type of the second event argument.</typeparam>
    /// <remarks>
    /// Allows the event to be raised safely from any thread. Calls to
    /// <see cref="Invoke(T1, T2)"/> are deferred, and subscribers are notified on
    /// the main thread through <see cref="MainThreadDispatcher"/>.
    /// </remarks>
    public sealed class ThreadSafeEvent<T1, T2> : IEvent<T1, T2>, IDisposable, MainThreadDispatcher.IFlushable
    {
        /// <inheritdoc/>
        public event Action<T1, T2> OnEvent;
    
        private readonly object _lock = new();
    
        private T1 _v1;
        private T2 _v2;
        private bool _hasValue;
    
        /// <inheritdoc/>
        public void Invoke(T1 v1, T2 v2)
        {
            lock (_lock)
            {
                _v1 = v1;
                _v2 = v2;
                _hasValue = true;
            }
    
            MainThreadDispatcher.MarkDirty(this);
        }
    
        /// <summary>
        /// Releases all event subscribers.
        /// </summary>
        public void Dispose()
        {
            Interlocked.Exchange(ref OnEvent, null);
        }
    
        void MainThreadDispatcher.IFlushable.Flush()
        {
            T1 v1;
            T2 v2;
    
            lock (_lock)
            {
                if (!_hasValue)
                    return;
    
                v1 = _v1;
                v2 = _v2;
                _hasValue = false;
            }
    
            Action<T1, T2> handler = OnEvent;
            handler?.Invoke(v1, v2);
        }
    }
    
    /// <summary>
    /// Thread-safe implementation of <see cref="IEvent{T1, T2, T3}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first event argument.</typeparam>
    /// <typeparam name="T2">The type of the second event argument.</typeparam>
    /// <typeparam name="T3">The type of the third event argument.</typeparam>
    /// <remarks>
    /// Allows the event to be raised safely from any thread. Calls to
    /// <see cref="Invoke(T1, T2, T3)"/> are deferred, and subscribers are notified
    /// on the main thread through <see cref="MainThreadDispatcher"/>.
    /// </remarks>
    public sealed class ThreadSafeEvent<T1, T2, T3> : IEvent<T1, T2, T3>, IDisposable, MainThreadDispatcher.IFlushable
    {
        /// <inheritdoc/>
        public event Action<T1, T2, T3> OnEvent;
    
        private readonly object _lock = new();
    
        private T1 _v1;
        private T2 _v2;
        private T3 _v3;
        private bool _hasValue;
    
        /// <inheritdoc/>
        public void Invoke(T1 v1, T2 v2, T3 v3)
        {
            lock (_lock)
            {
                _v1 = v1;
                _v2 = v2;
                _v3 = v3;
                _hasValue = true;
            }
    
            MainThreadDispatcher.MarkDirty(this);
        }
    
        /// <summary>
        /// Releases all event subscribers.
        /// </summary>
        public void Dispose()
        {
            Interlocked.Exchange(ref OnEvent, null);
        }
    
        void MainThreadDispatcher.IFlushable.Flush()
        {
            T1 v1;
            T2 v2;
            T3 v3;
    
            lock (_lock)
            {
                if (!_hasValue)
                    return;
    
                v1 = _v1;
                v2 = _v2;
                v3 = _v3;
                _hasValue = false;
            }
    
            Action<T1, T2, T3> handler = OnEvent;
            handler?.Invoke(v1, v2, v3);
        }
    }
    
    /// <summary>
    /// Thread-safe implementation of <see cref="IEvent{T1, T2, T3, T4}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first event argument.</typeparam>
    /// <typeparam name="T2">The type of the second event argument.</typeparam>
    /// <typeparam name="T3">The type of the third event argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth event argument.</typeparam>
    /// <remarks>
    /// Allows the event to be raised safely from any thread. Calls to
    /// <see cref="Invoke(T1, T2, T3, T4)"/> are deferred, and subscribers are
    /// notified on the main thread through <see cref="MainThreadDispatcher"/>.
    /// </remarks>
    public sealed class ThreadSafeEvent<T1, T2, T3, T4> : IEvent<T1, T2, T3, T4>, IDisposable,
        MainThreadDispatcher.IFlushable
    {
        /// <inheritdoc/>
        public event Action<T1, T2, T3, T4> OnEvent;
    
        private readonly object _lock = new();
    
        private T1 _v1;
        private T2 _v2;
        private T3 _v3;
        private T4 _v4;
        private bool _hasValue;
    
        /// <inheritdoc/>
        public void Invoke(T1 v1, T2 v2, T3 v3, T4 v4)
        {
            lock (_lock)
            {
                _v1 = v1;
                _v2 = v2;
                _v3 = v3;
                _v4 = v4;
                _hasValue = true;
            }
    
            MainThreadDispatcher.MarkDirty(this);
        }
    
        /// <summary>
        /// Releases all event subscribers.
        /// </summary>
        public void Dispose()
        {
            Interlocked.Exchange(ref OnEvent, null);
        }
    
        void MainThreadDispatcher.IFlushable.Flush()
        {
            T1 v1;
            T2 v2;
            T3 v3;
            T4 v4;
    
            lock (_lock)
            {
                if (!_hasValue)
                    return;
    
                v1 = _v1;
                v2 = _v2;
                v3 = _v3;
                v4 = _v4;
                _hasValue = false;
            }
    
            Action<T1, T2, T3, T4> handler = OnEvent;
            handler?.Invoke(v1, v2, v3, v4);
        }
    }
}
