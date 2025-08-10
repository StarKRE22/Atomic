using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Atomic.Entities
{
    /// <summary>
    /// Contains low-level utility methods for internal use within the Atomic.Entities framework.
    /// Includes optimized helpers for delegate management, primitive operations, and array manipulation.
    /// </summary>
    public static class EntityUtils
    {
        /// <summary>
        /// Returns <c>true</c> if the application is in Play Mode.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsPlayMode()
        {
#if UNITY_EDITOR
            return EditorApplication.isPlaying;
#else
            return true;
#endif
        }

        /// <summary>
        /// Returns <c>true</c> if the application is in Edit Mode and not compiling.
        /// </summary>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static bool IsEditMode()
        {
#if UNITY_EDITOR
            return !EditorApplication.isPlaying && !EditorApplication.isCompiling;
#else
            return false;
#endif
        }        
        
        /// <summary>
        /// Checks whether a object is marked to support edit mode lifecycle.
        /// </summary>
        internal static bool IsRunInEditModeDefined(object obj) =>
            obj.GetType().IsDefined(typeof(RunInEditModeAttribute));

        /// <summary>
        /// Computes the next prime number greater than or equal to the specified value.
        /// </summary>
        /// <param name="number">The number to evaluate.</param>
        /// <returns>A prime number greater than or equal to <paramref name="number"/>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int GetPrime(int number)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(nameof(number));

            if (number == 0)
                return 1;

            if (number <= 3)
                return number;

            while (!IsPrime(number))
                number++;

            return number;
        }

        /// <summary>
        /// Determines whether the given number is prime.
        /// </summary>
        /// <param name="number">The number to test.</param>
        /// <returns><c>true</c> if the number is prime; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool IsPrime(int number)
        {
            if (number % 2 == 0 || number % 3 == 0)
                return false;

            for (int i = 5; i * i <= number; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        /// <summary>
        /// Adds an item to a dynamically managed array, expanding it if needed.
        /// </summary>
        /// <typeparam name="T">The type of items in the array.</typeparam>
        /// <param name="array">The array reference to add to.</param>
        /// <param name="count">The current item count.</param>
        /// <param name="item">The item to add.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Add<T>(ref T[] array, ref int count, T item)
        {
            array ??= new T[1];

            if (count == array.Length)
                Expand(ref array);

            array[count] = item;
            count++;
        }

        /// <summary>
        /// Adds an item to the array only if it is not already present.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="array">The array reference to modify.</param>
        /// <param name="count">The number of items currently in the array.</param>
        /// <param name="item">The item to add.</param>
        /// <param name="comparer">An equality comparer for item comparison.</param>
        /// <returns><c>true</c> if the item was added; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool AddIfAbsent<T>(ref T[] array, ref int count, T item, IEqualityComparer<T> comparer,
            int initialCapacity = 1)
        {
            array ??= new T[initialCapacity];

            for (int i = 0; i < count; i++)
                if (comparer.Equals(array[i], item))
                    return false;

            if (count == array.Length)
                Expand(ref array);

            array[count++] = item;
            return true;
        }

        /// <summary>
        /// Determines whether the array contains the specified item.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="array">The array to search.</param>
        /// <param name="item">The item to search for.</param>
        /// <param name="comparer">An equality comparer.</param>
        /// <returns><c>true</c> if the item exists; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool Contains<T>(T[] array, T item, int count, IEqualityComparer<T> comparer)
        {
            if (array == null)
                return false;

            for (int i = 0; i < count; i++)
                if (comparer.Equals(array[i], item))
                    return true;

            return false;
        }

        /// <summary>
        /// Doubles the size of the array to accommodate more items.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="array">The array to expand.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Expand<T>(ref T[] array)
        {
            int capacity = array.Length;
            int newCapacity = capacity == 0 ? 1 : capacity * 2;

            if ((uint) newCapacity > int.MaxValue)
                newCapacity = int.MaxValue;

            Array.Resize(ref array, newCapacity);
        }

        /// <summary>
        /// Removes an item from the array, shifting subsequent elements to the left.
        /// </summary>
        /// <typeparam name="T">The item type.</typeparam>
        /// <param name="array">The array from which to remove.</param>
        /// <param name="count">Reference to the current count of valid items.</param>
        /// <param name="item">The item to remove.</param>
        /// <param name="comparer">An equality comparer.</param>
        /// <returns><c>true</c> if the item was removed; otherwise, <c>false</c>.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool Remove<T>(ref T[] array, ref int count, T item, IEqualityComparer<T> comparer)
        {
            if (count == 0)
                return false;

            for (int i = 0; i < count; i++)
            {
                if (!comparer.Equals(array[i], item))
                    continue;

                count--;

                for (int j = i; j < count; j++)
                    array[j] = array[j + 1];

                return true;
            }

            return false;
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool FindIndex<T>(T[] array, int count, T item, IEqualityComparer<T> comparer, out int i)
        {
            for (i = 0; i < count; i++)
                if (comparer.Equals(array[i], item))
                    return true;

            i = -1;
            return false; // если не найден
        }
        
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool RemoveAt<T>(ref T[] array, ref int count, int index)
        {
            if (index < 0 || index >= count)
                return false;

            count--;

            for (int i = index; i < count; i++)
                array[i] = array[i + 1];

            array[count] = default;
            return true;
        }

        /// <summary>
        /// Computes a 32-bit FNV-1a hash for the specified input string.
        /// </summary>
        /// <remarks>
        /// This implementation uses the Fowler–Noll–Vo hash function (FNV-1a), which offers good distribution
        /// and performance for hashing text. It is not cryptographically secure and is intended for use cases
        /// such as dictionaries, hash sets, quick comparisons, and ID generation.
        /// </remarks>
        /// <param name="input">The input string to hash. Must not be null.</param>
        /// <returns>
        /// A 32-bit signed integer hash representing the input string.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="input"/> is <c>null</c>.
        /// </exception>
        public static int StrongFnv1aHash(string input)
        {
            unchecked
            {
                const uint fnvOffsetBasis = 2166136261;
                const uint fnvPrime = 16777619;
                uint hash = fnvOffsetBasis;

                for (int i = 0, count = input.Length; i < count; i++)
                {
                    hash ^= input[i];
                    hash *= fnvPrime;
                }

                return (int) hash;
            }
        }
    }
}