#if UNITY_5_3_OR_NEWER
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
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
        /// <summary>
        /// Creates a new <see cref="SceneEntity"/> GameObject and configures it with optional tags, values, and behaviours.
        /// </summary>
        public static SceneEntity Create(
            string name = null,
            IEnumerable<int> tags = null,
            IReadOnlyDictionary<int, object> values = null,
            IEnumerable<IEntityBehaviour> behaviours = null
        )
        {
            GameObject gameObject = new GameObject(name);
            SceneEntity sceneEntity = gameObject.AddComponent<SceneEntity>();

            sceneEntity.Name = name;
            sceneEntity.AddTags(tags);
            sceneEntity.AddValues(values);
            sceneEntity.AddBehaviours(behaviours);
            return sceneEntity;
        }
        
        public static E Create<E>(
            string name = null,
            IEnumerable<int> tags = null,
            IReadOnlyDictionary<int, object> values = null,
            IEnumerable<IEntityBehaviour> behaviours = null
        ) where E : SceneEntity
        {
            GameObject gameObject = new GameObject(name);
            E sceneEntity = gameObject.AddComponent<E>();
            
            sceneEntity.Name = name;
            sceneEntity.AddTags(tags);
            sceneEntity.AddValues(values);
            sceneEntity.AddBehaviours(behaviours);
            return sceneEntity;
        }

        /// <summary>
        /// Instantiates a prefab and installs the resulting <see cref="SceneEntity"/> under the specified parent.
        /// </summary>
        public static SceneEntity Create(SceneEntity prefab, Transform parent)
        {
            SceneEntity entity = Instantiate(prefab, parent);
            entity.Install();
            return entity;
        }
        
        public static E Create<E>(E prefab, Transform parent) where E : SceneEntity
        {
            E entity = Instantiate(prefab, parent);
            entity.Install();
            return entity;
        }

        /// <summary>
        /// Instantiates a prefab at the given position and rotation with optional parent, then installs it.
        /// </summary>
        public static SceneEntity Create(
            SceneEntity prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null
        )
        {
            SceneEntity entity = Instantiate(prefab, position, rotation, parent);
            entity.Install();
            return entity;
        }
        
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
            if (TryCast(entity, out SceneEntity sceneEntity))
                Destroy(sceneEntity.gameObject, t);
        }

        /// <summary>
        /// Casts the <see cref="IEntity"/> to a <see cref="SceneEntity"/> if possible.
        /// </summary>
        public static SceneEntity Cast(IEntity entity) => entity switch
        {
            null => null,
            SceneEntity sceneEntity => sceneEntity,
            SceneEntityProxy proxy => proxy.Source,
            _ => throw new InvalidCastException($"Can't cast {entity.Name} to {nameof(SceneEntity)}")
        };
        
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
        public static bool TryCast(IEntity entity, out SceneEntity result)
        {
            if (entity is SceneEntity sceneEntity)
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
        public static void InstallAll(Scene scene)
        {
            GameObject[] gameObjects = scene.GetRootGameObjects();
            for (int g = 0, gameObjectCount = gameObjects.Length; g < gameObjectCount; g++)
            {
                GameObject gameObject = gameObjects[g];
                SceneEntity[] entities = gameObject.GetComponentsInChildren<SceneEntity>();
                for (int e = 0, entityCount = entities.Length; e < entityCount; e++)
                {
                    SceneEntity entity = entities[e];
                    if (!entity.Installed)
                        entity.Install();
                }
            }
        }
        
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