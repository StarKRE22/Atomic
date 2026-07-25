#if UNITY_5_3_OR_NEWER
using System;
using System.Threading.Tasks;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Manages multiple scene entity pools, each associated with a specific prefab.
    /// Provides centralized methods for renting and returning entities across those pools.
    /// </summary>
    /// <typeparam name="E">The type of scene entity being pooled. Must inherit from <see cref="MonoEntity"/>.</typeparam>
    public interface IPrefabEntityPool<E, in P> : IDisposable
        where E : IEntity
        where P : MonoEntity, E
    {
        /// <summary>
        /// Initializes the pool associated with the specified key by pre-populating it with entities.
        /// </summary>
        /// <param name="prefab">The prefab to use as the key for the pool.</param>
        /// <param name="count">The number of entities to preallocate in the pool.</param>
        void Init(P prefab, int count);
        
        ValueTask InitAsync(P prefab, int initialCount);
        
        /// <summary>
        /// Rents an entity instance from the pool associated with the specified prefab.
        /// </summary>
        /// <param name="prefab">The prefab to use as the key for the pool.</param>
        /// <returns>A rented instance of the specified prefab.</returns>
        E Rent(P prefab);

        /// <summary>
        /// Rents an entity instance and parents it under the specified transform.
        /// </summary>
        /// <param name="prefab">The prefab to use as the key for the pool.</param>
        /// <param name="parent">The transform to parent the entity under.</param>
        /// <returns>A rented and parented instance of the specified prefab.</returns>
        E Rent(P prefab, Transform parent);

        /// <summary>
        /// Rents an entity instance with a specific position and rotation, optionally setting a parent.
        /// </summary>
        /// <param name="prefab">The prefab to use as the key for the pool.</param>
        /// <param name="position">The world position for the entity.</param>
        /// <param name="rotation">The rotation for the entity.</param>
        /// <param name="parent">Optional parent transform for the entity.</param>
        /// <returns>A rented instance positioned and rotated as specified.</returns>
        E Rent(P prefab, Vector3 position, Quaternion rotation, Transform parent = null);

        /// <summary>
        /// Returns a previously rented entity to its corresponding pool.
        /// </summary>
        /// <param name="entity">The entity instance to return.</param>
        void Return(E entity);

        /// <summary>
        /// Clears the pool associated with the given prefab, destroying all pooled instances.
        /// </summary>
        /// <param name="prefab">The prefab whose associated pool should be cleared.</param>
        void Dispose(P prefab);
    }
}
#endif