using System.Collections.Generic;
// ReSharper disable PossibleInterfaceMemberAmbiguity

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive set that provides notifications when its contents change.
    /// Includes events for additions, removals, and global state changes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the set.</typeparam>
    public interface IReactiveSet<T> : ISet<T>, IReadOnlyCollection<T>
    {
        /// <summary>
        /// Event triggered when the overall state of the set changes.
        /// Useful for batch operations, clear calls, or external resets.
        /// </summary>
        event StateChangedHandler OnStateChanged;

        /// <summary>
        /// Event triggered when a new item is added to the set.
        /// </summary>
        event AddItemHandler<T> OnItemAdded;

        /// <summary>
        /// Event triggered when an item is removed from the set.
        /// </summary>
        event RemoveItemHandler<T> OnItemRemoved;
    }
}