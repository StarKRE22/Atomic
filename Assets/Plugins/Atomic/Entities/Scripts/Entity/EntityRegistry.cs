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
        private readonly Stack<int> _recycledIds = new();
        private int _lastId;

        /// <summary>
        /// Initializes a new instance of the <see cref="EntityRegistry"/> class.
        /// This constructor is private because the registry uses the singleton pattern.
        /// </summary>
        private EntityRegistry()
        {
        }

        /// <summary>
        /// Registers a new <see cref="IEntity"/> into the registry and assigns it a unique ID.
        /// </summary>
        /// <param name="entity">The entity to register.</param>
        /// <param name="id">The unique ID assigned to the entity.</param>
        internal void Register(IEntity entity, out int id)
        {
            if (!_recycledIds.TryPop(out id)) 
                id = ++_lastId;

            _entities.Add(id, entity);
            this.OnAdded?.Invoke(entity);
        }

        /// <summary>
        /// Unregisters an <see cref="IEntity"/> from the registry by its reference ID.
        /// </summary>
        /// <param name="id">The ID reference to remove. Will be set to -1 if successfully removed.</param>
        internal void Unregister(ref int id)
        {
            if (_entities.Remove(id, out IEntity entity))
            {
                _recycledIds.Push(id);
                id = -1;
                this.OnRemoved?.Invoke(entity);
            }
        }

        /// <summary>
        /// Clears all entities in the registry.
        /// </summary>
        internal void Clear()
        {
            _recycledIds.Clear();
            _entities.Clear();
            _lastId = 0;
        }

        /// <summary>
        /// Attempts to check an id is registered or not.
        /// </summary>
        public bool Contains(int id) => _entities.ContainsKey(id);

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

        /// <summary>
        /// Retrieves the entity with the specified ID.
        /// Throws an exception if not found.
        /// </summary>
        /// <param name="id">The unique ID of the entity.</param>
        /// <returns>The entity with the specified ID.</returns>
        public IEntity Get(int id) => _entities[id];

        /// <summary>
        /// Returns an enumerator that iterates through all registered entities.
        /// </summary>
        /// <returns>An enumerator over the registered entities.</returns>
        public IEnumerator<IEntity> GetEnumerator() => _entities.Values.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => this.GetEnumerator();

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