using System;
using System.Collections.Generic;
using UnityEditor;

namespace Atomic.Entities
{
    public sealed class EntityRegistry<E> where E : IEntity<E>
    {
        public static EntityRegistry<E> Instance => _instance ??= new EntityRegistry<E>();
        private static EntityRegistry<E> _instance;

        private readonly Dictionary<int, IEntity<E>> _entities = new();
        private readonly Stack<int> _recycledIds = new();
        private int _lastId;

        /// <summary>
        /// Returns all entities currently registered.
        /// </summary>
        public IReadOnlyCollection<IEntity<E>> GetAll() => _entities.Values;
        
        public bool Get(int id, out IEntity<E> entity) => _entities.TryGetValue(id, out entity);

        internal int Add(IEntity<E> entity)
        {
            if (!_recycledIds.TryPop(out int id))
                id = _lastId++;

            _entities.Add(id, entity);
            return id;
        }

        internal void Remove(int id)
        {
            if (_entities.Remove(id)) 
                _recycledIds.Push(id);
        }

        /// <summary>
        /// Tries to get the first entity with the given name.
        /// </summary>
        public bool TryGetByName(string name, out IEntity<E> result)
        {
            foreach (var entity in _entities.Values)
            {
                if (entity.Name == name)
                {
                    result = entity;
                    return true;
                }
            }

            result = default;
            return false;
        }

        /// <summary>
        /// Returns all entities with the given name.
        /// </summary>
        public List<IEntity<E>> GetAllByName(string name)
        {
            var list = new List<IEntity<E>>();

            foreach (var entity in _entities.Values)
            {
                if (entity.Name == name)
                    list.Add(entity);
            }

            return list;
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