using System;
using System.Collections;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Global registry responsible for tracking and managing all <see cref="IEntity"/> instances.
    /// Provides unique ID assignment, lookup, and name-based search utilities.
    /// </summary>
    public sealed class EntityRegistry : IReadOnlyEntityCollection<IEntity>
    {
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
        public int Count => _entities.Count;

        private readonly Dictionary<int, IEntity> _entities = new();
        private int _lastId;

        private EntityRegistry()
        {
        }
        
        internal void Register(IEntity entity, out int id)
        {
            id = _lastId++;
            _entities.Add(id, entity);
            this.OnAdded?.Invoke(entity);
        }
        
        internal void Unregister(ref int id)
        {
            if (_entities.Remove(id, out IEntity entity))
            {
                id = -1;
                this.OnRemoved?.Invoke(entity);
            }
        }

        /// <inheritdoc />
        public bool Contains(IEntity entity) => _entities.ContainsValue(entity);

        /// <inheritdoc />
        public void CopyTo(ICollection<IEntity> results)
        {
            results.Clear();
            foreach (IEntity entity in _entities.Values)
                results.Add(entity);
        }

        /// <inheritdoc />
        public void CopyTo(IEntity[] array, int arrayIndex) => _entities.Values.CopyTo(array, arrayIndex);

        /// <summary>
        /// Attempts to retrieve an entity by its ID.
        /// </summary>
        /// <param name="id">The ID of the entity.</param>
        /// <param name="entity">When this method returns, contains the entity if found; otherwise, null.</param>
        /// <returns>True if the entity was found; otherwise, false.</returns>
        public bool TryGet(int id, out IEntity entity) => _entities.TryGetValue(id, out entity);

        public IEntity Get(int id) => _entities[id];

        public IEnumerator<IEntity> GetEnumerator() => _entities.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

#if UNITY_EDITOR
        /// <summary>
        /// Resets the registry state in the Unity Editor before entering Play Mode.
        /// </summary>
        [InitializeOnEnterPlayMode]
#endif
        private static void ResetAll()
        {
            if (_instance != null)
            {
                _instance._lastId = -1;
                _instance._entities.Clear();
            }
        }
    }
}