using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a disposable subscription to an <see cref="IInitLifecycle"/>'s <see cref="IInitLifecycle.OnInitialized"/> event.
    /// </summary>
    /// <remarks>
    /// This struct allows temporarily reacting to initialization events of an <see cref="IInitLifecycle"/> instance.
    /// When the subscription is disposed, it automatically unsubscribes the callback, preventing memory leaks 
    /// or unintended repeated invocations.
    /// 
    /// Subscriptions are intended to be short-lived and do not require manual unsubscription as long as 
    /// <see cref="Dispose"/> is called or used in a <c>using</c> statement.
    /// </remarks>
    public readonly struct InitSubscription : IDisposable
    {
        private readonly IInitLifecycle _source;
        private readonly Action _callback;

        /// <summary>
        /// Initializes a new <see cref="InitSubscription"/> instance.
        /// Subscribes the specified callback to the <see cref="IInitLifecycle.OnInitialized"/> event of the provided source.
        /// </summary>
        /// <param name="source">The <see cref="IInitLifecycle"/> instance to subscribe to.</param>
        /// <param name="callback">The callback action to invoke when the source is initialized.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> or <paramref name="callback"/> is <c>null</c>.</exception>
        public InitSubscription(IInitLifecycle source, Action callback)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _source.OnInitialized += _callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="IInitLifecycle.OnInitialized"/> event.
        /// Calling this method ensures the callback will no longer be invoked.
        /// Safe to call multiple times.
        /// </summary>
        public void Dispose()
        {
            _source.OnInitialized -= _callback;
        }
    }
}