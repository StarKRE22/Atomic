using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public static class EntityWorldExtensions
    {
        public static void AddEntities(this IEntityWorld it, params IEntity[] entities)
        {
            for (int i = 0, count = entities.Length; i < count; i++)
            {
                it.AddEntity(entities[i]);
            }
        }

        public static void AddEntities(this IEntityWorld it, in IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                it.AddEntity(entity);
            }
        }

        public static void AddAllEntitiesFromScene(this IEntityWorld it, in bool includeInactive = true)
        {
            IEnumerable<IEntity> sceneEntities = GameObject.FindObjectsOfType<SceneEntity>(includeInactive);
            it.AddEntities(sceneEntities);
        }

        public static SceneEntity InstantiateEntity(
            this IEntityWorld world,
            in SceneEntity prefab,
            in Vector3 position,
            in Quaternion rotation,
            in Transform parent,
            bool init = true,
            bool enabled = true
        )
        {
            SceneEntity entity = GameObject.Instantiate(prefab, position, rotation, parent);
            
            if (!entity.Installed) entity.Install();
            if (init) entity.Init();
            if (enabled) entity.Enable();

            world.AddEntity(entity);
            
            return entity;
        }

        public static void DestroyEntity(
            this IEntityWorld world,
            in SceneEntity entity,
            in float delay = 0,
            in bool destroyAsGameObject = true
        )
        {
            if (entity == null)
            {
                return;
            }
            
            if (!world.DelEntity(entity))
            {
                return;
            }

            if (entity.Enabled)
            {
                entity.Disable();
            }

            if (entity.Initialized)
            {
                entity.Dispose();
            }
            
            if (destroyAsGameObject)
            {
                GameObject.Destroy(entity.gameObject, delay);
            }
            else
            {
                Object.Destroy(entity, delay);
            }
        }
    }
}