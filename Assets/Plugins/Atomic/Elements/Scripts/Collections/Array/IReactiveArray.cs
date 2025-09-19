using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive array that provides change notifications when elements are modified.
    /// Extends <see cref="IReadOnlyList{T}"/> with writable access and reactive events.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    public interface IReactiveArray<T> : IReadOnlyReactiveArray<T>
    {
        /// <summary>
        /// Gets or sets the element at the specified index.
        /// Setting a value will trigger the <see cref="IReadOnlyReactiveArray{T}.OnItemChanged"/> event if the value is changed.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        new T this[int index] { get; set; }

        /// <summary>
        /// Removes all elements from the array and triggers the <see cref="IReadOnlyReactiveArray{T}.OnStateChanged"/> event.
        /// </summary>
        void Clear();

        /// <summary>
        /// Updates the contents of the reactive array with values from the specified <paramref name="newItems"/> collection.
        /// </summary>
        /// <param name="newItems">The collection of new items to populate the array with.</param>
        void Populate(IEnumerable<T> newItems);

        /// <summary>
        /// Fills the array with the specified value.
        /// </summary>
        /// <param name="value">The value to set for all elements.</param>
        void Fill(T value);

        /// <summary>
        /// Resizes the array to the specified new size.
        /// </summary>
        /// <param name="newSize">The new length of the array. Must be non-negative.</param>
        void Resize(int newSize);
    }
}