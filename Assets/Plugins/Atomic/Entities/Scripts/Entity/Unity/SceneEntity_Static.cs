using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Atomic.Entities
{
    public partial class SceneEntity
    {
        private static readonly Dictionary<IEntity, SceneEntity> s_sceneEntities = new();

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

        public static SceneEntity Create(in SceneEntity prefab, in Transform parent)
        {
            SceneEntity entity = Instantiate(prefab, parent);
            entity.Install();
            return entity;
        }

        public static SceneEntity Create(
            in SceneEntity prefab,
            in Vector3 position,
            in Quaternion rotation,
            in Transform parent = null
        )
        {
            SceneEntity entity = Instantiate(prefab, position, rotation, parent);
            entity.Install();
            return entity;
        }

        public static void Destroy(IEntity entity, float t = 0)
        {
            if (TryCast(entity, out SceneEntity sceneEntity))
                Destroy(sceneEntity.gameObject, t);
        }

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

#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
        private static void OnEnterPlayMode()
        {
            s_sceneEntities.Clear();
        }
#endif


        public static void InstallAll(Scene scene)
        {
            foreach (GameObject gameObject in scene.GetRootGameObjects())
            foreach (SceneEntity entity in gameObject.GetComponentsInChildren<SceneEntity>())
                if (!entity.Installed)
                    entity.Install();
        }
    }
}