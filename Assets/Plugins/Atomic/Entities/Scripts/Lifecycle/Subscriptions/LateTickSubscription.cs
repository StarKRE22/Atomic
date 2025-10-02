using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A disposable subscription that detaches a callback from an <see cref="ITickLifecycle"/>'s
    /// <see cref="ITickLifecycle.OnLateTicked"/> event when disposed.
    /// </summary>
    /// <remarks>
    /// Useful for managing lifecycle-scoped or temporary subscriptions to late update events,
    /// such as when binding logic to specific runtime conditions.
    /// </remarks>
    public readonly struct LateTickSubscription : IDisposable
    {
        private readonly ITickLifecycle _source;
        private readonly Action<float> _callback;

        /// <summary>
        /// Initializes a new instance of the <see cref="LateTickSubscription"/> struct.
        /// </summary>
        /// <param name="source">The updatable source to subscribe to.</param>
        /// <param name="callback">The callback invoked during <c>LateUpdate</c> cycles.</param>
        public LateTickSubscription(ITickLifecycle source, Action<float> callback)
        {
            _source = source ?? throw new ArgumentNullException(nameof(source));
            _callback = callback ?? throw new ArgumentNullException(nameof(callback));
            _source.OnLateTicked += _callback;
        }

        /// <summary>
        /// Unsubscribes the callback from the <see cref="ITickLifecycle.OnLateTicked"/> event.
        /// </summary>
        public void Dispose()
        {
            _source.OnLateTicked -= _callback;
        }
    }
}