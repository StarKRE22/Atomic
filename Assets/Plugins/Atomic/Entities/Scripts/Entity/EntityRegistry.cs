using System;
using System.Collections.Generic;
using UnityEditor;

namespace Atomic.Entities
{
    public static class EntityRegistry<E> where E : IEntity<E>
    {
        private const int UNDEFINED_INDEX = -1;

        private static readonly Dictionary<int, IEntity<E>> s_entities = new();
        private static int s_maxId = -1;
        
        internal static int NextId(IEntity<E> entity)
        {
            do s_maxId++;
            while (s_entities.ContainsKey(s_maxId));

            s_entities[s_maxId] = entity;
            return s_maxId;
        }

        internal static void ChangeId(IEntity<E> entity, int previousId, int newId)
        {
            s_entities.Remove(previousId);
            s_entities[newId] = entity;
        }
        
        /// <summary>
        /// Finds an entity by its ID.
        /// </summary>
        /// <param name="id">The entity ID.</param>
        /// <param name="entity">The found entity, if any.</param>
        /// <returns>True if the entity exists; otherwise, false.</returns>
        public static bool Find(int id, out IEntity<E> entity) => s_entities.TryGetValue(id, out entity);

        /// <summary>
        /// Resets all static entity tracking information (used internally on play mode enter).
        /// </summary>
#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
#endif
        public static void ResetAll()
        {
            s_maxId = -1;
            s_entities.Clear();
        }
    }
}