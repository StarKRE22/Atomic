using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Entities
{
    public static partial class Extensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<E>(this IEntityCollection<E> it, params E[] entities) where E : IEntity
        {
            for (int i = 0, count = entities.Length; i < count; i++)
                it.Add(entities[i]);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddRange<E>(this IEntityCollection<E> it, IEnumerable<E> entities) where E : IEntity
        {
            foreach (E entity in entities)
                if (entity != null)
                    it.Add(entity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static E CreateEntity<E>(
            this IEntityCollection<E> it,
            E prefab,
            Vector3 position,
            Quaternion rotation,
            Transform parent = null
        ) where E : SceneEntity
        {
            E entity = SceneEntity.Create(prefab, position, rotation, parent);
            it.Add(entity);
            return entity;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DestroyEntity<E>(this IEntityCollection<E> it, E entity, float delay = 0)
            where E : SceneEntity
        {
            if (it.Remove(entity))
                GameObject.Destroy(entity, delay);
        }
    }
}