using System;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class AndPredicate : IPredicate
    {
        [SerializeReference, HideLabel]
        private IPredicate[] conditions;

        public AndPredicate()
        {
        }

        public AndPredicate(IPredicate[] conditions)
        {
            this.conditions = conditions;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Invoke()
        {
            if (this.conditions == null)
                return true;
            
            for (int i = 0, count = this.conditions.Length; i < count; i++)
                if (!this.conditions[i].Invoke())
                    return false;

            return true;
        }
    }
}