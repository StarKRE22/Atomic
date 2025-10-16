using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Provides a unified interface for converting between string-based entity names and unique integer identifiers,
    /// with internal caching for fast reverse lookups.
    /// </summary>
    public static class EntityNames
    {
        private static readonly Dictionary<string, int> _nameToId = new();
        private static readonly Dictionary<int, string> _idToName = new();

        private static IEntityNameAlgorithm _algorithm = new SequentialEntityNameAlgorithm();
        
        /// <summary>
        /// Sets the strategy used for generating IDs from entity names.
        /// Clears the current cache.
        /// </summary>
        /// <param name="algorithm">New strategy to use.</param>
        public static void SetAlgorithm(IEntityNameAlgorithm algorithm)
        {
            _algorithm = algorithm ?? throw new ArgumentNullException(nameof(algorithm));
            Reset();
        }

        /// <summary>
        /// Converts a string entity name into a unique integer ID.
        /// </summary>
        /// <param name="name">The entity name.</param>
        /// <returns>A unique integer ID corresponding to the name.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NameToId(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (_nameToId.TryGetValue(name, out int id))
                return id;

            id = _algorithm.NameToId(name);
            
            _nameToId[name] = id;
            _idToName[id] = name;
            return id;
        }

        /// <summary>
        /// Retrieves the original entity name for a given ID.
        /// </summary>
        /// <param name="id">The integer ID.</param>
        /// <returns>The original name if registered; otherwise <c>#Unknown:{id}</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string IdToName(int id) => _idToName.TryGetValue(id, out var name) ? name : $"#Unknown:{id}";

        /// <summary>
        /// Clears all cached mappings and resets the current algorithm.
        /// </summary>
#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
#endif
        public static void Reset()
        {
            _nameToId.Clear();
            _idToName.Clear();
            _algorithm.Reset();
        }
    }
}