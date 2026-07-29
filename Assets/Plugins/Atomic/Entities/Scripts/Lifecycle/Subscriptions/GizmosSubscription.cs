#if UNITY_EDITOR
using System;

namespace Atomic.Entities
{
    public readonly struct GizmosSubscription : IDisposable
    {
        private readonly IGizmosSource _source;
        private readonly Action _callback;

        public GizmosSubscription(IGizmosSource source, Action callback)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _source.OnGizmosDraw += _callback;
        }

        public void Dispose()
        {
            _source.OnGizmosDraw -= _callback;
        }
    }
}
#endif