using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive wrapper around <see cref="Dictionary{K,V}"/> that raises events
    /// when the collection is changed (items added, removed, or updated).
    /// Useful for data binding or reactive programming patterns in Unity.
    /// </summary>
    /// <typeparam name="K">The type of keys in the dictionary.</typeparam>
    /// <typeparam name="V">The type of values in the dictionary.</typeparam>
    public partial class ReactiveDictionary<K, V> : IReactiveDictionary<K, V>, IDisposable
    {
        public event StateChangedHandler OnStateChanged;
        public event SetItemHandler<K, V> OnItemChanged;
        public event AddItemHandler<K, V> OnItemAdded;
        public event RemoveItemHandler<K, V> OnItemRemoved;

        /// <summary>
        /// Gets a collection containing the keys in the dictionary.
        /// </summary>
        public ReadOnlyKeyCollection Keys => new(this);

        ICollection<K> IDictionary<K, V>.Keys => new ReadOnlyKeyCollection(this);

        IEnumerable<K> IReadOnlyDictionary<K, V>.Keys => new ReadOnlyKeyCollection(this);

        /// <summary>
        /// Gets a collection containing the values in the dictionary.
        /// </summary>
        public ReadOnlyValueCollection Values => new(this);

        ICollection<V> IDictionary<K, V>.Values => new ReadOnlyValueCollection(this);

        IEnumerable<V> IReadOnlyDictionary<K, V>.Values => new ReadOnlyValueCollection(this);

        /// <summary>
        /// Gets the number of key/value pairs contained in the dictionary.
        /// </summary>
        public int Count => _count;

        /// <summary>
        /// Gets a value indicating whether the dictionary is read-only.
        /// </summary>
        public bool IsReadOnly => false;

        private const int UNDEFINED_INDEX = -1;
        private static readonly IEqualityComparer<K> s_keyComparer = EqualityComparer.GetDefault<K>();
        private static readonly IEqualityComparer<V> s_valueComparer = EqualityComparer.GetDefault<V>();
        private static readonly ArrayPool<KeyValuePair<K, V>> s_pairArrayPool = ArrayPool<KeyValuePair<K, V>>.Shared;

        private struct Slot
        {
            public K key;
            public V value;
            public bool exists;
            public int next;
        }

        private Slot[] _slots;
        private int[] _buckets;
        private int _capacity;
        private int _count;
        private int _primeIndex;
        private int _freeList;
        private int _lastIndex;

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveDictionary{K,V}"/> class with the specified capacity.
        /// </summary>
        /// <param name="capacity">The initial number of elements the dictionary can contain.</param>
        public ReactiveDictionary(int capacity = 0)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _capacity = InternalUtils.CeilToPrime(capacity, out _primeIndex);
            _slots = new Slot[_capacity];
            _buckets = new int[_capacity];
            Array.Fill(_buckets, UNDEFINED_INDEX);

            _count = 0;
            _lastIndex = 0;
            _freeList = UNDEFINED_INDEX;
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        public V this[K key]
        {
            get => this.GetValue(key);
            set => this.SetValue(key, value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public V GetValue(K key)
        {
            if (_count == 0)
                throw new KeyNotFoundException(nameof(key));

            int hash = s_keyComparer.GetHashCode(key) & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            int index = _buckets[bucket];

            while (index >= 0)
            {
                ref readonly Slot slot = ref _slots[index];
                if (slot.exists && s_keyComparer.Equals(slot.key, key))
                    return slot.value;

                index = slot.next;
            }

            throw new KeyNotFoundException(nameof(key));
        }

        /// <summary>
        /// Attempts to get the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <param name="value">The value if found.</param>
        /// <returns>True if the key was found; otherwise, false.</returns>
        public bool TryGetValue(K key, out V value)
        {
            value = default;
            if (_count == 0)
                return false;

            int hash = s_keyComparer.GetHashCode(key) & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            int index = _buckets[bucket];

            while (index >= 0)
            {
                ref readonly Slot slot = ref _slots[index];
                if (slot.exists && s_keyComparer.Equals(slot.key, key))
                {
                    value = slot.value;
                    return true;
                }

                index = slot.next;
            }

            return false;
        }

        /// <summary>
        /// Sets the value associated with the specified key. If the key exists,
        /// the value is updated and <see cref="OnItemChanged"/> is triggered.
        /// </summary>
        /// <param name="key">The key of the element to set.</param>
        /// <param name="value">The new value.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void SetValue(K key, V value)
        {
            if (this.FindIndex(key, out int index))
            {
                ref Slot slot = ref _slots[index];
                if (s_valueComparer.Equals(slot.value, value))
                    return;

                slot.value = value;
                this.OnStateChanged?.Invoke();
                this.OnItemChanged?.Invoke(key, value);
            }
            else
            {
                this.AddInternal(key, value);
                this.OnStateChanged?.Invoke();
                this.OnItemAdded?.Invoke(key, value);
            }
        }

        /// <summary>
        /// Adds a key/value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(K key, V value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            if (this.FindIndex(key, out _))
                throw new ArgumentException($"Value with key {key} is already added!");

            this.AddInternal(key, value);
            this.OnStateChanged?.Invoke();
            this.OnItemAdded?.Invoke(key, value);
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
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(K key)
        {
            if (!this.RemoveInternal(key, out V value))
                return false;

            this.OnItemRemoved?.Invoke(key, value);
            this.OnStateChanged?.Invoke();
            return true;
        }

        /// <summary>
        /// Removes the value with the specified key and returns the removed value.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <param name="value">The removed value if found.</param>
        /// <returns>True if the element was removed; otherwise, false.</returns>
        public bool Remove(K key, out V value)
        {
            if (!this.RemoveInternal(key, out value))
                return false;

            this.OnItemRemoved?.Invoke(key, value);
            this.OnStateChanged?.Invoke();
            return true;
        }

        /// <summary>
        /// Removes the specified key/value pair from the dictionary.
        /// </summary>
        /// <param name="item">The key/value pair to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        public bool Remove(KeyValuePair<K, V> item)
        {
            if (!this.TryGetValue(item.Key, out V value)
                || !s_valueComparer.Equals(value, item.Value)
                || !this.RemoveInternal(item.Key, out value))
                return false;

            this.OnStateChanged?.Invoke();
            this.OnItemRemoved?.Invoke(item.Key, item.Value);
            return true;
        }

        /// <summary>
        /// Determines whether the dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>True if the key is found; otherwise, false.</returns>
        public bool ContainsKey(K key) => this.FindIndex(key, out _);

        /// <summary>
        /// Determines whether the dictionary contains the specified key/value pair.
        /// </summary>
        /// <param name="item">The key/value pair to locate.</param>
        /// <returns>True if the pair is found; otherwise, false.</returns>
        public bool Contains(KeyValuePair<K, V> item)
        {
            return this.TryGetValue(item.Key, out V value) && s_valueComparer.Equals(value, item.Value);
        }

        /// <summary>
        /// Removes all keys and values from the dictionary.
        /// </summary>
        public void Clear()
        {
            if (_count == 0)
                return;

            KeyValuePair<K, V>[] removedItems = s_pairArrayPool.Rent(_count);
            int removedCount = 0;

            for (int i = 0; i < _lastIndex; i++)
            {
                ref Slot slot = ref _slots[i];
                if (!slot.exists)
                    continue;

                slot.exists = false;
                slot.next = UNDEFINED_INDEX;
                removedItems[removedCount++] = new KeyValuePair<K, V>(slot.key, slot.value);
            }

            Array.Fill(_buckets, UNDEFINED_INDEX);

            _count = 0;
            _freeList = UNDEFINED_INDEX;
            _lastIndex = 0;

            try
            {
                this.OnStateChanged?.Invoke();

                for (int i = 0; i < removedCount; i++)
                {
                    ref readonly KeyValuePair<K, V> pair = ref removedItems[i];
                    this.OnItemRemoved?.Invoke(pair.Key, pair.Value);
                }
            }
            finally
            {
                s_pairArrayPool.Return(removedItems);
            }
        }

        /// <summary>
        /// Copies the elements of the dictionary to an array, starting at a particular index.
        /// </summary>
        /// <param name="array">The destination array.</param>
        /// <param name="arrayIndex">The zero-based index in the array at which copying begins.</param>
        public void CopyTo(KeyValuePair<K, V>[] array, int arrayIndex = 0)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            for (int i = 0; i < _lastIndex; i++)
            {
                ref readonly Slot slot = ref _slots[i];
                if (slot.exists)
                    array[arrayIndex++] = new KeyValuePair<K, V>(slot.key, slot.value);
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the dictionary.
        /// </summary>
        public Enumerator GetEnumerator() => new(this);

        /// <inheritdoc/>
        IEnumerator<KeyValuePair<K, V>> IEnumerable<KeyValuePair<K, V>>.GetEnumerator() => new Enumerator(this);

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Clear();
            this.OnStateChanged = null;
            this.OnItemChanged = null;
            this.OnItemAdded = null;
            this.OnItemRemoved = null;
        }

        public struct Enumerator : IEnumerator<KeyValuePair<K, V>>
        {
            private readonly ReactiveDictionary<K, V> _dictionary;
            private int _index;
            private KeyValuePair<K, V> _current;

            public KeyValuePair<K, V> Current => _current;
            object IEnumerator.Current => _current;

            public Enumerator(ReactiveDictionary<K, V> dictionary)
            {
                _dictionary = dictionary;
                _index = 0;
                _current = default;
            }

            public bool MoveNext()
            {
                while (_index < _dictionary._lastIndex)
                {
                    ref readonly Slot slot = ref _dictionary._slots[_index++];
                    if (slot.exists)
                    {
                        _current = new KeyValuePair<K, V>(slot.key, slot.value);
                        return true;
                    }
                }

                _current = default;
                return false;
            }

            void IEnumerator.Reset()
            {
                _index = 0;
                _current = default;
            }

            public void Dispose()
            {
                //Do nothing...
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool FindIndex(K key, out int index)
        {
            if (_count == 0)
            {
                index = UNDEFINED_INDEX;
                return false;
            }

            int hash = s_keyComparer.GetHashCode(key) & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            index = _buckets[bucket];

            while (index >= 0)
            {
                ref readonly Slot slot = ref _slots[index];
                if (slot.exists && s_keyComparer.Equals(slot.key, key))
                    return true;

                index = slot.next;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void AddInternal(K key, V value)
        {
            int index;
            if (_freeList >= 0)
            {
                index = _freeList;
                _freeList = _slots[index].next;
            }
            else
            {
                if (_lastIndex == _capacity)
                    this.IncreaseCapacity();

                index = _lastIndex;
                _lastIndex++;
            }

            int hash = s_keyComparer.GetHashCode(key) & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            ref int next = ref _buckets[bucket];

            _slots[index] = new Slot
            {
                key = key,
                value = value,
                next = next,
                exists = true
            };

            next = index;
            _count++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncreaseCapacity()
        {
            _capacity = InternalUtils.PrimeTable[++_primeIndex];

            Array.Resize(ref _slots, _capacity);
            Array.Resize(ref _buckets, _capacity);
            Array.Fill(_buckets, UNDEFINED_INDEX);

            for (int i = 0; i < _lastIndex; i++)
            {
                ref Slot slot = ref _slots[i];
                if (!slot.exists)
                    continue;

                int hash = s_keyComparer.GetHashCode(slot.key) & 0x7FFFFFFF;
                int bucket = hash % _capacity;
                ref int next = ref _buckets[bucket];

                slot.next = next;
                next = i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool RemoveInternal(K key, out V value)
        {
            if (_count == 0)
            {
                value = default;
                return false;
            }

            int hash = s_keyComparer.GetHashCode(key) & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            ref int next = ref _buckets[bucket];

            int index = next;
            int last = UNDEFINED_INDEX;

            while (index >= 0)
            {
                ref Slot slot = ref _slots[index];
                if (s_keyComparer.Equals(slot.key, key))
                {
                    if (last == UNDEFINED_INDEX)
                        next = slot.next;
                    else
                        _slots[last].next = slot.next;

                    value = slot.value;

                    slot.next = _freeList;
                    slot.exists = false;

                    _count--;

                    if (_count == 0)
                    {
                        _lastIndex = 0;
                        _freeList = UNDEFINED_INDEX;
                    }
                    else
                    {
                        _freeList = index;
                    }

                    return true;
                }

                last = index;
                index = slot.next;
            }

            value = default;
            return false;
        }

        public readonly struct ReadOnlyKeyCollection : ICollection<K>
        {
            private readonly ReactiveDictionary<K, V> _dictionary;

            internal ReadOnlyKeyCollection(ReactiveDictionary<K, V> dictionary) => _dictionary = dictionary;

            public int Count => _dictionary._count;

            public bool IsReadOnly => true;

            public Enumerator GetEnumerator() => new(_dictionary);

            IEnumerator<K> IEnumerable<K>.GetEnumerator() => new Enumerator(_dictionary);

            IEnumerator IEnumerable.GetEnumerator() => new Enumerator(_dictionary);

            public bool Contains(K item) => item != null && _dictionary.ContainsKey(item);

            public void CopyTo(K[] array, int arrayIndex)
            {
                if (array == null)
                    throw new ArgumentNullException(nameof(array));

                if (arrayIndex < 0)
                    throw new ArgumentOutOfRangeException(nameof(arrayIndex));

                int i = arrayIndex;
                for (int s = 0; s < _dictionary._lastIndex; s++)
                {
                    ref readonly Slot slot = ref _dictionary._slots[s];
                    if (slot.exists)
                        array[i++] = slot.key;
                }
            }

            void ICollection<K>.Add(K item) =>
                throw new NotSupportedException("KeyCollection is read-only.");

            void ICollection<K>.Clear() =>
                throw new NotSupportedException("KeyCollection is read-only.");

            bool ICollection<K>.Remove(K item) =>
                throw new NotSupportedException("KeyCollection is read-only.");

            public struct Enumerator : IEnumerator<K>
            {
                private readonly ReactiveDictionary<K, V> _dictionary;
                private int _index;
                private K _current;

                internal Enumerator(ReactiveDictionary<K, V> dictionary)
                {
                    _dictionary = dictionary;
                    _index = 0;
                    _current = default;
                }

                public K Current => _current;
                object IEnumerator.Current => _current;

                public bool MoveNext()
                {
                    while (_index < _dictionary._lastIndex)
                    {
                        ref readonly Slot slot = ref _dictionary._slots[_index++];
                        if (slot.exists)
                        {
                            _current = slot.key;
                            return true;
                        }
                    }

                    _current = default;
                    return false;
                }

                public void Reset()
                {
                    _index = 0;
                    _current = default;
                }

                public void Dispose()
                {
                    // ничего
                }
            }
        }

        public readonly struct ReadOnlyValueCollection : ICollection<V>
        {
            private readonly ReactiveDictionary<K, V> _dictionary;

            internal ReadOnlyValueCollection(ReactiveDictionary<K, V> dictionary) =>
                _dictionary = dictionary;

            public int Count => _dictionary._count;
            public bool IsReadOnly => true;

            public Enumerator GetEnumerator() => new(_dictionary);
            IEnumerator<V> IEnumerable<V>.GetEnumerator() => GetEnumerator();
            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

            public bool Contains(V item)
            {
                var comparer = s_valueComparer;
                for (int i = 0; i < _dictionary._lastIndex; i++)
                {
                    ref readonly Slot slot = ref _dictionary._slots[i];
                    if (slot.exists && comparer.Equals(slot.value, item))
                        return true;
                }

                return false;
            }

            public void CopyTo(V[] array, int arrayIndex)
            {
                if (array == null)
                    throw new ArgumentNullException(nameof(array));

                if (arrayIndex < 0)
                    throw new ArgumentOutOfRangeException(nameof(arrayIndex));

                int i = arrayIndex;
                for (int s = 0; s < _dictionary._lastIndex; s++)
                {
                    ref readonly Slot slot = ref _dictionary._slots[s];
                    if (slot.exists)
                        array[i++] = slot.value;
                }
            }

            public void Add(V item) =>
                throw new NotSupportedException("ValueCollection is read-only.");

            public void Clear() =>
                throw new NotSupportedException("ValueCollection is read-only.");

            public bool Remove(V item) =>
                throw new NotSupportedException("ValueCollection is read-only.");

            public struct Enumerator : IEnumerator<V>
            {
                private readonly ReactiveDictionary<K, V> _dictionary;
                private int _index;
                private V _current;

                internal Enumerator(ReactiveDictionary<K, V> dictionary)
                {
                    _dictionary = dictionary;
                    _index = 0;
                    _current = default;
                }

                public V Current => _current;
                object IEnumerator.Current => _current;

                public bool MoveNext()
                {
                    while (_index < _dictionary._lastIndex)
                    {
                        ref readonly Slot slot = ref _dictionary._slots[_index++];
                        if (slot.exists)
                        {
                            _current = slot.value;
                            return true;
                        }
                    }

                    _current = default;
                    return false;
                }

                public void Reset()
                {
                    _index = 0;
                    _current = default;
                }

                public void Dispose()
                {
                    // ничего
                }
            }
        }
    }

#if UNITY_5_3_OR_NEWER
    [Serializable]
    public partial class ReactiveDictionary<K, V> : ISerializationCallbackReceiver
    {
        [Serializable]
        internal struct SerializedKeyValuePair
        {
            public K key;
            public V value;
        }

        [SerializeField]
        internal SerializedKeyValuePair[] serializedItems;

        /// <summary>
        /// Unity callback invoked after the object has been deserialized.
        /// Reconstructs the internal dictionary from the serialized pair array.
        /// </summary>
        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            if (serializedItems == null)
                return;

            for (int i = 0, count = serializedItems.Length; i < count; i++)
            {
                SerializedKeyValuePair pair = serializedItems[i];
                this.Add(pair.key, pair.value);
            }

            this.OnStateChanged?.Invoke();
        }

        /// <summary>
        /// Unity callback invoked before the object is serialized.
        /// Flattens the internal dictionary to a serializable array of key-value pairs.
        /// </summary>
        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            serializedItems = new SerializedKeyValuePair[_count];

            int i = 0;
            foreach ((K key, V value) in this)
            {
                serializedItems[i++] = new SerializedKeyValuePair
                {
                    key = key,
                    value = value
                };
            }
        }
    }
#endif
}