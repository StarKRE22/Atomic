namespace Atomic.Elements
{
    /// <summary>
    /// Represents a basic request action.
    /// </summary>
    public interface IRequest : IAction
    {
        /// <summary>
        /// Gets a value indicating whether the request is required to be handled.
        /// </summary>
        bool Required { get; }

        /// <summary>
        /// Attempts to consume the request.
        /// Returns true if the request was successfully consumed.
        /// </summary>
        /// <returns>True if the request was consumed; otherwise, false.</returns>
        bool Consume();
    }

    /// <summary>
    /// Represents a typed request action with one argument.
    /// </summary>
    /// <typeparam name="T">The type of the argument.</typeparam>
    public interface IRequest<T> : IAction<T>
    {
        /// <summary>
        /// Gets a value indicating whether the request is required to be handled.
        /// </summary>
        bool Required { get; }

        /// <summary>
        /// Gets the request argument.
        /// </summary>
        T Arg { get; }

        /// <summary>
        /// Attempts to retrieve the argument.
        /// </summary>
        /// <param name="arg">The output argument if available.</param>
        /// <returns>True if the argument was retrieved successfully; otherwise, false.</returns>
        bool TryGet(out T arg);

        /// <summary>
        /// Attempts to consume the request and retrieve the argument.
        /// </summary>
        /// <param name="arg">The output argument if the request was consumed.</param>
        /// <returns>True if the request was consumed; otherwise, false.</returns>
        bool Consume(out T arg);
    }

    /// <summary>
    /// Represents a typed request action with two arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    public interface IRequest<T1, T2> : IAction<T1, T2>
    {
        /// <summary>
        /// Gets a value indicating whether the request is required to be handled.
        /// </summary>
        bool Required { get; }

        /// <summary>
        /// Gets the first argument of the request.
        /// </summary>
        T1 Arg1 { get; }

        /// <summary>
        /// Gets the second argument of the request.
        /// </summary>
        T2 Arg2 { get; }

        /// <summary>
        /// Attempts to retrieve both arguments.
        /// </summary>
        /// <param name="arg1">The output first argument.</param>
        /// <param name="arg2">The output second argument.</param>
        /// <returns>True if the arguments were retrieved successfully; otherwise, false.</returns>
        bool TryGet(out T1 arg1, out T2 arg2);

        /// <summary>
        /// Attempts to consume the request and retrieve both arguments.
        /// </summary>
        /// <param name="args1">The output first argument.</param>
        /// <param name="args2">The output second argument.</param>
        /// <returns>True if the request was consumed; otherwise, false.</returns>
        bool Consume(out T1 args1, out T2 args2);
    }

    /// <summary>
    /// Represents a typed request action with three arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    public interface IRequest<T1, T2, T3> : IAction<T1, T2, T3>
    {
        /// <summary>
        /// Gets a value indicating whether the request is required to be handled.
        /// </summary>
        bool Required { get; }

        /// <summary>
        /// Gets the first argument of the request.
        /// </summary>
        T1 Arg1 { get; }

        /// <summary>
        /// Gets the second argument of the request.
        /// </summary>
        T2 Arg2 { get; }

        /// <summary>
        /// Gets the third argument of the request.
        /// </summary>
        T3 Arg3 { get; }

        /// <summary>
        /// Attempts to retrieve all three arguments.
        /// </summary>
        /// <param name="arg1">The output first argument.</param>
        /// <param name="arg2">The output second argument.</param>
        /// <param name="arg3">The output third argument.</param>
        /// <returns>True if the arguments were retrieved successfully; otherwise, false.</returns>
        bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3);

        /// <summary>
        /// Attempts to consume the request and retrieve all three arguments.
        /// </summary>
        /// <param name="args1">The output first argument.</param>
        /// <param name="args2">The output second argument.</param>
        /// <param name="args3">The output third argument.</param>
        /// <returns>True if the request was consumed; otherwise, false.</returns>
        bool Consume(out T1 args1, out T2 args2, out T3 args3);
    }

    /// <summary>
    /// Represents a typed request action with four arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first argument.</typeparam>
    /// <typeparam name="T2">The type of the second argument.</typeparam>
    /// <typeparam name="T3">The type of the third argument.</typeparam>
    /// <typeparam name="T4">The type of the fourth argument.</typeparam>
    public interface IRequest<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
    {
        /// <summary>
        /// Gets a value indicating whether the request is required to be handled.
        /// </summary>
        bool Required { get; }

        /// <summary>
        /// Gets the first argument of the request.
        /// </summary>
        T1 Arg1 { get; }

        /// <summary>
        /// Gets the second argument of the request.
        /// </summary>
        T2 Arg2 { get; }

        /// <summary>
        /// Gets the third argument of the request.
        /// </summary>
        T3 Arg3 { get; }

        /// <summary>
        /// Gets the fourth argument of the request.
        /// </summary>
        T4 Arg4 { get; }

        /// <summary>
        /// Attempts to retrieve all four arguments.
        /// </summary>
        /// <param name="arg1">The output first argument.</param>
        /// <param name="arg2">The output second argument.</param>
        /// <param name="arg3">The output third argument.</param>
        /// <param name="arg4">The output fourth argument.</param>
        /// <returns>True if the arguments were retrieved successfully; otherwise, false.</returns>
        bool TryGet(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);

        /// <summary>
        /// Attempts to consume the request and retrieve all four arguments.
        /// </summary>
        /// <param name="arg1">The output first argument.</param>
        /// <param name="arg2">The output second argument.</param>
        /// <param name="arg3">The output third argument.</param>
        /// <param name="arg4">The output fourth argument.</param>
        /// <returns>True if the request was consumed; otherwise, false.</returns>
        bool Consume(out T1 arg1, out T2 arg2, out T3 arg3, out T4 arg4);
    }
}