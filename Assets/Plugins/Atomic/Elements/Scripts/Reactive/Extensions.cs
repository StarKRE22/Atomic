using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SubscribeAll<T>(this IReactive<T> it, IEnumerable<IAction<T>> actions)
        {
            if (actions != null)
            {
                foreach (var action in actions)
                {
                    if (action != null)
                    {
                        it.Subscribe(action.Invoke);
                    }
                }
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subscribe(this IReactive it, IAction action)
        {
            it.Subscribe(action.Invoke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe(this IReactive it, IAction action)
        {
            it.Unsubscribe(action.Invoke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subscribe<T>(this IReactive<T> it, IAction<T> action)
        {
            it.Subscribe(action.Invoke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T>(this IReactive<T> it, IAction<T> action)
        {
            it.Unsubscribe(action.Invoke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subscribe<T1, T2>(this IReactive<T1, T2> it, IAction<T1, T2> action)
        {
            it.Subscribe(action.Invoke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2>(this IReactive<T1, T2> it, IAction<T1, T2> action)
        {
            it.Unsubscribe(action.Invoke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Subscribe<T1, T2, T3>(this IReactive<T1, T2, T3> it,
            IAction<T1, T2, T3> action)
        {
            it.Subscribe(action.Invoke);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Unsubscribe<T1, T2, T3>(this IReactive<T1, T2, T3> it,
            IAction<T1, T2, T3> action)
        {
            it.Unsubscribe(action.Invoke);
        }
    }
}