using System.Collections.Generic;
using UnityEngine;

namespace Atomic.Entities
{
    public static partial class Extensions
    {
  
        
        public static E CreateEntity<E>(
            this IEntityWorld<E> world,
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
            this IEntityWorld<E> world,
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