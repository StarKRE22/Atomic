using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a logical AND expression composed of multiple parameterless boolean-returning functions.
    /// The expression returns <c>true</c> if all functions return <c>true</c>, or if no functions are present.
    /// </summary>
    [Serializable]
    public class AndExpression : ExpressionAbstract<bool>, IPredicate
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="AndExpression"/> class.
        /// </summary>
        public AndExpression()
        {
        }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of parameterless boolean-returning functions.</param>
        public AndExpression(params Func<bool>[] members) : base(members)
        {
        }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">A collection of parameterless boolean-returning functions.</param>
        public AndExpression(IEnumerable<Func<bool>> members) : base(members)
        {
        }

        /// <summary>
        /// Evaluates the logical AND expression by invoking each function in sequence.
        /// </summary>
        /// <param name="members">The list of boolean-returning function members.</param>
        /// <returns><c>true</c> if all functions return <c>true</c> or if none exist; otherwise, <c>false</c>.</returns>
        protected override bool Invoke(IReadOnlyList<Func<bool>> members)
        {
            int count = members.Count;
            if (count == 0)
                return true;

            for (int i = 0; i < count; i++)
                if (!members[i].Invoke())
                    return false;

            return true;
        }
    }

    /// <summary>
    /// Represents a generic logical AND expression composed of functions that take a single parameter of type <typeparamref name="T"/> and return a boolean.
    /// The expression returns <c>true</c> if all functions return <c>true</c> for the given input.
    /// </summary>
    /// <typeparam name="T">The input parameter type.</typeparam>
    [Serializable]
    public class AndExpression<T> : ExpressionAbstract<T, bool>, IPredicate<T>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="AndExpression{T}"/> class.
        /// </summary>
        public AndExpression()
        {
        }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of boolean-returning functions that take an argument of type <typeparamref name="T"/>.</param>
        public AndExpression(params Func<T, bool>[] members) : base(members)
        {
        }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">A collection of boolean-returning functions that take an argument of type <typeparamref name="T"/>.</param>
        public AndExpression(IEnumerable<Func<T, bool>> members) : base(members)
        {
        }

        /// <summary>
        /// Evaluates the expression with the specified input value.
        /// </summary>
        /// <param name="members">The list of boolean-returning function members.</param>
        /// <param name="args">The input value passed to each function.</param>
        /// <returns><c>true</c> if all functions return <c>true</c>; otherwise, <c>false</c>.</returns>
        protected override bool Invoke(IReadOnlyList<Func<T, bool>> members, T args)
        {
            int count = members.Count;

            if (count == 0)
                return true;

            for (int i = 0; i < count; i++)
                if (!members[i].Invoke(args))
                    return false;

            return true;
        }
    }

    /// <summary>
    /// Represents a generic logical AND expression composed of functions that take two parameters of types <typeparamref name="T1"/> and <typeparamref name="T2"/>, and return a boolean.
    /// The expression returns <c>true</c> if all functions return <c>true</c> for the given inputs.
    /// </summary>
    /// <typeparam name="T1">The first input parameter type.</typeparam>
    /// <typeparam name="T2">The second input parameter type.</typeparam>
    [Serializable]
    public class AndExpression<T1, T2> : ExpressionAbstract<T1, T2, bool>, IPredicate<T1, T2>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="AndExpression{T1, T2}"/> class.
        /// </summary>
        public AndExpression()
        {
        }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">An array of boolean-returning functions that take arguments of types <typeparamref name="T1"/> and <typeparamref name="T2"/>.</param>
        public AndExpression(params Func<T1, T2, bool>[] members) : base(members)
        {
        }

        /// <summary>
        /// Initializes the expression with the specified function members.
        /// </summary>
        /// <param name="members">A collection of boolean-returning functions that take arguments of types <typeparamref name="T1"/> and <typeparamref name="T2"/>.</param>
        public AndExpression(IEnumerable<Func<T1, T2, bool>> members) : base(members)
        {
        }

        /// <summary>
        /// Evaluates the expression with the specified input values.
        /// </summary>
        /// <param name="members">The list of boolean-returning function members.</param>
        /// <param name="arg1">The first input value.</param>
        /// <param name="arg2">The second input value.</param>
        /// <returns><c>true</c> if all functions return <c>true</c>; otherwise, <c>false</c>.</returns>
        protected override bool Invoke(IReadOnlyList<Func<T1, T2, bool>> members, T1 arg1, T2 arg2)
        {
            int count = members.Count;
            if (count == 0)
                return true;

            for (int i = 0; i < count; i++)
                if (!members[i].Invoke(arg1, arg2))
                    return false;

            return true;
        }
    }
}