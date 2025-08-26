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
            return index < PrimeTable.Length
                ? PrimeTable[index]
                : throw new InvalidOperationException($"Prime can't get larger than {PrimeTable.Length - 1}");
        }

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
    }
}