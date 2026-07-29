using System;
using UnityEngine;

namespace Atomic.Elements
{
    public sealed class RendererEvents : MonoBehaviour
    {
        public event Action<bool> OnVisible;
        
        private void OnBecameVisible()
        {
            this.OnVisible?.Invoke(true);
        }

        private void OnBecameInvisible()
        {
            this.OnVisible?.Invoke(false);
        }
    }
}