using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a read-only reactive collection that provides notifications
    /// when items are added, removed, or when the overall state changes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the collection.</typeparam>
    public interface IReadOnlyReactiveCollection<out T> : IReadOnlyCollection<T>
    {
        /// <summary>
        /// Occurs when the overall state of the collection changes.
        /// This can happen due to bulk operations or significant modifications.
        /// </summary>
        event Action OnStateChanged;

        /// <summary>
        /// Occurs when a new item is added to the collection.
        /// </summary>
        /// <remarks>
        /// Use this event to react to additions without iterating over the collection.
        /// </remarks>
        event Action<T> OnItemAdded;

        /// <summary>
        /// Occurs when an existing item is removed from the collection.
        /// </summary>
        /// <remarks>
        /// Use this event to react to removals without iterating over the collection.
        /// </remarks>
        event Action<T> OnItemRemoved;
    }
}