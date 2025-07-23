using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    /// <summary>
    /// Contains low-level utility methods for internal use within the Atomic.Entities framework.
    /// Includes optimized helpers for delegate management, primitive operations, and array manipulation.
    /// </summary>
    internal static class InternalUtils
    {
        /// <summary>
        /// Checks whether a object is marked to support edit mode lifecycle.
        /// </summary>
        public static bool IsExecuteInEditModeDefined(object obj) =>
            obj.GetType().IsDefined(typeof(ExecuteInEditModeAttribute));

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
        private static bool IsPrime(int number)
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
        internal static bool AddIfAbsent<T>(ref T[] array, ref int count, T item, IEqualityComparer<T> comparer)
        {
            array ??= new T[1];

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
        internal static bool Contains<T>(T[] array, T item, IEqualityComparer<T> comparer)
        {
            if (array == null)
                return false;

            for (int i = 0, count = array.Length; i < count; i++)
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