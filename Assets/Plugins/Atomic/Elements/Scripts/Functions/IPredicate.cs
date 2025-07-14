namespace Atomic.Elements
{
    /// <summary>
    /// Represents a predicate (boolean-returning function) with no input arguments.
    /// </summary>
    public interface IPredicate : IFunction<bool>
    {
    }

    /// <summary>
    /// Represents a predicate (boolean-returning function) with one input argument.
    /// </summary>
    /// <typeparam name="T">The type of the input argument.</typeparam>
    public interface IPredicate<in T> : IFunction<T, bool>
    {
    }

    /// <summary>
    /// Represents a predicate (boolean-returning function) with two input arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first input argument.</typeparam>
    /// <typeparam name="T2">The type of the second input argument.</typeparam>
    public interface IPredicate<in T1, in T2> : IFunction<T1, T2, bool>
    {
    }
}