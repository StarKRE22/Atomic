using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription handle that unregisters a callback from an <see cref="IEnableLifecycle"/>'s <see creIEnableSourcelee.OnDisabled"/> event upon disposal.
    /// </summary>
    /// <remarks>
    /// Useful for managing scoped or temporary subscriptions to disable events, ensuring the callback is removed when no longer needed.
    /// </remarks>
    public readonly struct DisableSubscription : IDisposable
    {
        private readonly IEnableLifecycle _source;
        private readonly Action _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="DisableSubscription"/> struct.
        /// </summary>
        /// <param name="source">The activatable source to subscribe to.</param>
        /// <param name="callback">The callback to invoke when the source is disabled.</param>
        internal DisableSubscription(IEnableLifecycle source, Action callback)
        {
            _source = source;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see creIEnableSourcelee.OnDisabled"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnDisabled -= _callback;
        }
    }

}