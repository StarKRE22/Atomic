using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a read-only reactive key-value dictionary that provides notifications
    /// when items are added, removed, updated, or when the overall state changes.
    /// </summary>
    /// <typeparam name="K">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="V">The type of values in the dictionary.</typeparam>
    public interface IReadOnlyReactiveDictionary<K, V> : IReadOnlyDictionary<K, V>
    {
        /// <summary>
        /// Event triggered when any change affects the state of the dictionary (e.g., bulk update, clear).
        /// </summary>
        event StateChangedHandler OnStateChanged;

        /// <summary>
        /// Event triggered when an existing key's value is changed.
        /// </summary>
        event SetItemHandler<K, V> OnItemChanged;

        /// <summary>
        /// Event triggered when a new key-value pair is added.
        /// </summary>
        event AddItemHandler<K, V> OnItemAdded;

        /// <summary>
        /// Event triggered when an item is removed from the dictionary.
        /// </summary>
        event RemoveItemHandler<K, V> OnItemRemoved;
    }
}