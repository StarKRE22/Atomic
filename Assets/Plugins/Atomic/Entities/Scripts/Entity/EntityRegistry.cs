using System;
using System.Collections.Generic;
using UnityEditor;

namespace Atomic.Entities
{
    public sealed class EntityRegistry<E> where E : IEntity<E>
    {
        public static EntityRegistry<E> Instance => _instance ??= new EntityRegistry<E>();
        private static EntityRegistry<E> _instance;

        public event Action<IEntity<E>> OnAdded;
        public event Action<IEntity<E>> OnRemoved;

        private readonly Dictionary<int, IEntity<E>> _entities = new();
        private readonly Stack<int> _recycledIds = new();
        private int _lastId;

        public bool Get(int id, out IEntity<E> entity) => _entities.TryGetValue(id, out entity);

        internal int Add(IEntity<E> entity)
        {
            if (!_recycledIds.TryPop(out int id))
                id = _lastId++;

            _entities.Add(id, entity);
            this.OnAdded?.Invoke(entity);
            return id;
        }

        internal void Remove(int id)
        {
            if (_entities.Remove(id, out IEntity<E> entity))
            {
                _recycledIds.Push(id);
                this.OnRemoved?.Invoke(entity);
            }
        }


#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
#endif
        public static void ResetAll()
        {
            if (_instance != null)
            {
                _instance._lastId = -1;
                _instance._entities.Clear();
            }
        }
    }
}