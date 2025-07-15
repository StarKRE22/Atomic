using System;
using UnityEngine;

namespace Atomic.Elements
{
    [AddComponentMenu("Atomic/Elements/Collision Event Receiver")]
    public sealed class CollisionEventReceiver : MonoBehaviour
    {
        public event Action<Collision> OnEntered;
        public event Action<Collision> OnExited;
        public event Action<Collision> OnStay;

        private void OnCollisionEnter(Collision collision) => this.OnEntered?.Invoke(collision);

        private void OnCollisionExit(Collision collision) => this.OnExited?.Invoke(collision);

        private void OnCollisionStay(Collision collision) => this.OnStay?.Invoke(collision);
    }
}