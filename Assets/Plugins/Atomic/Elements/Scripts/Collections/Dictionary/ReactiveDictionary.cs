using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    //TODO: ПЕРЕПИСАТЬ НА НАТИВНЫЙ СЛОВАРЬ!

    /// <summary>
    /// A reactive wrapper around <see cref="Dictionary{K,V}"/> that raises events
    /// when the collection is changed (items added, removed, or updated).
    /// Useful for data binding or reactive programming patterns in Unity.
    /// </summary>
    /// <typeparam name="K">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="V">The type of values in the dictionary.</typeparam>
    [Serializable]
    public class ReactiveDictionary<K, V> : IReactiveDictionary<K, V>,
#if UNITY_5_3_OR_NEWER
        ISerializationCallbackReceiver
#endif
    {
#if UNITY_5_3_OR_NEWER
        [Serializable]
        internal struct Pair
        {
            public K key;
            public V value;
        }

        [SerializeField]
        private Pair[] pairs;
#endif

        private static readonly IEqualityComparer<V> s_equalityComparer = EqualityComparer.GetDefault<V>();

        public event StateChangedHandler OnStateChanged;
        public event SetItemHandler<K, V> OnItemChanged;
        public event AddItemHandler<K, V> OnItemAdded;
        public event RemoveItemHandler<K, V> OnItemRemoved;
        public event ClearHandler OnCleared;

        private Dictionary<K, V> dictionary;

        /// <summary>
        /// Gets a collection containing the keys in the dictionary.
        /// </summary>
        public ICollection<K> Keys => this.dictionary.Keys;

        IEnumerable<K> IReadOnlyDictionary<K, V>.Keys => Keys;

        /// <summary>
        /// Gets a collection containing the values in the dictionary.
        /// </summary>
        public ICollection<V> Values => this.dictionary.Values;

        IEnumerable<V> IReadOnlyDictionary<K, V>.Values => Values;

        /// <summary>
        /// Gets the number of key/value pairs contained in the dictionary.
        /// </summary>
        public int Count => this.dictionary.Count;

        /// <summary>
        /// Gets a value indicating whether the dictionary is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveDictionary{K,V}"/> class.
        /// </summary>
        public ReactiveDictionary() => this.dictionary = new Dictionary<K, V>();

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveDictionary{K,V}"/> class with the specified capacity.
        /// </summary>
        /// <param name="capacity">The initial number of elements the dictionary can contain.</param>
        public ReactiveDictionary(int capacity = 0) => this.dictionary = new Dictionary<K, V>(capacity);

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        public V this[K key]
        {
            get => this.dictionary[key];
            set => this.Set(key, value);
        }

        /// <summary>
        /// Adds a key/value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        public void Add(K key, V value)
        {
            this.dictionary.Add(key, value);
            this.OnItemAdded?.Invoke(key, value);
            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Adds a <see cref="KeyValuePair{K,V}"/> to the dictionary.
        /// </summary>
        /// <param name="item">The key/value pair to add.</param>
        public void Add(KeyValuePair<K, V> item)
        {
            (K key, V value) = item;
            this.Add(key, value);
        }

        /// <summary>
        /// Removes the value with the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>True if the element is successfully removed; otherwise, false.</returns>
        public bool Remove(K key)
        {
            if (this.dictionary.Remove(key, out V value))
            {
                this.OnItemRemoved?.Invoke(key, value);
                this.OnStateChanged?.Invoke();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes the value with the specified key and returns the removed value.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <param name="value">The removed value if found.</param>
        /// <returns>True if the element was removed; otherwise, false.</returns>
        public bool Remove(K key, out V value)
        {
            if (this.dictionary.Remove(key, out value))
            {
                this.OnItemRemoved?.Invoke(key, value);
                this.OnStateChanged?.Invoke();
                return true;
            }

            return false;
        }

        /// <summary>
        /// Removes the specified key/value pair from the dictionary.
        /// </summary>
        /// <param name="item">The key/value pair to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        public bool Remove(KeyValuePair<K, V> item)
        {
            if (((ICollection<KeyValuePair<K, V>>) this.dictionary).Remove(item))
            {
                this.OnStateChanged?.Invoke();
                this.OnItemRemoved?.Invoke(item.Key, item.Value);
                return true;
            }

            return false;
        }

        /// <summary>
        /// Sets the value associated with the specified key. If the key exists,
        /// the value is updated and <see cref="OnItemChanged"/> is triggered.
        /// </summary>
        /// <param name="key">The key of the element to set.</param>
        /// <param name="value">The new value.</param>
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

        /// <summary>
        /// Determines whether the dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>True if the key is found; otherwise, false.</returns>
        public bool ContainsKey(K key) => this.dictionary.ContainsKey(key);

        /// <summary>
        /// Determines whether the dictionary contains the specified key/value pair.
        /// </summary>
        /// <param name="item">The key/value pair to locate.</param>
        /// <returns>True if the pair is found; otherwise, false.</returns>
        public bool Contains(KeyValuePair<K, V> item) =>
            ((ICollection<KeyValuePair<K, V>>) this.dictionary).Contains(item);

        /// <summary>
        /// Attempts to get the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">The value if found.</param>
        /// <returns>True if the key was found; otherwise, false.</returns>
        public bool TryGetValue(K key, out V value) => this.dictionary.TryGetValue(key, out value);

        /// <summary>
        /// Removes all keys and values from the dictionary.
        /// </summary>
        public void Clear()
        {
            if (this.dictionary.Count > 0)
            {
                this.dictionary.Clear();
                this.OnCleared?.Invoke();
                this.OnStateChanged?.Invoke();
            }
        }

        /// <summary>
        /// Copies the elements of the dictionary to an array, starting at a particular index.
        /// </summary>
        /// <param name="array">The destination array.</param>
        /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex = 0) =>
            ((ICollection<KeyValuePair<K, V>>) this.dictionary).CopyTo(array, arrayIndex);

        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        public IEnumerator<KeyValuePair<K, V>> GetEnumerator() => this.dictionary.GetEnumerator();

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Unity callback invoked after the object has been deserialized.
        /// Reconstructs the internal dictionary from the serialized pair array.
        /// </summary>
        public void OnAfterDeserialize()
        {
            this.dictionary = new Dictionary<K, V>();

            for (int i = 0, count = this.pairs.Length; i < count; i++)
            {
                Pair pair = this.pairs[i];
                this.dictionary[pair.key] = pair.value;
            }

            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Unity callback invoked before the object is serialized.
        /// Flattens the internal dictionary to a serializable array of key-value pairs.
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
#endif
    }
}