using System;
using System.Collections.Generic;

namespace Atomic.Events
{
    public sealed class EventBus : IEventBus
    {
        //TODO: HASH TABLE & UNSAFE!
        private readonly Dictionary<int, IEvent> _events = new();

        public IReadOnlyCollection<int> DeclaredEvents => _events.Keys;

        #region Def

        public void Declare(in int key)
        {
            if (_events.ContainsKey(key))
                throw new Exception($"Event {key} is already declared!");

            _events.Add(key, new Event());
        }

        public void Declare<T>(in int key)
        {
            if (_events.ContainsKey(key))
                throw new Exception($"Key {key} is already declared!");

            _events.Add(key, new Event<T>());
        }

        public void Declare<T1, T2>(in int key)
        {
            if (_events.ContainsKey(key))
                throw new Exception($"Key {key} is already declared!");

            _events.Add(key, new Event<T1, T2>());
        }

        public void Declare<T1, T2, T3>(in int key)
        {
            if (_events.ContainsKey(key))
                throw new Exception($"Key {key} is already declared!");

            _events.Add(key, new Event<T1, T2, T3>());
        }

        public bool IsDeclared(in int key)
        {
            return _events.ContainsKey(key);
        }

        public bool Undeclare(in int key)
        {
            return _events.Remove(key);
        }

        #endregion

        #region Subscribe

        public Action Subscribe(in int key, in Action action)
        {
            Event evt;
            if (_events.TryGetValue(key, out IEvent ptr))
            {
                evt = (Event) ptr;
            }
            else
            {
                evt = new Event();
                _events.Add(key, evt);
            }

            evt.Delegate += action;
            return action;
        }

        public Action<T> Subscribe<T>(in int key, in Action<T> action)
        {
            Event<T> evt;
            if (_events.TryGetValue(key, out IEvent ptr))
            {
                evt = (Event<T>) ptr;
            }
            else
            {
                evt = new Event<T>();
                _events.Add(key, evt);
            }

            evt.Delegate += action;

            return action;
        }

        public Action<T1, T2> Subscribe<T1, T2>(in int key, in Action<T1, T2> action)
        {
            Event<T1, T2> evt;
            if (_events.TryGetValue(key, out IEvent ptr))
            {
                evt = (Event<T1, T2>) ptr;
            }
            else
            {
                evt = new Event<T1, T2>();
                _events.Add(key, evt);
            }
            
            evt.Delegate += action;
            return action;
        }

        public Action<T1, T2, T3> Subscribe<T1, T2, T3>(in int key, in Action<T1, T2, T3> action)
        {
            Event<T1, T2, T3> evt;
            if (_events.TryGetValue(key, out IEvent ptr))
            {
                evt = (Event<T1, T2, T3>) ptr;
            }
            else
            {
                evt = new Event<T1, T2, T3>();
                _events.Add(key, evt);
            }
            
            evt.Delegate += action;
            return action;
        }
        
        #endregion

        #region Unsubscribe

        public void Unsubscribe(in int key, in Action action)
        {
            if (_events.TryGetValue(key, out IEvent ptr))
            {
                Event evt = (Event) ptr;
                evt.Delegate -= action;
            }
        }

        public void Unsubscribe<T>(in int key, in Action<T> action)
        {
            if (_events.TryGetValue(key, out IEvent ptr))
            {
                Event<T> evt = (Event<T>) ptr;
                evt.Delegate -= action;
            }
        }

        public void Unsubscribe<T1, T2>(in int key, in Action<T1, T2> action)
        {
            if (_events.TryGetValue(key, out IEvent ptr))
            {
                Event<T1, T2> evt = (Event<T1, T2>) ptr;
                evt.Delegate -= action;
            }
        }

        public void Unsubscribe<T1, T2, T3>(in int key, in Action<T1, T2, T3> action)
        {
            if (_events.TryGetValue(key, out IEvent ptr))
            {
                Event<T1, T2, T3> evt = (Event<T1, T2, T3>) ptr;
                evt.Delegate -= action;
            }
        }

        #endregion

        #region Invoke

        public void Invoke(in int key)
        {
            if (!_events.TryGetValue(key, out IEvent ptr))
                throw new Exception($"Event {key} is not declared yet!");

            Event evt = (Event) ptr;
            evt.Delegate?.Invoke();
        }

        public void Invoke<T>(in int key, in T arg)
        {
            if (!_events.TryGetValue(key, out IEvent ptr))
                throw new Exception($"Event {key} is not declared yet!");

            Event<T> evt = (Event<T>) ptr;
            evt.Delegate?.Invoke(arg);
        }

        public void Invoke<T1, T2>(in int key, in T1 arg1, in T2 arg2)
        {
            if (!_events.TryGetValue(key, out IEvent ptr))
                throw new Exception($"Event {key} is not declared yet!");

            Event<T1, T2> evt = (Event<T1, T2>) ptr;
            evt.Delegate?.Invoke(arg1, arg2);
        }

        public void Invoke<T1, T2, T3>(in int key, in T1 arg1, in T2 arg2, in T3 arg3)
        {
            if (!_events.TryGetValue(key, out IEvent ptr))
                throw new Exception($"Event {key} is not declared yet!");

            Event<T1, T2, T3> evt = (Event<T1, T2, T3>) ptr;
            evt.Delegate?.Invoke(arg1, arg2, arg3);
        }

        #endregion

        public void Dispose()
        {
            foreach (IEvent e in _events.Values)
                e.Dispose();

            _events.Clear();
        }
    }
}