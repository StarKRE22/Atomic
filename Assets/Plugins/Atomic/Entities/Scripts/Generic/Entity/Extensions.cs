using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using static Atomic.Entities.EntityUtils;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides extension methods for <see cref="IEntity"/> to simplify operations such as adding/removing tags, values, and behaviours.
    /// </summary>
    public static partial class Extensions
    {
        /// <summary>
        /// Adds multiple tags to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTags<E>(this IEntity<E> entity, IEnumerable<int> tags) where E : IEntity<E>
        {
            if (tags == null)
                return;

            foreach (int tag in tags)
                entity.AddTag(tag);
        }

        /// <summary>
        /// Adds multiple values to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValues<E>(this IEntity<E> entity, IReadOnlyDictionary<int, object> values)
            where E : IEntity<E>
        {
            if (values == null)
                return;

            foreach ((int key, object value) in values)
                entity.AddValue(key, value);
        }

        /// <summary>
        /// Adds multiple behaviours to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddBehaviours<E>(this IEntity<E> entity, IEnumerable<IBehaviour<E>> behaviours)
            where E : IEntity<E>
        {
            if (behaviours == null)
                return;

            foreach (IBehaviour<E> behaviour in behaviours)
                entity.AddBehaviour(behaviour);
        }

        /// <summary>
        /// Removes multiple behaviours from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelBehaviours<E>(this IEntity<E> entity, IEnumerable<IBehaviour<E>> behaviours)
            where E : IEntity<E>
        {
            if (behaviours == null)
                return;

            foreach (IBehaviour<E> behaviour in behaviours)
                entity.DelBehaviour(behaviour);
        }

        /// <summary>
        /// Adds a behaviour of the specified type to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddBehaviour<T, E>(this IEntity<E> entity)
            where T : IBehaviour<E>, new()
            where E : IEntity<E>
            => entity.AddBehaviour(new T());

        /// <summary>
        /// Checks if the entity contains all of the specified tags.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAllTags<E>(this IEntity<E> entity, params int[] tags) where E : IEntity<E>
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (!entity.HasTag(tags[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// Checks if the entity contains any of the specified tags.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyTag<E>(this IEntity<E> entity, params int[] tags) where E : IEntity<E>
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (entity.HasTag(tags[i]))
                    return true;

            return false;
        }

        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from the specified GameObject.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity<E>(this GameObject gameObject, out E entity) where E : IEntity<E> =>
            gameObject.TryGetComponent(out entity);

        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from the specified Component.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity<E>(this Component component, out E entity) where E : IEntity<E> =>
            component.TryGetComponent(out entity);

        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from a 2D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity<E>(this Collision2D collision2D, out E entity) where E : IEntity<E> =>
            collision2D.gameObject.TryGetComponent(out entity);

        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from a 3D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity<E>(this Collision collision, out E entity) where E : IEntity<E> =>
            collision.gameObject.TryGetComponent(out entity);

        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy of the GameObject.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent<E>(this GameObject gameObject, out E entity) where E : IEntity<E>
        {
            entity = gameObject.GetComponentInParent<E>();
            return entity != null;
        }

        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy of the Component.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent<E>(this Component component, out E entity) where E : IEntity<E>
        {
            entity = component.GetComponentInParent<E>();
            return entity != null;
        }

        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy from a 2D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent<E>(this Collision2D collision2D, out E entity) where E : IEntity<E>
        {
            entity = collision2D.gameObject.GetComponentInParent<E>();
            return entity != null;
        }

        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy from a 3D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent<E>(this Collision collision, out E entity) where E : IEntity<E>
        {
            entity = collision.gameObject.GetComponentInParent<E>();
            return entity != null;
        }

        /// <summary>
        /// Installs logic from an <see cref="IInstaller{E}"/> into the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E Install<E>(this E entity, IInstaller<E> installer) where E : IEntity<E>
        {
            installer.Install(entity);
            return entity;
        }

        /// <summary>
        /// Installs logic from <see cref="SceneInstaller{E}"/> components in the specified scene.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallFromScene<E>(this E entity, Scene scene, bool includeInactive = true)
            where E : IEntity<E>
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, goCount = gameObjects.Length; g < goCount; g++)
            {
                GameObject go = gameObjects[g];
                var installers = go.GetComponentsInChildren<SceneInstaller<E>>(includeInactive);
                for (int i = 0, installerCount = installers.Length; i < installerCount; i++)
                {
                    SceneInstaller<E> installer = installers[i];
                    installer.Install(entity);
                }
            }
        }

        /// <summary>
        /// Checks if the entity has the specified tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTag<E>(this IEntity<E> entity, string key) where E : IEntity<E> =>
            entity.HasTag(NameToId(key));

        /// <summary>
        /// Adds a tag to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTag<E>(this IEntity<E> entity, string key) where E : IEntity<E> =>
            entity.AddTag(NameToId(key));

        /// <summary>
        /// Adds a tag to the entity and returns its numeric ID.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTag<E>(this IEntity<E> entity, string key, out int id) where E : IEntity<E>
        {
            id = NameToId(key);
            return entity.AddTag(id);
        }

        /// <summary>
        /// Removes a tag from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTag<E>(this IEntity<E> entity, string tag) where E : IEntity<E> => entity
            .DelTag(NameToId(tag));

        /// <summary>
        /// Retrieves a value of type T associated with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T, E>(this IEntity<E> entity, string key) where E : IEntity<E> =>
            entity.GetValue<T>(NameToId(key));

        /// <summary>
        /// Tries to retrieve a value of type T associated with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T, E>(this IEntity<E> entity, string key, out T value) where E : IEntity<E>
            => entity.TryGetValue(NameToId(key), out value);

        /// <summary>
        /// Adds a value to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue<E>(this IEntity<E> entity, string key, object value) where E : IEntity<E>
            => entity.AddValue(NameToId(key), value);

        /// <summary>
        /// Adds a value to the entity and returns the corresponding ID.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue<E>(this IEntity<E> entity, string key, object value, out int id)
            where E : IEntity<E>
        {
            id = NameToId(key);
            entity.AddValue(id, value);
        }

        /// <summary>
        /// Adds a strongly-typed value to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue<T, E>(this IEntity<E> entity, string key, T value)
            where T : struct
            where E : IEntity<E> =>
            entity.AddValue(NameToId(key), value);

        /// <summary>
        /// Adds a strongly-typed value and retrieves its ID.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue<T, E>(this IEntity<E> entity, string key, T value, out int id)
            where T : struct
            where E : IEntity<E>
        {
            id = NameToId(key);
            entity.AddValue(id, value);
        }

        /// <summary>
        /// Removes a value from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelValue<E>(this IEntity<E> entity, string key)
            where E : IEntity<E> =>
            entity.DelValue(NameToId(key));

        /// <summary>
        /// Sets a value in the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue<E>(this IEntity<E> entity, string key, object value)
            where E : IEntity<E> =>
            entity.SetValue(NameToId(key), value);

        /// <summary>
        /// Sets a strongly-typed value in the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue<T, E>(this IEntity<E> entity, string key, T value)
            where T : struct where E : IEntity<E> =>
            entity.SetValue(NameToId(key), value);

        /// <summary>
        /// Checks if the entity has a value with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue<E>(this IEntity<E> entity, string key) where E : IEntity<E> =>
            entity.HasValue(NameToId(key));

        /// <summary>
        /// Adds multiple tags by string identifiers.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTags<E>(this IEntity<E> entity, IEnumerable<string> tags) where E : IEntity<E>
        {
            if (tags == null)
                return;

            foreach (string tag in tags)
                entity.AddTag(tag);
        }

        /// <summary>
        /// Adds multiple values by string keys.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValues<E>(this IEntity<E> entity, IReadOnlyDictionary<string, object> values)
            where E : IEntity<E>
        {
            if (values == null)
                return;

            foreach ((string key, object value) in values)
                entity.AddValue(key, value);
        }

        /// <summary>
        /// Checks if the entity has all the specified tags.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAllTags<E>(this IEntity<E> entity, params string[] tags)
            where E : IEntity<E>
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (!entity.HasTag(tags[i]))
                    return false;

            return true;
        }

        /// <summary>
        /// Checks if the entity has any of the specified tags.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAnyTag<E>(this IEntity<E> entity, params string[] tags) where E : IEntity<E>
        {
            for (int i = 0, count = tags.Length; i < count; i++)
                if (entity.HasTag(tags[i]))
                    return true;

            return false;
        }

        /// <summary>
        /// Disposes all disposable values stored in the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DisposeValues<E>(this IEntity<E> entity) where E : IEntity<E>
        {
            KeyValuePair<int, object>[] pairs = entity.GetValues();
            for (int i = 0, count = pairs.Length; i < count; i++)
                if (pairs[i].Value is IDisposable disposable)
                    disposable.Dispose();
        }
    }
}