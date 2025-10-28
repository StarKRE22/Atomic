#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Atomic.Entities
{
    public partial class SceneEntityBaker<E>
    {
        /// <summary>
        /// Finds all <see cref="SceneEntityBaker{E}"/> components in the scene and bakes them into entities.
        /// All corresponding GameObjects will be destroyed after baking.
        /// </summary>
        /// <param name="includeInactive">Whether to include inactive objects in the search.</param>
        /// <returns>Array of baked entities.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E[] BakeAll(bool includeInactive = true)
        {
#if UNITY_2023_1_OR_NEWER
            FindObjectsInactive include = includeInactive
                ? FindObjectsInactive.Include
                : FindObjectsInactive.Exclude;

            SceneEntityBaker<E>[] bakers = FindObjectsByType<SceneEntityBaker<E>>(include, FindObjectsSortMode.None);
#else
            SceneEntityBaker<E>[] bakers = FindObjectsOfType<SceneEntityBaker<E>>(includeInactive);
#endif
            int count = bakers.Length;
            E[] entities = new E[count];

            for (int i = 0; i < count; i++)
            {
                SceneEntityBaker<E> baker = bakers[i];
                if (includeInactive || baker.gameObject.activeInHierarchy)
                {
                    E entity = baker.Bake();
                    entities[i] = entity;
                }
            }

            return entities;
        }

        /// <summary>
        /// Collects entities from all <see cref="SceneEntityBaker{E}"/> components in the scene
        /// and adds them to the specified <paramref name="destination"/> collection.
        /// </summary>
        /// <typeparam name="E">The type of entity created by the bakers.</typeparam>
        /// <param name="destination">
        /// The collection where all baked entities will be stored.  
        /// Must not be <c>null</c>.
        /// </param>
        /// <param name="includeInactive">
        /// Whether to include bakers attached to inactive GameObjects.  
        /// If <c>false</c>, only active bakers are considered.
        /// </param>
        /// <exception cref="ArgumentNullException">
        /// Thrown if <paramref name="destination"/> is <c>null</c>.
        /// </exception>
        /// <remarks>
        /// This method finds all <see cref="SceneEntityBaker{E}"/> instances in the scene and
        /// invokes their <c>Create</c> method to generate entities.  
        /// The resulting entities are then added to <paramref name="destination"/>.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BakeAll(ICollection<E> destination, bool includeInactive = true)
        {
            if (destination == null)
                throw new ArgumentNullException(nameof(destination));

#if UNITY_2023_1_OR_NEWER
            FindObjectsInactive include = includeInactive
                ? FindObjectsInactive.Include
                : FindObjectsInactive.Exclude;

            SceneEntityBaker<E>[] bakers = FindObjectsByType<SceneEntityBaker<E>>(include, FindObjectsSortMode.None);
#else
            SceneEntityBaker<E>[] bakers = FindObjectsOfType<SceneEntityBaker<E>>(includeInactive);
#endif

            int count = bakers.Length;
            for (int i = 0; i < count; i++)
            {
                SceneEntityBaker<E> baker = bakers[i];
                if (includeInactive || baker.gameObject.activeInHierarchy)
                {
                    E entity = baker.Bake();
                    destination.Add(entity);
                }
            }
        }

        /// <summary>
        /// Bakes all <see cref="SceneEntityBaker{E}"/>s in a specific <see cref="Scene"/>.
        /// </summary>
        /// <param name="scene">The scene whose root objects should be searched.</param>
        /// <returns>List of baked entities.</returns>
        public static List<E> Bake(Scene scene, bool includeInactive = true)
        {
            var result = new List<E>();
            GameObject[] rootObjects = scene.GetRootGameObjects();

            for (int i = 0, rootCount = rootObjects.Length; i < rootCount; i++)
            {
                GameObject rootObject = rootObjects[i];
                SceneEntityBaker<E>[] bakers = rootObject.GetComponentsInChildren<SceneEntityBaker<E>>(includeInactive);
                for (int j = 0, bakerCount = bakers.Length; j < bakerCount; j++)
                {
                    SceneEntityBaker<E> baker = bakers[j];
                    if (includeInactive || baker.gameObject.activeInHierarchy)
                    {
                        E entity = baker.Bake();
                        result.Add(entity);
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// Bakes all <see cref="SceneEntityBaker{E}"/>s in a specific <see cref="Scene"/> and adds them to the provided collection.
        /// </summary>
        /// <param name="scene">The scene whose root objects should be searched.</param>
        /// <param name="results">The collection where baked entities will be added.</param>
        /// <param name="includeInactive">Whether to include inactive objects in the search.</param>
        public static void Bake(Scene scene, ICollection<E> results, bool includeInactive = true)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            GameObject[] objects = scene.GetRootGameObjects();
            for (int i = 0, objectCount = objects.Length; i < objectCount; i++)
            {
                GameObject go = objects[i];
                SceneEntityBaker<E>[] bakers = go.GetComponentsInChildren<SceneEntityBaker<E>>(includeInactive);

                for (int j = 0, bakerCount = bakers.Length; j < bakerCount; j++)
                {
                    SceneEntityBaker<E> baker = bakers[j];
                    if (includeInactive || baker.gameObject.activeInHierarchy)
                    {
                        E entity = baker.Bake();
                        results.Add(entity);
                    }
                }
            }
        }


        /// <summary>
        /// Bakes all <see cref="SceneEntityBaker{E}"/> components attached to or under the specified GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject to search.</param>
        /// <returns>Array of baked entities.</returns>
        public static E[] Bake(GameObject gameObject, bool includeInactive = true)
        {
            SceneEntityBaker<E>[] bakers = gameObject.GetComponentsInChildren<SceneEntityBaker<E>>(includeInactive);
            int count = bakers.Length;
            E[] entities = new E[count];

            for (int i = 0; i < count; i++)
            {
                SceneEntityBaker<E> baker = bakers[i];
                if (!includeInactive && !baker.gameObject.activeInHierarchy)
                    continue;

                E entity = baker.Bake();
                entities[i] = entity;
            }

            return entities;
        }

        /// <summary>
        /// Bakes all <see cref="SceneEntityBaker{E}"/> components attached to or under the specified GameObject
        /// and adds them to the provided collection.
        /// </summary>
        /// <param name="gameObject">The GameObject to search.</param>
        /// <param name="results">The collection where baked entities will be added.</param>
        /// <param name="includeInactive">Whether to include inactive objects in the search.</param>
        public static void Bake(GameObject gameObject, ICollection<E> results, bool includeInactive = true)
        {
            if (results == null)
                throw new ArgumentNullException(nameof(results));

            SceneEntityBaker<E>[] bakers = gameObject.GetComponentsInChildren<SceneEntityBaker<E>>(includeInactive);
            for (int i = 0, count = bakers.Length; i < count; i++)
            {
                SceneEntityBaker<E> baker = bakers[i];
                if (includeInactive || baker.gameObject.activeInHierarchy)
                {
                    E entity = baker.Bake();
                    results.Add(entity);
                }
            }
        }
    }
}
#endif