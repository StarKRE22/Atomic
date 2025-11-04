#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// A MonoBehaviour component that exposes Unity's 2D collision events
    /// (<c>OnCollisionEnter2D</c>, <c>OnCollisionExit2D</c>, <c>OnCollisionStay2D</c>)
    /// as C# events for external handling.
    /// </summary>
    [AddComponentMenu("Atomic/Elements/Collision Events 2D")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Utils/CollisionEvents2D.md")]
    public sealed class CollisionEvents2D : MonoBehaviour
    {
        /// <summary>
        /// Invoked when a 2D collision begins (corresponds to <see cref="MonoBehaviour.OnCollisionEnter2D(Collision2D)"/>).
        /// </summary>
        public event Action<Collision2D> OnEntered;

        /// <summary>
        /// Invoked when a 2D collision ends (corresponds to <see cref="MonoBehaviour.OnCollisionExit2D(Collision2D)"/>).
        /// </summary>
        public event Action<Collision2D> OnExited;

        /// <summary>
        /// Invoked every frame while a 2D collision persists (corresponds to <see cref="MonoBehaviour.OnCollisionStay2D(Collision2D)"/>).
        /// </summary>
        public event Action<Collision2D> OnStay;

        /// <summary>
        /// Unity callback for when this 2D collider/rigidbody starts colliding with another 2D collider.
        /// Triggers the <see cref="OnEntered"/> event.
        /// </summary>
        /// <param name="collision">Information about the 2D collision.</param>
        private void OnCollisionEnter2D(Collision2D collision) => this.OnEntered?.Invoke(collision);

        /// <summary>
        /// Unity callback for when this 2D collider/rigidbody stops colliding with another 2D collider.
        /// Triggers the <see cref="OnExited"/> event.
        /// </summary>
        /// <param name="collision">Information about the 2D collision.</param>
        private void OnCollisionExit2D(Collision2D collision) => this.OnExited?.Invoke(collision);

        /// <summary>
        /// Unity callback for each frame this 2D collider/rigidbody continues colliding with another 2D collider.
        /// Triggers the <see cref="OnStay"/> event.
        /// </summary>
        /// <param name="collision">Information about the 2D collision.</param>
        private void OnCollisionStay2D(Collision2D collision) => this.OnStay?.Invoke(collision);
    }
}
#endif