using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription that detaches a callback from an <see cref="IInitSource"/>'s <see cref="IInitSource.OnInitialized"/> event when disposed.
    /// </summary>
    /// <remarks>
    /// Useful for temporarily reacting to spawn events and ensuring automatic unsubscription upon disposal.
    /// </remarks>
    public readonly struct InitSubscription : IDisposable
    {
        private readonly IInitSource _source;
        private readonly Action _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="InitSubscription"/> struct.
        /// </summary>
        /// <param name="source">The spawnable source to subscribe to.</param>
        /// <param name="callback">The callback to invoke on spawn.</param>
        internal InitSubscription(IInitSource source, Action callback)
        {
            _source = source;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="IInitSource.OnInitialized"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnInitialized -= _callback;
        }
    }
}