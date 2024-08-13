using System;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Const<T> AsValue<T>(this T it)
        {
            return new Const<T>(it);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Observe<T>(this IReactiveValue<T> it, System.Action<T> action)
        {
            it.Subscribe(action);
            action.Invoke(it.Value);
        }
    }
}