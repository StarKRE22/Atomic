using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents an expression that computes the product of multiple parameterless integer-returning functions.
    /// </summary>
    [Serializable]
    public class IntMulExpression : ExpressionBase<int>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="IntMulExpression"/> class.
        /// </summary>
        public IntMulExpression(int capacity = INITIAL_CAPACITY) : base(capacity) { }

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

        protected override int Invoke(Enumerator enumerator)
        {
            int result = 1;
            while (enumerator.MoveNext())
                result *= enumerator.Current!.Invoke(); 

            return result;
        }
    }

    /// <summary>
    /// Represents an expression that computes the product of integer values returned from functions with a single input parameter.
    /// </summary>
    /// <typeparam name="T">The input parameter type.</typeparam>
    [Serializable]
    public class IntMulExpression<T> : ExpressionBase<T, int>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="IntMulExpression{T}"/> class.
        /// </summary>
        public IntMulExpression(int capacity = INITIAL_CAPACITY) : base(capacity) { }

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

        protected override int Invoke(Enumerator enumerator, T arg)
        {
            int result = 1;
            while (enumerator.MoveNext())
                result *= enumerator.Current!.Invoke(arg); 

            return result;
        }
    }

    /// <summary>
    /// Represents an expression that computes the product of integer values returned from functions with two input parameters.
    /// </summary>
    /// <typeparam name="T1">The first input parameter type.</typeparam>
    /// <typeparam name="T2">The second input parameter type.</typeparam>
    [Serializable]
    public class IntMulExpression<T1, T2> : ExpressionBase<T1, T2, int>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="IntMulExpression{T1, T2}"/> class.
        /// </summary>
        public IntMulExpression(int capacity = INITIAL_CAPACITY) : base(capacity) { }

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

        protected override int Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
        {
            int result = 1;
            while (enumerator.MoveNext())
                result *= enumerator.Current!.Invoke(arg1, arg2); 

            return result;
        }
    }
}