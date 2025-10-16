using System;
using System.Security.Cryptography;
using System.Text;

namespace Atomic.Entities
{
    /// <summary>
    /// Provides a hash-based algorithm for converting entity names into integer IDs using SHA-256.
    /// </summary>
    public sealed class SHA256EntityNameAlgorithm : IEntityNameAlgorithm
    {
        private static readonly SHA256 _sha256 = SHA256.Create();

        /// <summary>
        /// Converts a string entity name into a deterministic integer ID.
        /// </summary>
        /// <param name="name">The entity name to convert. Must not be <c>null</c>.</param>
        /// <returns>
        /// A 32-bit integer derived from the SHA-256 hash of the entity name.
        /// </returns>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="name"/> is <c>null</c>.</exception>
        public int NameToId(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            byte[] bytes = Encoding.UTF8.GetBytes(name);
            byte[] hash = _sha256.ComputeHash(bytes);
            return BitConverter.ToInt32(hash, 0);
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