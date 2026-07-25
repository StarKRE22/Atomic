using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    [Serializable]
    public abstract class EntitySystem<E> : EntitySystemBase<E>, IDisposable where E : IEntity
    {
        [ShowInInspector, ReadOnly, HideInEditorMode]
        private readonly Dictionary<E, int> _lookup;

        [ShowInInspector, ReadOnly, HideInEditorMode]
        private E[] _entities;

        [ShowInInspector, ReadOnly, HideInEditorMode]
        private int _entityCount;

        [ShowInInspector, ReadOnly, HideInEditorMode]
        private int _cursor;

        protected EntitySystem(IReadOnlyEntityCollection<E> source, Settings settings) :
            base(source, settings)
        {
            int initialCapacity = source.Count;
            _lookup = new Dictionary<E, int>(initialCapacity);
            _entities = new E[Math.Max(4, initialCapacity)];
        }

        public void Dispose()
        {
            Array.Clear(_entities, 0, _entityCount);
            _lookup.Clear();
            _entityCount = 0;
        }

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

        protected abstract void Update(E entity, float deltaTime);

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


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private void Swap(int index, int last)
        {
            _entities[index] = _entities[last];
            _lookup[_entities[index]] = index;
        }
    }
}