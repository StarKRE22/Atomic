using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a disposable subscription handle to an <see cref="ITickLifecycle"/>'s <see cref="ITickLifecycle.OnTicked"/> event.
    /// </summary>
    /// <remarks>
    /// Allows temporarily reacting to frame-based updates of an <see cref="ITickLifecycle"/> instance.
    /// When disposed, the subscription automatically detaches the callback to prevent memory leaks 
    /// or repeated invocations.
    /// 
    /// Subscriptions are intended to be short-lived and can be safely used in a <c>using</c> statement or manually disposed.
    /// </remarks>
    public readonly struct TickSubscription : IDisposable
    {
        private readonly ITickLifecycle _source;
        private readonly Action<float> _callback;

        /// <summary>
        /// Initializes a new <see cref="TickSubscription"/> instance.
        /// Subscribes the specified callback to the <see cref="ITickLifecycle.OnTicked"/> event of the provided source.
        /// </summary>
        /// <param name="source">The <see cref="ITickLifecycle"/> instance to subscribe to.</param>
        /// <param name="callback">The callback action to invoke on each update tick.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> or <paramref name="callback"/> is <c>null</c>.</exception>
        public TickSubscription(ITickLifecycle source, Action<float> callback)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _source.OnTicked += _callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="ITickLifecycle.OnTicked"/> event.
        /// Calling this method ensures the callback will no longer be invoked.
        /// Safe to call multiple times.
        /// </summary>
        public void Dispose()
        {
            _source.OnTicked -= _callback;
        }
    }
}