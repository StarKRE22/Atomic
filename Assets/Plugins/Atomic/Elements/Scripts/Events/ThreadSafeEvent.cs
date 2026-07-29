using System;
using System.Threading;

namespace Atomic.Elements
{
    public class ThreadSafeEvent : IEvent, IDisposable, 
        MainThreadDispatcher.IFlushable
    {
        public event Action OnEvent;

        public void Invoke()
        {
            MainThreadDispatcher.MarkDirty(this);
        }

        public void Dispose()
        {
            Interlocked.Exchange(ref OnEvent, null);
        }

        void MainThreadDispatcher.IFlushable.Flush()
        {
            Action handler = this.OnEvent;
            handler?.Invoke();
        }
    }
    
    public class ThreadSafeEvent<T> : IEvent<T>, IDisposable, 
        MainThreadDispatcher.IFlushable
    {
        public event Action<T> OnEvent;

        private readonly object _lock = new();
        private bool _hasValue;
        private T _value;

        public void Invoke(T value)
        {
            lock (_lock)
            {
                _value = value;
                _hasValue = true;
            }

            MainThreadDispatcher.MarkDirty(this);
        }

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

            var handler = OnEvent;
            handler?.Invoke(value);
        }
    }
    
    public class ThreadSafeEvent<T1, T2> : IEvent<T1, T2>, IDisposable, 
        MainThreadDispatcher.IFlushable
    {
        public event Action<T1, T2> OnEvent;

        private readonly object _lock = new();

        private T1 _v1;
        private T2 _v2;
        private bool _hasValue;

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

            var handler = OnEvent;
            handler?.Invoke(v1, v2);
        }
    }
    
    public class ThreadSafeEvent<T1, T2, T3> : IEvent<T1, T2, T3>, IDisposable, 
        MainThreadDispatcher.IFlushable
    {
        public event Action<T1, T2, T3> OnEvent;

        private readonly object _lock = new();

        private T1 _v1;
        private T2 _v2;
        private T3 _v3;
        private bool _hasValue;

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

            var handler = OnEvent;
            handler?.Invoke(v1, v2, v3);
        }
    }
    
    public class ThreadSafeEvent<T1, T2, T3, T4> : IEvent<T1, T2, T3, T4>, IDisposable, 
        MainThreadDispatcher.IFlushable
    {
        public event Action<T1, T2, T3, T4> OnEvent;

        private readonly object _lock = new();

        private T1 _v1;
        private T2 _v2;
        private T3 _v3;
        private T4 _v4;
        private bool _hasValue;

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

            var handler = OnEvent;
            handler?.Invoke(v1, v2, v3, v4);
        }
    }
}