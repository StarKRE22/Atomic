using System;
using UnityEngine;

namespace Atomic.Elements
{
    public sealed class TriggerEventReceiver2D : MonoBehaviour
    {
        public event Action<Collider2D> OnEntered;
        public event Action<Collider2D> OnExited;

        private void OnTriggerEnter2D(Collider2D other)
        {
            this.OnEntered?.Invoke(other);
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            this.OnExited?.Invoke(other);
        }
    }
}