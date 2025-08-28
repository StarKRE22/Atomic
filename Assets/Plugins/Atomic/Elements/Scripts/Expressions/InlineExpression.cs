using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// A flexible expression that uses a custom evaluation function to compute a result from a list of parameterless functions.
    /// </summary>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
    public class InlineExpression<R> : ExpressionBase<R>
    {
        private readonly Func<Enumerator, R> function;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineExpression{R}"/> class with a custom evaluation function.
        /// </summary>
        /// <param name="function">The function that defines how to evaluate the list of function enumerator.</param>
        public InlineExpression(Func<Enumerator, R> function) =>
            this.function = function;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineExpression{R}"/> class with a custom evaluation function and initial enumerator.
        /// </summary>
        /// <param name="function">The evaluation logic to be applied to the function enumerator.</param>
        /// <param name="enumerator">An array of function enumerator to add to the expression.</param>
        public InlineExpression(Func<Enumerator, R> function, params Func<R>[] enumerator) : base(enumerator) =>
            this.function = function;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineExpression{R}"/> class with a custom evaluation function and initial enumerator.
        /// </summary>
        /// <param name="function">The evaluation logic to be applied to the function enumerator.</param>
        /// <param name="enumerator">A collection of function enumerator to add to the expression.</param>
        public InlineExpression(Func<Enumerator, R> function, IEnumerable<Func<R>> enumerator) : base(enumerator) =>
            this.function = function;

        /// <summary>
        /// Invokes the expression using the custom evaluation logic.
        /// </summary>
        /// <param name="enumerator">The list of function enumerator to evaluate.</param>
        /// <returns>The result of the evaluation.</returns>
        protected override R Invoke(Enumerator enumerator) => this.function.Invoke(enumerator);
    }

    /// <summary>
    /// A generic expression that uses a custom function to evaluate a result from a list of single-parameter functions.
    /// </summary>
    /// <typeparam name="T">The input type passed to each function member.</typeparam>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
    public class InlineExpression<T, R> : ExpressionBase<T, R>
    {
        private readonly Func<Enumerator, T, R> function;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineExpression{T,R}"/> class with a custom evaluation function.
        /// </summary>
        /// <param name="function">The function that defines how to evaluate the expression given the enumerator and an input argument.</param>
        public InlineExpression(Func<Enumerator, T, R> function) =>
            this.function = function;

        /// <summary>
        /// Initializes the expression with a custom evaluation function and an array of function enumerator.
        /// </summary>
        /// <param name="function">The custom logic for evaluating the expression.</param>
        /// <param name="enumerator">An array of function enumerator to initialize with.</param>
        public InlineExpression(Func<Enumerator, T, R> function, params Func<T, R>[] enumerator) : base(enumerator) =>
            this.function = function;

        /// <summary>
        /// Initializes the expression with a custom evaluation function and a collection of function enumerator.
        /// </summary>
        /// <param name="function">The custom logic for evaluating the expression.</param>
        /// <param name="enumerator">A collection of function enumerator to initialize with.</param>
        public InlineExpression(Func<Enumerator, T, R> function, IEnumerable<Func<T, R>> enumerator) : base(enumerator) =>
            this.function = function;

        /// <summary>
        /// Invokes the expression using the custom evaluation logic.
        /// </summary>
        /// <param name="enumerator">The list of function enumerator.</param>
        /// <param name="arg">The input argument to pass to each function.</param>
        /// <returns>The result of the evaluation.</returns>
        protected override R Invoke(Enumerator enumerator, T arg) =>
            this.function.Invoke(enumerator, arg);
    }

    /// <summary>
    /// A binary-parameter expression that uses custom logic to evaluate the result from a list of functions.
    /// </summary>
    /// <typeparam name="T1">The first input parameter type.</typeparam>
    /// <typeparam name="T2">The second input parameter type.</typeparam>
    /// <typeparam name="R">The return type of the expression.</typeparam>
    [Serializable]
    public class InlineExpression<T1, T2, R> : ExpressionBase<T1, T2, R>
    {
        private readonly Func<Enumerator, T1, T2, R> function;

        /// <summary>
        /// Initializes a new instance of the <see cref="InlineExpression{T1,T2,R}"/> class with a custom evaluation function.
        /// </summary>
        /// <param name="function">The custom logic used to evaluate the expression.</param>
        public InlineExpression(Func<Enumerator, T1, T2, R> function) =>
            this.function = function;

        /// <summary>
        /// Initializes the expression with a custom evaluation function and an array of function enumerator.
        /// </summary>
        /// <param name="function">The function that defines how to evaluate the expression.</param>
        /// <param name="enumerator">An array of binary functions to initialize with.</param>
        public InlineExpression(Func<Enumerator, T1, T2, R> function, params Func<T1, T2, R>[] enumerator) : base(enumerator) =>
            this.function = function;

        /// <summary>
        /// Initializes the expression with a custom evaluation function and a collection of function enumerator.
        /// </summary>
        /// <param name="function">The function that defines how to evaluate the expression.</param>
        /// <param name="enumerator">A collection of binary functions to initialize with.</param>
        public InlineExpression(Func<Enumerator, T1, T2, R> function, IEnumerable<Func<T1, T2, R>> enumerator) : base(enumerator) =>
            this.function = function;

        /// <summary>
        /// Invokes the expression using the custom logic.
        /// </summary>
        /// <param name="enumerator">The list of function enumerator.</param>
        /// <param name="arg1">The first input argument.</param>
        /// <param name="arg2">The second input argument.</param>
        /// <returns>The result of the evaluation.</returns>
        protected override R Invoke(Enumerator enumerator, T1 arg1, T2 arg2) =>
            this.function.Invoke(enumerator, arg1, arg2);
    }
}