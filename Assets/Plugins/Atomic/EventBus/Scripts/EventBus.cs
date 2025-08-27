using System;
using System.Collections.Generic;

namespace Atomic.Events
{
    public sealed class EventBus : IEventBus
    {
        internal IReadOnlyDictionary<int, Delegate> Events => _events;

        private readonly Dictionary<int, Delegate> _events = new();

        #region Subscribe

        public Subscription Subscribe(int key, Action action)
        {
            if (_events.TryGetValue(key, out Delegate del))
                action = (Action) del + action;

            _events[key] = action;
            return new Subscription(this, key, action);
        }

        public Subscription<T> Subscribe<T>(int key, Action<T> action)
        {
            if (_events.TryGetValue(key, out Delegate del))
                action = (Action<T>) del + action;

            _events[key] = action;
            return new Subscription<T>(this, key, action);
        }

        public Subscription<T1, T2> Subscribe<T1, T2>(int key, Action<T1, T2> action)
        {
            if (_events.TryGetValue(key, out Delegate del))
                action = (Action<T1, T2>) del + action;

            _events[key] = action;
            return new Subscription<T1, T2>(this, key, action);
        }

        public Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            if (_events.TryGetValue(key, out Delegate del))
                action = (Action<T1, T2, T3>) del + action;

            _events[key] = action;
            return new Subscription<T1, T2, T3>(this, key, action);
        }

        public bool IsSubscribed(int key)
        {
            return _events.ContainsKey(key);
        }

        #endregion

        #region Unsubscribe

        public void Unsubscribe(int key, Action action)
        {
            if (!_events.TryGetValue(key, out Delegate del)) 
                return;
            
            del = (Action) del - action;
            if (del == null)
                _events.Remove(key);
            else
                _events[key] = del;
        }

        public void Unsubscribe<T>(int key, Action<T> action)
        {
            if (!_events.TryGetValue(key, out Delegate del)) 
                return;
            
            del = (Action<T>) del - action;
            if (del == null)
                _events.Remove(key);
            else
                _events[key] = del;
        }

        public void Unsubscribe<T1, T2>(int key, Action<T1, T2> action)
        {
            if (!_events.TryGetValue(key, out Delegate del)) 
                return;
            
            del = (Action<T1, T2>) del - action;
            if (del == null)
                _events.Remove(key);
            else
                _events[key] = del;
        }

        public void Unsubscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            if (!_events.TryGetValue(key, out Delegate del)) 
                return;
            
            del = (Action<T1, T2, T3>) del - action;
            if (del == null)
                _events.Remove(key);
            else
                _events[key] = del;
        }

        #endregion

        #region Invoke

        public void Invoke(int key)
        {
            if (_events.TryGetValue(key, out Delegate del))
                ((Action) del).Invoke();
        }

        public void Invoke<T>(int key, T arg)
        {
            if (_events.TryGetValue(key, out Delegate del))
                ((Action<T>) del).Invoke(arg);
        }

        public void Invoke<T1, T2>(int key, T1 arg1, T2 arg2)
        {
            if (_events.TryGetValue(key, out Delegate del))
                ((Action<T1, T2>) del).Invoke(arg1, arg2);
        }

        public void Invoke<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_events.TryGetValue(key, out Delegate del))
                ((Action<T1, T2, T3>) del).Invoke(arg1, arg2, arg3);
        }

        #endregion

        public void Dispose() => _events.Clear();

        public bool Dispose(int key) => _events.Remove(key);
    }
}