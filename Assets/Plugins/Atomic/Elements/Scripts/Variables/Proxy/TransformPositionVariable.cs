using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Reactive wrapper around a <see cref="UnityEngine.Transform"/> position that
    /// exposes an <see cref="IReactiveVariable{T}"/>-like API.
    /// </summary>
    /// <remarks>
    /// - Reading <see cref="Value"/> returns the cached position.
    /// - Setting <see cref="Value"/> updates the cache, assigns <see cref="Transform.position"/>,
    ///   and notifies subscribers via <see cref="OnValueChanged"/> (only when the value actually changes).
    /// - Use <see cref="Subscribe"/> / <see cref="Unsubscribe"/> to observe changes,
    ///   or dispose subscriptions via returned <see cref="Subscription{T}"/>.
    /// </remarks>
    public sealed class TransformPositionVariable : IReactiveVariable<Vector3>, IDisposable
    {
        /// <summary>
        /// Raised after <see cref="Value"/> changes and the underlying
        /// <see cref="Transform.position"/> is updated.
        /// </summary>
        public event Action<Vector3> OnValueChanged;

        /// <summary>
        /// Gets or sets the current position value.
        /// </summary>
        /// <remarks>
        /// Setter performs a change check:
        /// if the new value equals the cached position, no assignment or notification occurs.
        /// Otherwise, the internal cache is updated, <see cref="Transform.position"/> is set,
        /// and <see cref="OnValueChanged"/> is invoked.
        /// </remarks>
        public Vector3 Value
        {
            get { return _position; }
            set
            {
                if (_position != value)
                {
                    _position = value;
                    _transform.position = value;
                    this.OnValueChanged?.Invoke(value);
                }
            }
        }

        /// <summary>
        /// Backing <see cref="UnityEngine.Transform"/> whose position is controlled by this wrapper.
        /// </summary>
        private readonly Transform _transform;

        /// <summary>
        /// Cached position used to avoid redundant notifications and assignments.
        /// </summary>
        private Vector3 _position;

        /// <summary>
        /// Creates a new <see cref="TransformPositionVariable"/> bound to the provided <see cref="Transform"/>.
        /// </summary>
        /// <param name="transform">Target transform. Must not be <c>null</c>.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="transform"/> is <c>null</c>.</exception>
        public TransformPositionVariable(Transform transform)
        {
            _transform = transform ?? throw new ArgumentNullException(nameof(transform));
            _position = _transform.position;
        }

        /// <summary>
        /// Subscribes to value change notifications.
        /// </summary>
        /// <param name="action">Callback invoked with the new value after it changes.</param>
        /// <returns>
        /// A <see cref="Subscription{T}"/> that can be disposed to unsubscribe.
        /// </returns>
        public Subscription<Vector3> Subscribe(Action<Vector3> action)
        {
            this.OnValueChanged += action;
            return new Subscription<Vector3>(this, action);
        }

        /// <summary>
        /// Unsubscribes a previously registered callback.
        /// </summary>
        /// <param name="action">The callback to remove.</param>
        public void Unsubscribe(Action<Vector3> action)
        {
            this.OnValueChanged -= action;
        }

        /// <summary>
        /// Releases all subscribers by clearing <see cref="OnValueChanged"/>.
        /// </summary>
        /// <remarks>
        /// After disposal, further <see cref="Value"/> updates will no longer notify any listeners.
        /// </remarks>
        public void Dispose()
        {
            this.OnValueChanged = null;
        }
    }
}