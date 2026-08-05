using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods for working with expressions and enumerable collections.
    /// </summary>
    public partial class Extensions
    {
        #region Sum

        /// <summary>
        /// Calculates the sum of all values in the sequence.
        /// </summary>
        /// <param name="list">The sequence of floating-point values.</param>
        /// <returns>The sum of all elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="list"/> is <see langword="null"/>.
        /// </exception>       
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sum(this IEnumerable<float> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            float result = 0;
            foreach (float item in list)
                result += item;

            return result;
        }

        /// <summary>
        /// Calculates the sum of all values in the sequence.
        /// </summary>
        /// <param name="list">The sequence of integer values.</param>
        /// <returns>The sum of all elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="list"/> is <see langword="null"/>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Sum(this IEnumerable<int> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            float result = 0;
            foreach (float item in list)
                result += item;

            return result;
        }

        #endregion

        #region Mul
        
        /// <summary>
        /// Multiplies all values in the sequence.
        /// </summary>
        /// <param name="list">The sequence of floating-point values.</param>
        /// <returns>The product of all elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="list"/> is <see langword="null"/>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Multiply(this IEnumerable<float> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            float result = 1;
            foreach (float item in list)
                result *= item;

            return result;
        }

        /// <summary>
        /// Multiplies all values in the sequence.
        /// </summary>
        /// <param name="list">The sequence of floating-point values.</param>
        /// <returns>The product of all elements.</returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="list"/> is <see langword="null"/>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static float Multiply(this IEnumerable<int> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            float result = 1;
            foreach (float item in list)
                result *= item;

            return result;
        }

        #endregion

        #region And

        /// <summary>
        /// Returns <see langword="true"/> if every value in the sequence is <see langword="true"/>.
        /// </summary>
        /// <param name="list">The sequence of boolean values.</param>
        /// <returns>
        /// <see langword="true"/> if all elements are <see langword="true"/> or the sequence is empty;
        /// otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="list"/> is <see langword="null"/>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool And(this IEnumerable<bool> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            foreach (bool item in list)
                if (!item)
                    return false;

            return true;
        }

        #endregion

        #region Or

        /// <summary>
        /// Returns <see langword="true"/> if at least one value in the sequence is <see langword="true"/>.
        /// </summary>
        /// <param name="list">The sequence of boolean values.</param>
        /// <returns>
        /// <see langword="true"/> if any element is <see langword="true"/>; otherwise, <see langword="false"/>.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="list"/> is <see langword="null"/>.
        /// </exception>    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Or(this IEnumerable<bool> list)
        {
            if (list == null)
                throw new ArgumentNullException(nameof(list));

            foreach (bool item in list)
                if (!item)
                    return true;

            return false;
        }

        #endregion

        #region Extensions

        /// <summary>
        /// Adds a function associated with the specified source object.
        /// </summary>
        /// <typeparam name="R">The expression result type.</typeparam>
        /// <param name="it">The target expression.</param>
        /// <param name="source">The owner of the function.</param>
        /// <param name="func">The function to add.</param>
        /// <returns>The same expression instance.</returns>    
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<R> Add<R>(this IExpression<R> it, object source, Func<R> func)
        {
            it.Add(new ExpressionMember<R>(source, func));
            return it;
        }

        /// <summary>
        /// Adds a function object associated with the specified source object.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<R> Add<R>(this IExpression<R> it, object source, IFunction<R> func)
        {
            it.Add(new ExpressionMember<R>(source, func.Invoke));
            return it;
        }

        /// <summary>
        /// Adds a function object without specifying a source.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<R> Add<R>(this IExpression<R> it, IFunction<R> func)
        {
            it.Add(new ExpressionMember<R>(null, func.Invoke));
            return it;
        }

        /// <summary>
        /// Adds a source-function pair.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<R> Add<R>(this IExpression<R> it, KeyValuePair<object, IFunction<R>> function)
        {
            it.Add(new ExpressionMember<R>(function.Key, function.Value.Invoke));
            return it;
        }

        /// <summary>
        /// Adds a function without specifying a source.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<R> Add<R>(this IExpression<R> it, Func<R> func)
        {
            it.Add(new ExpressionMember<R>(null, func));
            return it;
        }

        /// <summary>
        /// Removes the first expression member associated with the specified source.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if a member was removed; otherwise, <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<R>(this IExpression<R> it, object source)
        {
            foreach (ExpressionMember<R> pair in it)
            {
                if (pair.Source == source)
                {
                    it.Remove(pair);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the first expression member using the specified delegate.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if a member was removed; otherwise, <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<R>(this IExpression<R> it, Func<R> value)
        {
            foreach (ExpressionMember<R> pair in it)
            {
                if (pair.EqualsFunction(value))
                {
                    it.Remove(pair);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the first expression member using the specified function object.
        /// </summary>
        /// <returns>
        /// <see langword="true"/> if a member was removed; otherwise, <see langword="false"/>.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<R>(this IExpression<R> it, IFunction<R> value) => 
            it.Remove(value.Invoke);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, T2, R> Add<T1, T2, R>(
            this IExpression<T1, T2, R> it,
            object source,
            Func<T1, T2, R> func)
        {
            it.Add(new ExpressionMember<T1, T2, R>(source, func));
            return it;
        }

        /// <summary>
        /// Adds a binary function associated with the specified source object.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, T2, R> Add<T1, T2, R>(
            this IExpression<T1, T2, R> it,
            object source,
            IFunction<T1, T2, R> func)
        {
            it.Add(new ExpressionMember<T1, T2, R>(source, func.Invoke));
            return it;
        }

        /// <summary>
        /// Adds a binary function object associated with the specified source object.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, T2, R> Add<T1, T2, R>(
            this IExpression<T1, T2, R> it,
            KeyValuePair<object, IFunction<T1, T2, R>> function)
        {
            it.Add(new ExpressionMember<T1, T2, R>(function.Key, function.Value.Invoke));
            return it;
        }

        /// <summary>
        /// Adds a source-function pair.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, T2, R> Add<T1, T2, R>(
            this IExpression<T1, T2, R> it,
            Func<T1, T2, R> func)
        {
            it.Add(new ExpressionMember<T1, T2, R>(null, func));
            return it;
        }

        /// <summary>
        /// Adds a binary function without specifying a source.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<T1, T2, R>(
            this IExpression<T1, T2, R> it,
            object source)
        {
            foreach (ExpressionMember<T1, T2, R> pair in it)
            {
                if (pair.Source == source)
                {
                    it.Remove(pair);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the first expression member associated with the specified source.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<R, T1, T2>(this IExpression<T1, T2, R> it, Func<T1, T2, R> value)
        {
            foreach (ExpressionMember<T1, T2, R> pair in it)
            {
                if (pair.EqualsFunction(value))
                {
                    it.Remove(pair);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the first expression member using the specified delegate.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<T1, T2, R>(this IExpression<T1, T2, R> it, IFunction<T1, T2, R> value) => 
            it.Remove(value.Invoke);

        /// <summary>
        /// Removes the first expression member using the specified function object.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, R> Add<T1, R>(
            this IExpression<T1, R> it,
            object source,
            Func<T1, R> func)
        {
            it.Add(new ExpressionMember<T1, R>(source, func));
            return it;
        }

        /// <summary>
        /// Adds a unary function associated with the specified source object.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, R> Add<T1, R>(
            this IExpression<T1, R> it,
            object source,
            IFunction<T1, R> func)
        {
            it.Add(new ExpressionMember<T1, R>(source, func.Invoke));
            return it;
        }

        /// <summary>
        /// Adds a unary function object associated with the specified source object.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, R> Add<T1, R>(
            this IExpression<T1, R> it,
            KeyValuePair<object, IFunction<T1, R>> function)
        {
            it.Add(new ExpressionMember<T1, R>(function.Key, function.Value.Invoke));
            return it;
        }

        /// <summary>
        /// Adds a source-function pair.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, R> Add<T1, R>(
            this IExpression<T1, R> it,
            Func<T1, R> func)
        {
            it.Add(new ExpressionMember<T1, R>(null, func));
            return it;
        }

        /// <summary>
        /// Adds a unary function without specifying a source.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<T, R>(
            this IExpression<T, R> it,
            object source)
        {
            foreach (var pair in it)
            {
                if (pair.Source == source)
                {
                    it.Remove(pair);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the first expression member associated with the specified source.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<R, T>(this IExpression<T, R> it, Func<T, R> value)
        {
            foreach (var pair in it)
            {
                if (pair.EqualsFunction(value))
                {
                    it.Remove(pair);
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Removes the first expression member using the specified delegate.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<T, R>(this IExpression<T, R> it, IFunction<T, R> value) => 
            it.Remove(value.Invoke);

        #endregion

        /// <summary>
        /// Removes the first expression member using the specified function object.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveList<ExpressionMember<R>>.StateChangedSubscription SubscribeState<R>(
            this IExpression<R> it, Action<R> action
        ) => new(it, () => action.Invoke(it.Invoke()));
    }
}
