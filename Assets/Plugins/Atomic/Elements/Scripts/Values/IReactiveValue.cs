namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive value that combines read-only access (<see cref="IValue{T}"/>) 
    /// with reactive observation (<see cref="IReactive{T}"/>).
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface IReactiveValue<T> : IValue<T>, IReactive<T>
    {
    }
}