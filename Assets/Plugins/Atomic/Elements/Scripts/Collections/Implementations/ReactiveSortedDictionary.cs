using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive sorted dictionary based on <see cref="SortedDictionary{K,V}"/>.
    /// Emits events on add, remove, change, and clear operations.
    /// Supports Unity serialization using a custom pair array.
    /// </summary>
    /// <typeparam name="K">The key type. Must be comparable.</typeparam>
    /// <typeparam name="V">The value type.</typeparam>
    [Serializable]
    public class ReactiveSortedDictionary<K, V> :
        IReactiveDictionary<K, V>,
#if UNITY_5_3_OR_NEWER
        ISerializationCallbackReceiver
#endif
    {
        /// <summary>
        /// Serializable key-value pair used for Unity serialization.
        /// </summary>
        [Serializable]
        internal struct Pair
        {
            public K key;
            public V value;
        }
#if UNITY_5_3_OR_NEWER
        [SerializeField]
#endif
        private Pair[] pairs;

        private static readonly IEqualityComparer<V> s_equalityComparer = EqualityComparer.GetDefault<V>();

        /// <inheritdoc/>
        public event StateChangedHandler OnStateChanged;

        /// <inheritdoc/>
        public event SetItemHandler<K, V> OnItemChanged;

        /// <inheritdoc/>
        public event AddItemHandler<K, V> OnItemAdded;

        /// <inheritdoc/>
        public event RemoveItemHandler<K, V> OnItemRemoved;

        /// <summary>
        /// Event triggered specifically when the dictionary is cleared.
        /// </summary>
        public event ClearHandler OnCleared;

        private SortedDictionary<K, V> dictionary;

        /// <summary>
        /// Gets a collection of the keys in the dictionary.
        /// </summary>
        public ICollection<K> Keys => this.dictionary.Keys;

        /// <summary>
        /// Gets a collection of the values in the dictionary.
        /// </summary>
        public ICollection<V> Values => this.dictionary.Values;

        IEnumerable<K> IReadOnlyDictionary<K, V>.Keys => this.Keys;
        IEnumerable<V> IReadOnlyDictionary<K, V>.Values => this.Values;

        /// <inheritdoc cref="IReactiveDictionary{K,V}.Count" />
        public int Count => this.dictionary.Count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        /// <summary>
        /// Initializes an empty <see cref="ReactiveSortedDictionary{K,V}"/>.
        /// </summary>
        public ReactiveSortedDictionary() =>
            this.dictionary = new SortedDictionary<K, V>();

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// Triggers events if the value changes.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        public V this[K key]
        {
            get => this.dictionary[key];
            set => this.Set(key, value);
        }

        /// <inheritdoc/>
        public IEnumerator<KeyValuePair<K, V>> GetEnumerator() =>
            this.dictionary.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        /// <summary>
        /// Adds or updates the value for a given key.
        /// Triggers <see cref="OnItemAdded"/> if new, or <see cref="OnItemChanged"/> if changed.
        /// </summary>
        /// <param name="key">The key to set.</param>
        /// <param name="value">The value to assign.</param>
        public void Set(K key, V value)
        {
            if (!this.dictionary.TryGetValue(key, out V prev))
            {
                this.Add(key, value);
                return;
            }

            if (!s_equalityComparer.Equals(prev, value))
            {
                this.dictionary[key] = value;
                this.OnStateChanged?.Invoke();
                this.OnItemChanged?.Invoke(key, value);
            }
        }

        /// <inheritdoc/>
        public void Add(K key, V value)
        {
            this.dictionary.Add(key, value);
            this.OnItemAdded?.Invoke(key, value);
            this.OnStateChanged?.Invoke();
        }

        /// <inheritdoc/>
        public bool Remove(K key)
        {
            if (!this.dictionary.TryGetValue(key, out V value))
                return false;

            this.dictionary.Remove(key);
            this.OnItemRemoved?.Invoke(key, value);
            this.OnStateChanged?.Invoke();
            return true;
        }

        /// <inheritdoc cref="IReactiveDictionary{K,V}.ContainsKey" />
        public bool ContainsKey(K key) => this.dictionary.ContainsKey(key);

        /// <inheritdoc cref="IDictionary{TKey,TValue}.TryGetValue" />
        public bool TryGetValue(K key, out V value) => this.dictionary.TryGetValue(key, out value);

        /// <inheritdoc/>
        public void Clear()
        {
            if (this.dictionary.Count > 0)
            {
                this.dictionary.Clear();
                this.OnStateChanged?.Invoke();
                this.OnCleared?.Invoke();
            }
        }

        /// <inheritdoc/>
        public void Add(KeyValuePair<K, V> item) => this.Add(item.Key, item.Value);

        /// <inheritdoc/>
        public bool Contains(KeyValuePair<K, V> item) =>
            ((ICollection<KeyValuePair<K, V>>) this.dictionary).Contains(item);

        /// <inheritdoc/>
        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex = 0) =>
            ((ICollection<KeyValuePair<K, V>>) this.dictionary).CopyTo(array, arrayIndex);

        /// <inheritdoc/>
        public bool Remove(KeyValuePair<K, V> item)
        {
            if (((ICollection<KeyValuePair<K, V>>) this.dictionary).Remove(item))
            {
                this.OnItemRemoved?.Invoke(item.Key, item.Value);
                this.OnStateChanged?.Invoke();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Unity callback for deserialization. Reconstructs the dictionary from serialized pairs.
        /// </summary>
        public void OnAfterDeserialize()
        {
            this.dictionary = new SortedDictionary<K, V>();

            for (int i = 0, count = this.pairs.Length; i < count; i++)
            {
                Pair pair = this.pairs[i];
                this.dictionary[pair.key] = pair.value;
            }

            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Unity callback for serialization. Converts the dictionary to a serializable array.
        /// </summary>
        public void OnBeforeSerialize()
        {
            this.pairs = new Pair[this.dictionary.Count];

            int i = 0;
            foreach (var (key, value) in this.dictionary)
            {
                this.pairs[i++] = new Pair
                {
                    key = key,
                    value = value
                };
            }
        }
    }
}