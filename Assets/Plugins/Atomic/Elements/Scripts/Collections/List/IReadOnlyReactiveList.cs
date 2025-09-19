// ReSharper disable PossibleInterfaceMemberAmbiguity
using System;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a read-only reactive list that notifies subscribers when its contents change.
    /// Includes events for inserts, deletions, modifications, and state changes.
    /// </summary>
    /// <typeparam name="T">The type of elements in the list.</typeparam>
    public interface IReadOnlyReactiveList<T> : IReadOnlyReactiveArray<T>, IReadOnlyReactiveCollection<T>
    {
        /// <summary>
        /// Event triggered when the overall state of the set changes.
        /// </summary>
        new event Action OnStateChanged;
        
        ///<inheritdoc cref="IReadOnlyReactiveArray{T}"/> 
        int IReadOnlyReactiveArray<T>.Length => this.Count;
        
        /// <summary>
        /// Event triggered when a new item is inserted at a specific index.
        /// </summary>
        new event Action<int, T> OnItemAdded;

        /// <summary>
        /// Event triggered when an item is deleted from a specific index.
        /// </summary>
        new event Action<int, T> OnItemRemoved;
    }
}