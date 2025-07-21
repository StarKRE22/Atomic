using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Global registry responsible for tracking and managing all <see cref="IEntity"/> instances.
    /// Provides unique ID assignment, lookup, and name-based search utilities.
    /// </summary>
    public sealed class GlobalEntityRegistry : IReadOnlyEntityCollection<IEntity>
    {
        /// <summary>
        /// Occurs when an <see cref="IEntity"/> is registered in the registry.
        /// </summary>
        public event Action<IEntity> OnAdded;

        /// <summary>
        /// Occurs when an <see cref="IEntity"/> is removed from the registry.
        /// </summary>
        public event Action<IEntity> OnDeleted;

        public int Count => _entities.Count;

        /// <summary>
        /// Gets the singleton instance of the <see cref="GlobalEntityRegistry"/>.
        /// </summary>
        public static GlobalEntityRegistry Instance => _instance ??= new GlobalEntityRegistry();

        private static GlobalEntityRegistry _instance;

        private readonly Dictionary<int, IEntity> _entities = new();
        private readonly Stack<int> _recycledIds = new();
        private int _lastId;

        private GlobalEntityRegistry()
        {
        }

        /// <summary>
        /// Registers an entity in the registry and assigns it a unique integer ID.
        /// </summary>
        /// <param name="entity">The entity to register.</param>
        /// <returns>The assigned unique ID.</returns>
        internal int Register(IEntity entity)
        {
            if (!_recycledIds.TryPop(out int id))
                id = _lastId++;

            _entities.Add(id, entity);
            this.OnAdded?.Invoke(entity);
            return id;
        }

        /// <summary>
        /// Unregisters an entity from the registry by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity to unregister.</param>
        internal void Unregister(int id)
        {
            if (_entities.Remove(id, out IEntity entity))
            {
                _recycledIds.Push(id);
                this.OnDeleted?.Invoke(entity);
            }
        }

        public bool Has(IEntity entity) => _entities.ContainsValue(entity);

        /// <summary>
        /// Returns all currently registered entities.
        /// </summary>
        public IEntity[] GetAll() => _entities.Values.ToArray();

        public int GetAll(IEntity[] results)
        {
            _entities.Values.CopyTo(results, 0);
            return _entities.Count;
        }

        public void CopyTo(ICollection<IEntity> results)
        {
            results.Clear();
            foreach (IEntity entity in _entities.Values)
                results.Add(entity);
        }

        /// <summary>
        /// Attempts to retrieve an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <param name="entity">When this method returns, contains the entity if found; otherwise, null.</param>
        /// <returns>True if the entity was found; otherwise, false.</returns>
        public bool Get(int id, out IEntity entity) => _entities.TryGetValue(id, out entity);

        /// <summary>
        /// Tries to get the first registered entity that matches the specified name.
        /// </summary>
        /// <param name="name">The name to search for.</param>
        /// <param name="result">The first matching entity, if any.</param>
        /// <returns>True if a matching entity was found; otherwise, false.</returns>
        public bool TryGetByName(string name, out IEntity result)
        {
            foreach (IEntity entity in _entities.Values)
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
        /// Returns all entities with the specified name.
        /// </summary>
        /// <param name="name">The name to match.</param>
        /// <returns>A list of all entities with the given name.</returns>
        public List<IEntity> GetAllByName(string name)
        {
            List<IEntity> list = new List<IEntity>();
            foreach (IEntity entity in _entities.Values)
                if (entity.Name == name)
                    list.Add(entity);

            return list;
        }

#if UNITY_EDITOR
        /// <summary>
        /// Resets the registry state in the Unity Editor before entering Play Mode.
        /// </summary>
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

        public IEnumerator<IEntity> GetEnumerator() => _entities.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}