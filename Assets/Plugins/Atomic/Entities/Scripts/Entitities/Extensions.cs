using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using static Atomic.Entities.EntityNames;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
using UnityEngine.SceneManagement;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Provides extension methods for <see cref="IEntity"/> to simplify operations such as adding/removing tags, values, and behaviours.
    /// </summary>
    public static partial class Extensions
    {
        #region Tags
        
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
        /// Removes a tag from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelTag(this IEntity entity, string tag) => entity.DelTag(NameToId(tag));


        /// <summary>
        /// Checks if the entity has the specified tag.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool HasTag(this IEntity entity, string key) => entity.HasTag(NameToId(key));

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

        #endregion

        #region Values

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
        /// Adds multiple values to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValues(this IEntity entity, IEnumerable<KeyValuePair<int, object>> values)
        {
            if (values == null)
                return;

            foreach ((int key, object value) in values)
                entity.AddValue(key, value);
        }
        
        /// <summary>
        /// Adds multiple values by string keys.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddValues(this IEntity entity, IEnumerable<KeyValuePair<string, object>> values)
        {
            if (values == null)
                return;

            foreach ((string key, object value) in values)
                entity.AddValue(key, value);
        }
           
        /// <summary>
        /// Removes a value from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool DelValue(this IEntity entity, string key) =>
            entity.DelValue(NameToId(key));
        
        /// <summary>
        /// Retrieves a value of type T associated with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T GetValue<T>(this IEntity entity, string key) =>
            entity.GetValue<T>(NameToId(key));

        /// <summary>
        /// Tries to retrieve a value of type T associated with the given key.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetValue<T>(this IEntity entity, string key, out T value) =>
            entity.TryGetValue(NameToId(key), out value);
        
        
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
        
        #endregion

        #region Behaviours

        /// <summary>
        /// Adds a behaviour of the specified type to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddBehaviour<T>(this IEntity entity) where T : IEntityBehaviour, new() =>
            entity.AddBehaviour(new T());

        /// <summary>
        /// Adds a subset of behaviours from an array to the specified entity.
        /// </summary>
        /// <param name="entity">The entity to which behaviours will be added.</param>
        /// <param name="behaviours">An array of behaviours to add. Can be <c>null</c>, in which case nothing is added.</param>
        /// <param name="startIndex">The starting index in the <paramref name="behaviours"/> array.</param>
        /// <param name="count">The number of behaviours to add from <paramref name="startIndex"/>.</param>
        /// <remarks>
        /// This method performs no action if the <paramref name="behaviours"/> array is <c>null</c>.
        /// Behaviours are added in order from <paramref name="startIndex"/> up to <paramref name="startIndex"/> + <paramref name="count"/>.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddBehaviours(this IEntity entity, IEntityBehaviour[] behaviours, int startIndex, int count)
        {
            if (behaviours == null)
                return;

            for (int i = startIndex, end = startIndex + count; i < end; i++)
                entity.AddBehaviour(behaviours[i]);
        }

        /// <summary>
        /// Adds multiple behaviours to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddBehaviours(this IEntity entity, IEnumerable<IEntityBehaviour> behaviours)
        {
            if (behaviours == null)
                return;

            foreach (IEntityBehaviour behaviour in behaviours)
                entity.AddBehaviour(behaviour);
        }

        /// <summary>
        /// Removes multiple behaviours from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelBehaviours(this IEntity entity, IEnumerable<IEntityBehaviour> behaviours)
        {
            if (behaviours == null)
                return;

            foreach (IEntityBehaviour behaviour in behaviours)
                entity.DelBehaviour(behaviour);
        }

        /// <summary>
        /// Removes multiple behaviours from the entity.
        /// </summary>
        /// <param name="behaviours">An array of behaviours to remove. Can be <c>null</c>, in which case nothing is removed.</param>
        /// <param name="startIndex">The starting index in the <paramref name="behaviours"/> array.</param>
        /// <param name="count">The number of behaviours to remove from <paramref name="startIndex"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelBehaviours(this IEntity entity, IEntityBehaviour[] behaviours, int startIndex, int count)
        {
            if (behaviours == null)
                return;

            for (int i = startIndex, end = startIndex + count; i < end; i++)
                entity.DelBehaviour(behaviours[i]);
        }

        #endregion
        
        #region Clearing

        /// <summary>
        /// Clears all data (tags, values, behaviours) from this entity.
        /// </summary>
        public static void Clear(this IEntity entity)
        {
            entity.ClearTags();
            entity.ClearValues();
            entity.ClearBehaviours();
        }

        #endregion
        
        #region Installing

        /// <summary>
        /// Installs logic from a single <see cref="IEntityInstaller"/> into the specified entity.
        /// </summary>
        /// <param name="entity">The entity to install the logic into.</param>
        /// <param name="installer">The installer that provides logic to install.</param>
        /// <returns>The same <paramref name="entity"/> after installation for chaining.</returns>
        /// <remarks>
        /// This method delegates the installation process to the <see cref="IEntityInstaller.Install(IEntity)"/> method.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IEntity Install(this IEntity entity, IEntityInstaller installer)
        {
            installer.Install(entity);
            return entity;
        }

        /// <summary>
        /// Installs logic from multiple <see cref="IEntityInstaller"/> instances into the specified entity.
        /// </summary>
        /// <param name="entity">The entity to install the logic into.</param>
        /// <param name="installers">A collection of installers. Can be <c>null</c>, in which case nothing is installed.</param>
        /// <remarks>
        /// Each installer in <paramref name="installers"/> will have its <see cref="IEntityInstaller.Install(IEntity)"/> method invoked.
        /// </remarks>
        public static void Install(this IEntity entity, IEnumerable<IEntityInstaller> installers)
        {
            if (installers == null)
                return;

            foreach (IEntityInstaller installer in installers)
                installer.Install(entity);
        }

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Installs logic from all <see cref="SceneEntityInstaller"/> components found in the specified scene.
        /// </summary>
        /// <param name="entity">The entity to install the logic into.</param>
        /// <param name="scene">The scene in which to search for installers.</param>
        /// <param name="includeInactive">
        /// If <c>true</c>, installers on inactive GameObjects will also be included; otherwise only active installers are considered.
        /// </param>
        /// <remarks>
        /// This method iterates over all root GameObjects in the scene and applies each found <see cref="SceneEntityInstaller"/> to the entity.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallFromScene(this IEntity entity, Scene scene, bool includeInactive = true)
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, goCount = gameObjects.Length; g < goCount; g++)
            {
                GameObject go = gameObjects[g];
                var installers = go.GetComponentsInChildren<SceneEntityInstaller>(includeInactive);
                for (int i = 0, installerCount = installers.Length; i < installerCount; i++)
                {
                    SceneEntityInstaller installer = installers[i];
                    installer.Install(entity);
                }
            }
        }

        /// <summary>
        /// Installs logic from all <see cref="SceneEntityInstaller{T}"/> components found in the specified scene for a generic entity type.
        /// </summary>
        /// <typeparam name="T">The entity type that implements <see cref="IEntity"/>.</typeparam>
        /// <param name="entity">The entity to install the logic into.</param>
        /// <param name="scene">The scene in which to search for installers.</param>
        /// <param name="includeInactive">
        /// If <c>true</c>, installers on inactive GameObjects will also be included; otherwise only active installers are considered.
        /// </param>
        /// <remarks>
        /// This method iterates over all root GameObjects in the scene and applies each found <see cref="SceneEntityInstaller{T}"/> to the entity.
        /// Useful for generic entities or strongly-typed scenarios.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallFromScene<T>(this T entity, Scene scene, bool includeInactive = true)
            where T : class, IEntity
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, count = gameObjects.Length; g < count; g++)
            {
                GameObject go = gameObjects[g];
                var installers = go.GetComponentsInChildren<SceneEntityInstaller<T>>(includeInactive);

                for (int i = 0, installerCount = installers.Length; i < installerCount; i++)
                {
                    SceneEntityInstaller<T> installer = installers[i];
                    installer.Install(entity);
                }
            }
        }
