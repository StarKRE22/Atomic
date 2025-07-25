#if UNITY_5_3_OR_NEWER
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for <see cref="SceneEntityBaker{IEntity}"/>.
    /// </summary>
    public abstract class SceneEntityBaker : SceneEntityBaker<IEntity>
    {
    }

    /// <summary>
    /// Scene entity baker that allows converting authoring components (MonoBehaviours)
    /// into <typeparamref name="E"/> entities at runtime and removing their GameObjects.
    /// </summary>
    /// <typeparam name="E">The type of entity to create.</typeparam>
    public abstract class SceneEntityBaker<E> : SceneEntityFactory<E> where E : IEntity
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
            SceneEntityBaker<E>[] bakers = Object.FindObjectsOfType<SceneEntityBaker<E>>(includeInactive);
#endif
            int count = bakers.Length;
            E[] entities = new E[count];

            for (int i = 0; i < count; i++)
            {
                SceneEntityBaker<E> baker = bakers[i];
                E entity = baker.Create();
                entities[i] = entity;
                Destroy(baker.gameObject);
            }

            return entities;
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
                    E entity = baker.Create();
                    result.Add(entity);
                    Destroy(baker.gameObject);
                }
            }

            return result;
        }

        /// <summary>
        /// Bakes all <see cref="SceneEntityBaker{E}"/> components attached to or under the specified GameObject.
        /// </summary>
        /// <param name="gameObject">The GameObject to search.</param>
        /// <returns>Array of baked entities.</returns>
        public static E[] Bake(GameObject gameObject)
        {
            SceneEntityBaker<E>[] bakers = gameObject.GetComponentsInChildren<SceneEntityBaker<E>>(true);
            int count = bakers.Length;
            E[] entities = new E[count];

            for (int i = 0; i < count; i++)
            {
                SceneEntityBaker<E> baker = bakers[i];
                E entity = baker.Create();
                entities[i] = entity;
                Destroy(baker.gameObject);
            }

            return entities;
        }
    }
}
#endif