#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// A MonoBehaviour that listens to Unity's OnTriggerEnter and OnTriggerExit events
    /// and exposes them as C# events for external subscription.
    /// </summary>
    [AddComponentMenu("Atomic/Elements/Trigger Events")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Utils/TriggerEvents.md")]
    public sealed class TriggerEvents : MonoBehaviour
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
        /// Event triggered every frame while another collider remains within this 3D trigger collider.
        /// This corresponds to Unity's <see cref="MonoBehaviour.OnTriggerStay(UnityEngine.Collider)"/> callback.
        /// </summary>
        public event Action<Collider> OnStay; 

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

        /// <summary>
        /// Unity callback invoked every frame while another collider remains within this trigger collider.
        /// Triggers the <see cref="OnStay"/> event if any listeners are registered.
        /// </summary>
        /// <param name="other">The <see cref="Collider2D"/> currently staying inside the trigger.</param>
        private void OnTriggerStay(Collider other) => this.OnStay?.Invoke(other);
    }
}
#endif