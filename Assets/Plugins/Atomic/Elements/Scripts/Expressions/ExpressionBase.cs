using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a base implementation of <see cref="IExpression{R}"/> that aggregates multiple parameterless functions returning a value of type <typeparamref name="R"/>.
    /// Inherits from <see cref="ReactiveLinkedList{T}"/> and provides dynamic evaluation of its function members.
    /// </summary>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class ExpressionBase<R> : ReactiveLinkedList<Func<R>>, IExpression<R>
    {
        /// <summary>
        /// Gets the evaluated result of the expression by invoking all registered function members.
        /// </summary>
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public R Value => this.Invoke();

        /// <summary>
        /// Invokes all function members of the expression and returns the result.
        /// </summary>
        /// <returns>The evaluated result of type <typeparamref name="R"/>.</returns>
        public R Invoke() => this.Invoke(new Enumerator(this));

        /// <summary>
        /// Template method that evaluates the expression using the specified enumerator over the function members.
        /// Must be implemented by derived classes to define how the functions are aggregated.
        /// </summary>
        /// <param name="enumerator">Enumerator over the function members.</param>
        /// <returns>The aggregated result of type <typeparamref name="R"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract R Invoke(Enumerator enumerator);

        /// <summary>Initializes an empty expression with the specified capacity.</summary>
        protected ExpressionBase(int capacity = INITIAL_CAPACITY) : base(capacity)
        {
        }

        /// <summary>Initializes the expression with an array of function members.</summary>
        protected ExpressionBase(params Func<R>[] members) : base(members)
        {
        }

        /// <summary>Initializes the expression with an enumerable of function members.</summary>
        protected ExpressionBase(IEnumerable<Func<R>> members) : base(members)
        {
        }
    }

    /// <summary>
    /// Represents a base implementation of <see cref="IExpression{T, R}"/> that aggregates multiple functions with one input parameter of type <typeparamref name="T"/>.
    /// Inherits from <see cref="ReactiveLinkedList{T}"/> and provides dynamic evaluation of its function members.
    /// </summary>
    /// <typeparam name="T">The input parameter type of the functions.</typeparam>
    /// <typeparam name="R">The return type of the functions.</typeparam>
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class ExpressionBase<T, R> : ReactiveLinkedList<Func<T, R>>, IExpression<T, R>
    {
        /// <summary>
        /// Invokes all function members of the expression with the specified argument and returns the aggregated result.
        /// </summary>
        /// <param name="arg">The input argument of type <typeparamref name="T"/>.</param>
        /// <returns>The aggregated result of type <typeparamref name="R"/>.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T arg) => this.Invoke(new Enumerator(this), arg);

        /// <summary>
        /// Template method that evaluates the expression using the specified enumerator over the function members and the input argument.
        /// Must be implemented by derived classes to define how the functions are aggregated.
        /// </summary>
        /// <param name="enumerator">Enumerator over the function members.</param>
        /// <param name="arg">The input argument of type <typeparamref name="T"/>.</param>
        /// <returns>The aggregated result of type <typeparamref name="R"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract R Invoke(Enumerator enumerator, T arg);

        protected ExpressionBase(int capacity = INITIAL_CAPACITY) : base(capacity)
        {
        }

        protected ExpressionBase(params Func<T, R>[] members) : base(members)
        {
        }

        protected ExpressionBase(IEnumerable<Func<T, R>> members) : base(members)
        {
        }
    }

    /// <summary>
    /// Represents a base implementation of <see cref="IExpression{T1, T2, R}"/> that aggregates multiple functions with two input parameters of type <typeparamref name="T1"/> and <typeparamref name="T2"/>.
    /// Inherits from <see cref="ReactiveLinkedList{T}"/> and provides dynamic evaluation of its function members.
    /// </summary>
    /// <typeparam name="T1">The first input parameter type of the functions.</typeparam>
    /// <typeparam name="T2">The second input parameter type of the functions.</typeparam>
    /// <typeparam name="R">The return type of the functions.</typeparam>
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class ExpressionBase<T1, T2, R> : ReactiveLinkedList<Func<T1, T2, R>>, IExpression<T1, T2, R>
    {
        /// <summary>
        /// Invokes all function members of the expression with the specified arguments and returns the aggregated result.
        /// </summary>
        /// <param name="arg1">The first input argument of type <typeparamref name="T1"/>.</param>
        /// <param name="arg2">The second input argument of type <typeparamref name="T2"/>.</param>
        /// <returns>The aggregated result of type <typeparamref name="R"/>.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T1 arg1, T2 arg2) => this.Invoke(new Enumerator(this), arg1, arg2);

        /// <summary>
        /// Template method that evaluates the expression using the specified enumerator over the function members and the input arguments.
        /// Must be implemented by derived classes to define how the functions are aggregated.
        /// </summary>
        /// <param name="enumerator">Enumerator over the function members.</param>
        /// <param name="arg1">The first input argument of type <typeparamref name="T1"/>.</param>
        /// <param name="arg2">The second input argument of type <typeparamref name="T2"/>.</param>
        /// <returns>The aggregated result of type <typeparamref name="R"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected abstract R Invoke(Enumerator enumerator, T1 arg1, T2 arg2);

        protected ExpressionBase(int capacity = INITIAL_CAPACITY) : base(capacity)
        {
        }

        protected ExpressionBase(params Func<T1, T2, R>[] members) : base(members)
        {
        }

        protected ExpressionBase(IEnumerable<Func<T1, T2, R>> members) : base(members)
        {
        }
    }
}