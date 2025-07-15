namespace Atomic.Elements
{
    /// <summary>
    /// Represents a parameterless executable action.
    /// </summary>
    public interface IAction
    {
        /// <summary>
        /// Executes the action logic.
        /// </summary>
        void Invoke();
    }

    /// <summary>
    /// Represents an executable action that takes one argument of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    public interface IAction<in T>
    {
        /// <summary>
        /// Executes the action with the specified argument.
        /// </summary>
        /// <param name="arg">The input parameter.</param>
        void Invoke(T arg);
    }

    /// <summary>
    /// Represents an executable action that takes two arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    public interface IAction<in T1, in T2>
    {
        /// <summary>
        /// Executes the action with the specified arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        void Invoke(T1 arg1, T2 arg2);
    }

    /// <summary>
    /// Represents an executable action that takes three arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    public interface IAction<in T1, in T2, in T3>
    {
        /// <summary>
        /// Executes the action with the specified arguments.
        /// </summary>
        /// <param name="args1">The first argument.</param>
        /// <param name="args2">The second argument.</param>
        /// <param name="args3">The third argument.</param>
        void Invoke(T1 args1, T2 args2, T3 args3);
    }

    /// <summary>
    /// Represents an action that can be invoked with four input arguments.
    /// Typically used to emit events or trigger reactive handlers.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    public interface IAction<in T1, in T2, in T3, in T4>
    {
        /// <summary>
        /// Invokes the action with the specified arguments.
        /// </summary>
        /// <param name="args1">The first argument.</param>
        /// <param name="args2">The second argument.</param>
        /// <param name="args3">The third argument.</param>
        /// <param name="args4">The fourth argument.</param>
        void Invoke(T1 args1, T2 args2, T3 args3, T4 args4);
    }
}