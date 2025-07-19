using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public static partial class Extensions
    {
        public static void AddEntities<E>(this IWorld<E> it, params E[] entities) where E : IEntity<E>
        {
            for (int i = 0, count = entities.Length; i < count; i++)
                it.Add(entities[i]);
        }

        public static void AddEntities<E>(this IWorld<E> it, IEnumerable<E> entities) where E : IEntity<E>
        {
            foreach (E entity in entities)
                if (entity != null)
                    it.Add(entity);
        }
        
        public static E CreateEntity<E>(
            this IWorld<E> world,
            E prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null
        ) where E : SceneEntity<E>
        {
            E entity = SceneEntity<E>.Create(prefab, position, rotation, parent);
            world.Add(entity);
            return entity;
        }

        public static void DestroyEntity<E>(
            this IWorld<E> world,
            E entity,
            float delay = 0
        )  where E : SceneEntity<E>
        {
            if (!world.Del(entity))
                return;
            
            GameObject.Destroy(entity, delay);
        }
    }
}