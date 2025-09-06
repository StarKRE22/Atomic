using System.Collections.Generic;
using System.Runtime.CompilerServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Provides bidirectional mapping between string entity names and unique integer identifiers.
    /// This is useful for efficiently identifying entities at runtime using compact integer IDs,
    /// while still allowing reverse lookup for debugging or editor visualization.
    /// </summary>
    public static class EntityNames
    {
        /// <summary>
        /// Internal dictionary mapping string names to integer IDs.
        /// </summary>
        private static readonly Dictionary<string, int> _nameToId = new();

        /// <summary>
        /// Internal dictionary mapping integer IDs back to string names.
        /// </summary>
        private static readonly Dictionary<int, string> _idToName = new();

        /// <summary>
        /// The next ID to assign to a new name.
        /// </summary>
        private static int _nextId = 1;

        /// <summary>
        /// Converts a string entity name to a unique integer ID.
        /// If the name has already been registered, the existing ID is returned.
        /// Otherwise, a new ID is assigned and stored.
        /// </summary>
        /// <param name="name">The string name to convert to an ID.</param>
        /// <returns>A unique integer identifier corresponding to the provided name.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NameToId(string name)
        {
            if (_nameToId.TryGetValue(name, out int id))
                return id;

            id = _nextId++;
            _nameToId[name] = id;
            _idToName[id] = name;
            return id;
        }

        /// <summary>
        /// Retrieves the original entity name from a given integer ID.
        /// </summary>
        /// <param name="id">The ID to convert back to a string name.</param>
        /// <returns>
        /// The original string name associated with the given ID,
        /// or a fallback string in the format <c>#Unknown:{id}</c> if the ID was not registered.
        /// </returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string IdToName(int id) => _idToName.TryGetValue(id, out var name) ? name : $"#Unknown:{id}";

        /// <summary>
        /// Clears all name-to-ID mappings and resets the ID counter.
        /// Automatically called when entering play mode in the Unity Editor.
        /// </summary>
#if UNITY_EDITOR
        [InitializeOnEnterPlayMode]
#endif
        public static void Clear()
        {
            _nameToId.Clear();
            _idToName.Clear();
            _nextId = 1;
        }
    }
}