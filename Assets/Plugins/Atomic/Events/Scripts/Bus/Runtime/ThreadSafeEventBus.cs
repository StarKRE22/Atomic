using System;
using System.Collections.Concurrent;
using UnityEngine;

namespace Atomic.Events
{
    public class ThreadSafeEventBus : IEventBus
    {
        private readonly IEventBus _inner;
        private readonly ConcurrentQueue<Action> _queue = new();

        public ThreadSafeEventBus()
        {
            _inner = new EventBus();
        }
        
        public ThreadSafeEventBus(IEventBus inner)
        {
            _inner = inner;
        }

        public Subscription Subscribe(int key, Action action) =>
            _inner.Subscribe(key, action);

        public Subscription<T> Subscribe<T>(int key, Action<T> action) =>
            _inner.Subscribe(key, action);

        public Subscription<T1, T2> Subscribe<T1, T2>(int key, Action<T1, T2> action) =>
            _inner.Subscribe(key, action);

        public Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action) =>
            _inner.Subscribe(key, action);

        public Subscription SubscribeUnsafe(int key, Action action) =>
            _inner.SubscribeUnsafe(key, action);

        public Subscription<T> SubscribeUnsafe<T>(int key, Action<T> action) =>
            _inner.SubscribeUnsafe(key, action);

        public Subscription<T1, T2> SubscribeUnsafe<T1, T2>(int key, Action<T1, T2> action) =>
            _inner.SubscribeUnsafe(key, action);

        public Subscription<T1, T2, T3> SubscribeUnsafe<T1, T2, T3>(int key, Action<T1, T2, T3> action) =>
            _inner.SubscribeUnsafe(key, action);

        public bool IsSubscribed(int key) => _inner.IsSubscribed(key);
        
        public bool Dispose(int key) => _inner.Dispose(key);

        public void Unsubscribe(int key, Action action) =>
            _inner.Unsubscribe(key, action);

        public void Unsubscribe<T>(int key, Action<T> action) =>
            _inner.Unsubscribe(key, action);

        public void Unsubscribe<T1, T2>(int key, Action<T1, T2> action) =>
            _inner.Unsubscribe(key, action);

        public void Unsubscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action) =>
            _inner.Unsubscribe(key, action);

        public void UnsubscribeUnsafe(int key, Action action) =>
            _inner.UnsubscribeUnsafe(key, action);

        public void UnsubscribeUnsafe<T>(int key, Action<T> action) =>
            _inner.UnsubscribeUnsafe(key, action);

        public void UnsubscribeUnsafe<T1, T2>(int key, Action<T1, T2> action) =>
            _inner.UnsubscribeUnsafe(key, action);

        public void UnsubscribeUnsafe<T1, T2, T3>(int key, Action<T1, T2, T3> action) =>
            _inner.UnsubscribeUnsafe(key, action);

        // Background Thread
        public void Invoke(int key) => _queue.Enqueue(() => _inner.Invoke(key));
        public void Invoke<T>(int key, T arg) => _queue.Enqueue(() => _inner.Invoke(key, arg));
        public void Invoke<T1, T2>(int key, T1 arg1, T2 arg2) => _queue.Enqueue(() => _inner.Invoke(key, arg1, arg2));
        public void Invoke<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3) => _queue.Enqueue(() => _inner.Invoke(key, arg1, arg2, arg3));

        public void InvokeUnsafe(int key) => _queue.Enqueue(() => _inner.InvokeUnsafe(key));
        public void InvokeUnsafe<T>(int key, T arg) => _queue.Enqueue(() => _inner.InvokeUnsafe(key, arg));
        public void InvokeUnsafe<T1, T2>(int key, T1 arg1, T2 arg2) => _queue.Enqueue(() => _inner.InvokeUnsafe(key, arg1, arg2));
        public void InvokeUnsafe<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3) => _queue.Enqueue(() => _inner.InvokeUnsafe(key, arg1, arg2, arg3));

        // Main Thread
        public void Flush()
        {
            if (_queue.Count == 0)
                return;
            
            while (_queue.TryDequeue(out Action action))
            {
                try
                {
                    action.Invoke();
                }
                catch (Exception e)
                {
                    Debug.LogException(e);
                }
            }
        }

        public void Dispose()
        {
            _inner.Dispose();
            _queue.Clear();
        }
    }
}