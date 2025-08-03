using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription handle that unregisters a callback from an <see cref="IEnableable"/>'s <see cref="IEnableable.OnEnabled"/> event upon disposal.
    /// </summary>
    /// <remarks>
    /// This struct is intended to simplify the management of temporary or scoped subscriptions
    /// to activation events, ensuring that the provided callback is properly unsubscribed when no longer needed.
    /// </remarks>
    public readonly struct EnableSubscription : IDisposable
    {
        private readonly IEnableable _source;
        private readonly Action _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="EnableSubscription"/> struct.
        /// </summary>
        /// <param name="source">The activatable source object to subscribe to.</param>
        /// <param name="callback">The callback to invoke when the source is enabled.</param>
        internal EnableSubscription(IEnableable source, Action callback)
        {
            _source = source;
            _callback = callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="IEnableable.OnEnabled"/> event.
        /// </summary>
        public void Dispose()
        {
            if (_source != null && _callback != null)
                _source.OnEnabled -= _callback;
        }
    }
}