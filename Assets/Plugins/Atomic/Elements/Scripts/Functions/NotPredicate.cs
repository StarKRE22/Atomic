using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class NotPredicate : IPredicate
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeReference]
        private IPredicate condition;
        
        public bool Invoke()
        {
            return this.condition != null && !this.condition.Invoke();
        }
    }
}