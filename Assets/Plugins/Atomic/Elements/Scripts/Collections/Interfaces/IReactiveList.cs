using System.Collections.Generic;
// ReSharper disable PossibleInterfaceMemberAmbiguity

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive list that notifies subscribers when its contents change.
    /// Includes events for inserts, deletions, modifications, and state changes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public interface IReactiveList<T> : IList<T>, IReadOnlyList<T>
    {
        /// <summary>
        /// Event triggered when the entire list state changes (e.g., reset, bulk update).
        /// </summary>
        event StateChangedHandler OnStateChanged;

        /// <summary>
        /// Event triggered when an existing item is modified at a specific index.
        /// </summary>
        event ChangeItemHandler<T> OnItemChanged;

        /// <summary>
        /// Event triggered when a new item is inserted at a specific index.
        /// </summary>
        event InsertItemHandler<T> OnItemInserted;

        /// <summary>
        /// Event triggered when an item is deleted from a specific index.
        /// </summary>
        event DeleteItemHandler<T> OnItemDeleted;
    }
}