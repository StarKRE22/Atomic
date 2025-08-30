using System;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods for working with <see cref="IExpression{T}"/> and related expression interfaces.
    /// </summary>
    public static partial class Extensions
    {
        #region AddRange

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
                it.Add(members[i]);
        }

        /// <summary>
        /// Adds multiple function members to the expression with one input parameter.
        /// </summary>
        /// <typeparam name="T1">The input parameter type.</typeparam>
        /// <typeparam name="R">The return type of the function members.</typeparam>
        /// <param name="it">The target expression.</param>
        /// <param name="members">An array of function delegates to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T1, R>(this IExpression<T1, R> it, params Func<T1, R>[] members)
        {
            for (int i = 0, count = members.Length; i < count; i++)
                it.Add(members[i]);
        }

        /// <summary>
        /// Adds multiple function members to the expression with two input parameters.
        /// </summary>
        /// <typeparam name="T1">The first input parameter type.</typeparam>
        /// <typeparam name="T2">The second input parameter type.</typeparam>
        /// <typeparam name="R">The return type of the function members.</typeparam>
        /// <param name="it">The target expression.</param>
        /// <param name="members">An array of function delegates to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<T1, T2, R>(this IExpression<T1, T2, R> it, params Func<T1, T2, R>[] members)
        {
            for (int i = 0, count = members.Length; i < count; i++)
                it.Add(members[i]);
        }

        #endregion

        #region Add

        /// <summary>
        /// Adds a parameterless function as a member to a <see cref="IExpression{T}"/> expression.
        /// </summary>
        /// <typeparam name="T">The return type of the expression.</typeparam>
        /// <param name="it">The expression to add to.</param>
        /// <param name="member">The function object to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<R>(this IExpression<R> it, IFunction<R> member) =>
            it.Add(member.Invoke);

        /// <summary>
        /// Adds a parameterless function as a member to a <see cref="IExpression{T, R}"/> expression using a wrapper.
        /// </summary>
        /// <typeparam name="T">The input parameter type for the expression.</typeparam>
        /// <typeparam name="R">The return type of the expression.</typeparam>
        /// <param name="it">The expression to add to.</param>
        /// <param name="member">The function object to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T, R>(this IExpression<T, R> it, IFunction<R> member) =>
            it.Add(_ => member.Invoke());

        /// <summary>
        /// Adds a parameterless function as a member to a <see cref="IExpression{T1, T2, R}"/> expression using a wrapper.
        /// </summary>
        /// <typeparam name="T1">The first input parameter type for the expression.</typeparam>
        /// <typeparam name="T2">The second input parameter type for the expression.</typeparam>
        /// <typeparam name="R">The return type of the expression.</typeparam>
        /// <param name="it">The expression to add to.</param>
        /// <param name="member">The function object to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T1, T2, R>(this IExpression<T1, T2, R> it, IFunction<R> member) =>
            it.Add((_, __) => member.Invoke());

        #endregion

        #region Remove

        /// <summary>
        /// Removes a parameterless function from a <see cref="IExpression{T}"/> expression.
        /// </summary>
        /// <typeparam name="R">The return type of the expression.</typeparam>
        /// <param name="it">The expression to remove from.</param>
        /// <param name="member">The function object to remove.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove<R>(this IExpression<R> it, IFunction<R> member) =>
            it.Remove(member.Invoke);

        /// <summary>
        /// Removes a parameterless function (wrapped) from a <see cref="IExpression{T, R}"/> expression.
        /// </summary>
        /// <typeparam name="T">The input parameter type.</typeparam>
        /// <typeparam name="R">The return type of the expression.</typeparam>
        /// <param name="it">The expression to remove from.</param>
        /// <param name="member">The function object to remove.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove<T, R>(this IExpression<T, R> it, IFunction<R> member) =>
            it.Remove(_ => member.Invoke());

        /// <summary>
        /// Removes a parameterless function (wrapped) from a <see cref="IExpression{T1, T2, R}"/> expression.
        /// </summary>
        /// <typeparam name="T1">The first input parameter type.</typeparam>
        /// <typeparam name="T2">The second input parameter type.</typeparam>
        /// <typeparam name="R">The return type of the expression.</typeparam>
        /// <param name="it">The expression to remove from.</param>
        /// <param name="member">The function object to remove.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove<T1, T2, R>(this IExpression<T1, T2, R> it, IFunction<R> member) =>
            it.Remove((_, _) => member.Invoke());

        #endregion
    }
}