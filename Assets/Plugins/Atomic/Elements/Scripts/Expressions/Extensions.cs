using System;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods for working with <see cref="IExpression{T}"/> and related expression interfaces.
    /// </summary>
    public static partial class Extensions
    {
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
        
        #endregion
        
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
                    it.Add(member);
            }
        }
        


        
        /// <summary>
        /// Removes a parameterless function from a <see cref="IExpression{T}"/> expression.
        /// </summary>
        /// <typeparam name="R">The return type of the expression.</typeparam>
        /// <param name="it">The expression to remove from.</param>
        /// <param name="member">The function object to remove.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove<R>(this IExpression<R> it, IFunction<R> member) =>
            it.Remove(member.Invoke);
    }
}
