#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// A MonoBehaviour component that exposes Unity's collision events (<c>OnCollisionEnter</c>, <c>OnCollisionExit</c>, <c>OnCollisionStay</c>)
    /// as C# events for easier binding and external reaction.
    ///
    /// This component:
    /// <list type="bullet">
    /// <item><description>Tracks currently colliding objects using a <see cref="HashSet{T}"/>.</description></item>
    /// <item><description>Avoids duplicate <c>Enter</c> and <c>Exit</c> events.</description></item>
    /// <item><description>Automatically clears all tracked collisions on <see cref="OnDisable"/> and fires proper exit events.</description></item>
    /// </list>
    /// Ideal for gameplay systems that must reliably know what is colliding at any moment:
    /// movement effects, damage triggers, physics-based abilities, etc.
    /// </summary>
    [AddComponentMenu("Atomic/Elements/Collision Events")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Utils/CollisionEvents.md")]
    public sealed class CollisionEvents : MonoBehaviour
    {
        /// <summary>
        /// Occurs when a collision begins (corresponds to <see cref="MonoBehaviour.OnCollisionEnter(Collision)"/>).
        /// Fired only once per collider until it exits.
        /// </summary>
        public event Action<Collision> OnEntered;

        /// <summary>
        /// Occurs when a collision ends (corresponds to <see cref="MonoBehaviour.OnCollisionExit(Collision)"/>).
        /// Fired only if the collision was previously tracked.
        /// </summary>
        public event Action<Collision> OnExited;

        /// <summary>
        /// Occurs every frame while a collision persists (corresponds to <see cref="MonoBehaviour.OnCollisionStay(Collision)"/>).
        /// Invoked only if listeners are attached.
        /// </summary>
        public event Action<Collision> OnStay;

        /// <summary>
        /// Gets a read-only collection of currently colliding objects.
        /// Cleared automatically when the component is disabled.
        /// </summary>
        public IReadOnlyCollection<Collision> CurrentCollisions => _currentCollisions;

        private readonly HashSet<Collision> _currentCollisions = new();

        /// <summary>
        /// Unity callback when this collider/rigidbody starts colliding with another collider.
        /// Registers the collision and fires <see cref="OnEntered"/> only if not tracked yet.
        /// </summary>
        private void OnCollisionEnter(Collision collision)
        {
            if (_currentCollisions.Add(collision))
                OnEntered?.Invoke(collision);
        }

        /// <summary>
        /// Unity callback when this collider/rigidbody stops colliding with another collider.
        /// Removes the collision and fires <see cref="OnExited"/> only if previously tracked.
        /// </summary>
        private void OnCollisionExit(Collision collision)
        {
            if (_currentCollisions.Remove(collision))
                OnExited?.Invoke(collision);
        }

        /// <summary>
        /// Unity callback each frame while this collider/rigidbody continues colliding with another collider.
        /// Fires <see cref="OnStay"/> only if listeners exist.
        /// </summary>
        private void OnCollisionStay(Collision collision)
        {
            if (OnStay != null)
                OnStay(collision);
        }

        /// <summary>
        /// Ensures cleanup when the component or GameObject is disabled.
        /// Fires <see cref="OnExited"/> for all currently tracked collisions
        /// and clears the internal set.
        /// This prevents stale references from remaining due to Unity's
        /// lack of guaranteed OnCollisionExit callbacks in certain cases.
        /// </summary>
        private void OnDisable()
        {
            if (_currentCollisions.Count == 0)
                return;

            foreach (var col in _currentCollisions)
                OnExited?.Invoke(col);

            _currentCollisions.Clear();
        }
    }
}
#endif