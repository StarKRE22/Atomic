using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// A flexible expression that uses a custom evaluation function to compute a result from a list of parameterless functions.
    /// </summary>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
    public class BaseExpression<R> : AbstractExpression<R>
    {
        private readonly Func<IReadOnlyList<Func<R>>, R> function;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseExpression{R}"/> class with a custom evaluation function.
        /// </summary>
        /// <param name="function">The function that defines how to evaluate the list of function members.</param>
        public BaseExpression(Func<IReadOnlyList<Func<R>>, R> function) =>
            this.function = function;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseExpression{R}"/> class with a custom evaluation function and initial members.
        /// </summary>
        /// <param name="function">The evaluation logic to be applied to the function members.</param>
        /// <param name="members">An array of function members to add to the expression.</param>
        public BaseExpression(Func<IReadOnlyList<Func<R>>, R> function, params Func<R>[] members)
            : base(members) =>
            this.function = function;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseExpression{R}"/> class with a custom evaluation function and initial members.
        /// </summary>
        /// <param name="function">The evaluation logic to be applied to the function members.</param>
        /// <param name="members">A collection of function members to add to the expression.</param>
        public BaseExpression(Func<IReadOnlyList<Func<R>>, R> function, IEnumerable<Func<R>> members)
            : base(members) =>
            this.function = function;

        /// <summary>
        /// Invokes the expression using the custom evaluation logic.
        /// </summary>
        /// <param name="members">The list of function members to evaluate.</param>
        /// <returns>The result of the evaluation.</returns>
        protected override R Invoke(IReadOnlyList<Func<R>> members) => this.function.Invoke(members);
    }

    /// <summary>
    /// A generic expression that uses a custom function to evaluate a result from a list of single-parameter functions.
    /// </summary>
    /// <typeparam name="T">The input type passed to each function member.</typeparam>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
    public class BaseExpression<T, R> : AbstractExpression<T, R>
    {
        private readonly Func<IReadOnlyList<Func<T, R>>, T, R> function;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseExpression{T, R}"/> class with a custom evaluation function.
        /// </summary>
        /// <param name="function">The function that defines how to evaluate the expression given the members and an input argument.</param>
        public BaseExpression(Func<IReadOnlyList<Func<T, R>>, T, R> function) =>
            this.function = function;

        /// <summary>
        /// Initializes the expression with a custom evaluation function and an array of function members.
        /// </summary>
        /// <param name="function">The custom logic for evaluating the expression.</param>
        /// <param name="members">An array of function members to initialize with.</param>
        public BaseExpression(Func<IReadOnlyList<Func<T, R>>, T, R> function, params Func<T, R>[] members)
            : base(members) =>
            this.function = function;

        /// <summary>
        /// Initializes the expression with a custom evaluation function and a collection of function members.
        /// </summary>
        /// <param name="function">The custom logic for evaluating the expression.</param>
        /// <param name="members">A collection of function members to initialize with.</param>
        public BaseExpression(Func<IReadOnlyList<Func<T, R>>, T, R> function, IEnumerable<Func<T, R>> members)
            : base(members) =>
            this.function = function;

        /// <summary>
        /// Invokes the expression using the custom evaluation logic.
        /// </summary>
        /// <param name="members">The list of function members.</param>
        /// <param name="arg">The input argument to pass to each function.</param>
        /// <returns>The result of the evaluation.</returns>
        protected override R Invoke(IReadOnlyList<Func<T, R>> members, T arg) =>
            this.function.Invoke(members, arg);
    }

    /// <summary>
    /// A binary-parameter expression that uses custom logic to evaluate the result from a list of functions.
    /// </summary>
    /// <typeparam name="T1">The first input parameter type.</typeparam>
    /// <typeparam name="T2">The second input parameter type.</typeparam>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
    public class BaseExpression<T1, T2, R> : AbstractExpression<T1, T2, R>
    {
        private readonly Func<IReadOnlyList<Func<T1, T2, R>>, T1, T2, R> function;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseExpression{T1, T2, R}"/> class with a custom evaluation function.
        /// </summary>
        /// <param name="function">The custom logic used to evaluate the expression.</param>
        public BaseExpression(Func<IReadOnlyList<Func<T1, T2, R>>, T1, T2, R> function) =>
            this.function = function;

        /// <summary>
        /// Initializes the expression with a custom evaluation function and an array of function members.
        /// </summary>
        /// <param name="function">The function that defines how to evaluate the expression.</param>
        /// <param name="members">An array of binary functions to initialize with.</param>
        public BaseExpression(Func<IReadOnlyList<Func<T1, T2, R>>, T1, T2, R> function,
            params Func<T1, T2, R>[] members)
            : base(members) =>
            this.function = function;

        /// <summary>
        /// Initializes the expression with a custom evaluation function and a collection of function members.
        /// </summary>
        /// <param name="function">The function that defines how to evaluate the expression.</param>
        /// <param name="members">A collection of binary functions to initialize with.</param>
        public BaseExpression(Func<IReadOnlyList<Func<T1, T2, R>>, T1, T2, R> function,
            IEnumerable<Func<T1, T2, R>> members)
            : base(members) =>
            this.function = function;

        /// <summary>
        /// Invokes the expression using the custom logic.
        /// </summary>
        /// <param name="members">The list of function members.</param>
        /// <param name="arg1">The first input argument.</param>
        /// <param name="arg2">The second input argument.</param>
        /// <returns>The result of the evaluation.</returns>
        protected override R Invoke(IReadOnlyList<Func<T1, T2, R>> members, T1 arg1, T2 arg2) =>
            this.function.Invoke(members, arg1, arg2);
    }
}