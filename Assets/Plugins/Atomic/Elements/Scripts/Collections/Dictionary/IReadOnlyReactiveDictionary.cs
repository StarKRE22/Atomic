using System;
using System.Collections.Generic;

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a read-only reactive key-value dictionary that provides notifications
    /// when items are added, removed, updated, or when the overall state changes.
    /// </summary>
    /// <typeparam name="K">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="V">The type of values in the dictionary.</typeparam>
    public interface IReadOnlyReactiveDictionary<K, V> : 
        IReadOnlyDictionary<K, V>,
        IReadOnlyReactiveCollection<KeyValuePair<K, V>>
    {
        /// <summary>
        /// Occurs when a new key-value pair is added to the dictionary.
        /// </summary>
        /// <remarks>
        /// Use this event to react to newly inserted items.
        /// </remarks>
        new event Action<K, V> OnItemAdded; 
        
        /// <summary>
        /// Occurs when a key-value pair is removed from the dictionary.
        /// </summary>
        /// <remarks>
        /// Use this event to react to deleted items.
        /// </remarks>
        new event Action<K, V> OnItemRemoved; 
        
        /// <summary>
        /// Event triggered when an existing key's value is changed.
        /// </summary>
        event Action<K, V> OnItemChanged;
    }
}