using System;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ReactiveVariable<T> AsReactiveVariable<T>(this T it) => new(it);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static ProxyVariable<R> AsProxyVariable<T, R>(this T it, Func<T, R> getter, Action<T, R> setter) => 
            new(() => getter.Invoke(it), value => setter.Invoke(it, value));
    }
}