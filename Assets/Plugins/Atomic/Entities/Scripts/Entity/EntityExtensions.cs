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
    public static class EntityExtensions
    {
        /// <summary>
        /// Adds multiple tags to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTags(this IEntity entity, IEnumerable<int> tags)
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
        public static void AddValues(this IEntity entity, IReadOnlyDictionary<int, object> values)
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
        public static void AddBehaviours(this IEntity entity, IEnumerable<IBehaviour> behaviours)
        {
            if (behaviours == null)
                return;

            foreach (IBehaviour behaviour in behaviours)
                entity.AddBehaviour(behaviour);
        }
        
        /// <summary>
        /// Removes multiple behaviours from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelBehaviours(this IEntity entity, IEnumerable<IBehaviour> behaviours)
        {
            if (behaviours == null)
                return;

            foreach (IBehaviour behaviour in behaviours)
                entity.DelBehaviour(behaviour);
        }

        /// <summary>
        /// Adds a behaviour of the specified type to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddBehaviour<T>(this IEntity entity) where T : IBehaviour, new() =>
            entity.AddBehaviour(new T());

        /// <summary>
        /// Checks if the entity contains all of the specified tags.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasAllTags(this IEntity entity, params int[] tags)
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
        public static bool HasAnyTag(this IEntity entity, params int[] tags)
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
        public static bool TryGetEntity(this GameObject gameObject, out IEntity entity) =>
            gameObject.TryGetComponent(out entity);

        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from the specified Component.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Component component, out IEntity entity) =>
            component.TryGetComponent(out entity);

        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from a 2D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision2D collision2D, out IEntity entity) =>
            collision2D.gameObject.TryGetComponent(out entity);

        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from a 3D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision collision, out IEntity entity) =>
            collision.gameObject.TryGetComponent(out entity);

        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy of the GameObject.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this GameObject gameObject, out IEntity entity)
        {
            entity = gameObject.GetComponentInParent<IEntity>();
            return entity != null;
        }

        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy of the Component.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Component component, out IEntity entity)
        {
            entity = component.GetComponentInParent<IEntity>();
            return entity != null;
        }
        
        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy from a 2D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Collision2D collision2D, out IEntity entity)
        {
            entity = collision2D.gameObject.GetComponentInParent<IEntity>();
            return entity != null;
        }

        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy from a 3D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Collision collision, out IEntity entity)
        {
            entity = collision.gameObject.GetComponentInParent<IEntity>();
            return entity != null;
        }
        
        /// <summary>
        /// Installs logic from an <see cref="IEntityInstaller"/> into the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEntity Install(this IEntity entity, IEntityInstaller installer)
        {
            installer.Install(entity);
            return entity;
        }

        /// <summary>
        /// Installs logic from <see cref="SceneEntityInstaller"/> components in the specified scene.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallFromScene(this IEntity obj, Scene scene, bool includeInactive = true)
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, goCount = gameObjects.Length; g < goCount; g++)
            {
                GameObject go = gameObjects[g];
                var installers = go.GetComponentsInChildren<SceneEntityInstaller>(includeInactive);
                for (int i = 0, installerCount = installers.Length; i < installerCount; i++)
                {
                    SceneEntityInstaller installer = installers[i];
                    installer.Install(obj);
                }
            }
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallFromScene<T>(this T obj, Scene scene, bool includeInactive = true) where T : class, IEntity
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, count = gameObjects.Length; g < count; g++)
            {
                GameObject go = gameObjects[g];
                var installers = go.GetComponentsInChildren<SceneEntityInstaller<T>>(includeInactive);

                for (int i = 0, installerCount = installers.Length; i < installerCount; i++)
                {
                    SceneEntityInstaller<T> installer = installers[i];
                    installer.Install(obj);
                }
            }
        }

        /// <summary>
        /// Checks if the entity has the specified tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTag(this IEntity entity, string key) => entity.HasTag(NameToId(key));

        /// <summary>
        /// Adds a tag to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTag(this IEntity entity, string key) => entity.AddTag(NameToId(key));

        /// <summary>
        /// Adds a tag to the entity and returns its numeric ID.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool AddTag(this IEntity entity, string key, out int id)
        {
            id = NameToId(key);
            return entity.AddTag(id);
        }

        /// <summary>
        /// Removes a tag from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTag(this IEntity entity, string tag) => entity.DelTag(NameToId(tag));

        /// <summary>
        /// Retrieves a value of type T associated with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IEntity entity, string key) => entity.GetValue<T>(NameToId(key));

        /// <summary>
        /// Tries to retrieve a value of type T associated with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IEntity entity, string key, out T value) =>
            entity.TryGetValue(NameToId(key), out value);

        /// <summary>
        /// Adds a value to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue(this IEntity entity, string key, object value) =>
            entity.AddValue(NameToId(key), value);
        
        /// <summary>
        /// Adds a value to the entity and returns the corresponding ID.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue(this IEntity entity, string key, object value, out int id)
        {
            id = NameToId(key);
            entity.AddValue(id, value);
        }

        /// <summary>
        /// Adds a strongly-typed value to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue<T>(this IEntity entity, string key, T value) where T : struct =>
            entity.AddValue(NameToId(key), value);
        
        /// <summary>
        /// Adds a strongly-typed value and retrieves its ID.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValue<T>(this IEntity entity, string key, T value, out int id) where T : struct
        {
            id = NameToId(key);
            entity.AddValue(id, value);
        }

        /// <summary>
        /// Removes a value from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelValue(this IEntity entity, string key) =>
            entity.DelValue(NameToId(key));

        /// <summary>
        /// Sets a value in the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue(this IEntity entity, string key, object value) =>
            entity.SetValue(NameToId(key), value);

        /// <summary>
        /// Sets a strongly-typed value in the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void SetValue<T>(this IEntity entity, string key, T value) where T : struct =>
            entity.SetValue(NameToId(key), value);

        /// <summary>
        /// Checks if the entity has a value with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasValue(this IEntity entity, string key) => entity.HasValue(NameToId(key));

        /// <summary>
        /// Adds multiple tags by string identifiers.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddTags(this IEntity entity, IEnumerable<string> tags)
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
        public static void AddValues(this IEntity entity, IReadOnlyDictionary<string, object> values)
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
        public static bool HasAllTags(this IEntity entity, params string[] tags)
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
        public static bool HasAnyTag(this IEntity entity, params string[] tags)
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
        public static void DisposeValues(this IEntity entity)
        {
            KeyValuePair<int, object>[] pairs = entity.GetValues();
            for (int i = 0, count = pairs.Length; i < count; i++)
                if (pairs[i].Value is IDisposable disposable)
                    disposable.Dispose();
        }
    }
}