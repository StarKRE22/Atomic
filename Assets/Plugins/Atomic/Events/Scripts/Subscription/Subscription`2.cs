using System;

namespace Atomic.Events
{
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
}