namespace Atomic.Elements
{
    /// <summary>
    /// Represents a read-write variable that exposes both getter and setter interfaces.
    /// </summary>
    /// <typeparam name="T">The type of the value.</typeparam>
    public interface IVariable<T> : IValue<T>, ISetter<T>
    {
        /// <summary>
        /// Gets or sets the current value.
        /// </summary>
        new T Value { get; set; }

        /// <inheritdoc/>
        T IValue<T>.Value
        {
            get { return this.Value; }
        }

        /// <inheritdoc/>
        T ISetter<T>.Value
        {
            set => this.Value = value;
        }
    }
}