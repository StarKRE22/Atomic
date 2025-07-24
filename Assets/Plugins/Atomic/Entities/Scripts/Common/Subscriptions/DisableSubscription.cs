using System;

namespace Atomic.Entities
{
    public readonly struct DisableSubscription : IDisposable
    {
        private readonly IActivatable _source;
        private readonly Action _callback;

        internal DisableSubscription(IActivatable source, Action callback)
        {
            _source = source;
            _callback = callback;
        }
        
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnDisabled -= _callback;
        }
    }
}