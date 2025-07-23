using System.Collections.Generic;
using System.Runtime.CompilerServices;

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
    }
}