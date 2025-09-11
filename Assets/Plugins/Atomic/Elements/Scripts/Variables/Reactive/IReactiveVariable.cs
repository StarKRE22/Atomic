namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive variable that supports both read/write access and change notifications.
    /// Inherits from <see cref="IVariable{T}"/> and <see cref="IReactiveValue{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface IReactiveVariable<T> : IVariable<T>, IReactiveValue<T>
    {
    }
}