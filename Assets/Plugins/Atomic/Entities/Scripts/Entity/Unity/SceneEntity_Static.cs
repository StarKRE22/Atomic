#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides static factory and utility methods for working with <see cref="SceneEntity"/>.
    /// Includes creation, casting, and batch installation logic.
    /// </summary>
    public partial class SceneEntity
    {
        [Serializable]
        public struct CreateArgs
        {
            public string name;
            public IEnumerable<int> tags;
            public IReadOnlyDictionary<int, object> values;
            public IEnumerable<IEntityBehaviour> behaviours;
            public List<SceneEntityInstaller> installers;
            public List<SceneEntity> children;

            public int initialTagCapacity;
            public int initialValueCapacity;
            public int initialBehaviourCapacity;

            public bool installOnAwake;
            public bool disposeValues;
            public bool useUnityLifecycle;
        }

        /// <summary>
        /// Creates a new <see cref="SceneEntity"/> GameObject and configures it with optional tags, values, and behaviours.
        /// </summary>
        public static SceneEntity Create(in CreateArgs args) => Create<SceneEntity>(in args);

        public static E Create<E>(in CreateArgs args) where E : SceneEntity
        {
            GameObject gameObject = new GameObject();
            gameObject.SetActive(false);

            E sceneEntity = gameObject.AddComponent<E>();

            sceneEntity.name = args.name;

            sceneEntity.installers = args.installers;
            sceneEntity.children = args.children;

            sceneEntity.installOnAwake = args.installOnAwake;
            sceneEntity.disposeValues = args.disposeValues;
            sceneEntity.useUnityLifecycle = args.useUnityLifecycle;

            sceneEntity.initialBehaviourCapacity = Mathf.Max(1, args.initialBehaviourCapacity);
            sceneEntity.initialTagCapacity = Mathf.Max(1, args.initialTagCapacity);
            sceneEntity.initialValueCapacity = Mathf.Max(1, args.initialValueCapacity);

            sceneEntity.AddTags(args.tags);
            sceneEntity.AddValues(args.values);
            sceneEntity.AddBehaviours(args.behaviours);

            gameObject.SetActive(true);
            return sceneEntity;
        }

        public static E Create<E>(
            string name = null,
            IEnumerable<int> tags = null,
            IReadOnlyDictionary<int, object> values = null,
            IEnumerable<IEntityBehaviour> behaviours = null,
            bool installOnAwake = true,
            bool disposeValues = true,
            bool useUnityLifecycle = true,
            int initialTagCount = 1,
            int initialValueCount = 1,
            int initialBehaviourCount = 1
        ) where E : SceneEntity => Create<E>(new CreateArgs
        {
            name = name,
            tags = tags,
            values = values,
            behaviours = behaviours,
            installOnAwake = installOnAwake,
            disposeValues = disposeValues,
            useUnityLifecycle = useUnityLifecycle,
            initialTagCapacity = initialTagCount,
            initialValueCapacity = initialValueCount,
            initialBehaviourCapacity = initialBehaviourCount
        });

        public static SceneEntity Create(
            string name = null,
            IEnumerable<int> tags = null,
            IReadOnlyDictionary<int, object> values = null,
            IEnumerable<IEntityBehaviour> behaviours = null,
            bool installOnAwake = true,
            bool disposeValues = true,
            bool useUnityLifecycle = true,
            int initialTagCount = 1,
            int initialValueCount = 1,
            int initialBehaviourCount = 1
        ) => Create(new CreateArgs
        {
            name = name,
            tags = tags,
            values = values,
            behaviours = behaviours,
            installOnAwake = installOnAwake,
            disposeValues = disposeValues,
            useUnityLifecycle = useUnityLifecycle,
            initialTagCapacity = initialTagCount,
            initialValueCapacity = initialValueCount,
            initialBehaviourCapacity = initialBehaviourCount
        });

        /// <summary>
        /// Instantiates a prefab and installs the resulting <see cref="SceneEntity"/> under the specified parent.
        /// </summary>
        public static SceneEntity Create(SceneEntity prefab, Transform parent = null) =>
            Create(prefab, Vector3.zero, Quaternion.identity, parent);

        public static E Create<E>(E prefab, Transform parent = null) where E : SceneEntity =>
            Create(prefab, Vector3.zero, Quaternion.identity, parent);

        /// <summary>
        /// Instantiates a prefab at the given position and rotation with optional parent, then installs it.
        /// </summary>
        public static SceneEntity Create(
            SceneEntity prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null
        ) => Create<SceneEntity>(prefab, position, rotation, parent);

        public static E Create<E>(
            E prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null
        ) where E : SceneEntity
        {
            E entity = Instantiate(prefab, position, rotation, parent);
            entity.Install();
            return entity;
        }

        /// <summary>
        /// Destroys the associated GameObject of a given <see cref="IEntity"/> if it is a <see cref="SceneEntity"/>.
        /// </summary>
        public static void Destroy(IEntity entity, float t = 0)
        {
            SceneEntity sceneEntity = Cast(entity);
            Destroy(sceneEntity.gameObject, t);
        }
        
        public static void Destroy(SceneEntity entity, float t = 0)
        {
            Destroy(entity.gameObject, t);
        }

        /// <summary>
        /// Casts the <see cref="IEntity"/> to a <see cref="SceneEntity"/> if possible.
        /// </summary>
        public static SceneEntity Cast(IEntity entity) => Cast<SceneEntity>(entity);

        public static E Cast<E>(IEntity entity) where E : SceneEntity => entity switch
        {
            null => null,
            E sceneEntity => sceneEntity,
            SceneEntityProxy<E> proxy => proxy.Source,
            _ => throw new InvalidCastException($"Can't cast {entity.Name} to {typeof(E).Name}")
        };

        /// <summary>
        /// Attempts to cast the <see cref="IEntity"/> to a <see cref="SceneEntity"/>.
        /// </summary>
        public static bool TryCast(IEntity entity, out SceneEntity result) =>
            TryCast<SceneEntity>(entity, out result);

        public static bool TryCast<E>(IEntity entity, out E result) where E : SceneEntity
        {
            if (entity is E sceneEntity)
            {
                result = sceneEntity;
                return true;
            }

            if (entity is SceneEntityProxy<E> proxy)
            {
                result = proxy.Source;
                return true;
            }

            result = null;
            return false;
        }

        /// <summary>
        /// Installs all <see cref="SceneEntity"/> instances found in the given scene that are not yet installed.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallAll(Scene scene) => InstallAll<SceneEntity>(scene);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void InstallAll<E>(Scene scene) where E : SceneEntity
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, gameObjectCount = gameObjects.Length; g < gameObjectCount; g++)
            {
                GameObject gameObject = gameObjects[g];
                E[] entities = gameObject.GetComponentsInChildren<E>();
                for (int e = 0, entityCount = entities.Length; e < entityCount; e++)
                {
                    E entity = entities[e];
                    if (!entity.Installed)
                        entity.Install();
                }
            }
        }
    }
}
#endif