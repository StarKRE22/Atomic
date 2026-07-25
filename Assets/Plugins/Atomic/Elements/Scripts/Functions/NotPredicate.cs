using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Atomic.Elements
{
    [Serializable]
    public sealed class NotPredicate : IPredicate
    {
        [SerializeReference, HideLabel]
        private IPredicate condition;
        
        public bool Invoke()
        {
            return this.condition != null && !this.condition.Invoke();
        }
    }
}