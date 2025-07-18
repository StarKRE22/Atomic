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
        /// <summary>
        /// Safely removes all subscribers from the specified <see cref="Action"/> and sets it to null.
        /// </summary>
        /// <param name="action">The action to clear.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Dispose(ref Action action)
        {
            if (action == null)
                return;

            Delegate[] delegates = action.GetInvocationList();
            for (int i = 0, count = delegates.Length; i < count; i++)
                action -= (Action)delegates[i];

            action = null;
        }

        /// <summary>
        /// Safely removes all subscribers from a delegate of any type and sets it to null.
        /// </summary>
        /// <typeparam name="T">The delegate type.</typeparam>
        /// <param name="del">The delegate to clear.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Dispose<T>(ref T del) where T : Delegate
        {
            if (del == null)
                return;

            Delegate[] delegates = del.GetInvocationList();
            for (int i = 0, count = delegates.Length; i < count; i++)
                del = (T)Delegate.Remove(del, delegates[i]);

            del = null;
        }

        /// <summary>
        /// Safely removes all subscribers from the specified <see cref="Action{T}"/> and sets it to null.
        /// </summary>
        /// <typeparam name="T">The type of the parameter for the action.</typeparam>
        /// <param name="action">The action to clear.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Dispose<T>(ref Action<T> action)
        {
            if (action == null)
                return;

            Delegate[] delegates = action.GetInvocationList();
            for (int i = 0, count = delegates.Length; i < count; i++)
                action -= (Action<T>)delegates[i];

            action = null;
        }

        /// <summary>
        /// Calculates the next prime number greater than the specified value.
        /// </summary>
        /// <param name="number">The starting number to test.</param>
        /// <returns>The next prime number greater than <paramref name="number"/>.</returns>
        internal static int NextPrime(int number)
        {
            if (number < 2)
                return 2;

            do number++;
            while (!IsPrime(number));
            return number;
        }

        /// <summary>
        /// Determines whether a number is a prime number.
        /// </summary>
        /// <param name="number">The number to check.</param>
        /// <returns><c>true</c> if the number is prime; otherwise, <c>false</c>.</returns>
        internal static bool IsPrime(int number)
        {
            if (number is >= 0 and <= 3)
                return true;

            if (number % 2 == 0 || number % 3 == 0)
                return false;

            for (int i = 5; i * i <= number; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        /// <summary>
        /// Checks if a collection contains a specific item using a custom equality comparer.
        /// </summary>
        /// <typeparam name="T">The type of items in the collection.</typeparam>
        /// <param name="other">The collection to search.</param>
        /// <param name="item">The item to look for.</param>
        /// <param name="comparer">The equality comparer to use for comparison.</param>
        /// <returns><c>true</c> if the item is found; otherwise, <c>false</c>.</returns>
        internal static bool Contains<T>(in IEnumerable<T> other, in T item, in IEqualityComparer<T> comparer)
        {
            foreach (T o in other)
                if (comparer.Equals(o, item))
                    return true;

            return false;
        }
    }
}