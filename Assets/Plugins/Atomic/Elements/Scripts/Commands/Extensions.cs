using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides extension methods for working with <see cref="ICommand"/> instances.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Adds an execution condition represented by an <see cref="IFunction{TResult}"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCondition(this ICommand command, IFunction<bool> condition) =>
            command.AddCondition(condition.Invoke);

        /// <summary>
        /// Removes a previously registered execution condition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCondition(this ICommand command, IFunction<bool> condition) =>
            command.RemoveCondition(condition.Invoke);

        /// <summary>
        /// Adds an action represented by an <see cref="IAction"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAction(this ICommand command, IAction action) =>
            command.AddAction(action.Invoke);

        /// <summary>
        /// Removes a previously registered action.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAction(this ICommand command, IAction action) =>
            command.RemoveAction(action.Invoke);

        /// <summary>
        /// Adds an execution condition represented by an <see cref="IFunction{T, TResult}"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCondition<T>(this ICommand<T> command, IFunction<T, bool> condition) =>
            command.AddCondition(condition.Invoke);

        /// <summary>
        /// Removes a previously registered execution condition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCondition<T>(this ICommand<T> command, IFunction<T, bool> condition) =>
            command.RemoveCondition(condition.Invoke);

        /// <summary>
        /// Adds an execution condition represented by an <see cref="IFunction{T1, T2, TResult}"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCondition<T1, T2>(this ICommand<T1, T2> command, IFunction<T1, T2, bool> condition) =>
            command.AddCondition(condition.Invoke);

        /// <summary>
        /// Removes a previously registered execution condition.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCondition<T1, T2>(this ICommand<T1, T2> command, IFunction<T1, T2, bool> condition) =>
            command.RemoveCondition(condition.Invoke);

        /// <summary>
        /// Adds an action represented by an <see cref="IAction{T}"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAction<T>(this ICommand<T> command, IAction<T> action) =>
            command.AddAction(action.Invoke);

        /// <summary>
        /// Removes a previously registered action.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAction<T>(this ICommand<T> command, IAction<T> action) =>
            command.RemoveAction(action.Invoke);

        /// <summary>
        /// Adds an action represented by an <see cref="IAction{T1, T2}"/>.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAction<T1, T2>(this ICommand<T1, T2> command, IAction<T1, T2> action) =>
            command.AddAction(action.Invoke);

        /// <summary>
        /// Removes a previously registered action.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAction<T1, T2>(this ICommand<T1, T2> command, IAction<T1, T2> action) =>
            command.RemoveAction(action.Invoke);
    }
}
