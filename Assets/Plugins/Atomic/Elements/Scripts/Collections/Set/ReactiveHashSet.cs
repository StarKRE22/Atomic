using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static Atomic.Elements.InternalUtils;

// ReSharper disable ConditionIsAlwaysTrueOrFalse
// ReSharper disable HeuristicUnreachableCode
// ReSharper disable AssignNullToNotNullAttribute

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// A reactive hash set that raises events when items are added, removed, or the entire set is modified.
    /// Implements a custom internal hash-based storage system with support for efficient lookups and updates.
    /// </summary>
    /// <typeparam name="T">The element type.</typeparam>
    public partial class ReactiveHashSet<T> : IReactiveSet<T>, IDisposable
    {
        /// <inheritdoc/>
        public event Action OnStateChanged;

        /// <inheritdoc/>
        public event Action<T> OnItemAdded;

        /// <inheritdoc/>
        public event Action<T> OnItemRemoved;

        /// <inheritdoc cref="ICollection{T}.Count" />
        public int Count => _count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        private struct Slot
        {
            public T value;
            public int next;
            public bool exists;
        }

        private const int UNDEFINED_INDEX = -1;
        private static readonly IEqualityComparer<T> s_comparer = EqualityComparer.GetDefault<T>();
        private static readonly ArrayPool<T> s_arrayPool = ArrayPool<T>.Shared;

        private Slot[] _slots;
        private int[] _buckets;
        private int _capacity;
        private int _count;
        private int _freeList;
        private int _lastIndex;
        private int _primeIndex;

        /// <summary>
        /// Initializes the set with a predefined capacity.
        /// </summary>
        /// <param name="capacity">Initial number of slots to allocate.</param>
        public ReactiveHashSet(int capacity = 0)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _capacity = CeilToPrime(capacity, out _primeIndex);
            _slots = new Slot[_capacity];
            _buckets = new int[_capacity];
            Array.Fill(_buckets, UNDEFINED_INDEX);

            _count = 0;
            _lastIndex = 0;
            _freeList = UNDEFINED_INDEX;
        }

        /// <summary>
        /// Initializes the set with a collection of elements.
        /// </summary>
        /// <param name="elements">The initial elements to add.</param>
        public ReactiveHashSet(params T[] elements) : this(elements.Length) => this.UnionWith(elements);

        /// <summary>
        /// Initializes the set with a collection of elements.
        /// </summary>
        /// <param name="elements">The initial elements to add.</param>
        public ReactiveHashSet(IReadOnlyCollection<T> elements) : this(elements.Count) => this.UnionWith(elements);

        /// <summary>
        /// Initializes the set with a collection of elements.
        /// </summary>
        /// <param name="elements">The initial elements to add.</param>
        public ReactiveHashSet(IEnumerable<T> elements) : this(elements.Count()) => this.UnionWith(elements);

        /// <summary>
        /// Checks whether the set contains the given item.
        /// </summary>
        /// <param name="item">The item to look for.</param>
        /// <returns>True if the item exists in the set; otherwise, false.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Contains(T item)
        {
            if (item != null && _count > 0)
            {
                int hash = item.GetHashCode() & 0x7FFFFFFF;
                int bucket = hash % _capacity;
                int index = _buckets[bucket];

                while (index >= 0)
                {
                    ref readonly Slot slot = ref _slots[index];
                    if (slot.exists && s_comparer.Equals(slot.value, item))
                        return true;

                    index = slot.next;
                }
            }

            return false;
        }

        /// <summary>
        /// Returns true if the set has no elements.
        /// </summary>
        public bool IsEmpty() => _count == 0;

        /// <summary>
        /// Returns true if the set contains at least one element.
        /// </summary>
        public bool IsNotEmpty() => _count > 0;

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Add(T item)
        {
            if (item == null)
                return false;
            
            int hash = item.GetHashCode() & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            int index = _buckets[bucket];

            while (index >= 0)
            {
                ref readonly Slot slot = ref _slots[index];
                if (slot.exists && s_comparer.Equals(slot.value, item))
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
                value = item,
                next = next,
                exists = true
            };

            next = index;

            _count++;
            
            this.OnItemAdded?.Invoke(item);
            this.OnStateChanged?.Invoke();
            return true;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void UnionWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            T[] addedItems = s_arrayPool.Rent(other.Count());
            int addedCount = 0;

            foreach (T item in other)
                if (item != null && this.AddInternal(item))
                    addedItems[addedCount++] = item;

            try
            {
                if (addedCount > 0)
                {
                    for (int i = 0; i < addedCount; i++)
                        this.OnItemAdded?.Invoke(addedItems[i]);

                    this.OnStateChanged?.Invoke();
                }
            }
            finally
            {
                s_arrayPool.Return(addedItems);
            }
        }

        /// <inheritdoc/>
        public bool Remove(T item)
        {
            if (item == null || _count == 0)
                return false;

            int hash = item.GetHashCode() & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            ref int cur = ref _buckets[bucket];

            while (true)
            {
                int idx = cur;
                if (idx < 0)
                    break;

                ref Slot node = ref _slots[idx];

                if (s_comparer.Equals(node.value, item))
                {
                    cur = node.next;

                    node.exists = false;
                    node.next = _freeList;

                    _count--;
                    if (_count == 0)
                    {
                        _lastIndex = 0;
                        _freeList = UNDEFINED_INDEX;
                    }
                    else
                    {
                        _freeList = idx;
                    }

                    this.OnItemRemoved?.Invoke(item);
                    this.OnStateChanged?.Invoke();
                    return true;
                }

                cur = ref node.next;
            }

            return false;
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void Clear()
        {
            if (_count == 0)
                return;
            
            int removeCount = 0;
            T[] removedItems = s_arrayPool.Rent(_count);

            for (int i = 0; i < _lastIndex; i++)
            {
                ref Slot slot = ref _slots[i];
                if (!slot.exists)
                    continue;

                slot.exists = false;
                slot.next = UNDEFINED_INDEX;
                removedItems[removeCount++] = slot.value;
            }
            
            Array.Fill(_buckets, UNDEFINED_INDEX);

            _count = 0;
            _freeList = UNDEFINED_INDEX;
            _lastIndex = 0;

            try
            {
                for (int i = 0; i < removeCount; i++)
                    this.OnItemRemoved?.Invoke(removedItems[i]);

                this.OnStateChanged?.Invoke();
            }
            finally
            {
                s_arrayPool.Return(removedItems);
            }
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            this.Clear();

            this.OnItemAdded = null;
            this.OnItemRemoved = null;
            this.OnStateChanged = null;
        }

        /// <summary>
        /// Copies the elements of the set into the specified array, starting at the given index.
        /// </summary>
        /// <param name="array">Destination array.</param>
        /// <param name="arrayIndex">Starting index in the destination array.</param>
        /// <exception cref="ArgumentNullException">Thrown if the array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is negative.</exception>
        public void CopyTo(T[] array, int arrayIndex = 0)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            for (int i = 0; i < _lastIndex; i++)
            {
                ref readonly Slot slot = ref _slots[i];
                if (slot.exists)
                    array[arrayIndex++] = slot.value;
            }
        }

        /// <summary>
        /// Adds an item to the set. Part of <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void ICollection<T>.Add(T item) => this.Add(item);

        /// <summary>
        /// Returns an enumerator that iterates through the set.
        /// </summary>
        public Enumerator GetEnumerator() => new(this);

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        /// <inheritdoc/>
        IEnumerator<T> IEnumerable<T>.GetEnumerator() => new Enumerator(this);

        /// <summary>
        /// Removes all elements in the specified collection from the set.
        /// </summary>
        /// <param name="other">Collection of elements to remove.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="other"/> is null.</exception>
        public void ExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (_count == 0)
                return;

            T[] removedItems = s_arrayPool.Rent(other.Count());
            int removedCount = 0;

            foreach (T item in other)
                if (item != null && this.RemoveInternal(item))
                    removedItems[removedCount++] = item;

            try
            {
                if (removedCount > 0)
                {
                    for (int i = 0; i < removedCount; i++)
                        this.OnItemRemoved?.Invoke(removedItems[i]);

                    this.OnStateChanged?.Invoke();
                }
            }
            finally
            {
                s_arrayPool.Return(removedItems);
            }
        }

        /// <summary>
        /// Modifies the current set to contain only elements that are also in a specified collection.
        /// </summary>
        /// <param name="other">Collection to intersect with.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="other"/> is null.</exception>
        public void IntersectWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (_count == 0)
                return;

            Span<bool> intersectedItems = stackalloc bool[_lastIndex];

            foreach (T item in other)
                if (this.FindIndex(item, out int index))
                    intersectedItems[index] = true;

            T[] removedItems = s_arrayPool.Rent(other.Count());
            int removedCount = 0;

            for (int i = 0; i < _lastIndex; i++)
            {
                ref readonly Slot slot = ref _slots[i];
                if (slot.exists && !intersectedItems[i] && this.RemoveInternal(slot.value))
                    removedItems[removedCount++] = slot.value;
            }

            try
            {
                if (removedCount > 0)
                    for (int i = 0; i < removedCount; i++)
                        this.OnItemRemoved?.Invoke(removedItems[i]);

                this.OnStateChanged?.Invoke();
            }
            finally
            {
                s_arrayPool.Return(removedItems);
            }
        }

        /// <summary>
        /// Modifies the current set to contain elements that are in either the set or the specified collection,
        /// but not both.
        /// </summary>
        /// <param name="other">The collection to compare against.</param>
        /// <exception cref="ArgumentNullException">If <paramref name="other"/> is null.</exception>
        public void SymmetricExceptWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            T[] addedItems = s_arrayPool.Rent(_lastIndex);
            int addedCount = 0;

            T[] removedItems = s_arrayPool.Rent(_lastIndex);
            int removedCount = 0;

            foreach (T item in other)
            {
                if (item == null)
                    continue;

                if (this.AddInternal(item))
                    addedItems[addedCount++] = item;
                else if (this.RemoveInternal(item))
                    removedItems[removedCount++] = item;
            }

            try
            {
                for (int i = 0; i < addedCount; i++)
                    this.OnItemAdded?.Invoke(addedItems[i]);

                for (int i = 0; i < removedCount; i++)
                    this.OnItemRemoved?.Invoke(removedItems[i]);

                if (addedCount > 0 || removedCount > 0)
                    this.OnStateChanged?.Invoke();
            }
            finally
            {
                s_arrayPool.Return(addedItems);
                s_arrayPool.Return(removedItems);
            }
        }

        /// <summary>
        /// Determines whether the current set is a proper (strict) subset of a specified collection.
        /// </summary>
        /// <param name="other">Collection to compare with the current set.</param>
        /// <returns>
        /// True if the current set is a proper subset of <paramref name="other"/>; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="other"/> is null.</exception>
        public bool IsProperSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (other.Count() <= _count)
                return false;

            for (int i = 0; i < _lastIndex; i++)
            {
                Slot slot = _slots[i];
                if (!slot.exists)
                    continue;

                if (!InternalUtils.Contains(other, slot.value, s_comparer))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether the current set is a subset of a specified collection.
        /// </summary>
        /// <param name="other">Collection to compare with the current set.</param>
        /// <returns>True if the current set is a subset of <paramref name="other"/>.</returns>
        /// <exception cref="ArgumentNullException">If <paramref name="other"/> is null.</exception>
        public bool IsSubsetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (other.Count() < _count)
                return false;

            for (int i = 0; i < _lastIndex; i++)
            {
                Slot slot = _slots[i];
                if (!slot.exists)
                    continue;

                if (!InternalUtils.Contains(other, slot.value, s_comparer))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Determines whether the current set is a proper (strict) superset of a specified collection.
        /// </summary>
        /// <param name="other">Collection to compare with the current set.</param>
        /// <returns>
        /// True if the current set is a proper superset of <paramref name="other"/>; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">If <paramref name="other"/> is null.</exception>
        public bool IsProperSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (other.Count() >= _count)
                return false;

            foreach (T o in other)
                if (!this.Contains(o))
                    return false;

            return true;
        }

        /// <summary>
        /// Determines whether the current set is a superset of a specified collection.
        /// </summary>
        /// <param name="other">The collection to compare to the current set.</param>
        /// <returns>
        /// True if the current set is a superset of <paramref name="other"/>; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="other"/> is null.</exception>
        public bool IsSupersetOf(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (other.Count() > _count)
                return false;

            foreach (T o in other)
                if (!this.Contains(o))
                    return false;

            return true;
        }

        /// <summary>
        /// Determines whether the current set overlaps with the specified collection,
        /// i.e., they share at least one common element.
        /// </summary>
        /// <param name="other">The collection to compare with the current set.</param>
        /// <returns>
        /// True if the current set and <paramref name="other"/> share at least one common element; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="other"/> is null.</exception>
        public bool Overlaps(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (_count == 0)
                return false;

            foreach (T o in other)
                if (this.Contains(o))
                    return true;

            return false;
        }

        /// <summary>
        /// Determines whether the current set and the specified collection contain the same elements.
        /// </summary>
        /// <param name="other">The collection to compare with the current set.</param>
        /// <returns>
        /// True if both collections contain the same elements; otherwise, false.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="other"/> is null.</exception>
        public bool SetEquals(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            if (_count != other.Count())
                return false;

            foreach (T o in other)
                if (!this.Contains(o))
                    return false;

            return true;
        }

        /// <summary>
        /// Replaces all elements in the current set with elements from the specified collection.
        /// Triggers OnItemAdded and OnItemRemoved events for differences.
        /// </summary>
        /// <param name="other">The collection whose elements should replace the contents of the set.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="other"/> is null.</exception>
        public void ReplaceWith(IEnumerable<T> other)
        {
            if (other == null)
                throw new ArgumentNullException(nameof(other));

            int otherCount = other.Count();
            if (otherCount == 0)
            {
                this.Clear();
                return;
            }

            T[] addedItems = s_arrayPool.Rent(otherCount);
            int addedCount = 0;

            foreach (T item in other)
                if (item != null && this.AddInternal(item))
                    addedItems[addedCount++] = item;

            T[] removedItems = s_arrayPool.Rent(_lastIndex);
            int removedCount = 0;

            for (int i = 0; i < _lastIndex; i++)
            {
                ref readonly Slot slot = ref _slots[i];
                if (!slot.exists)
                    continue;

                T item = slot.value;
                if (!InternalUtils.Contains(other, item, s_comparer))
                    removedItems[removedCount++] = item;
            }

            for (int i = 0; i < removedCount; i++)
                this.RemoveInternal(removedItems[i]);

            try
            {
                for (int i = 0; i < addedCount; i++)
                    this.OnItemAdded?.Invoke(addedItems[i]);

                for (int i = 0; i < removedCount; i++)
                    this.OnItemRemoved?.Invoke(removedItems[i]);

                if (addedCount > 0 || removedCount > 0)
                    this.OnStateChanged?.Invoke();
            }
            finally
            {
                s_arrayPool.Return(addedItems);
                s_arrayPool.Return(removedItems);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void IncreaseCapacity()
        {
            _capacity = PrimeTable[++_primeIndex];
            Array.Resize(ref _slots, _capacity);
            Array.Resize(ref _buckets, _capacity);
            Array.Fill(_buckets, UNDEFINED_INDEX);

            for (int i = 0; i < _lastIndex; i++)
            {
                ref Slot slot = ref _slots[i];
                if (!slot.exists)
                    continue;
                
                int hash = s_comparer.GetHashCode(slot.value) & 0x7FFFFFFF;
                int bucket = hash % _capacity;
                ref int next = ref _buckets[bucket];
                slot.next = next;
                next = i;
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool RemoveInternal(T item)
        {
            if (_count == 0)
                return false;

            var comparer = s_comparer;
            var slots = _slots;    
            var buckets = _buckets;
            int capacity = _capacity;

            int hash = comparer.GetHashCode(item) & 0x7FFFFFFF;
            int bucket = hash % capacity;

            ref int cur = ref buckets[bucket];

            while (true)
            {
                int idx = cur;  
                if (idx < 0)
                    break;

                ref Slot node = ref slots[idx];

                if (comparer.Equals(node.value, item))
                {
                    cur = node.next;

                    node.exists = false;
                    node.next = _freeList;

                    _count--;
                    if (_count == 0)
                    {
                        _lastIndex = 0;
                        _freeList = UNDEFINED_INDEX;
                    }
                    else
                    {
                        _freeList = idx;
                    }

                    return true;
                }

                cur = ref node.next;
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool AddInternal(T item)
        {
            if (this.FindIndex(item, out int index))
                return false;

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
            
            int hash = item.GetHashCode() & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            ref int next = ref _buckets[bucket];

            _slots[index] = new Slot
            {
                value = item,
                next = next,
                exists = true
            };

            next = index;

            _count++;
            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool FindIndex(T item, out int index)
        {
            if (item == null || _count == 0)
            {
                index = UNDEFINED_INDEX;
                return false;
            }

            int hash = item.GetHashCode() & 0x7FFFFFFF;
            int bucket = hash % _capacity;
            index = _buckets[bucket];

            while (index >= 0)
            {
                ref readonly Slot slot = ref _slots[index];
                if (slot.exists && s_comparer.Equals(slot.value, item))
                    return true;

                index = slot.next;
            }

            return false;
        }

        //+
        public struct Enumerator : IEnumerator<T>
        {
            private readonly ReactiveHashSet<T> _set;
            private int _index;
            private T _current;

            public T Current => _current;
            object IEnumerator.Current => _current;

            internal Enumerator(ReactiveHashSet<T> set)
            {
                _set = set;
                _index = 0;
                _current = default;
            }

            public bool MoveNext()
            {
                while (_index < _set._lastIndex)
                {
                    ref readonly Slot slot = ref _set._slots[_index++];
                    if (slot.exists)
                    {
                        _current = slot.value;
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
    }
}

#if UNITY_5_3_OR_NEWER

namespace Atomic.Elements
{
    [Serializable]
    public partial class ReactiveHashSet<T> : ISerializationCallbackReceiver
    {
#if ODIN_INSPECTOR
        [HideInPlayMode]
#endif
        [SerializeField]
        internal T[] serializedItems;

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            this.serializedItems = this.ToArray();
        }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            this.Clear();

            if (this.serializedItems != null)
                this.UnionWith(serializedItems);
        }
    }
}

#endif