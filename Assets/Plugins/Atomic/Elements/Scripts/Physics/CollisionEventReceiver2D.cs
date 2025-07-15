using System;
using UnityEngine;

namespace Atomic.Elements
{
    [AddComponentMenu("Atomic/Elements/Collision Event Receiver 2D")]
    public sealed class CollisionEventReceiver2D : MonoBehaviour
    {
        public event Action<Collision2D> OnEntered;
        public event Action<Collision2D> OnExited;
        public event Action<Collision2D> OnStay;

        private void OnCollisionEnter2D(Collision2D collision) => this.OnEntered?.Invoke(collision);

        private void OnCollisionExit2D(Collision2D collision) => this.OnExited?.Invoke(collision);

        private void OnCollisionStay2D(Collision2D collision) => this.OnStay?.Invoke(collision);
    }
}