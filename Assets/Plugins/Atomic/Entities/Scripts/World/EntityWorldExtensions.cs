using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public static class EntityWorldExtensions
    {
        public static void AddEntities(this IEntityWorld it, params IEntity[] entities)
        {
            for (int i = 0, count = entities.Length; i < count; i++)
                it.Add(entities[i]);
        }

        public static void AddEntities(this IEntityWorld it, in IEnumerable<IEntity> entities)
        {
            foreach (IEntity entity in entities)
                it.Add(entity);
        }
        
        public static SceneEntity CreateEntity(
            this IEntityWorld world,
            in SceneEntity prefab,
            in Vector3 position,
            in Quaternion rotation,
            in Transform parent = null,
            in int id = -1
        )
        {
            SceneEntity entity = SceneEntity.Create(prefab, position, rotation, parent);
            if (id >= 0) entity.Id = id;
            world.Add(entity);
            return entity;
        }

        public static void DestroyEntity(
            this IEntityWorld world,
            in SceneEntity entity,
            in float delay = 0
        )
        {
            if (world.Del(entity)) 
                SceneEntity.Destroy(entity.gameObject, delay);
        }
        
        public static void DestroyEntity(
            this IEntityWorld world,
            in IEntity entity,
            in float delay = 0
        )
        {
            if (world.Del(entity)) 
                SceneEntity.Destroy(entity, delay);
        }
    }
}