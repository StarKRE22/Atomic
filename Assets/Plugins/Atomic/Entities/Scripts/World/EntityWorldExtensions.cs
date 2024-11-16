using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public static class EntityWorldExtensions
    {
        public static void AddEntities(this IEntityWorld it, params IEntity[] entities)
        {
            for (int i = 0, count = entities.Length; i < count; i++) 
                it.AddEntity(entities[i]);
        }

        public static void AddEntities(this IEntityWorld it, in IEnumerable<IEntity> entities)
        {
            foreach (IEntity entity in entities) 
                it.AddEntity(entity);
        }

        public static void AddAllEntitiesFromScene(this IEntityWorld it, in bool includeInactive = true)
        {
            IEnumerable<IEntity> sceneEntities = GameObject.FindObjectsOfType<SceneEntity>(includeInactive);
            it.AddEntities(in sceneEntities);
        }

        public static SceneEntity InstantiateEntity(
            this IEntityWorld world,
            in SceneEntity prefab,
            in Vector3 position,
            in Quaternion rotation,
            in Transform parent,
            in bool init = true,
            in bool enabled = true
        )
        {
            SceneEntity entity = GameObject.Instantiate(prefab, position, rotation, parent);
            world.AttachEntity(in entity, in init, in enabled);
            return entity;
        }

        public static void AttachEntity(this IEntityWorld world,
            in SceneEntity entity,
            in bool init = true,
            in bool enabled = true)
        {
            if (!entity.Installed) entity.Install();
            if (init) entity.Init();
            if (enabled) entity.Enable();
            world.AddEntity(entity);
        }

        public static bool DetachEntity(this IEntityWorld world, in IEntity entity)
        {
            if (entity == null)
                return false;

            if (!world.DelEntity(entity))
                return false;

            if (entity.Enabled) entity.Disable();
            if (entity.Initialized) entity.Dispose();
            return true;
        }

        public static void DestroyEntity(
            this IEntityWorld world,
            in SceneEntity entity,
            in float delay = 0,
            in bool destroyGO = true
        )
        {
            world.DetachEntity(entity);

            if (destroyGO)
                GameObject.Destroy(entity.gameObject, delay);
            else
                Object.Destroy(entity, delay);
        }
    }
}