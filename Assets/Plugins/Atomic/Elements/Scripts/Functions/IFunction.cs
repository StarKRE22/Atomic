namespace Atomic.Elements
{
    /// <summary>
    /// Represents a function with no input arguments that returns a result.
    /// </summary>
    /// <typeparam name="R">The type of the return value.</typeparam>
    public interface IFunction<out R>
    {
        /// <summary>
        /// Invokes the function and returns the result.
        /// </summary>
        /// <returns>The result of the function.</returns>
        R Invoke();
    }

    /// <summary>
    /// Represents a function with one input argument that returns a result.
    /// </summary>
    /// <typeparam name="T">The type of the input argument.</typeparam>
    /// <typeparam name="R">The type of the return value.</typeparam>
    public interface IFunction<in T, out R>
    {
        /// <summary>
        /// Invokes the function with the specified argument and returns the result.
        /// </summary>
        /// <param name="args">The input argument.</param>
        /// <returns>The result of the function.</returns>
        R Invoke(T args);
    }

    /// <summary>
    /// Represents a function with two input arguments that returns a result.
    /// </summary>
    /// <typeparam name="T1">The type of the first input argument.</typeparam>
    /// <typeparam name="T2">The type of the second input argument.</typeparam>
    /// <typeparam name="R">The type of the return value.</typeparam>
    public interface IFunction<in T1, in T2, out R>
    {
        /// <summary>
        /// Invokes the function with the specified arguments and returns the result.
        /// </summary>
        /// <param name="args1">The first input argument.</param>
        /// <param name="args2">The second input argument.</param>
        /// <returns>The result of the function.</returns>
        R Invoke(T1 args1, T2 args2);
    }
}