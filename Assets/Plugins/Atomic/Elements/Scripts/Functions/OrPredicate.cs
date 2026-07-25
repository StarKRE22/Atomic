using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class OrPredicate : IPredicate
    {
        [SerializeReference, HideLabel]
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