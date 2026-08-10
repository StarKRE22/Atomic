using System;
using System.Runtime.CompilerServices;

namespace Atomic.Events
{
    /// <summary>
    /// Contains low-level utility methods for internal use within the Atomic.Events framework.
    /// </summary>
    internal static class InternalUtils
    {
        /// <summary>
        /// Predefined table of prime numbers used for sizing collections or other internal computations.
        /// </summary>
        internal static readonly int[] PrimeTable =
        {
            2, 3, 7, 17, 29, 53, 97, 193, 389, 769, 1543, 3079,
            6151, 12289, 24593, 49157, 98317, 196613, 393241, 786433,
            1572869, 3145739, 6291469, 12582917, 25165843, 50331653,
            100663319, 201326611, 402653189, 805306457, 1610612741
        };

        /// <summary>
        /// Returns the smallest prime number from <see cref="PrimeTable"/> that is greater than or equal to the specified value.
        /// </summary>
        /// <param name="value">The value to compare against the prime table.</param>
        /// <param name="index">
        /// When this method returns, contains the index of the prime number in <see cref="PrimeTable"/>.
        /// </param>
        /// <returns>The smallest prime number greater than or equal to <paramref name="value"/>.</returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown if the requested prime would exceed the range of the <see cref="PrimeTable"/>.
        /// </exception>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int CeilToPrime(int value, out int index)
        {
            index = Array.BinarySearch(PrimeTable, value);
            if (index >= 0)
                return PrimeTable[index];

            index = ~index;
            return index < PrimeTable.Length
                ? PrimeTable[index]
                : throw new InvalidOperationException($"Prime can't get larger than {PrimeTable.Length - 1}");
        }
    }
}
