using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a predicate that returns the logical negation
    /// of another predicate.
    /// </summary>
    [Serializable]
    public sealed class NotPredicate : IPredicate
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        /// <summary>
        /// The predicate to negate.
        /// </summary>
        [SerializeReference]
        private IPredicate condition;

        /// <summary>
        /// Evaluates the predicate and returns its logical negation.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if the assigned predicate evaluates to
        /// <see langword="false"/>; otherwise, <see langword="false"/>.
        /// Returns <see langword="false"/> if no predicate is assigned.
        /// </returns>
        public bool Invoke()
        {
            return this.condition != null && !this.condition.Invoke();
        }
    }
}
