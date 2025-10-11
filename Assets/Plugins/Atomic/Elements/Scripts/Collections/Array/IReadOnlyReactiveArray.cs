using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a read-only reactive array that notifies about changes to its elements and global state.
    /// </summary>
    /// <typeparam name="T">The type of elements contained in the array.</typeparam>
    public interface IReadOnlyReactiveArray<T> : IReadOnlyList<T>
    {
        /// <summary>
        /// Occurs when an item at a specific index is changed.
        /// </summary>
        event Action<int, T> OnItemChanged;

        /// <summary>
        /// Occurs when the state of the array is changed globally.
        /// For example, when multiple items are updated, cleared, or reset.
        /// </summary>
        event Action OnStateChanged;

        /// <summary>
        /// Gets the total number of elements in the array.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get.</param>
        /// <returns>The element at the specified index.</returns>
        new T this[int index] { get; }

        /// <inheritdoc/>
        int IReadOnlyCollection<T>.Count => this.Length;

        /// <summary><para>Determines whether the current collection contains a specific value.</para></summary>
        /// <param name="item">The object to locate in the current collection.</param>
        bool Contains(T item);

        /// <summary>
        /// Copies a range of elements from this array to the specified destination array.
        /// </summary>
        /// <param name="array">The destination array.</param>
        /// <param name="arrayIndex">The zero-based index in the destination array at which storing begins.</param>
        void CopyTo(T[] array, int arrayIndex);

        /// <summary>
        /// Copies a range of elements from this array to the specified destination array.
        /// </summary>
        /// <param name="sourceIndex">The zero-based index in this array at which copying begins.</param>
        /// <param name="destination">The destination array.</param>
        /// <param name="destinationIndex">The zero-based index in the destination array at which storing begins.</param>
        /// <param name="length">The number of elements to copy.</param>
        void CopyTo(int sourceIndex, T[] destination, int destinationIndex, int length);
    }
}