namespace Atomic.Elements
{
    /// <summary>
    /// Represents a read-only value provider interface.
    /// Inherits from <see cref="IFunction{T}"/> and exposes a strongly-typed <c>Value</c> property.
    /// </summary>
    /// <typeparam name="T">The type of the value being returned.</typeparam>
    public interface IValue<out T> : IFunction<T>
    {
        /// <summary>
        /// Gets the current value.
        /// </summary>
        T Value { get; }

        /// <summary>
        /// Invokes the function and returns the value.
        /// This is the default implementation from <see cref="IFunction{T}"/> returning <see cref="Value"/>.
        /// </summary>
        /// <returns>The current value.</returns>
        T IFunction<T>.Invoke() => this.Value;
    }
}