using System;

namespace Atomic.Entities
{
    public readonly struct UpdateSubscription : IDisposable
    {
        private readonly IUpdatable _source;
        private readonly Action<float> _callback;

        internal UpdateSubscription(IUpdatable source, Action<float> callback)
        {
            _source = source;
            _callback = callback;
        }

        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnUpdated -= _callback;
        }
    }
}