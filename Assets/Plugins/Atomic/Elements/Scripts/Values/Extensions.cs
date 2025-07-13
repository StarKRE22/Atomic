using System;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<T> AsСonst<T>(this T it) => new(it);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Observe<T>(this IReactiveValue<T> it, Action<T> action)
        {
            it.Subscribe(action);
            action.Invoke(it.Value);
        }
    }
}