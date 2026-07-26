using Atomic.Entities;
using UnityEngine;

namespace Game.UI
{
    [RequireComponent(typeof(ParticleSystem))]
    public sealed class PoolableParticle : MonoBehaviour
    {
        private UIContext uiContext;

        private void Awake()
        {
            uiContext = UIContext.Instance;
        }

        private void OnParticleSystemStopped()
        {
            uiContext.GetValue(UIContextAPI.GameObjectPrefabPool).Return(this.gameObject);
        }
    }
}