using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;

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
        /// <summary>
        /// Occurs when the overall state of the dictionary changes.
        /// This includes bulk operations or any modification affecting the dictionary as a whole.
        /// </summary>
        public event Action OnStateChanged;

        /// <inheritdoc/>
        public event Action<K, V> OnItemChanged;

        /// <inheritdoc/>
        public event Action<K, V> OnItemAdded;

        /// <inheritdoc/>
        public event Action<K, V> OnItemRemoved;

        /// <inheritdoc cref="IReadOnlyReactiveCollection{T}.OnItemAdded"/> 
        event Action<KeyValuePair<K, V>> IReadOnlyReactiveCollection<KeyValuePair<K, V>>.OnItemAdded
        {
            add => this.onItemAdded += value;
            remove => this.onItemAdded -= value;
        }

        /// <inheritdoc cref="IReadOnlyReactiveCollection{T}.OnItemRemoved"/> 
        event Action<KeyValuePair<K, V>> IReadOnlyReactiveCollection<KeyValuePair<K, V>>.OnItemRemoved
        {
            add => this.onItemRemoved += value;
            remove => this.onItemRemoved -= value;
        }

        private Action<KeyValuePair<K, V>> onItemAdded;
        private Action<KeyValuePair<K, V>> onItemRemoved;

        /// <summary>
        /// Gets a collection containing the keys in the dictionary.
        /// </summary>
        public ReadOnlyKeyCollection Keys => new(this);

        /// <inheritdoc/>
        ICollection<K> IDictionary<K, V>.Keys => new ReadOnlyKeyCollection(this);

        /// <inheritdoc/>
        IEnumerable<K> IReadOnlyDictionary<K, V>.Keys => new ReadOnlyKeyCollection(this);

        /// <summary>
        /// Gets a collection containing the values in the dictionary.
        /// </summary>
        public ReadOnlyValueCollection Values => new(this);

        /// <inheritdoc/>
        ICollection<V> IDictionary<K, V>.Values => new ReadOnlyValueCollection(this);

        /// <inheritdoc/>
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
        /// Initializes a new instance of the <see cref="ReactiveDictionary{K,V}"/> class 
        /// with elements from the specified collection of key-value pairs.
        /// </summary>
        /// <param name="source">The sequence of key-value pairs to copy into the dictionary.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="source"/> contains duplicate keys.</exception>
        public ReactiveDictionary(IEnumerable<KeyValuePair<K, V>> source) : this(source.Count())
        {
            foreach (KeyValuePair<K, V> pair in source) this.Add(pair);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveDictionary{K,V}"/> class 
        /// with elements from the specified sequence of tuples.
        /// </summary>
        /// <param name="source">The sequence of (key, value) tuples to copy into the dictionary.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="source"/> contains duplicate keys.</exception>
        public ReactiveDictionary(IEnumerable<(K, V)> source) : this(source.Count())
        {
            foreach ((K, V) pair in source) this.Add(pair.Item1, pair.Item2);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveDictionary{K,V}"/> class 
        /// with elements from the specified array of key-value pairs.
        /// </summary>
        /// <param name="source">An array of key-value pairs to copy into the dictionary.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="source"/> contains duplicate keys.</exception>
        public ReactiveDictionary(params KeyValuePair<K, V>[] source) : this(source.Length)
        {
            for (int i = 0, count = source.Length; i < count; i++) this.Add(source[i]);
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="ReactiveDictionary{K,V}"/> class 
        /// with elements from the specified array of (key, value) tuples.
        /// </summary>
        /// <param name="source">An array of (key, value) tuples to copy into the dictionary.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="source"/> is null.</exception>
        /// <exception cref="ArgumentException">Thrown if <paramref name="source"/> contains duplicate keys.</exception>
        public ReactiveDictionary(params (K, V)[] source) : this(source.Length)
        {
            for (int i = 0, count = source.Length; i < count; i++)
            {
                (K, V) tuple = source[i];
                this.Add(tuple.Item1, tuple.Item2);
            }
        }

        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <param name="key">The key of the value to get or set.</param>
        /// <returns>The value associated with the specified key.</returns>
        public V this[K key]
        {
            get
            {
                if (_count == 0)
                    throw new KeyNotFoundException(nameof(key));

                int hash = key.GetHashCode() & 0x7FFFFFFF;
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
            set
            {
                int index = -1;

                //Find index:
                if (_count > 0)
                {
                    int hash = key.GetHashCode() & 0x7FFFFFFF;
                    int bucket = hash % _capacity;
                    index = _buckets[bucket];

                    while (index >= 0)
                    {
                        ref readonly Slot slot = ref _slots[index];
                        if (slot.exists && s_keyComparer.Equals(slot.key, key))
                            break;

                        index = slot.next;
                    }
                }

                //If item exists
                if (index >= 0)
                {
                    ref Slot slot = ref _slots[index];
                    if (s_valueComparer.Equals(slot.value, value))
                        return;

                    slot.value = value;
                    this.OnStateChanged?.Invoke();
                    this.OnItemChanged?.Invoke(key, value);
                }

                //Add item
                else
                {
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

                    int hash = key.GetHashCode() & 0x7FFFFFFF;
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

                    this.OnStateChanged?.Invoke();
                    this.OnItemAdded?.Invoke(key, value);
                    this.onItemAdded?.Invoke(new KeyValuePair<K, V>(key, value));
                }
            }
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
        /// Adds a key/value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Add(K key, V value)
        {
            if (value == null)
                throw new ArgumentNullException(nameof(value));

            int hash = key.GetHashCode() & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            int index = _buckets[bucket];

            while (index >= 0)
            {
                ref readonly Slot slot = ref _slots[index];
                if (slot.exists && s_keyComparer.Equals(slot.key, key))
                    throw new ArgumentException($"Value with key {key} is already added!");

                index = slot.next;
            }

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
                bucket = hash % _capacity;

                _lastIndex++;
            }

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

            this.OnStateChanged?.Invoke();
            this.OnItemAdded?.Invoke(key, value);
            this.onItemAdded?.Invoke(new KeyValuePair<K, V>(key, value));
        }
        
        /// <summary>
        /// Attempts to add the specified <see cref="KeyValuePair{K,V}"/> to the dictionary.
        /// </summary>
        /// <param name="item">The key/value pair to add. The key must be unique within the dictionary.</param>
        /// <returns>
        /// <c>true</c> if the key/value pair was added successfully; 
        /// <c>false</c> if the key already exists in the dictionary.
        /// </returns>
        /// <remarks>
        /// This method does not throw an exception if the key already exists, 
        /// unlike <see cref="Add(K,V)"/>.
        /// </remarks>
        public bool TryAdd(KeyValuePair<K, V> item)
        {
            (K key, V value) = item;
            return this.TryAdd(key, value);
        }
        
        /// <summary>
        /// Attempts to add key/value pair to the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to add.</param>
        /// <param name="value">The value of the element to add.</param>
        /// <returns>
        /// <c>true</c> if the key/value pair was added successfully; 
        /// <c>false</c> if the key already exists in the dictionary.
        /// </returns>
        /// <remarks>
        /// This method does not throw an exception if the key already exists, 
        /// unlike <see cref="Add(K,V)"/>.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool TryAdd(K key, V value)
        {
            if (value == null)
                return false;

            int hash = key.GetHashCode() & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            int index = _buckets[bucket];

            while (index >= 0)
            {
                ref readonly Slot slot = ref _slots[index];
                if (slot.exists && s_keyComparer.Equals(slot.key, key))
                    return false;
                
                index = slot.next;
            }

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
                bucket = hash % _capacity;

                _lastIndex++;
            }

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

            this.OnStateChanged?.Invoke();
            this.OnItemAdded?.Invoke(key, value);
            this.onItemAdded?.Invoke(new KeyValuePair<K, V>(key, value));
            return true;
        }
        
        /// <summary>
        /// Removes the value with the specified key from the dictionary.
        /// </summary>
        /// <param name="key">The key of the element to remove.</param>
        /// <returns>True if the element is successfully removed; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(K key)
        {
            if (_count == 0)
                return false;

            int hash = key.GetHashCode() & 0x7FFFFFFF;
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

                    V value = slot.value;

                    slot.next = _freeList;
                    slot.exists = false;
                    _freeList = index;

                    _count--;

                    if (_count == 0)
                    {
                        _lastIndex = 0;
                        _freeList = UNDEFINED_INDEX;
                    }

                    OnItemRemoved?.Invoke(key, value);
                    onItemRemoved?.Invoke(new KeyValuePair<K, V>(key, value));
                    OnStateChanged?.Invoke();
                    return true;
                }

                last = index;
                index = slot.next;
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
            if (_count == 0)
            {
                value = default;
                return false;
            }

            int hash = key.GetHashCode() & 0x7FFFFFFF;
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
                    _freeList = index;

                    _count--;

                    if (_count == 0)
                    {
                        _lastIndex = 0;
                        _freeList = UNDEFINED_INDEX;
                    }

                    OnItemRemoved?.Invoke(key, value);
                    onItemRemoved?.Invoke(new KeyValuePair<K, V>(key, value));
                    OnStateChanged?.Invoke();
                    return true;
                }

                last = index;
                index = slot.next;
            }

            value = default;
            return false;
        }

        /// <summary>
        /// Removes the specified key/value pair from the dictionary.
        /// </summary>
        /// <param name="item">The key/value pair to remove.</param>
        /// <returns>True if the item was removed; otherwise, false.</returns>
        public bool Remove(KeyValuePair<K, V> item)
        {
            return this.Contains(item) && this.Remove(item.Key);
        }
        
        /// <summary>
        /// Determines whether the dictionary contains the specified key.
        /// </summary>
        /// <param name="key">The key to locate.</param>
        /// <returns>True if the key is found; otherwise, false.</returns>
        public bool ContainsKey(K key)
        {
            if (_count > 0)
            {
                int hash = key.GetHashCode() & 0x7FFFFFFF;
                int bucket = hash % _capacity;
                int index = _buckets[bucket];

                while (index >= 0)
                {
                    ref readonly Slot slot = ref _slots[index];
                    if (slot.exists && s_keyComparer.Equals(slot.key, key))
                        return true;

                    index = slot.next;
                }
            }

            return false;
        }

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

            int hash = key.GetHashCode() & 0x7FFFFFFF;
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
        /// Removes all keys and values from the dictionary.
        /// </summary>
        public void Clear()
        {
            if (_count == 0) return;

            for (int i = 0; i < _lastIndex; i++)
            {
                ref Slot slot = ref _slots[i];
                if (!slot.exists) continue;

                slot.exists = false;
                slot.next = UNDEFINED_INDEX;
                OnItemRemoved?.Invoke(slot.key, slot.value);
                onItemRemoved?.Invoke(new KeyValuePair<K, V>(slot.key, slot.value));
            }

            Array.Fill(_buckets, UNDEFINED_INDEX);

            _count = 0;
            _freeList = UNDEFINED_INDEX;
            _lastIndex = 0;

            OnStateChanged?.Invoke();
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

                int hash = slot.key.GetHashCode() & 0x7FFFFFFF;
                int bucket = hash % _capacity;
                ref int next = ref _buckets[bucket];

                slot.next = next;
                next = i;
            }
        }
    }
}