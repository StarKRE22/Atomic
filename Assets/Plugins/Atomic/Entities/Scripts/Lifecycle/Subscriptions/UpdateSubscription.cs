using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription that detaches a callback from an <see cref="IUpdateSource"/>'s <see cref="IUpdateSource.OnUpdated"/> event when disposed.
    /// </summary>
    /// <remarks>
    /// Useful for subscribing to frame-based updates and ensuring clean unsubscription to prevent memory leaks.
    /// </remarks>
    public readonly struct UpdateSubscription : IDisposable
    {
        private readonly IUpdateSource _source;
        private readonly Action<float> _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSubscription"/> struct.
        /// </summary>
        /// <param name="source">The updatable source to subscribe to.</param>
        /// <param name="callback">The callback to invoke on update.</param>
        internal UpdateSubscription(IUpdateSource source, Action<float> callback)
        {
            _source = source;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="IUpdateSource.OnUpdated"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnUpdated -= _callback;
        }
    }
}