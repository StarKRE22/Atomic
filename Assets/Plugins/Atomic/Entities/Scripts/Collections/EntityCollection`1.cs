using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    /// <summary>
    /// A performant and flexible collection designed to store unique <see cref="IEntity"/> elements
    /// with fast lookup, insertion, and deletion. Combines hash table and linked list semantics
    /// for both efficient access and ordered enumeration.
    /// </summary>
    /// <typeparam name="E">The type of the entity. Must implement <see cref="IEntity"/>.</typeparam>
    public class EntityCollection<E> : IEntityCollection<E> where E : IEntity
    {
        private protected const int UNDEFINED_INDEX = -1;
        private protected const int DEFAULT_CAPACITY = 3;

        private protected static readonly ArrayPool<E> s_arrayPool = ArrayPool<E>.Shared;

        private protected struct Slot
        {
            public E value;
            public int hashCode;

            public int next; //hash collision chain
            public int orderIndex;
        }

        /// <inheritdoc/>
        public event Action OnStateChanged;

        /// <inheritdoc/>
        public event Action<E> OnAdded;

        /// <inheritdoc/>
        public event Action<E> OnRemoved;

        public int Count => _count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        private protected int _capacity;
        private protected int _count;
        private int _primeIndex;

        private protected Slot[] _slots;
        private protected int[] _buckets;
        private protected int _freeList;
        private protected int _lastIndex;

        private protected int[] _order;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection{E}"/> class with default capacity.
        /// </summary>
        public EntityCollection() : this(DEFAULT_CAPACITY)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection{E}"/> class with a predefined capacity.
        /// </summary>
        /// <param name="capacity">Initial capacity of the collection.</param>
        public EntityCollection(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            _capacity = CeilToPrime(capacity, out _primeIndex);
            _order = new int[_capacity];
            _count = 0;
            _lastIndex = 0;

            _freeList = UNDEFINED_INDEX;
            _slots = new Slot[_capacity];
            _buckets = new int[_capacity];

            Array.Fill(_buckets, UNDEFINED_INDEX, 0, _capacity);
        }

        /// <summary>
        /// Initializes a new instance with an enumerable of entities.
        /// </summary>
        /// <param name="elements">Enumerable to populate the collection with.</param>
        public EntityCollection(IEnumerable<E> elements) : this(elements.Count()) => this.AddRange(elements);
        
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection{E}"/> class with the provided entities.
        /// </summary>
        /// <param name="entities">Array of initial entities.</param>
        public EntityCollection(params E[] entities) : this(entities.Length) => this.AddRange(entities);

        /// <summary>
        /// Initializes a new instance with a collection of entities.
        /// </summary>
        /// <param name="elements">Entities to populate the collection with.</param>
        public EntityCollection(IReadOnlyCollection<E> elements) : this(elements.Count) => this.AddRange(elements);


        public E this[int index] => index < 0 || index >= _count
            ? throw new ArgumentOutOfRangeException(nameof(index))
            : _slots[_order[index]].value;

        public bool TryGetAt(int index, out E entity)
        {
            if (index < 0 || index >= _count)
            {
                entity = default;
                return false;
            }

            entity = _slots[_order[index]].value;
            return true;
        }

        /// <inheritdoc cref="IEntityCollection{E}.Contains" />
        public bool Contains(E item)
        {
            if (_count == 0 || item == null)
                return false;

            int hashCode = item.InstanceID;
            int bucket = hashCode % _capacity;
            int current = _buckets[bucket];

            while (current != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[current];
                if (slot.hashCode == hashCode)
                    return true;

                current = slot.next;
            }

            return false;
        }

        /// <summary>
        /// Adds an item to the set. Part of <see cref="ICollection{T}"/>.
        /// </summary>
        /// <param name="item">The item to add.</param>
        void ICollection<E>.Add(E item) => this.Add(item);

        /// <inheritdoc/>
        public bool Add(E item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            int hashCode = item.InstanceID;
            int bucket = hashCode % _capacity;
            ref int head = ref _buckets[bucket];

            // Check if item already exists
            int current = head;
            while (current != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[current];
                if (slot.hashCode == hashCode)
                    return false;

                current = slot.next;
            }

            int index;

            // Allocate new slot or reuse from a free list
            if (_freeList >= 0)
            {
                index = _freeList;
                _freeList = _slots[index].next;
            }
            else
            {
                // Expand capacity if needed
                if (_lastIndex == _capacity)
                {
                    int newCapacity = PrimeTable[++_primeIndex];
                    Array.Resize(ref _order, newCapacity);

                    var newSlots = new Slot[newCapacity];
                    Array.Copy(_slots, newSlots, _lastIndex);
                    _slots = newSlots;

                    var newBuckets = new int[newCapacity];
                    Array.Fill(newBuckets, UNDEFINED_INDEX);

                    for (int j = 0; j < _lastIndex; j++)
                    {
                        ref Slot s = ref _slots[j];
                        int bucketIndex = (s.hashCode & 0x7FFFFFFF) % newCapacity;
                        s.next = newBuckets[bucketIndex];
                        newBuckets[bucketIndex] = j;
                    }

                    _buckets = newBuckets;
                    _capacity = newCapacity;

                    // Recalculate bucket and head after resize
                    bucket = hashCode % _capacity;
                    head = ref _buckets[bucket];
                }

                index = _lastIndex++;
            }

            // Store slot
            _slots[index].value = item;
            _slots[index].hashCode = hashCode;
            _slots[index].next = head;
            _slots[index].orderIndex = _count;

            // Update bucket
            head = index;

            _order[_count] = index;
            _count++;

            this.OnAdd(item);
            this.OnAdded?.Invoke(item);
            this.OnStateChanged?.Invoke();
            return true;
        }

        /// <summary>
        /// Called automatically when entity was added.
        /// </summary>
        /// <param name="entity">The added entity</param>
        protected virtual void OnAdd(E entity)
        {
        }

        /// <inheritdoc/>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public bool Remove(E item)
        {
            if (_count == 0 || item == null)
                return false;

            var slots = _slots;
            var buckets = _buckets;

            int hash = item.InstanceID;
            int bucket = hash % _capacity;
            ref int cur = ref buckets[bucket];

            while (true)
            {
                int idx = cur;
                if (idx < 0)
                    break;

                ref Slot slot = ref slots[idx];
                if (slot.hashCode == hash)
                {
                    cur = slot.next;

                    slot.next = _freeList;
                    _freeList = idx;

                    int orderIndex = slot.orderIndex;

                    slot.hashCode = UNDEFINED_INDEX;
                    slot.value = default;
                    slot.orderIndex = UNDEFINED_INDEX;

                    int lastOrderIndex = _count - 1;

                    if (orderIndex != lastOrderIndex)
                    {
                        int swappedSlotIndex = _order[lastOrderIndex];

                        _order[orderIndex] = swappedSlotIndex;
                        _slots[swappedSlotIndex].orderIndex = orderIndex;
                    }

                    _order[lastOrderIndex] = UNDEFINED_INDEX;

                    _count--;
                    if (_count == 0)
                    {
                        _lastIndex = 0;
                        _freeList = UNDEFINED_INDEX;
                    }

                    this.OnRemove(item);
                    this.OnRemoved?.Invoke(item);
                    this.OnStateChanged?.Invoke();
                    return true;
                }

                cur = ref slot.next;
            }

            return false;
        }

        /// <summary>
        /// Called automatically when entity was removed
        /// </summary>
        /// <param name="entity">The removed entity</param>
        protected virtual void OnRemove(E entity)
        {
        }

        /// <inheritdoc/>
        public void Clear()
        {
            if (_count == 0)
                return;

            Array.Clear(_order, 0, _count);

            int removeCount = 0;
            E[] removedEntities = s_arrayPool.Rent(_count);

            for (int i = 0; i < _lastIndex; i++)
            {
                _buckets[i] = UNDEFINED_INDEX;

                ref Slot slot = ref _slots[i];
                if (slot.hashCode == UNDEFINED_INDEX)
                    continue;

                slot.hashCode = UNDEFINED_INDEX;
                slot.next = UNDEFINED_INDEX;

                removedEntities[removeCount++] = slot.value;
            }

            _count = 0;
            _lastIndex = 0;
            _freeList = UNDEFINED_INDEX;

            try
            {
                for (int i = 0; i < removeCount; i++)
                {
                    E entity = removedEntities[i];
                    this.OnRemove(entity);
                    this.OnRemoved?.Invoke(entity);
                }

                this.OnStateChanged?.Invoke();
            }
            finally
            {
                s_arrayPool.Return(removedEntities);
            }
        }

        /// <summary>
        /// Clears the collection and releases events.
        /// </summary>
        public virtual void Dispose()
        {
            this.Clear();

            //Unsubscribe events:
            this.OnAdded = null;
            this.OnRemoved = null;
            this.OnStateChanged = null;
        }

        /// <summary>
        /// Copies the elements of the set into the specified array, starting at the given index.
        /// </summary>
        /// <param name="array">Destination array.</param>
        /// <param name="arrayIndex">Starting index in the destination array.</param>
        /// <exception cref="ArgumentNullException">Thrown if the array is null.</exception>
        /// <exception cref="ArgumentOutOfRangeException">Thrown if the index is negative.</exception>
        public void CopyTo(E[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            if (array.Length - arrayIndex < _count)
                throw new ArgumentException("The target array is too small to hold all elements.");

            for (int i = 0; i < _count; i++) 
                array[arrayIndex + i] = _slots[_order[i]].value;
        }

        /// <summary>
        /// Copies the elements of the set into the specified array, starting at the given index.
        /// </summary>
        /// <param name="results">Destination collection</param>
        public void CopyTo(ICollection<E> results)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            for (int i = 0; i < _count; i++)
                results.Add(_slots[_order[i]].value);
        }

        /// <summary>
        /// Returns an enumerator that iterates through the set.
        /// </summary>
        public Enumerator GetEnumerator() => new(this);

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        /// <inheritdoc/>
        IEnumerator<E> IEnumerable<E>.GetEnumerator() => new Enumerator(this);

        /// <inheritdoc/>
        public struct Enumerator : IEnumerator<E>
        {
            public E Current => _current;
            object IEnumerator.Current => _current;

            private readonly EntityCollection<E> _collection;
            private int _index;
            private E _current;

            public Enumerator(EntityCollection<E> collection)
            {
                _collection = collection;
                _index = 0;
                _current = default;
            }

            public bool MoveNext()
            {
                if (_index >= _collection._count)
                    return false;

                int slotIndex = _collection._order[_index++];
                _current = _collection._slots[slotIndex].value;
                return true;
            }

            public void Reset()
            {
                _index = 0;
            }

            public void Dispose()
            {
            }
        }

        /// <summary>
        /// Notifies subscribers that the internal state of the collection has changed.
        /// </summary>
        /// <remarks>
        /// This method invokes the <see cref="OnStateChanged"/> event if there are any subscribers.
        /// It should be called after operations that modify the state of the collection,
        /// such as <see cref="Add(E)"/>, <see cref="Remove(E)"/>, or <see cref="Clear()"/>.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private protected void NotifyAboutStateChanged() => this.OnStateChanged?.Invoke();
    }
}