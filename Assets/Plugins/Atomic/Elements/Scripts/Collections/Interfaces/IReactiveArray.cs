using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive array that provides change notifications when elements are modified.
    /// Extends <see cref="IReadOnlyList{T}"/> with writable access and reactive events.
    /// </summary>
    /// <typeparam name="T">The type of elements in the array.</typeparam>
    public interface IReactiveArray<T> : IReadOnlyList<T>
    {
        /// <summary>
        /// Event triggered when an item at a specific index is changed.
        /// </summary>
        event ChangeItemHandler<T> OnItemChanged;

        /// <summary>
        /// Event triggered when the array's state is changed globally.
        /// For example, when multiple items are updated or reset.
        /// </summary>
        event StateChangedHandler OnStateChanged;

        /// <summary>
        /// Gets the total number of elements in the array.
        /// </summary>
        int Length { get; }

        /// <summary>
        /// Gets or sets the element at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index of the element to get or set.</param>
        /// <returns>The element at the specified index.</returns>
        new T this[int index] { get; set; }

        /// <inheritdoc/>
        T IReadOnlyList<T>.this[int index] => this[index];

        /// <inheritdoc/>
        int IReadOnlyCollection<T>.Count => this.Length;
    }
}