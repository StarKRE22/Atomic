using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Provides extension methods for working with <see cref="IEntityCollection{E}"/>.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Adds a range of entities to the collection.
        /// </summary>
        /// <typeparam name="E">The entity type.</typeparam>
        /// <param name="it">The target entity collection.</param>
        /// <param name="entities">The array of entities to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<E>(this IEntityCollection<E> it, params E[] entities) where E : IEntity
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            for (int i = 0, count = entities.Length; i < count; i++)
                it.Add(entities[i]);
        }

        /// <summary>
        /// Adds a range of entities from an enumerable to the collection.
        /// Ignores null entities.
        /// </summary>
        /// <typeparam name="E">The entity type.</typeparam>
        /// <param name="it">The target entity collection.</param>
        /// <param name="entities">The enumerable of entities to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<E>(this IEntityCollection<E> it, IEnumerable<E> entities) where E : IEntity
        {
            if (entities == null)
                throw new ArgumentNullException(nameof(entities));

            foreach (E entity in entities)
                it.Add(entity);
        }

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Instantiates a new entity based on the given prefab and adds it to the collection.
        /// </summary>
        /// <typeparam name="E">The type of scene entity.</typeparam>
        /// <param name="it">The target entity collection.</param>
        /// <param name="prefab">The prefab to instantiate.</param>
        /// <param name="position">The spawn position.</param>
        /// <param name="rotation">The spawn rotation.</param>
        /// <param name="parent">Optional parent transform.</param>
        /// <returns>The newly created and added entity instance.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E CreateEntity<E>(
            this IEntityCollection<E> it,
            E prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null
        ) where E : SceneEntity
        {
            E entity = SceneEntity.Create(prefab, position, rotation, parent);
            it.Add(entity);
            return entity;
        }

#endif

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Removes the entity from the collection and destroys its game object.
        /// </summary>
        /// <typeparam name="E">The type of scene entity.</typeparam>
        /// <param name="it">The target entity collection.</param>
        /// <param name="entity">The entity to remove and destroy.</param>
        /// <param name="delay">Optional delay before destruction, in seconds.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DestroyEntity<E>(this IEntityCollection<E> it, E entity, float delay = 0)
            where E : SceneEntity
        {
            if (it.Remove(entity))
                SceneEntity.Destroy(entity, delay);
        }
#endif

        /// <summary>
        /// Initializes all entities in the specified collection by calling <see cref="IEntity.Init"/> on each entity.
        /// </summary>
        /// <typeparam name="E">The type of entity contained in the collection.</typeparam>
        /// <param name="it">The collection of entities to initialize.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InitEntities<E>(this IEntityCollection<E> it) where E : IEntity
        {
            foreach (E entity in it)
                entity.Init();
        }

        /// <summary>
        /// Disposes all entities in the specified collection by calling <see cref="IDisposable.Dispose"/> on each entity.
        /// </summary>
        /// <typeparam name="E">The type of entity contained in the collection.</typeparam>
        /// <param name="it">The collection of entities to dispose.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeEntities<E>(this IEntityCollection<E> it) where E : IEntity
        {
            foreach (E entity in it)
                entity.Dispose();
        }

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Collects all <see cref="SceneEntity"/> instances present in the active scene,
        /// calls their <see cref="SceneEntity.Install"/> method, and adds them to the given collection.
        /// </summary>
        /// <typeparam name="E">Type of scene entity to collect.</typeparam>
        /// <param name="collection">The target collection where entities will be added.</param>
        /// <param name="includeInactive">Whether to include inactive GameObjects.</param>
        public static void CollectAllEntities<E>(this IEntityCollection<E> collection, bool includeInactive = false)
            where E : SceneEntity
        {
#if UNITY_2023_1_OR_NEWER
            FindObjectsInactive findObjectsInactive = includeInactive
                ? FindObjectsInactive.Include
                : FindObjectsInactive.Exclude;

            E[] entities = GameObject.FindObjectsByType<E>(findObjectsInactive, FindObjectsSortMode.None);
#else
            E[] entities = GameObject.FindObjectsOfType<E>(includeInactive);
#endif
            for (int i = 0, count = entities.Length; i < count; i++)
            {
                E entity = entities[i];
                entity.Install();
                collection.Add(entity);
            }
        }
#endif
    }
}