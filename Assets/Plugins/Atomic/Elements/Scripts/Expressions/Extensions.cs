using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public static partial class Extensions
    {
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
                    expression.Add(predicate.Invoke);
        }

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

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T, R>(this IExpression<T, R> it, IFunction<R> member) => it.Add(_ => member.Invoke());

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Add<T>(this IExpression<T> it, IFunction<T> member) => it.Add(member.Invoke);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Remove<T>(this IExpression<T> it, IFunction<T> member) => it.Remove(member.Invoke);
    }
}