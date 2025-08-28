using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a base implementation of <see cref="IExpression{R}"/> that aggregates multiple parameterless functions returning values of type <typeparamref name="R"/>.
    /// Supports dynamic modification and invocation of its function members via a linked-chain-like structure.
    /// </summary>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class ExpressionBase<R> : LinkedList<Func<R>>, IExpression<R>
    {
        
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public R Value => this.Invoke();
        
#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke() => this.Invoke(new Enumerator(this)); 

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract R Invoke(Enumerator enumerator);

        protected ExpressionBase(int capacity = INITIAL_CAPACITY) : base(capacity)
        {
        }

        protected ExpressionBase(params Func<R>[] members) : base(members)
        {
        }

        protected ExpressionBase(IEnumerable<Func<R>> members) : base(members)
        {
        }
    }
}