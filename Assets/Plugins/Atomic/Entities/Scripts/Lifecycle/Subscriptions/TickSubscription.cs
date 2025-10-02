using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription that detaches a callback from an <see cref="ITickLifecycle"/>'s
    /// <see cref="ITickLifecycle.OnTicked"/> event when disposed.
    /// </summary>
    /// <remarks>
    /// Useful for subscribing to frame-based updates and ensuring clean unsubscription to prevent memory leaks.
    /// </remarks>
    public readonly struct TickSubscription : IDisposable
    {
        private readonly ITickLifecycle _source;
        private readonly Action<float> _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="TickSubscription"/> struct.
        /// </summary>
        /// <param name="source">The updatable source to subscribe to.</param>
        /// <param name="callback">The callback to invoke on update.</param>
        public TickSubscription(ITickLifecycle source, Action<float> callback)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _source.OnTicked += _callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="ITickLifecycle.OnTicked"/> event.
        /// </summary>
        public void Dispose()
        {
            _source.OnTicked -= _callback;
        }
    }
}