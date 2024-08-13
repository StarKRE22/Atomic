using UnityEngine;

namespace GameExample.Engine
{
    [DisallowMultipleComponent]
    public class TriggerEventReceiver : MonoBehaviour
    {
        public event System.Action<Collider> OnEntered;
        public event System.Action<Collider> OnExited;
        
        private void OnTriggerEnter(Collider collider)
        {
            this.OnEntered?.Invoke(collider);
        }

        private void OnTriggerExit(Collider collider)
        {
            this.OnExited?.Invoke(collider);
        }
    }
}