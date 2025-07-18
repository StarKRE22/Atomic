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
        /// Stores a mapping between <see cref="IEntity"/> and its corresponding <see cref="SceneEntity"/>.
        /// </summary>
        private static readonly Dictionary<IEntity, SceneEntity> s_sceneEntities = new();

        /// <summary>
        /// Creates a new <see cref="SceneEntity"/> GameObject and configures it with optional tags, values, and behaviours.
        /// </summary>
        public static SceneEntity Create(
            string name = null,
            IEnumerable<int> tags = null,
            IReadOnlyDictionary<int, object> values = null,
            IEnumerable<IBehaviour> behaviours = null
        )
        {
            GameObject gameObject = new GameObject(name);
            SceneEntity sceneEntity = gameObject.AddComponent<SceneEntity>();
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
        public static SceneEntity Create(SceneEntity prefab, Transform parent)
        {
            SceneEntity entity = Instantiate(prefab, parent);
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
        public static SceneEntity Cast(IEntity entity)
        {
            if (entity == null)
                return null;

            if (entity is SceneEntity sceneEntity)
                return sceneEntity;

            if (entity is SceneEntityProxy proxy)
                return proxy._source;

            s_sceneEntities.TryGetValue(entity, out sceneEntity);
            return sceneEntity;
        }

        /// <summary>
        /// Attempts to cast the <see cref="IEntity"/> to a <see cref="SceneEntity"/>.
        /// </summary>
        public static bool TryCast(IEntity entity, out SceneEntity result)
        {
            if (entity == null)
            {
                result = null;
                return false;
            }

            if (entity is SceneEntity sceneEntity)
            {
                result = sceneEntity;
                return true;
            }

            if (entity is SceneEntityProxy proxy)
            {
                result = proxy._source;
                return true;
            }

            return s_sceneEntities.TryGetValue(entity, out result);
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

#if UNITY_EDITOR
        /// <summary>
        /// Clears entity cache when entering Play Mode (Editor only).
        /// </summary>
        [InitializeOnEnterPlayMode]
        private static void OnEnterPlayMode() => s_sceneEntities.Clear();
#endif
    }
}