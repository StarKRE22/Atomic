using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents an expression that computes the sum of multiple parameterless integer-returning functions.
    /// </summary>
    [Serializable]
    public class IntSumExpression : ExpressionBase<int>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="IntSumExpression"/> class.
        /// </summary>
        public IntSumExpression(int capacity = INITIAL_CAPACITY) : base(capacity)
        {
        }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of functions that return an integer value.</param>
        public IntSumExpression(params Func<int>[] members) : base(members)
        {
        }

        /// <summary>
        /// Initializes the expression with a collection of function members.
        /// </summary>
        /// <param name="members">A collection of functions that return an integer value.</param>
        public IntSumExpression(IEnumerable<Func<int>> members) : base(members)
        {
        }

        protected override int Invoke(Enumerator enumerator)
        {
            int result = 0;
            while (enumerator.MoveNext())
                result += enumerator.Current!.Invoke();

            return result;
        }
    }

    /// <summary>
    /// Represents an expression that computes the sum of integer values returned from functions with a single input parameter.
    /// </summary>
    /// <typeparam name="T">The input parameter type.</typeparam>
    [Serializable]
    public class IntSumExpression<T> : ExpressionBase<T, int>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="IntSumExpression{T}"/> class.
        /// </summary>
        public IntSumExpression(int capacity = INITIAL_CAPACITY) : base(capacity)
        {
        }

        /// <summary>
        /// Initializes the expression with a collection of function members.
        /// </summary>
        /// <param name="members">A collection of functions that take <typeparamref name="T"/> and return an integer value.</param>
        public IntSumExpression(IEnumerable<Func<T, int>> members) : base(members)
        {
        }

        /// <summary>
        /// Initializes the expression with an array of function members.
        /// </summary>
        /// <param name="members">An array of functions that take <typeparamref name="T"/> and return an integer value.</param>
        public IntSumExpression(params Func<T, int>[] members) : base(members)
        {
        }

        protected override int Invoke(Enumerator enumerator, T arg)
        {
            int result = 0;
            while (enumerator.MoveNext())
                result += enumerator.Current!.Invoke(arg);

            return result;
        }
    }

    /// <summary>
    /// Represents an expression that computes the sum of integer values returned from functions with two input parameters.
    /// </summary>
    /// <typeparam name="T1">The first input parameter type.</typeparam>
    /// <typeparam name="T2">The second input parameter type.</typeparam>
    [Serializable]
    public class IntSumExpression<T1, T2> : ExpressionBase<T1, T2, int>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="IntSumExpression{T1, T2}"/> class.
        /// </summary>
        public IntSumExpression(int capacity = INITIAL_CAPACITY) : base(capacity)
        {
        }

        /// <summary>
        /// Initializes the expression with a collection of function members.
        /// </summary>
        /// <param name="members">A collection of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return an integer.</param>
        public IntSumExpression(IEnumerable<Func<T1, T2, int>> members) : base(members)
        {
        }

        /// <summary>
        /// Initializes the expression with an array of function members.
        /// </summary>
        /// <param name="members">An array of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return an integer.</param>
        public IntSumExpression(params Func<T1, T2, int>[] members) : base(members)
        {
        }

        protected override int Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
        {
            int result = 0;
            while (enumerator.MoveNext())
                result += enumerator.Current!.Invoke(arg1, arg2);

            return result;
        }
    }
}