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

    public abstract class SceneEntityBaker<E> : MonoBehaviour where E : IEntity
    {
        [Tooltip("Should destroy this Game Object after baking?")]
        [SerializeField]
        internal bool _destroyAfterBake = true;
        
        [Space]
        [SerializeField]
        internal ScriptableEntityFactory<E> _factory;

        public E Bake()
        {
            E entity = _factory.Create();
            this.Install(entity);

            if (_destroyAfterBake)
                Destroy(this.gameObject);

            return entity;
        }

        protected abstract void Install(E entity);

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
                if (!includeInactive && !baker.gameObject.activeInHierarchy)
                    continue;

                E entity = baker.Bake();
                entities[i] = entity;
            }

            return entities;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void BakeAll(ICollection<E> destination, bool includeInactive = true)
        {
            if (destination == null)
                throw new System.ArgumentNullException(nameof(destination));

#if UNITY_2023_1_OR_NEWER
            FindObjectsInactive include = includeInactive
                ? FindObjectsInactive.Include
                : FindObjectsInactive.Exclude;

            SceneEntityBaker<E>[] bakers = FindObjectsByType<SceneEntityBaker<E>>(include, FindObjectsSortMode.None);
#else
            SceneEntityBaker<E>[] bakers = Object.FindObjectsOfType<SceneEntityBaker<E>>(includeInactive);
#endif

            int count = bakers.Length;
            for (int i = 0; i < count; i++)
            {
                SceneEntityBaker<E> baker = bakers[i];
                if (!includeInactive && !baker.gameObject.activeInHierarchy)
                    continue;

                E entity = baker.Bake();
                destination.Add(entity);
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
                    if (!includeInactive && !baker.gameObject.activeInHierarchy)
                        continue;

                    E entity = baker.Bake();
                    result.Add(entity);
                }
            }

            return result;
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
    }
}
#endif