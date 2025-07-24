using System;
using System.Buffers;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using static Atomic.Entities.InternalUtils;

namespace Atomic.Entities
{
    public class EntityCollection : EntityCollection<IEntity>
    {
        public EntityCollection()
        {
        }

        public EntityCollection(int capacity) : base(capacity)
        {
        }

        public EntityCollection(params IEntity[] entities) : base(entities)
        {
        }

        public EntityCollection(IReadOnlyCollection<IEntity> elements) : base(elements)
        {
        }

        public EntityCollection(IEnumerable<IEntity> elements) : base(elements)
        {
        }
    }

    public class EntityCollection<E> : IEntityCollection<E> where E : IEntity
    {
        private const int UNDEFINED_INDEX = -1;

        private protected static readonly IEqualityComparer<E> s_comparer = EqualityComparer<E>.Default;
        private protected static readonly ArrayPool<E> s_arrayPool = ArrayPool<E>.Shared;

        private protected struct Slot
        {
            public bool exists;
            public E value;
            
            //Hash set
            public int next;
            
            //Linked list
            public int left;
            public int right;
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

        private protected int _count;
        
        //Hash set
        private protected Slot[] _slots;
        private protected int[] _buckets;
        private protected int _freeList;
        private protected int _lastIndex;
        
        //Linked list
        private protected int _head;
        private protected int _tail;

        public EntityCollection() : this(0)
        {
        }

        public EntityCollection(int capacity) => this.Initialize(capacity);

        public EntityCollection(params E[] entities) : this(entities.Length) => this.AddRange(entities);

        public EntityCollection(IReadOnlyCollection<E> elements) : this(elements.Count) => this.AddRange(elements);

        public EntityCollection(IEnumerable<E> elements) : this(elements.Count()) => this.AddRange(elements);

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

            for (int i = 0; i < _count; i++)
                array[arrayIndex++] = _items[i];
        }

        public E this[int index] => _items[index];

        public void CopyTo(ICollection<E> results)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            for (int i = 0; i < _count; i++)
                results.Add(_items[i]);
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

            public Enumerator(EntityCollection<E> loop)
            {
                _collection = loop;
                _index = -1;
                _current = default;
            }

            public bool MoveNext()
            {
                if (_index + 1 == _collection._count)
                    return false;

                _current = _collection._items[++_index];
                return true;
            }

            public void Reset()
            {
                _index = -1;
                _current = default;
            }

            public void Dispose()
            {
                //Nothing...
            }
        }

        private void IncreaseCapacity()
        {
            int size = GetPrime(_slots.Length);
            Array.Resize(ref _slots, size);
            Array.Resize(ref _buckets, size);
            Array.Resize(ref _items, size);

            for (int i = 0; i < size; i++)
                _buckets[i] = UNDEFINED_INDEX;

            for (int i = 0; i < _lastIndex; i++)
            {
                ref Slot slot = ref _slots[i];
                if (!slot.exists)
                    continue;

                ref int bucket = ref this.Bucket(slot.value);
                slot.next = bucket;
                bucket = i;
            }
        }

        private bool RemoveInternal(E item)
        {
            if (_count == 0)
                return false;

            ref int bucket = ref this.Bucket(item);

            int index = bucket;
            int last = UNDEFINED_INDEX;

            while (index >= 0)
            {
                ref Slot node = ref _slots[index];
                if (s_comparer.Equals(node.value, item))
                {
                    if (last == UNDEFINED_INDEX)
                        bucket = node.next;
                    else
                        _slots[last].next = node.next;

                    node.next = _freeList;
                    node.exists = false;

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

                    for (int i = node.index; i < _count; i++)
                        _items[i] = _items[i + 1];

                    return true;
                }

                last = index;
                index = node.next;
            }

            return false;
        }

        private bool AddInternal(E item)
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
                if (_lastIndex == _slots.Length)
                    this.IncreaseCapacity();

                index = _lastIndex;
                _lastIndex++;
            }

            ref int bucket = ref this.Bucket(item);

            _slots[index] = new Slot
            {
                value = item,
                next = bucket,
                index = _count,
                exists = true
            };

            _items[_count] = item;

            bucket = index;
            _count++;
            return true;
        }

        private bool FindIndex(E item, out int index)
        {
            if (_count == 0)
            {
                index = UNDEFINED_INDEX;
                return false;
            }

            index = this.Bucket(item);
            while (index >= 0)
            {
                Slot slot = _slots[index];
                if (slot.exists && s_comparer.Equals(slot.value, item))
                    return true;

                index = slot.next;
            }

            return false;
        }

        private ref int Bucket(E item)
        {
            int index = (int) ((uint) item.GetHashCode() % _slots.Length);
            return ref _buckets[index];
        }

        private void Initialize(int capacity)
        {
            if (capacity < 0)
                throw new ArgumentOutOfRangeException(nameof(capacity));

            int size = GetPrime(capacity);

            _slots = new Slot[size];
            _buckets = new int[size];
            _items = new E[size];

            for (int i = 0; i < size; i++)
                _buckets[i] = UNDEFINED_INDEX;

            _count = 0;
            _lastIndex = 0;
            _freeList = UNDEFINED_INDEX;
        }
    }
}