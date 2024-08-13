using System;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProxyFunction<R> AsFunction<R>(this Func<R> func)
        {
            return new ProxyFunction<R>(func);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProxyFunction<R> AsFunction<T, R>(this T it, Func<T, R> func)
        {
            return new ProxyFunction<R>(() => func.Invoke(it));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProxyFunction<bool> AsNot(this IValue<bool> it)
        {
            return new ProxyFunction<bool>(() => !it.Value);
        }
    }
}