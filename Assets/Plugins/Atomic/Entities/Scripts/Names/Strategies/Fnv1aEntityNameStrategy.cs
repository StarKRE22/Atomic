using System;
using System.Collections.Generic;
using System.Text;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides a hash-based entity name strategy using 32-bit FNV-1a with caching.
    /// </summary>
    /// <remarks>
    /// This strategy computes a 32-bit FNV-1a hash for each entity name and caches
    /// the mappings between names and IDs to allow reverse lookup.
    /// <para>
    /// The same entity name will always produce the same ID within a session.
    /// </para>
    /// </remarks>
    public sealed class Fnv1aEntityNameStrategy : IEntityNameStrategy
    {
        private readonly Dictionary<string, int> _nameToId = new();
        private readonly Dictionary<int, string> _idToName = new();

        private const uint FnvPrime = 16777619;
        private const uint OffsetBasis = 2166136261;

        /// <summary>
        /// Converts a string entity name into a 32-bit FNV-1a hash ID.
        /// If the name was already registered, returns the cached ID.
        /// </summary>
        /// <param name="name">The entity name to convert.</param>
        /// <returns>The deterministic 32-bit ID corresponding to the name.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is null.</exception>
        public int NameToId(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            if (_nameToId.TryGetValue(name, out int cachedId))
                return cachedId;

            uint hash = OffsetBasis;
            byte[] data = Encoding.UTF8.GetBytes(name);

            for (int i = 0, length = data.Length; i < length; i++)
            {
                hash ^= data[i];
                hash *= FnvPrime;
            }

            int id = unchecked((int) hash);

            _nameToId[name] = id;
            _idToName[id] = name;

            return id;
        }

        /// <summary>
        /// Retrieves the original entity name from the cached ID.
        /// </summary>
        /// <param name="id">The integer ID to resolve back to a name.</param>
        /// <returns>
        /// The original entity name if it was previously registered,
        /// or a fallback string in the format <c>#Unknown:{id}</c> otherwise.
        /// </returns>
        public string IdToName(int id)
        {
            return _idToName.TryGetValue(id, out var name) ? name : $"#Unknown:{id}";
        }

        /// <summary>
        /// Clears all cached name-to-ID and ID-to-name mappings.
        /// </summary>
        public void Reset()
        {
            _nameToId.Clear();
            _idToName.Clear();
        }
    }
}