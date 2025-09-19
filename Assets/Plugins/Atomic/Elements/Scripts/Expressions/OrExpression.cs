using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a logical OR expression composed of parameterless boolean-returning functions.
    /// The expression evaluates to <c>true</c> if at least one function returns <c>true</c>.
    /// </summary>
    [Serializable]
    public class OrExpression : ExpressionBase<bool>, IPredicate
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="OrExpression"/> class.
        /// </summary>
        public OrExpression(int capacity = INITIAL_CAPACITY) : base(capacity)
        {
        }

        /// <summary>
        /// Initializes the expression with a collection of function members.
        /// </summary>
        /// <param name="members">A collection of functions that return <c>true</c> or <c>false</c>.</param>
        public OrExpression(IEnumerable<Func<bool>> members) : base(members)
        {
        }

        /// <summary>
        /// Initializes the expression with an array of function members.
        /// </summary>
        /// <param name="members">An array of functions that return <c>true</c> or <c>false</c>.</param>
        public OrExpression(params Func<bool>[] members) : base(members)
        {
        }

        protected override bool Invoke(Enumerator enumerator)
        {
            while (enumerator.MoveNext())
                if (enumerator.Current!.Invoke())
                    return true;

            return false;
        }
    }

    /// <summary>
    /// Represents a logical OR expression composed of functions that take a single argument and return a boolean value.
    /// </summary>
    /// <typeparam name="T">The input type for the predicate functions.</typeparam>
    [Serializable]
    public class OrExpression<T> : ExpressionBase<T, bool>, IPredicate<T>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="OrExpression{T}"/> class.
        /// </summary>
        public OrExpression(int capacity = INITIAL_CAPACITY) : base(capacity)
        {
        }


        /// <summary>
        /// Initializes the expression with an array of predicate functions.
        /// </summary>
        /// <param name="members">An array of functions that take <typeparamref name="T"/> and return a boolean.</param>
        public OrExpression(params Func<T, bool>[] members) : base(members)
        {
        }

        /// <summary>
        /// Initializes the expression with a collection of predicate functions.
        /// </summary>
        /// <param name="members">A collection of functions that take <typeparamref name="T"/> and return a boolean.</param>
        public OrExpression(IEnumerable<Func<T, bool>> members) : base(members)
        {
        }

        protected override bool Invoke(Enumerator enumerator, T arg)
        {
            while (enumerator.MoveNext())
                if (enumerator.Current!.Invoke(arg))
                    return true;

            return false;
        }
    }

    /// <summary>
    /// Represents a logical OR expression composed of functions that take two arguments and return a boolean value.
    /// </summary>
    /// <typeparam name="T1">The type of the first input argument.</typeparam>
    /// <typeparam name="T2">The type of the second input argument.</typeparam>
    [Serializable]
    public class OrExpression<T1, T2> : ExpressionBase<T1, T2, bool>, IPredicate<T1, T2>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="OrExpression{T1, T2}"/> class.
        /// </summary>
        public OrExpression(int capacity = INITIAL_CAPACITY) : base(capacity)
        {
        }

        /// <summary>
        /// Initializes the expression with an array of binary predicate functions.
        /// </summary>
        /// <param name="members">An array of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return a boolean.</param>
        public OrExpression(params Func<T1, T2, bool>[] members) : base(members)
        {
        }

        /// <summary>
        /// Initializes the expression with a collection of binary predicate functions.
        /// </summary>
        /// <param name="members">A collection of functions that take <typeparamref name="T1"/> and <typeparamref name="T2"/> and return a boolean.</param>
        public OrExpression(IEnumerable<Func<T1, T2, bool>> members) : base(members)
        {
        }

        protected override bool Invoke(Enumerator enumerator, T1 arg1, T2 arg2)
        {
            while (enumerator.MoveNext())
                if (enumerator.Current!.Invoke(arg1, arg2))
                    return true;

            return false;
        }
    }
}