#endif

        #endregion
        
        #region Retrieval

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from the specified GameObject.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this GameObject gameObject, out IEntity entity) =>
            gameObject.TryGetComponent(out entity);
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from the specified Component.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Component component, out IEntity entity) =>
            component.TryGetComponent(out entity);
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from a 2D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision2D collision2D, out IEntity entity) =>
            collision2D.gameObject.TryGetComponent(out entity);
#endif

#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Tries to retrieve the <see cref="IEntity"/> component from a 3D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool TryGetEntity(this Collision collision, out IEntity entity) =>
            collision.gameObject.TryGetComponent(out entity);
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy of the GameObject.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this GameObject gameObject, out IEntity entity)
        {
            entity = gameObject.GetComponentInParent<IEntity>();
            return entity != null;
        }
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy of the Component.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Component component, out IEntity entity)
        {
            entity = component.GetComponentInParent<IEntity>();
            return entity != null;
        }
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy from a 2D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Collision2D collision2D, out IEntity entity)
        {
            entity = collision2D.gameObject.GetComponentInParent<IEntity>();
            return entity != null;
        }
#endif
#if UNITY_5_3_OR_NEWER
        /// <summary>
        /// Finds an <see cref="IEntity"/> in the parent hierarchy from a 3D collision.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool FindEntityInParent(this Collision collision, out IEntity entity)
        {
            entity = collision.gameObject.GetComponentInParent<IEntity>();
            return entity != null;
        }
#endif

        #endregion
    }
}