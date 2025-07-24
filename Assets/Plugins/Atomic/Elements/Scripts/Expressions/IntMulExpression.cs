using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents an expression that computes the product of multiple parameterless integer-returning functions.
    /// </summary>
    [Serializable]
    public class IntMulExpression : ExpressionAbstract<int>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="IntMulExpression"/> class.
        /// </summary>
        public IntMulExpression() { }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of integer-returning functions.</param>
        public IntMulExpression(params Func<int>[] members) : base(members) { }

        /// <summary>
        /// Initializes the expression with a collection of function members.
        /// </summary>
        /// <param name="members">A collection of integer-returning functions.</param>
        public IntMulExpression(IEnumerable<Func<int>> members) : base(members) { }

        /// <summary>
        /// Evaluates the expression by multiplying the results of all function members.
        /// </summary>
        /// <param name="members">The list of function members.</param>
        /// <returns>The product of all evaluated functions, or 1 if none exist.</returns>
        protected override int Invoke(IReadOnlyList<Func<int>> members)
        {
            int result = 1;
            int count = members.Count;
            if (count == 0)
                return result;

            for (int i = 0; i < count; i++)
                result *= members[i].Invoke();

            return result;
        }
    }

    /// <summary>
    /// Represents an expression that computes the product of integer values returned from functions with a single input parameter.
    /// </summary>
    /// <typeparam name="T">The input parameter type.</typeparam>
    [Serializable]
    public class IntMulExpression<T> : ExpressionAbstract<T, int>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="IntMulExpression{T}"/> class.
        /// </summary>
        public IntMulExpression() { }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of functions that take a <typeparamref name="T"/> and return an integer.</param>
        public IntMulExpression(params Func<T, int>[] members) : base(members) { }

        /// <summary>
        /// Initializes the expression with a collection of function members.
        /// </summary>
        /// <param name="members">A collection of functions that take a <typeparamref name="T"/> and return an integer.</param>
        public IntMulExpression(IEnumerable<Func<T, int>> members) : base(members) { }

        /// <summary>
        /// Evaluates the expression by multiplying the results of all function members for a given input.
        /// </summary>
        /// <param name="members">The list of function members.</param>
        /// <param name="arg">The input value passed to each function.</param>
        /// <returns>The product of all evaluated results, or 1 if none exist.</returns>
        protected override int Invoke(IReadOnlyList<Func<T, int>> members, T arg)
        {
            int result = 1;
            int count = members.Count;
            if (count == 0)
                return result;

            for (int i = 0; i < count; i++)
                result *= members[i].Invoke(arg);

            return result;
        }
    }

    /// <summary>
    /// Represents an expression that computes the product of integer values returned from functions with two input parameters.
    /// </summary>
    /// <typeparam name="T1">The first input parameter type.</typeparam>
    /// <typeparam name="T2">The second input parameter type.</typeparam>
    [Serializable]
    public class IntMulExpression<T1, T2> : ExpressionAbstract<T1, T2, int>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="IntMulExpression{T1, T2}"/> class.
        /// </summary>
        public IntMulExpression() { }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return an integer.</param>
        public IntMulExpression(params Func<T1, T2, int>[] members) : base(members) { }

        /// <summary>
        /// Initializes the expression with a collection of function members.
        /// </summary>
        /// <param name="members">A collection of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return an integer.</param>
        public IntMulExpression(IEnumerable<Func<T1, T2, int>> members) : base(members) { }

        /// <summary>
        /// Evaluates the expression by multiplying the results of all function members for the given input values.
        /// </summary>
        /// <param name="members">The list of function members.</param>
        /// <param name="arg1">The first input value.</param>
        /// <param name="arg2">The second input value.</param>
        /// <returns>The product of all evaluated results, or 1 if none exist.</returns>
        protected override int Invoke(IReadOnlyList<Func<T1, T2, int>> members, T1 arg1, T2 arg2)
        {
            int result = 1;
            int count = members.Count;
            if (count == 0)
                return result;

            for (int i = 0; i < count; i++)
                result *= members[i].Invoke(arg1, arg2);

            return result;
        }
    }
}