using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides static factory and utility methods for working with <see cref="SceneEntity"/>.
    /// Includes creation, casting, and batch installation logic.
    /// </summary>
    public partial class SceneEntity<E>
    {
        /// <summary>
        /// Creates a new <see cref="SceneEntity"/> GameObject and configures it with optional tags, values, and behaviours.
        /// </summary>
        public static SceneEntity<E> Create(
            string name = null,
            IEnumerable<int> tags = null,
            IReadOnlyDictionary<int, object> values = null,
            IEnumerable<IBehaviour<E>> behaviours = null
        )
        {
            GameObject gameObject = new GameObject(name);
            SceneEntity<E> sceneEntity = gameObject.AddComponent<SceneEntity<E>>();
            sceneEntity.Name = name;

            sceneEntity.AddTags(tags);
            sceneEntity.AddValues(values);
            sceneEntity.AddBehaviours(behaviours);

            sceneEntity.Install();
            return sceneEntity;
        }

        /// <summary>
        /// Instantiates a prefab and installs the resulting <see cref="SceneEntity"/> under the specified parent.
        /// </summary>
        public static SceneEntity<E> Create(SceneEntity<E> prefab, Transform parent)
        {
            SceneEntity<E> entity = Instantiate(prefab, parent);
            entity.Install();
            return entity;
        }

        /// <summary>
        /// Instantiates a prefab at the given position and rotation with optional parent, then installs it.
        /// </summary>
        public static SceneEntity<E> Create(
            SceneEntity<E> prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null
        )
        {
            SceneEntity<E> entity = Instantiate(prefab, position, rotation, parent);
            entity.Install();
            return entity;
        }

        /// <summary>
        /// Destroys the associated GameObject of a given <see cref="IEntity"/> if it is a <see cref="SceneEntity"/>.
        /// </summary>
        public static void Destroy(SceneEntity<E> entity, float t = 0)
        {
            if (TryCast(entity, out SceneEntity<E> sceneEntity))
                Destroy(sceneEntity.gameObject, t);
        }

        /// <summary>
        /// Casts the <see cref="IEntity"/> to a <see cref="SceneEntity"/> if possible.
        /// </summary>
        public static SceneEntity<E> Cast(IEntity<E> entity)
        {
            return entity switch
            {
                null => null,
                SceneEntity<E> sceneEntity => sceneEntity,
                SceneEntityProxy sceneEntityProxy => sceneEntityProxy.Source,
                _ => throw new Exception("Can't convert entity to SceneEntity")
            };
        }

        /// <summary>
        /// Attempts to cast the <see cref="IEntity"/> to a <see cref="SceneEntity"/>.
        /// </summary>
        public static bool TryCast(IEntity<E> entity, out SceneEntity<E> result)
        {
            if (entity is SceneEntity<E> sceneEntity)
            {
                result = sceneEntity;
                return true;
            }

            if (entity is SceneEntityProxy proxy)
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
        public static void InstallAll(Scene scene)
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, gameObjectCount = gameObjects.Length; g < gameObjectCount; g++)
            {
                GameObject gameObject = gameObjects[g];
                SceneEntity<E>[] entities = gameObject.GetComponentsInChildren<SceneEntity<E>>();
                for (int e = 0, entityCount = entities.Length; e < entityCount; e++)
                {
                    SceneEntity<E> entity = entities[e];
                    if (!entity.Installed)
                        entity.Install();
                }
            }
        }
    }
}