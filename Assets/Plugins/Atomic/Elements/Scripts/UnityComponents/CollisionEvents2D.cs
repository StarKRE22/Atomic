#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides safe and consistent 2D collision notifications by wrapping Unity's
    /// <see cref="OnCollisionEnter2D"/>, <see cref="OnCollisionExit2D"/> and
    /// <see cref="OnCollisionStay2D"/> callbacks into managed C# events.
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
    [AddComponentMenu("Atomic/Elements/Collision Events 2D")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Utils/CollisionEvents2D.md")]
    public sealed class CollisionEvents2D : MonoBehaviour
    {
        /// <summary>
        /// Occurs when a 2D collision begins.
        /// Fired only once per collider until it exits.
        /// </summary>
        public event Action<Collision2D> OnEntered;

        /// <summary>
        /// Occurs when a 2D collision ends.
        /// Fired only if the collision was previously tracked.
        /// </summary>
        public event Action<Collision2D> OnExited;

        /// <summary>
        /// Occurs every frame while a 2D collision persists.
        /// Invoked only if listeners are attached.
        /// </summary>
        public event Action<Collision2D> OnStay;

        /// <summary>
        /// Gets a read-only collection of currently colliding 2D objects.
        /// Cleared automatically when the component is disabled.
        /// </summary>
        public IReadOnlyCollection<Collision2D> CurrentCollisions => _currentCollisions;

        private readonly HashSet<Collision2D> _currentCollisions = new();

        /// <summary>
        /// Unity callback invoked when this 2D collider/rigidbody starts colliding with another 2D collider.
        /// Registers the collision and fires <see cref="OnEntered"/> only if not tracked yet.
        /// </summary>
        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (_currentCollisions.Add(collision))
                OnEntered?.Invoke(collision);
        }

        /// <summary>
        /// Unity callback invoked when this 2D collider/rigidbody stops colliding with another 2D collider.
        /// Removes the collision and fires <see cref="OnExited"/> only if previously tracked.
        /// </summary>
        private void OnCollisionExit2D(Collision2D collision)
        {
            if (_currentCollisions.Remove(collision))
                OnExited?.Invoke(collision);
        }

        /// <summary>
        /// Unity callback invoked each frame while this 2D collider/rigidbody continues colliding with another 2D collider.
        /// Fires <see cref="OnStay"/> only if listeners exist.
        /// </summary>
        private void OnCollisionStay2D(Collision2D collision)
        {
            if (OnStay != null)
                OnStay(collision);
        }

        /// <summary>
        /// Ensures cleanup when the component or GameObject is disabled.
        /// Fires <see cref="OnExited"/> for all currently tracked collisions
        /// and clears the internal set.
        /// This prevents stale references from remaining due to Unity's
        /// lack of guaranteed OnCollisionExit2D callbacks in certain cases.
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