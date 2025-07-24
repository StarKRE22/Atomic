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
        private const int UNDEFINED_INDEX = -1;

        private protected static readonly IEqualityComparer<E> s_comparer = EqualityComparer<E>.Default;
        private protected static readonly ArrayPool<E> s_arrayPool = ArrayPool<E>.Shared;

        private protected struct Slot
        {
            public E value;
            public bool exists;

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
        public EntityCollection() : this(0) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityCollection{E}"/> class with a predefined capacity.
        /// </summary>
        /// <param name="capacity">Initial capacity of the collection.</param>
        public EntityCollection(int capacity) => this.Initialize(capacity);

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
        public bool Contains(E item) => item != null && this.FindIndex(item, out _);

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

            if (!this.AddInternal(item))
                return false;

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
            if (item == null)
                return false;

            if (!this.RemoveInternal(item))
                return false;

            this.OnRemove(item);
            this.OnRemoved?.Invoke(item);
            this.OnStateChanged?.Invoke();
            return true;
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
            E[] removedItems = s_arrayPool.Rent(_count);

            for (int i = 0; i < _lastIndex; i++)
            {
                _buckets[i] = UNDEFINED_INDEX;

                ref Slot slot = ref _slots[i];
                if (!slot.exists)
                    continue;

                slot.exists = false;
                slot.next = UNDEFINED_INDEX;
                slot.left = UNDEFINED_INDEX;
                slot.right = UNDEFINED_INDEX;

                E removedItem = slot.value;
                removedItems[removeCount++] = removedItem;
                this.OnRemove(removedItem);
            }

            _count = 0;
            _freeList = UNDEFINED_INDEX;
            _lastIndex = 0;

            try
            {
                for (int i = 0; i < removeCount; i++)
                    this.OnRemoved?.Invoke(removedItems[i]);

                this.OnStateChanged?.Invoke();
            }
            finally
            {
                s_arrayPool.Return(removedItems);
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
                ref Slot slot = ref _slots[currentIndex];
                array[arrayIndex++] = slot.value;
                currentIndex = slot.right;
            }
        }

        public void CopyTo(ICollection<E> results)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            int currentIndex = _head;
            while (currentIndex != UNDEFINED_INDEX)
            {
                ref Slot slot = ref _slots[currentIndex];
                results.Add(slot.value);
                currentIndex = slot.right;
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
            private int _currentIndex;
            private E _current;

            public Enumerator(EntityCollection<E> collection)
            {
                _collection = collection;
                _currentIndex = _collection._head;
                _current = default;
            }

            public bool MoveNext()
            {
                if (_currentIndex == UNDEFINED_INDEX)
                    return false;

                ref Slot slot = ref _collection._slots[_currentIndex];
                _current = slot.value;
                _currentIndex = slot.right;
                return true;
            }

            public void Reset()
            {
                _currentIndex = _collection._head;
                _current = default;
            }

            public void Dispose()
            {
            }
        }

        private ref int Bucket(E item)
        {
            int hash = item.GetHashCode() & 0x7FFFFFFF;
            int index = hash % _slots.Length;
            return ref _buckets[index];
        }

        private void Initialize(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            int size = GetPrime(capacity);

            _slots = new Slot[size];
            _buckets = new int[size];

            for (int i = 0; i < size; i++)
                _buckets[i] = UNDEFINED_INDEX;

            _count = 0;
            _lastIndex = 0;
            _freeList = UNDEFINED_INDEX;
            _tail = UNDEFINED_INDEX;
            _head = UNDEFINED_INDEX;
        }

        private bool FindIndex(E item, out int index)
        {
            index = UNDEFINED_INDEX;

            if (_count == 0 || item == null)
                return false;

            int i = this.Bucket(item);
            while (i != UNDEFINED_INDEX)
            {
                ref Slot slot = ref _slots[i];

                if (slot.exists && s_comparer.Equals(slot.value, item))
                {
                    index = i;
                    return true;
                }

                i = slot.next;
            }

            return false;
        }

        private void IncreaseCapacity()
        {
            int size = GetPrime(_slots.Length);

            Array.Resize(ref _slots, size);

            // Important: after resizing _slots, we use updated _slots.Length in Bucket()
            Array.Resize(ref _buckets, size);

            for (int i = 0; i < size; i++)
                _buckets[i] = UNDEFINED_INDEX;

            for (int i = 0; i < _lastIndex; i++)
            {
                ref Slot slot = ref _slots[i];
                if (!slot.exists)
                    continue;

                // Recalculate bucket for the existing slot with updated size
                ref int bucket = ref this.Bucket(slot.value);
                slot.next = bucket;
                bucket = i;
            }
        }

        private bool AddInternal(E item)
        {
            // Check if the item already exists in the hash set
            if (this.FindIndex(item, out int index))
                return false;

            // Get a free slot from the free list, or allocate a new one
            if (_freeList >= 0)
            {
                // Reuse a previously freed slot
                index = _freeList;
                _freeList = _slots[index].next;
            }
            else
            {
                // If the slot array is full, increase capacity
                if (_lastIndex == _slots.Length)
                    this.IncreaseCapacity();

                // Use the next available index
                index = _lastIndex;
                _lastIndex++;
            }

            // Get the reference to the corresponding bucket for the item
            ref int bucket = ref this.Bucket(item);

            // Save the current head of the chain in case of hash collision
            int next = bucket;

            int left, right;

            if (_tail == UNDEFINED_INDEX)
            {
                // This is the first element in the list
                _head = index; // Set head to the new element
                _tail = index; // Set tail to the new element
                left = UNDEFINED_INDEX;
                right = UNDEFINED_INDEX;
            }
            else
            {
                // Append to the end of the linked list
                left = _tail;
                right = UNDEFINED_INDEX;

                // Link current tail to new element
                _slots[_tail].right = index;

                // Update tail
                _tail = index;
            }

            // Update the hash bucket to point to the new slot
            bucket = index;

            // Store the new slot
            _slots[index] = new Slot
            {
                value = item,
                exists = true,
                next = next, // Hash collision chain
                left = left, // Previous in linked list
                right = right // Next in linked list
            };

            // Increase element count
            _count++;
            return true;
        }

        private bool RemoveInternal(E item)
        {
            // If the structure is empty, there's nothing to remove
            if (_count == 0)
                return false;

            // Get a reference to the bucket where the item should be
            ref int bucket = ref this.Bucket(item);

            int index = bucket;
            int last = UNDEFINED_INDEX;

            // Traverse the hash chain (linked list in the bucket)
            while (index >= 0)
            {
                ref Slot node = ref _slots[index];

                // If the item is found
                if (s_comparer.Equals(node.value, item))
                {
                    // Remove from the hash chain
                    if (last == UNDEFINED_INDEX)
                        bucket = node.next;
                    else
                        _slots[last].next = node.next;

                    // Remove from the doubly linked list
                    if (node.left != UNDEFINED_INDEX)
                        _slots[node.left].right = node.right;

                    if (node.right != UNDEFINED_INDEX)
                        _slots[node.right].left = node.left;

                    // Update _tail if needed
                    if (_tail == index)
                        _tail = node.left;

                    // Update _head if needed
                    if (_head == index)
                        _head = node.right;

                    // Mark slot as free and add it to the free list
                    node.exists = false;
                    node.next = _freeList;
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

                    return true;
                }

                // Move to the next node in the hash chain
                last = index;
                index = node.next;
            }

            // Item not found
            return false;
        }
    }
}