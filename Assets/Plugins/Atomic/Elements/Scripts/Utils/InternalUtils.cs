using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    /// <summary>
    /// Provides internal utility methods for delegate cleanup and common algorithmic operations.
    /// </summary>
    internal static class InternalUtils
    {
        internal static readonly int[] PrimeTable =
        {
            2, 3, 7, 17, 29, 53, 97, 193, 389, 769, 1543, 3079,
            6151, 12289, 24593, 49157, 98317, 196613, 393241, 786433,
            1572869, 3145739, 6291469, 12582917, 25165843, 50331653,
            100663319, 201326611, 402653189, 805306457, 1610612741
        };

        internal static int CeilToPrime(int value, out int index)
        {
            index = Array.BinarySearch(PrimeTable, value);
            if (index >= 0)
                return PrimeTable[index];

            index = ~index;
            if (index < PrimeTable.Length)
                return PrimeTable[index];

            throw new InvalidOperationException($"Prime can't get larger than {PrimeTable.Length - 1}");
        }

        // /// <summary>
        // /// Calculates the next prime number greater than the specified value.
        // /// </summary>
        // /// <param name="number">The starting number to test.</param>
        // /// <returns>The next prime number greater than <paramref name="number"/>.</returns>
        // internal static int GetPrime(int number)
        // {
        //     if (number < 2)
        //         return 2;
        //
        //     do number++;
        //     while (!IsPrime(number));
        //     return number;
        // }
        //
        // /// <summary>
        // /// Determines whether a number is a prime number.
        // /// </summary>
        // /// <param name="number">The number to check.</param>
        // /// <returns><c>true</c> if the number is prime; otherwise, <c>false</c>.</returns>
        // internal static bool IsPrime(int number)
        // {
        //     if (number is >= 0 and <= 3)
        //         return true;
        //
        //     if (number % 2 == 0 || number % 3 == 0)
        //         return false;
        //
        //     for (int i = 5; i * i <= number; i += 2)
        //         if (number % i == 0)
        //             return false;
        //
        //     return true;
        // }

        /// <summary>
        /// Checks if a collection contains a specific item using a custom equality comparer.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="other">The collection to search.</param>
        /// <param name="item">The item to look for.</param>
        /// <param name="comparer">The equality comparer to use for comparison.</param>
        /// <returns><c>true</c> if the item is found; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool Contains<T>(IEnumerable<T> other, T item, IEqualityComparer<T> comparer)
        {
            foreach (T o in other)
                if (comparer.Equals(o, item))
                    return true;

            return false;
        }
    }
}