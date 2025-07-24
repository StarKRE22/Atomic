using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription handle that unregisters a callback from an <see cref="IUpdatable"/>'s <see cref="IUpdatable.OnFixedUpdated"/> event upon disposal.
    /// </summary>
    /// <remarks>
    /// Use this struct to manage scoped or temporary subscriptions to fixed update events,
    /// ensuring the callback is automatically removed when no longer needed.
    /// </remarks>
    public readonly struct FixedUpdateSubscription : IDisposable
    {
        private readonly IUpdatable _source;
        private readonly Action<float> _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="FixedUpdateSubscription"/> struct.
        /// </summary>
        /// <param name="source">The updatable source object to subscribe to.</param>
        /// <param name="callback">The callback to invoke on each fixed update.</param>
        internal FixedUpdateSubscription(IUpdatable source, Action<float> callback)
        {
            _source = source;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="IUpdatable.OnFixedUpdated"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnFixedUpdated -= _callback;
        }
    }
}