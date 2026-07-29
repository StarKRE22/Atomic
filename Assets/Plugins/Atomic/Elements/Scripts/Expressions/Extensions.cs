using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public partial class Extensions
    {
        #region Sum

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<R> Add<R>(this IExpression<R> it, object source, Func<R> func)
        {
            it.Add(new ExpressionMember<R>(source, func));
            return it;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<R> Add<R>(this IExpression<R> it, object source, IFunction<R> func)
        {
            it.Add(new ExpressionMember<R>(source, func.Invoke));
            return it;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<R> Add<R>(this IExpression<R> it, IFunction<R> func)
        {
            it.Add(new ExpressionMember<R>(null, func.Invoke));
            return it;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<R> Add<R>(this IExpression<R> it, KeyValuePair<object, IFunction<R>> function)
        {
            it.Add(new ExpressionMember<R>(function.Key, function.Value.Invoke));
            return it;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<R> Add<R>(this IExpression<R> it, Func<R> func)
        {
            it.Add(new ExpressionMember<R>(null, func));
            return it;
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, T2, R> Add<T1, T2, R>(
            this IExpression<T1, T2, R> it,
            object source,
            IFunction<T1, T2, R> func)
        {
            it.Add(new ExpressionMember<T1, T2, R>(source, func.Invoke));
            return it;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, T2, R> Add<T1, T2, R>(
            this IExpression<T1, T2, R> it,
            KeyValuePair<object, IFunction<T1, T2, R>> function)
        {
            it.Add(new ExpressionMember<T1, T2, R>(function.Key, function.Value.Invoke));
            return it;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, T2, R> Add<T1, T2, R>(
            this IExpression<T1, T2, R> it,
            Func<T1, T2, R> func)
        {
            it.Add(new ExpressionMember<T1, T2, R>(null, func));
            return it;
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<T1, T2, R>(this IExpression<T1, T2, R> it, IFunction<T1, T2, R> value) => 
            it.Remove(value.Invoke);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, R> Add<T1, R>(
            this IExpression<T1, R> it,
            object source,
            Func<T1, R> func)
        {
            it.Add(new ExpressionMember<T1, R>(source, func));
            return it;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, R> Add<T1, R>(
            this IExpression<T1, R> it,
            object source,
            IFunction<T1, R> func)
        {
            it.Add(new ExpressionMember<T1, R>(source, func.Invoke));
            return it;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, R> Add<T1, R>(
            this IExpression<T1, R> it,
            KeyValuePair<object, IFunction<T1, R>> function)
        {
            it.Add(new ExpressionMember<T1, R>(function.Key, function.Value.Invoke));
            return it;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IExpression<T1, R> Add<T1, R>(
            this IExpression<T1, R> it,
            Func<T1, R> func)
        {
            it.Add(new ExpressionMember<T1, R>(null, func));
            return it;
        }

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
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool Remove<T, R>(this IExpression<T, R> it, IFunction<T, R> value) => 
            it.Remove(value.Invoke);

        #endregion

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveList<ExpressionMember<R>>.StateChangedSubscription SubscribeState<R>(
            this IExpression<R> it, Action<R> action
        ) => new(it, () => action.Invoke(it.Invoke()));
    }
}