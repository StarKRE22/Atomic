using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Base implementation of <see cref="EntitySystemBase{E}"/> that maintains
    /// an internal array of active entities and updates them in adaptive batches.
    /// </summary>
    /// <typeparam name="E">Type of entity processed by the system.</typeparam>
    [Serializable]
    public abstract class EntitySystem<E> : EntitySystemBase<E>, IDisposable where E : IEntity
    {
#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private readonly Dictionary<E, int> _lookup;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private E[] _entities;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private int _entityCount;

#if ODIN_INSPECTOR
        [ShowInInspector, ReadOnly, HideInEditorMode]
#endif
        private int _cursor;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntitySystem{E}"/> class.
        /// </summary>
        /// <param name="source">Source collection of entities.</param>
        /// <param name="settings">System configuration.</param>
        protected EntitySystem(IReadOnlyEntityCollection<E> source, Settings settings) :
            base(source, settings)
        {
            int initialCapacity = source.Count;
            _lookup = new Dictionary<E, int>(initialCapacity);
            _entities = new E[Math.Max(4, initialCapacity)];
        }

        /// <summary>
        /// Releases all internal resources and removes references to tracked entities.
        /// </summary>
        public override void Dispose()
        {
            Array.Clear(_entities, 0, _entityCount);
            _lookup.Clear();
            _entityCount = 0;
        }

        /// <summary>
        /// Updates a batch of entities starting from the current cursor position.
        /// The cursor wraps around the entity array to ensure all entities are
        /// processed fairly over multiple frames.
        /// </summary>
        /// <param name="batchSize">Maximum number of entities to update.</param>
        /// <param name="deltaTime">Time elapsed since the previous update.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected sealed override void OnUpdate(int batchSize, float deltaTime)
        {
            int count = _entityCount;
            if (count == 0)
                return;

            int cursor = _cursor;

            if (count < batchSize)
                batchSize = count;

            for (int i = 0; i < batchSize; i++)
            {
                if (cursor >= count)
                    cursor = 0;

                E entity = _entities[cursor++];
                this.Update(entity, deltaTime);
            }

            _cursor = cursor;
        }

        /// <summary>
        /// Updates a single entity.
        /// </summary>
        /// <param name="entity">Entity to update.</param>
        /// <param name="deltaTime">Time elapsed since the previous update.</param>
        protected abstract void Update(E entity, float deltaTime);

        /// <summary>
        /// Adds an entity to the internal update list if it is not already tracked.
        /// </summary>
        /// <param name="entity">Entity to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected sealed override void OnAddEntity(E entity)
        {
            if (_lookup.ContainsKey(entity))
                return;

            if (_entityCount >= _entities.Length)
                Array.Resize(ref _entities, _entities.Length * 2);

            _entities[_entityCount] = entity;
            _lookup[entity] = _entityCount;
            _entityCount++;
        }

        /// <summary>
        /// Removes an entity from the internal update list.
        /// Removal is performed in constant time by swapping the entity
        /// with the last element in the array.
        /// </summary>
        /// <param name="entity">Entity to remove.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        protected sealed override void OnRemoveEntity(E entity)
        {
            if (!_lookup.TryGetValue(entity, out int index))
                return;

            int last = _entityCount - 1;
            if (index != last)
                this.Swap(index, last);

            _lookup.Remove(entity);
            _entityCount--;
        }

        /// <summary>
        /// Moves the last entity into the specified index and updates its lookup entry.
        /// </summary>
        /// <param name="index">Destination index.</param>
        /// <param name="last">Index of the last entity.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Swap(int index, int last)
        {
            _entities[index] = _entities[last];
            _lookup[_entities[index]] = index;
        }
    }
}
