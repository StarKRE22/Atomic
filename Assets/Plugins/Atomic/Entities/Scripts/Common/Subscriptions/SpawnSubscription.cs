using System;

namespace Atomic.Entities
{
    public readonly struct SpawnSubscription : IDisposable
    {
        private readonly ISpawnable _source;
        private readonly Action _callback;
        
        internal SpawnSubscription(ISpawnable source, Action callback)
        {
            _source = source;
            _callback = callback;
        }
        
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnSpawned -= _callback;
        }
    }
}