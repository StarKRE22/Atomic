#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// A MonoBehaviour that listens for 2D trigger events on this GameObject.
    /// Exposes those events through C# delegates for external subscriptions.
    /// </summary>
    [AddComponentMenu("Atomic/Elements/Trigger Events 2D")]
    [DisallowMultipleComponent]
    [HelpURL("https://github.com/StarKRE22/Atomic/blob/main/Docs/Elements/Utils/TriggerEvents2D.md")]
    public sealed class TriggerEvents2D : MonoBehaviour
    {
        /// <summary>
        /// Invoked when a 2D collider enters the trigger area of this GameObject.
        /// </summary>
        public event Action<Collider2D> OnEntered;

        /// <summary>
        /// Invoked when a 2D collider exits the trigger area of this GameObject.
        /// </summary>
        public event Action<Collider2D> OnExited;

        /// <summary>
        /// Event triggered every frame while another collider remains within this 2D trigger collider.
        /// This corresponds to Unity's <see cref="MonoBehaviour.OnTriggerStay(UnityEngine.Collider2D)"/> callback.
        /// </summary>
        public event Action<Collider2D> OnStay; 

        /// <summary>
        /// Unity callback invoked when another 2D collider enters this trigger.
        /// Calls the <see cref="OnEntered"/> event.
        /// </summary>
        /// <param name="other">The 2D collider that entered the trigger area.</param>
        private void OnTriggerEnter2D(Collider2D other) => this.OnEntered?.Invoke(other);

        /// <summary>
        /// Unity callback invoked when another 2D collider exits this trigger.
        /// Calls the <see cref="OnExited"/> event.
        /// </summary>
        /// <param name="other">The 2D collider that exited the trigger area.</param>
        private void OnTriggerExit2D(Collider2D other) => this.OnExited?.Invoke(other);

        /// <summary>
        /// Unity callback invoked every frame while another 2D collider remains within this trigger collider.
        /// Triggers the <see cref="OnStay"/> event if any listeners are registered.
        /// </summary>
        /// <param name="other">The <see cref="Collider2D"/> currently staying inside the trigger.</param>
        private void OnTriggerStay2D(Collider2D other) => this.OnStay?.Invoke(other);
    }
}
#endif