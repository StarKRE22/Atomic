using System;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseFunction<R> AsFunction<R>(this Func<R> func)
        {
            return new BaseFunction<R>(func);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseFunction<R> AsFunction<T, R>(this T it, Func<T, R> func)
        {
            return new BaseFunction<R>(() => func.Invoke(it));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static BaseFunction<bool> AsNot(this IValue<bool> it)
        {
            return new BaseFunction<bool>(() => !it.Value);
        }
    }
}