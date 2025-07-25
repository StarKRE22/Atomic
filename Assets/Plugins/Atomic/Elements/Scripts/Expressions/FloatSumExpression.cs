using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents an expression that computes the sum of multiple parameterless float-returning functions.
    /// </summary>
    [Serializable]
    public class FloatSumExpression : ExpressionBase<float>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="FloatSumExpression"/> class.
        /// </summary>
        public FloatSumExpression() { }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of float-returning functions.</param>
        public FloatSumExpression(params Func<float>[] members) : base(members) { }

        /// <summary>
        /// Initializes the expression with a collection of function members.
        /// </summary>
        /// <param name="members">A collection of float-returning functions.</param>
        public FloatSumExpression(IEnumerable<Func<float>> members) : base(members) { }

        /// <summary>
        /// Evaluates the expression by summing the results of all function members.
        /// </summary>
        /// <param name="members">The list of function members.</param>
        /// <returns>The sum of all evaluated functions, or 0 if none exist.</returns>
        protected override float Invoke(IReadOnlyList<Func<float>> members)
        {
            int count = members.Count;
            if (count == 0)
                return 0;

            float result = 0;

            for (int i = 0; i < count; i++) 
                result += members[i].Invoke();

            return result;
        }
    }

    /// <summary>
    /// Represents an expression that computes the sum of float values returned from functions with a single input parameter.
    /// </summary>
    /// <typeparam name="T">The input parameter type.</typeparam>
    [Serializable]
    public class FloatSumExpression<T> : ExpressionBase<T, float>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="FloatSumExpression{T}"/> class.
        /// </summary>
        public FloatSumExpression() { }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of functions that take a <typeparamref name="T"/> and return a float.</param>
        public FloatSumExpression(params Func<T, float>[] members) : base(members) { }

        /// <summary>
        /// Initializes the expression with the specified collection of function members.
        /// </summary>
        /// <param name="members">A collection of functions that take a <typeparamref name="T"/> and return a float.</param>
        public FloatSumExpression(IEnumerable<Func<T, float>> members) : base(members) { }

        /// <summary>
        /// Evaluates the expression by summing the results of all function members for a given input.
        /// </summary>
        /// <param name="members">The list of function members.</param>
        /// <param name="arg">The input value passed to each function.</param>
        /// <returns>The sum of all evaluated results, or 0 if none exist.</returns>
        protected override float Invoke(IReadOnlyList<Func<T, float>> members, T arg)
        {
            int count = members.Count;
            if (count == 0)
                return 0;

            float result = 0;

            for (int i = 0; i < count; i++) 
                result += members[i].Invoke(arg);

            return result;
        }
    }

    /// <summary>
    /// Represents an expression that computes the sum of float values returned from functions with two input parameters.
    /// </summary>
    /// <typeparam name="T1">The first input parameter type.</typeparam>
    /// <typeparam name="T2">The second input parameter type.</typeparam>
    [Serializable]
    public class FloatSumExpression<T1, T2> : ExpressionBase<T1, T2, float>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="FloatSumExpression{T1, T2}"/> class.
        /// </summary>
        public FloatSumExpression() { }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return a float.</param>
        public FloatSumExpression(params Func<T1, T2, float>[] members) : base(members) { }

        /// <summary>
        /// Initializes the expression with the specified collection of function members.
        /// </summary>
        /// <param name="members">A collection of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return a float.</param>
        public FloatSumExpression(IEnumerable<Func<T1, T2, float>> members) : base(members) { }

        /// <summary>
        /// Evaluates the expression by summing the results of all function members for the given input values.
        /// </summary>
        /// <param name="members">The list of function members.</param>
        /// <param name="arg1">The first input value.</param>
        /// <param name="arg2">The second input value.</param>
        /// <returns>The sum of all evaluated results, or 0 if none exist.</returns>
        protected override float Invoke(IReadOnlyList<Func<T1, T2, float>> members, T1 arg1, T2 arg2)
        {
            int count = members.Count;
            if (count == 0)
                return 0;

            float result = 0;

            for (int i = 0; i < count; i++) 
                result += members[i].Invoke(arg1, arg2);

            return result;
        }
    }
}