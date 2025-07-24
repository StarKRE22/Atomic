using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription that detaches a callback from an <see cref="ISpawnable"/>'s <see cref="ISpawnable.OnSpawned"/> event when disposed.
    /// </summary>
    /// <remarks>
    /// Useful for temporarily reacting to spawn events and ensuring automatic unsubscription upon disposal.
    /// </remarks>
    public readonly struct SpawnSubscription : IDisposable
    {
        private readonly ISpawnable _source;
        private readonly Action _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="SpawnSubscription"/> struct.
        /// </summary>
        /// <param name="source">The spawnable source to subscribe to.</param>
        /// <param name="callback">The callback to invoke on spawn.</param>
        internal SpawnSubscription(ISpawnable source, Action callback)
        {
            _source = source;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="ISpawnable.OnSpawned"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnSpawned -= _callback;
        }
    }

}