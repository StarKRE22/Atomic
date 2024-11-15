using System;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    internal static class DelegateUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Dispose(ref System.Action action)
        {
            if (action == null)
            {
                return;
            }

            Delegate[] delegates = action.GetInvocationList();
            for (int i = 0, count = delegates.Length; i < count; i++)
            {
                action -= (System.Action) delegates[i];
            }

            action = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Dispose<T>(ref T del) where T : Delegate
        {
            if (del == null)
            {
                return;
            }

            Delegate[] delegates = del.GetInvocationList();
            foreach (Delegate value in delegates)
            {
                del = (T) Delegate.Remove(del, value);
            }

            del = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Dispose<T>(ref System.Action<T> action)
        {
            if (action == null)
            {
                return;
            }

            Delegate[] delegates = action.GetInvocationList();
            foreach (var del in delegates)
            {
                action -= (System.Action<T>) del;
            }

            action = null;
        }
    }
}