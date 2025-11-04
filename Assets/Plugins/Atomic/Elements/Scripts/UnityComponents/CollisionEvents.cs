#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// A MonoBehaviour component that exposes Unity's collision events (<c>OnCollisionEnter</c>, <c>OnCollisionExit</c>, <c>OnCollisionStay</c>)
    /// as C# events for easier binding and external reaction.
    /// </summary>
    [AddComponentMenu("Atomic/Elements/Collision Events")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Utils/CollisionEvents.md")]
    public sealed class CollisionEvents : MonoBehaviour
    {
        /// <summary>
        /// Invoked when a collision begins (corresponds to <see cref="MonoBehaviour.OnCollisionEnter(Collision)"/>).
        /// </summary>
        public event Action<Collision> OnEntered;

        /// <summary>
        /// Invoked when a collision ends (corresponds to <see cref="MonoBehaviour.OnCollisionExit(Collision)"/>).
        /// </summary>
        public event Action<Collision> OnExited;

        /// <summary>
        /// Invoked every frame while a collision persists (corresponds to <see cref="MonoBehaviour.OnCollisionStay(Collision)"/>).
        /// </summary>
        public event Action<Collision> OnStay;

        /// <summary>
        /// Unity callback for when this collider/rigidbody starts colliding with another collider.
        /// Triggers the <see cref="OnEntered"/> event.
        /// </summary>
        /// <param name="collision">Information about the collision.</param>
        private void OnCollisionEnter(Collision collision) => this.OnEntered?.Invoke(collision);

        /// <summary>
        /// Unity callback for when this collider/rigidbody stops colliding with another collider.
        /// Triggers the <see cref="OnExited"/> event.
        /// </summary>
        /// <param name="collision">Information about the collision.</param>
        private void OnCollisionExit(Collision collision) => this.OnExited?.Invoke(collision);

        /// <summary>
        /// Unity callback for each frame this collider/rigidbody continues colliding with another collider.
        /// Triggers the <see cref="OnStay"/> event.
        /// </summary>
        /// <param name="collision">Information about the collision.</param>
        private void OnCollisionStay(Collision collision) => this.OnStay?.Invoke(collision);
    }
}
#endif