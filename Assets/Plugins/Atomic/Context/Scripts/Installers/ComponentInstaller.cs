#if ODIN_INSPECTOR
using System;
using UnityEngine;

namespace Atomic.Contexts
{
    [Serializable]
    public sealed class ComponentInstaller : IContextInstaller
    {
        [ContextKey]
        [SerializeField]
        private int key;

        [SerializeField]
        private Component value;
        
        public void Install(IContext context)
        {
            context.AddValue(this.key, value);
        }
    }
}
#endif