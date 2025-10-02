using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription handle that unregisters a callback from an <see cref="IInitLifecycle"/>'s
    /// <see cref="IInitLifecycle.OnDisposed"/> event upon disposal.
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
        public DisposeSubscription(IInitLifecycle source, Action callback)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _source.OnDisposed += _callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="IInitLifecycle.OnDisposed"/> event.
        /// </summary>
        public void Dispose()
        {
            _source.OnDisposed -= _callback;
        }
    }
}