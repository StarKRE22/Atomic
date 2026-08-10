using System;

namespace Atomic.Events
{
    public class EventBus : IEventBus
    {
        internal EventTable Events => _events;

        private readonly EventTable _events = new();

        #region Subscribe

        public Subscription Subscribe(int key, Action action)
        {
            if (_events.TryGetEvent<Delegate>(key, out Delegate del))
                action = (Action) del + action;

            _events.SetEvent(key, action);
            return new Subscription(this, key, action);
        }

        public Subscription<T> Subscribe<T>(int key, Action<T> action)
        {
            if (_events.TryGetEvent<Delegate>(key, out Delegate del))
                action = (Action<T>) del + action;

            _events.SetEvent(key, action);
            return new Subscription<T>(this, key, action);
        }

        public Subscription<T1, T2> Subscribe<T1, T2>(int key, Action<T1, T2> action)
        {
            if (_events.TryGetEvent<Delegate>(key, out Delegate del))
                action = (Action<T1, T2>) del + action;

            _events.SetEvent(key, action);
            return new Subscription<T1, T2>(this, key, action);
        }

        public Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            if (_events.TryGetEvent<Delegate>(key, out Delegate del))
                action = (Action<T1, T2, T3>) del + action;

            _events.SetEvent(key, action);
            return new Subscription<T1, T2, T3>(this, key, action);
        }

        #endregion

        #region SubscribeUnsafe

        public Subscription SubscribeUnsafe(int key, Action action)
        {
            _events.AddEvent(key, action);
            return new Subscription(this, key, action);
        }

        public Subscription<T> SubscribeUnsafe<T>(int key, Action<T> action)
        {
            _events.AddEvent(key, action);
            return new Subscription<T>(this, key, action);
        }

        public Subscription<T1, T2> SubscribeUnsafe<T1, T2>(int key, Action<T1, T2> action)
        {
            _events.AddEvent(key, action);
            return new Subscription<T1, T2>(this, key, action);
        }

        public Subscription<T1, T2, T3> SubscribeUnsafe<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            _events.AddEvent(key, action);
            return new Subscription<T1, T2, T3>(this, key, action);
        }

        public bool IsSubscribed(int key)
        {
            return _events.ContainsEvent(key);
        }

        #endregion

        #region Unsubscribe

        public void Unsubscribe(int key, Action action)
        {
            if (!_events.TryGetEvent<Delegate>(key, out Delegate del)) 
                return;
            
            del = (Action) del - action;
            if (del == null)
                _events.RemoveEvent(key);
            else
                _events.SetEvent(key, del);
        }

        public void Unsubscribe<T>(int key, Action<T> action)
        {
            if (!_events.TryGetEvent<Delegate>(key, out Delegate del)) 
                return;
            
            del = (Action<T>) del - action;
            if (del == null)
                _events.RemoveEvent(key);
            else
                _events.SetEvent(key, del);
        }

        public void Unsubscribe<T1, T2>(int key, Action<T1, T2> action)
        {
            if (!_events.TryGetEvent<Delegate>(key, out Delegate del)) 
                return;
            
            del = (Action<T1, T2>) del - action;
            if (del == null)
                _events.RemoveEvent(key);
            else
                _events.SetEvent(key, del);
        }

        public void Unsubscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            if (!_events.TryGetEvent<Delegate>(key, out Delegate del)) 
                return;
            
            del = (Action<T1, T2, T3>) del - action;
            if (del == null)
                _events.RemoveEvent(key);
            else
                _events.SetEvent(key, del);
        }

        #endregion

        #region UnsubscribeUnsafe

        public void UnsubscribeUnsafe(int key, Action action)
        {
            Delegate del = _events.GetEventUnsafe<Delegate>(key);
            del = (Action) del - action;
            if (del == null)
                _events.RemoveEvent(key);
            else
                _events.SetEvent(key, del);
        }

        public void UnsubscribeUnsafe<T>(int key, Action<T> action)
        {
            Delegate del = _events.GetEventUnsafe<Delegate>(key);
            del = (Action<T>) del - action;
            if (del == null)
                _events.RemoveEvent(key);
            else
                _events.SetEvent(key, del);
        }

        public void UnsubscribeUnsafe<T1, T2>(int key, Action<T1, T2> action)
        {
            Delegate del = _events.GetEventUnsafe<Delegate>(key);
            del = (Action<T1, T2>) del - action;
            if (del == null)
                _events.RemoveEvent(key);
            else
                _events.SetEvent(key, del);
        }

        public void UnsubscribeUnsafe<T1, T2, T3>(int key, Action<T1, T2, T3> action)
        {
            Delegate del = _events.GetEventUnsafe<Delegate>(key);
            del = (Action<T1, T2, T3>) del - action;
            if (del == null)
                _events.RemoveEvent(key);
            else
                _events.SetEvent(key, del);
        }

        #endregion

        #region Invoke

        public void Invoke(int key)
        {
            if (_events.TryGetEvent<Action>(key, out Action del))
                del.Invoke();
        }

        public void Invoke<T>(int key, T arg)
        {
            if (_events.TryGetEvent<Action<T>>(key, out Action<T> del))
                del.Invoke(arg);
        }

        public void Invoke<T1, T2>(int key, T1 arg1, T2 arg2)
        {
            if (_events.TryGetEvent<Action<T1, T2>>(key, out Action<T1, T2> del))
                del.Invoke(arg1, arg2);
        }

        public void Invoke<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3)
        {
            if (_events.TryGetEvent<Action<T1, T2, T3>>(key, out Action<T1, T2, T3> del))
                del.Invoke(arg1, arg2, arg3);
        }

        #endregion

        #region InvokeUnsafe

        public void InvokeUnsafe(int key) =>
            _events.GetEventUnsafe<Action>(key).Invoke();

        public void InvokeUnsafe<T>(int key, T arg) =>
            _events.GetEventUnsafe<Action<T>>(key).Invoke(arg);

        public void InvokeUnsafe<T1, T2>(int key, T1 arg1, T2 arg2) =>
            _events.GetEventUnsafe<Action<T1, T2>>(key).Invoke(arg1, arg2);

        public void InvokeUnsafe<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3) =>
            _events.GetEventUnsafe<Action<T1, T2, T3>>(key).Invoke(arg1, arg2, arg3);

        #endregion

        public void Dispose() => _events.ClearEvents();

        public bool Dispose(int key) => _events.RemoveEvent(key);
    }
}