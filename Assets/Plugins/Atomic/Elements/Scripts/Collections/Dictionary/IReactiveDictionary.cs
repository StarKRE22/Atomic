using System.Collections.Generic;
// ReSharper disable PossibleInterfaceMemberAmbiguity

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a reactive key-value dictionary that supports change notifications
    /// when items are added, removed, updated, or when the overall state changes.
    /// </summary>
    /// <typeparam name="K">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="V">The type of values in the dictionary.</typeparam>
    public interface IReactiveDictionary<K, V> : IDictionary<K, V>, IReadOnlyDictionary<K, V>
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

        /// <summary>
        /// Gets the number of key-value pairs contained in the dictionary.
        /// </summary>
        new int Count { get; }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        new V this[K key] { get; set; }

        /// <summary>
        /// Determines whether the dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate in the dictionary.</param>
        /// <returns><c>true</c> if the dictionary contains an element with the specified key; otherwise, <c>false</c>.</returns>
        new bool ContainsKey(K key);
    }
}