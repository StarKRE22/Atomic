using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCondition(this ICommand command, IFunction<bool> condition) => 
            command.AddCondition(condition.Invoke);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCondition(this ICommand command, IFunction<bool> condition) => 
            command.RemoveCondition(condition.Invoke);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAction(this ICommand command, IAction action) => 
            command.AddAction(action.Invoke);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAction(this ICommand command, IAction action) => 
            command.RemoveAction(action.Invoke);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCondition<T>(this ICommand<T> command, IFunction<T, bool> condition) =>
            command.AddCondition(condition.Invoke);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCondition<T>(this ICommand<T> command, IFunction<T, bool> condition) =>
            command.RemoveCondition(condition.Invoke);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddCondition<T1, T2>(this ICommand<T1, T2> command, IFunction<T1, T2, bool> condition) =>
            command.AddCondition(condition.Invoke);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveCondition<T1, T2>(this ICommand<T1, T2> command, IFunction<T1, T2, bool> condition) =>
            command.RemoveCondition(condition.Invoke);
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAction<T>(this ICommand<T> command, IAction<T> action) =>
            command.AddAction(action.Invoke);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAction<T>(this ICommand<T> command, IAction<T> action) =>
            command.RemoveAction(action.Invoke);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddAction<T1, T2>(this ICommand<T1, T2> command, IAction<T1, T2> action) =>
            command.AddAction(action.Invoke);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void RemoveAction<T1, T2>(this ICommand<T1, T2> command, IAction<T1, T2> action) =>
            command.RemoveAction(action.Invoke);
    }
}