using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class OrPredicate : IPredicate
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        [SerializeReference]
        private IPredicate[] conditions;

        public OrPredicate()
        {
        }

        public OrPredicate(IPredicate[] conditions)
        {
            this.conditions = conditions;
        }

        public bool Invoke()
        {
            if (this.conditions == null)
                return false;
            
            for (int i = 0, count = this.conditions.Length; i < count; i++)
                if (this.conditions[i].Invoke())
                    return true;

            return false;
        }
    }
}