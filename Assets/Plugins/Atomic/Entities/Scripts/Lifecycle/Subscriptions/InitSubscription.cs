using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a disposable subscription to an <see cref="IInitSource"/>'s <see cref="IInitSource.OnInitialized"/> event.
    /// </summary>
    /// <remarks>
    /// This struct allows temporarily reacting to initialization events of an <see cref="IInitSource"/> instance.
    /// When the subscription is disposed, it automatically unsubscribes the callback, preventing memory leaks 
    /// or unintended repeated invocations.
    /// 
    /// Subscriptions are intended to be short-lived and do not require manual unsubscription as long as 
    /// <see cref="Dispose"/> is called or used in a <c>using</c> statement.
    /// </remarks>
    public readonly struct InitSubscription : IDisposable
    {
        private readonly IInitSource _source;
        private readonly Action _callback;

        /// <summary>
        /// Initializes a new <see cref="InitSubscription"/> instance.
        /// Subscribes the specified callback to the <see cref="IInitSource.OnInitialized"/> event of the provided source.
        /// </summary>
        /// <param name="source">The <see cref="IInitSource"/> instance to subscribe to.</param>
        /// <param name="callback">The callback action to invoke when the source is initialized.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> or <paramref name="callback"/> is <c>null</c>.</exception>
        public InitSubscription(IInitSource source, Action callback)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _source.OnInitialized += _callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="IInitSource.OnInitialized"/> event.
        /// Calling this method ensures the callback will no longer be invoked.
        /// Safe to call multiple times.
        /// </summary>
        public void Dispose()
        {
            _source.OnInitialized -= _callback;
        }
    }
}