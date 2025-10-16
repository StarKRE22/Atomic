using System;
using System.Text;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides a stateless algorithm for converting entity names into 32-bit FNV-1a hash IDs.
    /// </summary>
    /// <remarks>
    /// <para>
    /// This algorithm computes a 32-bit FNV-1a hash for each entity name.
    /// It is stateless and does not store any mappings, so reverse lookup is not possible.
    /// </para>
    /// <para>
    /// The same entity name will always produce the same ID.
    /// </para>
    /// </remarks>
    public sealed class Fnv1AEntityNameAlgorithm : IEntityNameAlgorithm
    {
        private const uint FnvPrime = 16777619;
        private const uint OffsetBasis = 2166136261;

        /// <summary>
        /// Converts a string entity name into a deterministic 32-bit FNV-1a hash ID.
        /// </summary>
        /// <param name="name">The entity name to convert. Must not be <c>null</c>.</param>
        /// <returns>A 32-bit integer corresponding to the FNV-1a hash of the name.</returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <c>null</c>.</exception>
        public int NameToId(string name)
        {
            uint hash = OffsetBasis;
            byte[] data = Encoding.UTF8.GetBytes(name);

            for (int i = 0, length = data.Length; i < length; i++)
            {
                hash ^= data[i];
                hash *= FnvPrime;
            }

            return unchecked((int) hash);
        }

        /// <summary>
        /// Resets the internal state of the algorithm.
        /// </summary>
        /// <remarks>
        /// This algorithm is stateless; this method performs no operations.
        /// </remarks>
        public void Reset()
        {
            // Stateless algorithm — nothing to reset
        }
    }
}