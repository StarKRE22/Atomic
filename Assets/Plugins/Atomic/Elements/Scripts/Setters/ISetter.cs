using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a setter interface that accepts a value of type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of the value to be set.</typeparam>
    public interface ISetter<in T> : IAction<T>
    {
        /// <summary>
        /// Sets the value.
        /// </summary>
        T Value { set; }

        /// <summary>
        /// Invokes the setter by assigning the provided value.
        /// </summary>
        /// <param name="arg">The value to set.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        void IAction<T>.Invoke(T arg) => this.Value = arg;
    }
}