using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

#if UNITY_5_3_OR_NEWER
using Unsafe = Unity.Collections.LowLevel.Unsafe.UnsafeUtility;

#else
using Unsafe = System.Runtime.CompilerServices.Unsafe;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Global registry responsible for tracking and managing all <see cref="IEntity"/> instances.
    /// Provides unique ID assignment, lookup, and name-based search utilities.
    /// </summary>
    public sealed class EntityRegistry : IReadOnlyEntityCollection<IEntity>
    {
        private const int UNDEFINED_INDEX = -1;

        private const int INITIAL_CAPACITY = 4;

        /// <summary>
        /// Gets the singleton instance of the <see cref="EntityRegistry"/>.
        /// </summary>
        public static EntityRegistry Instance => _instance ??= new EntityRegistry();

        private static EntityRegistry _instance;

        /// <inheritdoc/>
        public event Action OnStateChanged;

        /// <summary>
        /// Occurs when an <see cref="IEntity"/> is registered in the registry.
        /// </summary>
        public event Action<IEntity> OnAdded;

        /// <summary>
        /// Occurs when an <see cref="IEntity"/> is removed from the registry.
        /// </summary>
        public event Action<IEntity> OnRemoved;

        /// <inheritdoc />
        public int Count => _count;

        internal struct Slot
        {
            public int id;
            public IEntity entity;
            public int orderIndex;
            public int next;
        }

        private Slot[] _slots;
        private int[] _order;
        private int _entityCapacity;
        private int _count;
        private int _primeIndex;

        private int[] _buckets;
        private int _lastIndex;
        private int _freeList;

        private int[] _recycledIds;
        private int _recycledCount;
        private int _maxId;

        private EntityRegistry()
        {
            _entityCapacity = InternalUtils.CeilToPrime(INITIAL_CAPACITY, out _primeIndex);
            _slots = new Slot[_entityCapacity];
            _buckets = new int[_entityCapacity];
            _order = new int[_entityCapacity];
            Array.Fill(_buckets, UNDEFINED_INDEX);

            _count = 0;
            _lastIndex = 0;
            _freeList = UNDEFINED_INDEX;
        }

        public IEntity this[int index] => index < 0 || index >= _count
            ? throw new ArgumentOutOfRangeException(nameof(index))
            : _slots[_order[index]].entity;

        public bool TryGetAt(int index, out IEntity entity)
        {
            if (index < 0 || index >= _count)
            {
                entity = null;
                return false;
            }

            entity = _slots[_order[index]].entity;
            return true;
        }
        
        /// <summary>
        /// Determines whether the registry contains the specified entity.
        /// </summary>
        /// <param name="entity">The entity to check for existence.</param>
        /// <returns>True, if the entity is registered; otherwise, false.</returns>
        public bool Contains(IEntity entity)
        {
            if (_count > 0 && entity != null)
            {
                int id = entity.InstanceID;
                if (id > 0)
                {
                    int bucket = id % _entityCapacity;
                    int index = _buckets[bucket];

                    while (index >= 0)
                    {
                        ref readonly Slot slot = ref _slots[index];
                        if (slot.id == id)
                            return true;

                        index = slot.next;
                    }
                }
            }

            return false;
        }

        /// <summary>
        /// Determines whether the registry contains the specified entity with id.
        /// </summary>
        /// <param name="id">The entity id to check for existence.</param>
        /// <returns>True if the entity is registered; otherwise, false.</returns>
        public bool Contains(int id)
        {
            if (_count > 0 && id > 0)
            {
                int bucket = id % _entityCapacity;
                int index = _buckets[bucket];

                while (index >= 0)
                {
                    ref readonly Slot slot = ref _slots[index];
                    if (slot.id == id)
                        return true;

                    index = slot.next;
                }
            }

            return false;
        }

        /// <summary>
        /// Copies all registered entities into the provided collection.
        /// </summary>
        /// <param name="results">The target collection.</param>
        public void CopyTo(ICollection<IEntity> results)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            var slots = _slots;
            var order = _order;

            for (int i = 0; i < _count; i++)
                results.Add(slots[order[i]].entity);
        }

        /// <summary>
        /// Copies all registered entities into the specified array, starting at the given index.
        /// </summary>
        /// <param name="array">Target array to copy entities into.</param>
        /// <param name="arrayIndex">Starting index in the target array.</param>
        public void CopyTo(IEntity[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0 || arrayIndex > array.Length)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex));

            int count = 0;

            for (int i = 0; i < _lastIndex; i++)
            {
                ref readonly Slot slot = ref _slots[i];
                if (slot.id == 0)
                    continue;

                array[arrayIndex + count] = slot.entity;
                count++;
            }
        }

        /// <summary>
        /// Retrieves an entity by its unique ID.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>The entity instance.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if no entity with the specified ID exists.</exception>
        public IEntity Get(int id)
        {
            if (id <= 0 || _count == 0)
                throw new KeyNotFoundException($"Entity with ID {id} not found.");

            int bucket = id % _entityCapacity;
            int index = _buckets[bucket];

            while (index >= 0)
            {
                ref readonly Slot slot = ref _slots[index];
                if (slot.id == id && slot.entity != null)
                    return slot.entity;

                index = slot.next;
            }

            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }

        /// <summary>
        /// Attempts to retrieve an entity by its unique ID without throwing exceptions.
        /// </summary>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="entity">Output parameter that contains the found entity if successful; otherwise null.</param>
        /// <returns>True if the entity was found, otherwise false.</returns>
        public bool TryGet(int id, out IEntity entity)
        {
            entity = null;

            if (id <= 0 || _count == 0)
                return false;

            int bucket = id % _entityCapacity;
            int index = _buckets[bucket];

            while (index >= 0)
            {
                ref readonly Slot slot = ref _slots[index];
                if (slot.id == id)
                {
                    entity = slot.entity;
                    return true;
                }

                index = slot.next;
            }

            return false;
        }

        /// <summary>
        /// Retrieves an entity by its unique ID using an unsafe cast for maximum performance.
        /// </summary>
        /// <typeparam name="T">The expected type of the entity.</typeparam>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <returns>
        /// The entity instance cast to <typeparamref name="T"/> if found; otherwise, throws <see cref="KeyNotFoundException"/>.
        /// </returns>
        /// <exception cref="KeyNotFoundException">Thrown if no entity with the specified ID exists.</exception>
        public T GetUnsafe<T>(int id) where T : class, IEntity
        {
            if (_count == 0 || id <= 0)
                return null;

            int bucket = id % _entityCapacity;
            int index = _buckets[bucket];

            while (index >= 0)
            {
                ref Slot slot = ref _slots[index];

                if (slot.id == id && slot.entity != null)
                    return Unsafe.As<IEntity, T>(ref slot.entity);

                index = slot.next;
            }

            throw new KeyNotFoundException($"Entity with ID {id} not found.");
        }

        /// <summary>
        /// Attempts to retrieve an entity by its unique ID using an unsafe cast for maximum performance.
        /// </summary>
        /// <typeparam name="T">The expected type of the entity.</typeparam>
        /// <param name="id">The unique identifier of the entity.</param>
        /// <param name="entity">
        /// When this method returns, contains the found entity cast to <typeparamref name="T"/> if successful; otherwise, null.
        /// </param>
        /// <returns>True if the entity was found; otherwise, false.</returns>
        public bool TryGetUnsafe<T>(int id, out T entity) where T : class, IEntity
        {
            entity = null;

            if (_count == 0 || id <= 0)
                return false;

            int bucket = id % _entityCapacity;
            int index = _buckets[bucket];

            while (index >= 0)
            {
                ref Slot slot = ref _slots[index];
                if (slot.id == id && slot.entity != null)
                {
                    entity = Unsafe.As<IEntity, T>(ref slot.entity);
                    return true;
                }

                index = slot.next;
            }

            return false;
        }

        /// <summary>
        /// Returns an enumerator that iterates through the registered entities.
        /// </summary>
        /// <returns>An enumerator for the registered entities.</returns>
        public IEnumerator<IEntity> GetEnumerator() => new Enumerator(this);

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

        public struct Enumerator : IEnumerator<IEntity>
        {
            private readonly EntityRegistry _registry;

            private int _index;
            private IEntity _current;

            public IEntity Current => _current;
            object IEnumerator.Current => _current;

            public Enumerator(EntityRegistry registry)
            {
                _registry = registry;
                _index = 0;
                _current = null;
            }

            public bool MoveNext()
            {
                if (_index >= _registry._count)
                    return false;

                int slotIndex = _registry._order[_index++];
                _current = _registry._slots[slotIndex].entity;
                return true;
            }

            public void Reset()
            {
                _index = 0;
                _current = null;
            }

            public void Dispose()
            {
                //Do nothing...
            }
        }

        internal void Register(IEntity entity)
        {
            int index;

            if (_freeList >= 0)
            {
                index = _freeList;
                _freeList = _slots[index].next;
            }
            else
            {
                if (_lastIndex == _entityCapacity)
                    this.Resize();

                index = _lastIndex;
                _lastIndex++;
            }

            int id = _recycledCount > 0
                ? _recycledIds[--_recycledCount]
                : ++_maxId;

            int bucket = id % _entityCapacity;
            ref int next = ref _buckets[bucket];

            _slots[index] = new Slot
            {
                id = id,
                entity = entity,
                next = next,
            };

            next = index;
            
            _order[_count] = index;
            _slots[index].orderIndex = _count;
            _count++;

            entity.InstanceID = id;
            
            this.OnAdded?.Invoke(entity);
            this.OnStateChanged?.Invoke();
        }

        internal void Unregister(IEntity entity)
        {
            int id = entity.InstanceID;
            int bucket = id % _entityCapacity;
            ref int next = ref _buckets[bucket];

            int index = next;
            int last = UNDEFINED_INDEX;

            while (index >= 0)
            {
                ref Slot slot = ref _slots[index];
                if (slot.id == id)
                {
                    if (last == UNDEFINED_INDEX)
                        next = slot.next;
                    else
                        _slots[last].next = slot.next;

                    slot.id = 0;
                    slot.entity = null;
                    slot.next = _freeList;

                    // Recycle ID to stack
                    _recycledIds ??= new int[INITIAL_CAPACITY];

                    if (_recycledCount >= _recycledIds.Length)
                        Array.Resize(ref _recycledIds, _recycledCount * 2);

                    _recycledIds[_recycledCount++] = id;

                    int orderIndex = slot.orderIndex;
                    int lastOrderIndex = _count - 1;

                    if (orderIndex != lastOrderIndex)
                    {
                        int swapped = _order[lastOrderIndex];

                        _order[orderIndex] = swapped;
                        _slots[swapped].orderIndex = orderIndex;
                    }

                    _order[lastOrderIndex] = UNDEFINED_INDEX;
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

                    // Reset instance id 
                    entity.InstanceID = 0;

                    this.OnRemoved?.Invoke(entity);
                    this.OnStateChanged?.Invoke();
                }

                last = index;
                index = slot.next;
            }
        }

        internal void Clear()
        {
            if (_count == 0)
                return;

            for (int i = 0; i < _lastIndex; i++)
            {
                ref Slot slot = ref _slots[i];
                if (slot is {id: > 0, entity: not null})
                {
                    // Reset instance ID
                    slot.entity.InstanceID = UNDEFINED_INDEX;

                    // Fire OnRemoved event
                    this.OnRemoved?.Invoke(slot.entity);

                    // Clear slot
                    slot.id = 0;
                    slot.entity = null;
                    slot.next = UNDEFINED_INDEX;
                }
            }

            // Reset buckets
            Array.Fill(_buckets, UNDEFINED_INDEX);

            // Reset counters and free list
            _count = 0;
            _lastIndex = 0;
            _freeList = UNDEFINED_INDEX;

            // Reset recycled IDs
            _recycledIds = null;
            _recycledCount = 0;
            _maxId = 0;

            // Fire state changed event
            this.OnStateChanged?.Invoke();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Resize()
        {
            _entityCapacity = InternalUtils.PrimeTable[++_primeIndex];
            
            Array.Resize(ref _order, _entityCapacity);
            Array.Resize(ref _slots, _entityCapacity);
            Array.Resize(ref _buckets, _entityCapacity);
            Array.Fill(_buckets, UNDEFINED_INDEX);

            for (int i = 0; i < _lastIndex; i++)
            {
                ref Slot slot = ref _slots[i];
                if (slot.entity == null)
                    continue;

                int bucket = slot.id % _entityCapacity;
                ref int next = ref _buckets[bucket];

                slot.next = next;
                next = i;
            }
        }

#if UNITY_EDITOR
        /// <summary>
        /// Resets the registry state in the Unity Editor before entering Play Mode.
        /// </summary>
        [InitializeOnEnterPlayMode]
#endif

        internal static void ResetAll()
        {
            if (_instance != null)
                _instance.Clear();
        }
    }
}