using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic version of <see cref="EntityCollection{E}"/> that operates specifically on <see cref="IEntity"/> instances.
    /// Provides the same functionality as the generic base class but simplifies usage when generic typing is unnecessary.
    /// </summary>
    public class EntityCollection : EntityCollection<IEntity>
    {
        /// <summary>
        /// Initializes a new empty instance of the <see cref="EntityCollection"/> class.
        /// </summary>
        public EntityCollection()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> class with the specified initial capacity.
        /// </summary>
        /// <param name="capacity">The number of entities the collection can initially store without resizing.</param>
        public EntityCollection(int capacity) : base(capacity)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> class with the provided entities.
        /// </summary>
        /// <param name="entities">A parameter array of entities to populate the collection with.</param>
        public EntityCollection(params IEntity[] entities) : base(entities)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> class using a read-only collection.
        /// </summary>
        /// <param name="elements">A read-only collection of entities to populate the collection with.</param>
        public EntityCollection(IReadOnlyCollection<IEntity> elements) : base(elements)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection"/> class using an enumerable.
        /// </summary>
        /// <param name="elements">An enumerable of entities to populate the collection with.</param>
        public EntityCollection(IEnumerable<IEntity> elements) : base(elements)
        {
        }
    }

    /// <summary>
    /// A performant and flexible collection designed to store unique <see cref="IEntity"/> elements
    /// with fast lookup, insertion, and deletion. Combines hash table and linked list semantics
    /// for both efficient access and ordered enumeration.
    /// </summary>
    /// <typeparam name="E">The type of the entity. Must implement <see cref="IEntity"/>.</typeparam>
    public class EntityCollection<E> : IEntityCollection<E> where E : IEntity
    {
        private protected const int UNDEFINED_INDEX = -1;

        private protected static readonly ArrayPool<E> s_arrayPool = ArrayPool<E>.Shared;

        private protected struct Slot
        {
            public E value;
            public int hashCode;

            public int next; //hash collision chain
            public int left; //previous in linked list
            public int right; //next in linked list
        }

        /// <inheritdoc/>
        public virtual event Action OnStateChanged;

        /// <inheritdoc/>
        public event Action<E> OnAdded;

        /// <inheritdoc/>
        public event Action<E> OnRemoved;

        public int Count => _count;

        /// <inheritdoc/>
        public bool IsReadOnly => false;

        private protected Slot[] _slots;
        private protected int _capacity;
        private protected int _count;

        //hash table
        private protected int[] _buckets;
        private protected int _freeList;
        private protected int _lastIndex;

        //linked list
        private protected int _tail;
        private protected int _head;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection{E}"/> class with default capacity.
        /// </summary>
        public EntityCollection() : this(3)
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

            _capacity = GetPrime(capacity);
            _count = 0;
            _lastIndex = 0;
            
            _freeList = UNDEFINED_INDEX;
            _tail = UNDEFINED_INDEX;
            _head = UNDEFINED_INDEX;

            _slots = new Slot[_capacity];
            _buckets = new int[_capacity];

            Array.Fill(_buckets, UNDEFINED_INDEX, 0, _capacity);
        }

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

        /// <summary>
        /// Initializes a new instance with an enumerable of entities.
        /// </summary>
        /// <param name="elements">Enumerable to populate the collection with.</param>
        public EntityCollection(IEnumerable<E> elements) : this(elements.Count()) => this.AddRange(elements);

        /// <inheritdoc cref="IEntityCollection{E}.Contains" />
        public bool Contains(E item)
        {
            if (_count == 0 || item == null)
                return false;

            int hashCode = item.GetHashCode();
            int bucket = (hashCode & 0x7FFFFFFF) % _capacity;
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

            int itemHash = item.GetHashCode();
            int itemMaskedHash = itemHash & 0x7FFFFFFF;
            int bucket = itemMaskedHash % _capacity;
            ref int head = ref _buckets[bucket];

            // Check if item already exists
            int current = head;
            while (current != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[current];
                if (slot.hashCode == itemHash)
                    return false;

                current = slot.next;
            }

            int index;

            // Allocate new slot or reuse from free list
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
                    int newCapacity = GetPrime(_capacity * 2);
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

                    // Recalculate bucket + head after resize
                    bucket = itemMaskedHash % _capacity;
                    head = ref _buckets[bucket];
                }

                index = _lastIndex++;
            }

            // Insert into doubly linked list
            if (_count == 0)
            {
                _head = _tail = index;
                _slots[index].left = UNDEFINED_INDEX;
                _slots[index].right = UNDEFINED_INDEX;
            }
            else
            {
                _slots[_tail].right = index;
                _slots[index].left = _tail;
                _slots[index].right = UNDEFINED_INDEX;
                _tail = index;
            }

            // Store slot
            _slots[index].value = item;
            _slots[index].hashCode = itemHash;
            _slots[index].next = head;

            // Update bucket
            head = index;

            _count++;

            this.OnAdd(item);
            this.OnAdded?.Invoke(item);
            this.OnStateChanged?.Invoke();
            return true;
        }

        protected virtual void OnAdd(E entity)
        {
        }

        /// <inheritdoc/>
        public bool Remove(E item)
        {
            if (item == null || _count == 0)
                return false;

            // Get a reference to the bucket where the item should be
            int hashCode = item.GetHashCode();
            int bucket = (hashCode & 0x7FFFFFFF) % _capacity;
            ref int next = ref _buckets[bucket];

            int index = next;
            int last = UNDEFINED_INDEX;

            // Traverse the hash chain (linked list in the bucket)
            while (index >= 0)
            {
                ref Slot slot = ref _slots[index];

                // If the item is found
                if (slot.hashCode == hashCode)
                {
                    // Remove from the hash chain
                    if (last == UNDEFINED_INDEX)
                        next = slot.next;
                    else
                        _slots[last].next = slot.next;

                    // Remove from the doubly linked list
                    if (slot.left != UNDEFINED_INDEX)
                        _slots[slot.left].right = slot.right;

                    if (slot.right != UNDEFINED_INDEX)
                        _slots[slot.right].left = slot.left;

                    // Update _tail if needed
                    if (_tail == index)
                        _tail = slot.left;

                    // Update _head if needed
                    if (_head == index)
                        _head = slot.right;

                    // Mark slot as free and add it to the free list
                    slot.hashCode = UNDEFINED_INDEX;
                    slot.next = _freeList;
                    _freeList = index;

                    _count--;

                    // If this was the last element, reset everything
                    if (_count == 0)
                    {
                        _lastIndex = 0;
                        _freeList = UNDEFINED_INDEX;
                        _tail = UNDEFINED_INDEX;
                        _head = UNDEFINED_INDEX;
                    }

                    this.OnRemove(item);
                    this.OnRemoved?.Invoke(item);
                    this.OnStateChanged?.Invoke();
                    return true;
                }

                // Move to the next node in the hash chain
                last = index;
                index = slot.next;
            }

            // Item not found
            return false;
        }

        protected virtual void OnRemove(E entity)
        {
        }

        /// <inheritdoc/>
        public void Clear()
        {
            if (_count == 0)
                return;

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
                slot.left = UNDEFINED_INDEX;
                slot.right = UNDEFINED_INDEX;

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

        public virtual void Dispose()
        {
            this.Clear();
            this.UnsubscribeAll();
        }

        public virtual void UnsubscribeAll()
        {
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

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[currentIndex];
                array[arrayIndex++] = slot.value;
                currentIndex = slot.right;
            }
        }

        public void CopyTo(ICollection<E> results)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            int index = _head;
            while (index != UNDEFINED_INDEX)
            {
                ref readonly Slot slot = ref _slots[index];
                results.Add(slot.value);
                index = slot.right;
            }
        }

        /// <summary>
        /// Returns an enumerator that iterates through the set.
        /// </summary>
        public Enumerator GetEnumerator() => new(this);

        /// <inheritdoc/>
        IEnumerator IEnumerable.GetEnumerator() => new Enumerator(this);

        /// <inheritdoc/>
        IEnumerator<E> IEnumerable<E>.GetEnumerator() => new Enumerator(this);

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
                _index = collection._head;
                _current = default;
            }

            public bool MoveNext()
            {
                if (_index == UNDEFINED_INDEX)
                    return false;

                ref readonly Slot slot = ref _collection._slots[_index];
                _current = slot.value;
                _index = slot.right;
                return true;
            }

            public void Reset()
            {
                _index = _collection._head;
            }

            public void Dispose() { }
        }
    }
}