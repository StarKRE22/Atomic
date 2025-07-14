using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// A MonoBehaviour that listens for 2D trigger events on this GameObject.
    /// Exposes those events through C# delegates for external subscriptions.
    /// </summary>
    public sealed class TriggerEventReceiver2D : MonoBehaviour
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
    }
}