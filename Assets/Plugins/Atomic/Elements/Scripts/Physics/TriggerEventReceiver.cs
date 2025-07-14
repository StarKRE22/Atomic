using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// A MonoBehaviour that listens to Unity's OnTriggerEnter and OnTriggerExit events
    /// and exposes them as C# events for external subscription.
    /// </summary>
    public sealed class TriggerEventReceiver : MonoBehaviour
    {
        /// <summary>
        /// Invoked when a Collider enters the trigger zone of this GameObject.
        /// </summary>
        public event Action<Collider> OnEntered;

        /// <summary>
        /// Invoked when a Collider exits the trigger zone of this GameObject.
        /// </summary>
        public event Action<Collider> OnExited;

        /// <summary>
        /// Unity callback when another Collider enters the trigger.
        /// Triggers the <see cref="OnEntered"/> event.
        /// </summary>
        /// <param name="other">The Collider that entered the trigger zone.</param>
        private void OnTriggerEnter(Collider other) => this.OnEntered?.Invoke(other);

        /// <summary>
        /// Unity callback when another Collider exits the trigger.
        /// Triggers the <see cref="OnExited"/> event.
        /// </summary>
        /// <param name="other">The Collider that exited the trigger zone.</param>
        private void OnTriggerExit(Collider other) => this.OnExited?.Invoke(other);
    }
}