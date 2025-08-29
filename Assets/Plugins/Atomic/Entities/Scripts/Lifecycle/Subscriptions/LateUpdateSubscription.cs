using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription that detaches a callback from an <see cref="IUpdateSource"/>'s <see cref="IUpdateSource.OnLateUpdated"/> event when disposed.
    /// </summary>
    /// <remarks>
    /// Useful for managing lifecycle-scoped or temporary subscriptions to late update events,
    /// such as when binding logic to specific runtime conditions.
    /// </remarks>
    public readonly struct LateUpdateSubscription : IDisposable
    {
        private readonly IUpdateSource _source;
        private readonly Action<float> _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="LateUpdateSubscription"/> struct.
        /// </summary>
        /// <param name="source">The updatable source to subscribe to.</param>
        /// <param name="callback">The callback invoked during <c>LateUpdate</c> cycles.</param>
        internal LateUpdateSubscription(IUpdateSource source, Action<float> callback)
        {
            _source = source;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="IUpdateSource.OnLateUpdated"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnLateUpdated -= _callback;
        }
    }
}