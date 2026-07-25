using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive array that provides change notifications when elements are modified.
    /// Extends <see cref="IReadOnlyList{T}"/> with writable access and reactive events.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    public interface IReactiveArray<T> : IArray<T>, IReadOnlyReactiveArray<T>
    {
        new int Length { get; }
        
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// Setting a value will trigger the <see cref="IReadOnlyReactiveArray{T}.OnItemChanged"/> event if the value is changed.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        new T this[int index] { get; set; }
    }
}