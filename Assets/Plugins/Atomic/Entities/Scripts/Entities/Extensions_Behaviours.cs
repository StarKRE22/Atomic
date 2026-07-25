using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    public static partial class Extensions
    {
        /// <summary>
        /// Adds a behaviour of the specified type to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static T AddBehaviour<T>(this IEntity entity) where T : IEntityBehaviour, new()
        {
            T behaviour = new T();
            entity.AddBehaviour(behaviour);
            return behaviour;
        }

        /// <summary>
        /// Adds a subset of behaviours from an array to the specified entity.
        /// </summary>
        /// <param name="entity">The entity to which behaviours will be added.</param>
        /// <param name="behaviours">An array of behaviours to add. Can be <c>null</c>, in which case nothing is added.</param>
        /// <param name="startIndex">The starting index in the <paramref name="behaviours"/> array.</param>
        /// <param name="count">The number of behaviours to add from <paramref name="startIndex"/>.</param>
        /// <remarks>
        /// This method performs no action if the <paramref name="behaviours"/> array is <c>null</c>.
        /// Behaviours are added in order from <paramref name="startIndex"/> up to <paramref name="startIndex"/> + <paramref name="count"/>.
        /// </remarks>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddBehaviours(this IEntity entity, IEntityBehaviour[] behaviours, int startIndex, int count)
        {
            if (behaviours == null)
                return;

            for (int i = startIndex, end = startIndex + count; i < end; i++)
                entity.AddBehaviour(behaviours[i]);
        }

        /// <summary>
        /// Adds multiple behaviours to the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void AddBehaviours(this IEntity entity, IEnumerable<IEntityBehaviour> behaviours)
        {
            if (behaviours == null)
                return;

            foreach (IEntityBehaviour behaviour in behaviours)
                entity.AddBehaviour(behaviour);
        }

        /// <summary>
        /// Removes multiple behaviours from the entity.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelBehaviours(this IEntity entity, IEnumerable<IEntityBehaviour> behaviours)
        {
            if (behaviours == null)
                return;

            foreach (IEntityBehaviour behaviour in behaviours)
                entity.DelBehaviour(behaviour);
        }

        /// <summary>
        /// Removes multiple behaviours from the entity.
        /// </summary>
        /// <param name="behaviours">An array of behaviours to remove. Can be <c>null</c>, in which case nothing is removed.</param>
        /// <param name="startIndex">The starting index in the <paramref name="behaviours"/> array.</param>
        /// <param name="count">The number of behaviours to remove from <paramref name="startIndex"/>.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void DelBehaviours(this IEntity entity, IEntityBehaviour[] behaviours, int startIndex, int count)
        {
            if (behaviours == null)
                return;

            for (int i = startIndex, end = startIndex + count; i < end; i++)
                entity.DelBehaviour(behaviours[i]);
        }
    }
}