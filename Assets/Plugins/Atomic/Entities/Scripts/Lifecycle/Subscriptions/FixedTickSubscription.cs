using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription handle that unregisters a callback from an <see cref="ITickLifecycle"/>'s <see cref="ITickLifecycle.OnFixedTicked"/> event upon disposal.
    /// </summary>
    /// <remarks>
    /// Use this struct to manage scoped or temporary subscriptions to fixed update events,
    /// ensuring the callback is automatically removed when no longer needed.
    /// </remarks>
    public readonly struct FixedTickSubscription : IDisposable
    {
        private readonly ITickLifecycle _source;
        private readonly Action<float> _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedTickSubscription"/> struct.
        /// </summary>
        /// <param name="source">The updatable source object to subscribe to.</param>
        /// <param name="callback">The callback to invoke on each fixed update.</param>
        internal FixedTickSubscription(ITickLifecycle source, Action<float> callback)
        {
            _source = source;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="ITickLifecycle.OnFixedTicked"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnFixedTicked -= _callback;
        }
    }
}