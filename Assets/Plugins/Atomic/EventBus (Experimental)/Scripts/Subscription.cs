using System;

namespace Atomic.Events
{
    public readonly struct Subscription : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly int _key;
        private readonly Action _action;

        internal Subscription(IEventBus eventBus, int key, Action action)
        {
            _eventBus = eventBus;
            _key = key;
            _action = action;
        }

        public void Dispose() => _eventBus.Unsubscribe(_key, _action);
    }

    public readonly struct Subscription<T> : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly int _key;
        private readonly Action<T> _action;

        internal Subscription(IEventBus eventBus, int key, Action<T> action)
        {
            _eventBus = eventBus;
            _key = key;
            _action = action;
        }

        public void Dispose() => _eventBus.Unsubscribe(_key, _action);
    }

    public readonly struct Subscription<T1, T2> : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly int _key;
        private readonly Action<T1, T2> _action;

        internal Subscription(IEventBus eventBus, int key, Action<T1, T2> action)
        {
            _eventBus = eventBus;
            _key = key;
            _action = action;
        }

        public void Dispose() => _eventBus.Unsubscribe(_key, _action);
    }

    public readonly struct Subscription<T1, T2, T3> : IDisposable
    {
        private readonly IEventBus _eventBus;
        private readonly int _key;
        private readonly Action<T1, T2, T3> _action;

        internal Subscription(IEventBus eventBus, int key, Action<T1, T2, T3> action)
        {
            _eventBus = eventBus;
            _key = key;
            _action = action;
        }

        public void Dispose() => _eventBus.Unsubscribe(_key, _action);
    }
}