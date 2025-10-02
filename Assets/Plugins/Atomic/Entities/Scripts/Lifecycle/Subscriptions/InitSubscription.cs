using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription that detaches a callback from an <see cref="IInitLifecycle"/>'s
    /// <see cref="IInitLifecycle.OnInitialized"/> event when disposed.
    /// </summary>
    /// <remarks>
    /// Useful for temporarily reacting to spawn events and ensuring automatic unsubscription upon disposal.
    /// </remarks>
    public readonly struct InitSubscription : IDisposable
    {
        private readonly IInitLifecycle _source;
        private readonly Action _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitSubscription"/> struct.
        /// </summary>
        /// <param name="source">The spawnable source to subscribe to.</param>
        /// <param name="callback">The callback to invoke on spawn.</param>
        public InitSubscription(IInitLifecycle source, Action callback)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _source.OnInitialized += _callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="IInitLifecycle.OnInitialized"/> event.
        /// </summary>
        public void Dispose()
        {
            _source.OnInitialized -= _callback;
        }
    }
}