#if UNITY_5_3_OR_NEWER
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Utility class for converting between entity names and their corresponding hashed identifiers.
    /// Useful for mapping string names to unique integer IDs and vice versa.
    /// </summary>
    public static class EntityAPIUtils
    {
        /// <summary>
        /// Converts a string name into a hashed integer ID.
        /// </summary>
        /// <param name="name">The string name to convert.</param>
        /// <returns>An integer hash code representing the given name.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int NameToId(in string name) => new PropertyName(name).GetHashCode();

        /// <summary>
        /// For debugging purposes only. Retrieves the base string name from a hashed ID.
        /// Strips any extra data after a colon character.
        /// </summary>
        /// <param name="id">The hashed identifier.</param>
        /// <returns>The base name associated with the ID.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string IdToName(in int id) => IdToFullName(id).Split(':')[0];

        /// <summary>
        /// For debugging purposes only. Retrieves the full string representation of a hashed ID.
        /// May include additional contextual information after a colon.
        /// </summary>
        /// <param name="id">The hashed identifier.</param>
        /// <returns>The full name associated with the ID.</returns>
        public static string IdToFullName(in int id) => new PropertyName(id).ToString();
    }
}
#endif