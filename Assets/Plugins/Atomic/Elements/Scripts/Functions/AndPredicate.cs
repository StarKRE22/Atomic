using System;
using System.Runtime.CompilerServices;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a composite predicate that evaluates to <see langword="true"/>
    /// only if all contained predicates evaluate to <see langword="true"/>.
    /// </summary>
    [Serializable]
    public sealed class AndPredicate : IPredicate
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
        /// Initializes a new instance of the <see cref="AndPredicate"/> class.
        /// </summary>
        public AndPredicate()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AndPredicate"/> class
        /// with the specified predicates.
        /// </summary>
        /// <param name="conditions">
        /// The predicates that must all evaluate to <see langword="true"/>.
        /// </param>
        public AndPredicate(IPredicate[] conditions)
        {
            this.conditions = conditions;
        }

        /// <summary>
        /// Evaluates all contained predicates.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if every predicate evaluates to <see langword="true"/>,
        /// or if no predicates are assigned; otherwise, <see langword="false"/>.
        /// </returns>
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
