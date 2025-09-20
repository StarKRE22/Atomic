using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription handle that unregisters a callback from an <see cref="IInitLifecycle"/>'s <see cref="IILifecycleInitOnDisposed"/> event upon disposal.
    /// </summary>
    /// <remarks>
    /// Useful for managing temporary or one-time subscriptions to despawn events in a safe and deterministic way.
    /// </remarks>
    public readonly struct DisposeSubscription : IDisposable
    {
        private readonly IInitLifecycle _source;
        private readonly Action _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisposeSubscription"/> struct.
        /// </summary>
        /// <param name="source">The spawnable entity the callback is subscribed to.</param>
        /// <param name="callback">The callback to invoke on despawn.</param>
        internal DisposeSubscription(IInitLifecycle source, Action callback)
        {
            _source = source;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="IILifecycleInitOnDisposed"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnDisposed -= _callback;
        }
    }
}