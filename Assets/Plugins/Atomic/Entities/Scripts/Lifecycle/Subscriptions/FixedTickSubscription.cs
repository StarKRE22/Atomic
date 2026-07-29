using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a disposable subscription handle to an <see cref="ITickSource"/>'s <see cref="ITickSource.OnFixedTicked"/> event.
    /// </summary>
    /// <remarks>
    /// Allows temporarily reacting to fixed update ticks of an <see cref="ITickSource"/> instance.
    /// When disposed, the subscription automatically detaches the callback to prevent memory leaks
    /// or repeated invocations.
    /// 
    /// Subscriptions are intended to be short-lived and can be safely used in a <c>using</c> statement or manually disposed.
    /// </remarks>
    public readonly struct FixedTickSubscription : IDisposable
    {
        private readonly ITickSource _source;
        private readonly Action<float> _callback;

        /// <summary>
        /// Initializes a new <see cref="FixedTickSubscription"/> instance.
        /// Subscribes the specified callback to the <see cref="ITickSource.OnFixedTicked"/> event of the provided source.
        /// </summary>
        /// <param name="source">The <see cref="ITickSource"/> instance to subscribe to.</param>
        /// <param name="callback">The callback action to invoke on each fixed update tick.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> or <paramref name="callback"/> is <c>null</c>.</exception>
        public FixedTickSubscription(ITickSource source, Action<float> callback)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _source.OnFixedTicked += _callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="ITickSource.OnFixedTicked"/> event.
        /// Calling this method ensures the callback will no longer be invoked.
        /// Safe to call multiple times.
        /// </summary>
        public void Dispose()
        {
            _source.OnFixedTicked -= _callback;
        }
    }
}