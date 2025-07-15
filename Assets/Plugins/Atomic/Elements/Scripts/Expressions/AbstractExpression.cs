using System;
using System.Collections.Generic;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a base implementation of <see cref="IExpression{R}"/> that aggregates multiple parameterless functions returning values of type <typeparamref name="R"/>.
    /// Supports dynamic modification and invocation of its function members.
    /// </summary>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class AbstractExpression<R> : IExpression<R>
    {
        private readonly List<Func<R>> members;

        /// <summary>
        /// Gets the evaluated result of the expression by invoking all registered function members.
        /// </summary>
        public R Value => this.Invoke(this.members);

        /// <summary>
        /// Gets the number of function members currently contained in the expression.
        /// </summary>
        public int Count => this.members.Count;

        /// <summary>
        /// Initializes a new empty instance of the <see cref="AbstractExpression{R}"/> class.
        /// </summary>
        protected AbstractExpression() => this.members = new List<Func<R>>();

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of functions to initialize the expression with.</param>
        protected AbstractExpression(params Func<R>[] members) => this.members = new List<Func<R>>(members);

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">A collection of functions to initialize the expression with.</param>
        protected AbstractExpression(IEnumerable<Func<R>> members) => this.members = new List<Func<R>>(members);

        /// <inheritdoc/>
        public void Add(Func<R> member)
        {
            if (member != null) this.members.Add(member);
        }

        /// <inheritdoc/>
        public void Remove(Func<R> member)
        {
            if (member != null) this.members.Remove(member);
        }

        /// <inheritdoc/>
        public bool Contains(Func<R> member) => this.members.Contains(member);

        /// <inheritdoc/>
        public void Clear() => this.members.Clear();

        /// <summary>
        /// Invokes the expression using all registered function members.
        /// </summary>
        /// <returns>The result of the expression.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke() => this.Invoke(this.members);

        /// <summary>
        /// Template method that evaluates the expression using the specified list of function members.
        /// </summary>
        /// <param name="members">The list of function members to evaluate.</param>
        /// <returns>The result of the expression.</returns>
        protected abstract R Invoke(IReadOnlyList<Func<R>> members);
    }

    /// <summary>
    /// Represents a base implementation of <see cref="IExpression{T, R}"/> that aggregates multiple single-parameter functions.
    /// </summary>
    /// <typeparam name="T">The input type of the functions.</typeparam>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class AbstractExpression<T, R> : IExpression<T, R>
    {
        private readonly List<Func<T, R>> members;

        /// <summary>
        /// Gets the number of function members in the expression.
        /// </summary>
        public int Count => this.members.Count;

        /// <summary>
        /// Initializes a new empty instance of the <see cref="AbstractExpression{T, R}"/> class.
        /// </summary>
        protected AbstractExpression() => this.members = new List<Func<T, R>>();

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of functions to initialize the expression with.</param>
        protected AbstractExpression(params Func<T, R>[] members) => this.members = new List<Func<T, R>>(members);

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">A collection of functions to initialize the expression with.</param>
        protected AbstractExpression(IEnumerable<Func<T, R>> members) => this.members = new List<Func<T, R>>(members);

        /// <inheritdoc/>
        public void Add(Func<T, R> member)
        {
            if (member != null) this.members.Add(member);
        }

        /// <inheritdoc/>
        public void Remove(Func<T, R> member)
        {
            if (member != null) this.members.Remove(member);
        }

        /// <inheritdoc/>
        public bool Contains(Func<T, R> member) => this.members.Contains(member);

        /// <inheritdoc/>
        public void Clear() => this.members.Clear();

        /// <summary>
        /// Invokes the expression with the given input argument.
        /// </summary>
        /// <param name="args">The input argument to pass to all functions.</param>
        /// <returns>The result of the evaluated expression.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T args) => this.Invoke(this.members, args);

        /// <summary>
        /// Template method that evaluates the expression using the specified list of function members.
        /// </summary>
        /// <param name="members">The list of function members.</param>
        /// <param name="args">The input argument to pass to each function.</param>
        /// <returns>The result of the expression.</returns>
        protected abstract R Invoke(IReadOnlyList<Func<T, R>> members, T args);
    }

    /// <summary>
    /// Represents a base implementation of <see cref="IExpression{T1, T2, R}"/> that aggregates multiple binary functions.
    /// </summary>
    /// <typeparam name="T1">The first input type.</typeparam>
    /// <typeparam name="T2">The second input type.</typeparam>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    public abstract class AbstractExpression<T1, T2, R> : IExpression<T1, T2, R>
    {
        private readonly List<Func<T1, T2, R>> members;

        /// <summary>
        /// Gets the number of function members in the expression.
        /// </summary>
        public int Count => this.members.Count;

        /// <summary>
        /// Initializes a new empty instance of the <see cref="AbstractExpression{T1, T2, R}"/> class.
        /// </summary>
        protected AbstractExpression() => this.members = new List<Func<T1, T2, R>>();

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of functions to initialize the expression with.</param>
        protected AbstractExpression(params Func<T1, T2, R>[] members) =>
            this.members = new List<Func<T1, T2, R>>(members);

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">A collection of functions to initialize the expression with.</param>
        protected AbstractExpression(IEnumerable<Func<T1, T2, R>> members) =>
            this.members = new List<Func<T1, T2, R>>(members);

        /// <inheritdoc/>
        public void Add(Func<T1, T2, R> member)
        {
            if (member != null) this.members.Add(member);
        }

        /// <inheritdoc/>
        public void Remove(Func<T1, T2, R> member)
        {
            if (member != null) this.members.Remove(member);
        }

        /// <inheritdoc/>
        public bool Contains(Func<T1, T2, R> member) => this.members.Contains(member);

        /// <inheritdoc/>
        public void Clear() => this.members.Clear();

        /// <summary>
        /// Invokes the expression using the given input arguments.
        /// </summary>
        /// <param name="arg1">The first argument to pass to the functions.</param>
        /// <param name="arg2">The second argument to pass to the functions.</param>
        /// <returns>The result of the evaluated expression.</returns>
#if ODIN_INSPECTOR
        [Button]
#endif
        public R Invoke(T1 arg1, T2 arg2) => this.Invoke(this.members, arg1, arg2);

        /// <summary>
        /// Template method that evaluates the expression using the specified list of function members and input arguments.
        /// </summary>
        /// <param name="members">The list of function members.</param>
        /// <param name="arg1">The first input argument.</param>
        /// <param name="arg2">The second input argument.</param>
        /// <returns>The result of the expression.</returns>
        protected abstract R Invoke(IReadOnlyList<Func<T1, T2, R>> members, T1 arg1, T2 arg2);
    }
}