using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods for working with <see cref="IExpression{T}"/> and related expression interfaces.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Adds multiple function members to the expression.
        /// </summary>
        /// <typeparam name="T">The return type of the function members.</typeparam>
        /// <param name="it">The target expression.</param>
        /// <param name="members">An array of function delegates to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(this IExpression<T> it, params Func<T>[] members)
        {
            for (int i = 0, count = members.Length; i < count; i++)
            {
                Func<T> member = members[i];
                if (member != null)
                    it.AddLast(member);
            }
        }

        /// <summary>
        /// Adds multiple boolean predicates to a boolean expression.
        /// </summary>
        /// <param name="expression">The target boolean expression.</param>
        /// <param name="predicates">The collection of predicate functions to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange(
            this IExpression<bool> expression,
            in IEnumerable<IFunction<bool>> predicates
        )
        {
            if (predicates == null)
                return;

            foreach (IFunction<bool> predicate in predicates)
                if (predicate != null)
                    expression.AddLast(predicate.Invoke);
        }

        /// <summary>
        /// Adds multiple predicates to a generic boolean expression.
        /// </summary>
        /// <typeparam name="T">The input type of the predicates.</typeparam>
        /// <param name="expression">The target expression.</param>
        /// <param name="predicates">The collection of predicates to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T>(
            this IExpression<T, bool> expression,
            in IEnumerable<IFunction<T, bool>> predicates
        )
        {
            if (predicates == null)
                return;

            foreach (IFunction<T, bool> predicate in predicates)
                if (predicate != null)
                    expression.Add(predicate.Invoke);
        }

        /// <summary>
        /// Adds a parameterless function as a member to a <see cref="IExpression{T, R}"/> expression using a wrapper.
        /// </summary>
        /// <typeparam name="T">The input parameter type for the expression.</typeparam>
        /// <typeparam name="R">The return type of the expression.</typeparam>
        /// <param name="it">The expression to add to.</param>
        /// <param name="member">The function object to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T, R>(this IExpression<T, R> it, IFunction<R> member) =>
            it.AddLast(_ => member.Invoke());

        /// <summary>
        /// Adds a parameterless function as a member to a <see cref="IExpression{T}"/> expression.
        /// </summary>
        /// <typeparam name="T">The return type of the expression.</typeparam>
        /// <param name="it">The expression to add to.</param>
        /// <param name="member">The function object to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this IExpression<T> it, IFunction<T> member) =>
            it.AddLast(member.Invoke);

        /// <summary>
        /// Removes a parameterless function from a <see cref="IExpression{T}"/> expression.
        /// </summary>
        /// <typeparam name="T">The return type of the expression.</typeparam>
        /// <param name="it">The expression to remove from.</param>
        /// <param name="member">The function object to remove.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove<T>(this IExpression<T> it, IFunction<T> member) =>
            it.Remove(member.Invoke);
    }
}
