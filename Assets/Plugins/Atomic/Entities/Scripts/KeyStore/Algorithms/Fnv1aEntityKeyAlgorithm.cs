
using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Implements deterministic FNV-1a 32-bit hashing
    /// for converting entity names into integer identifiers.
    /// </summary>
    public sealed class Fnv1aEntityKeyAlgorithm : IEntityKeyAlgorithm
    {
        private const uint OffsetBasis = 2166136261;
        private const uint Prime = 16777619;

        public int NameToId(string name)
        {
            if (name == null)
                throw new ArgumentNullException(nameof(name));

            unchecked
            {
                uint hash = OffsetBasis;

                foreach (char c in name)
                {
                    hash ^= (byte)c;
                    hash *= Prime;

                    hash ^= (byte)(c >> 8);
                    hash *= Prime;
                }

                return (int)hash;
            }
        }

        public void Reset()
        {
            // Stateless algorithm. Nothing to reset.
        }
    }
}
