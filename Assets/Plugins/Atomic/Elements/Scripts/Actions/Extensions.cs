using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Extension methods for working with <see cref="IAction"/> collections.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Invokes all actions in the given enumerable sequence.
        /// Null actions are safely skipped.
        /// </summary>
        /// <param name="actions">A sequence of actions to invoke.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeRange(this IEnumerable<IAction> actions)
        {
            if (actions != null)
                foreach (IAction action in actions)
                    action?.Invoke();
        }

        /// <summary>
        /// Invokes all actions in the given enumerable sequence with one parameter.
        /// Null actions are safely skipped.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeRange<T1>(this IEnumerable<IAction<T1>> actions, T1 arg1)
        {
            if (actions != null)
                foreach (var action in actions)
                    action?.Invoke(arg1);
        }

        /// <summary>
        /// Invokes all actions in the given enumerable sequence with two parameters.
        /// Null actions are safely skipped.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeRange<T1, T2>(this IEnumerable<IAction<T1, T2>> actions, T1 arg1, T2 arg2)
        {
            if (actions != null)
                foreach (var action in actions)
                    action?.Invoke(arg1, arg2);
        }

        /// <summary>
        /// Invokes all actions in the given enumerable sequence with three parameters.
        /// Null actions are safely skipped.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeRange<T1, T2, T3>(this IEnumerable<IAction<T1, T2, T3>> actions, T1 arg1, T2 arg2,
            T3 arg3)
        {
            if (actions != null)
                foreach (var action in actions)
                    action?.Invoke(arg1, arg2, arg3);
        }

        /// <summary>
        /// Invokes all actions in the given enumerable sequence with four parameters.
        /// Null actions are safely skipped.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InvokeRange<T1, T2, T3, T4>(this IEnumerable<IAction<T1, T2, T3, T4>> actions, T1 arg1,
            T2 arg2, T3 arg3, T4 arg4)
        {
            if (actions != null)
                foreach (var action in actions)
                    action?.Invoke(arg1, arg2, arg3, arg4);
        }
    }
}