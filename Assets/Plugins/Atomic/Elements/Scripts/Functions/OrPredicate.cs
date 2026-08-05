using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a composite predicate that evaluates to <see langword="true"/>
    /// if at least one contained predicate evaluates to <see langword="true"/>.
    /// </summary>
    [Serializable]
    public sealed class OrPredicate : IPredicate
    {
#if ODIN_INSPECTOR
        [HideLabel]
#endif
        /// <summary>
        /// The collection of predicates to evaluate.
        /// </summary>
        [SerializeReference]
        private IPredicate[] conditions;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrPredicate"/> class.
        /// </summary>
        public OrPredicate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="OrPredicate"/> class
        /// with the specified predicates.
        /// </summary>
        /// <param name="conditions">
        /// The predicates to evaluate.
        /// </param>
        public OrPredicate(IPredicate[] conditions)
        {
            this.conditions = conditions;
        }

        /// <summary>
        /// Evaluates the contained predicates.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if at least one predicate evaluates to
        /// <see langword="true"/>; otherwise, <see langword="false"/>.
        /// Returns <see langword="false"/> if no predicates are assigned.
        /// </returns>
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
