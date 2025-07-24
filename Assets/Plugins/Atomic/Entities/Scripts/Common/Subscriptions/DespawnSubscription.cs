using System;

namespace Atomic.Entities
{
    public readonly struct DespawnSubscription : IDisposable
    {
        private readonly ISpawnable _source;
        private readonly Action _callback;

        internal DespawnSubscription(ISpawnable source, Action callback)
        {
            _source = source;
            _callback = callback;
        }
        
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnDespawned -= _callback;
        }
    }
}