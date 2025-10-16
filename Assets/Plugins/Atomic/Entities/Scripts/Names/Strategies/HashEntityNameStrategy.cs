using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides a hash-based implementation of the <see cref="IEntityNameStrategy"/> interface.
    /// </summary>
    /// <remarks>
    /// This strategy generates deterministic integer IDs for entity names using the SHA-256 hash function.
    /// It also caches mappings internally to support reverse lookups between IDs and names.
    /// <para>
    /// The same entity name will always produce the same ID value across sessions,
    /// as long as the hash algorithm and truncation method remain unchanged.
    /// </para>
    /// <para>
    /// Because only the first four bytes of the SHA-256 hash are used to form the integer ID,
    /// there is a very small chance of collisions. However, this is negligible for typical use cases.
    /// </para>
    /// </remarks>
    public sealed class HashEntityNameStrategy : IEntityNameStrategy
    {
        /// <summary>
        /// Stores mappings from entity names to their corresponding hashed integer IDs.
        /// </summary>
        private readonly Dictionary<string, int> _nameToId = new();

        /// <summary>
        /// Stores mappings from hashed integer IDs back to their original entity names.
        /// </summary>
        private readonly Dictionary<int, string> _idToName = new();

        /// <summary>
        /// Converts a string-based entity name into a unique, deterministic integer identifier.
        /// </summary>
        /// <param name="name">The string name to convert to an integer ID.</param>
        /// <returns>
        /// A deterministic integer identifier derived from the SHA-256 hash of the provided name.
        /// If the name was previously converted, the cached ID is returned instead.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <c>null</c>.</exception>
        public int NameToId(string name)
        {
            if (_nameToId.TryGetValue(name, out int id))
                return id;

            using SHA256 sha = SHA256.Create();
            byte[] hash = sha.ComputeHash(Encoding.UTF8.GetBytes(name));
            id = BitConverter.ToInt32(hash, 0);

            _nameToId[name] = id;
            _idToName[id] = name;
            return id;
        }

        /// <summary>
        /// Retrieves the original entity name associated with the specified integer ID.
        /// </summary>
        /// <param name="id">The integer identifier to resolve back to an entity name.</param>
        /// <returns>
        /// The original entity name that corresponds to the given ID.
        /// If the ID has not been registered, returns a fallback string in the format <c>#Unknown:{id}</c>.
        /// </returns>
        public string IdToName(int id)
        {
            return _idToName.TryGetValue(id, out var name) ? name : $"#Unknown:{id}";
        }

        /// <summary>
        /// Clears all cached mappings between entity names and IDs.
        /// </summary>
        /// <remarks>
        /// This operation removes all stored relationships and resets the internal state.
        /// Typically used when resetting an entity system or reloading the scene.
        /// </remarks>
        public void Reset()
        {
            _nameToId.Clear();
            _idToName.Clear();
        }
    }
}