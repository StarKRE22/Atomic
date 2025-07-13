using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Entities
{
    internal sealed class AtomicHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Unsubscribe<T>(ref T del) where T : Delegate
        {
            if (del == null)
                return;

            Delegate[] delegates = del.GetInvocationList();
            foreach (Delegate value in delegates)
                del = (T) Delegate.Remove(del, value);

            del = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Unsubscribe(ref Action action)
        {
            if (action == null)
                return;

            Delegate[] delegates = action.GetInvocationList();
            for (int i = 0, count = delegates.Length; i < count; i++)
                action -= (Action) delegates[i];

            action = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Unsubscribe<T>(ref Action<T> action)
        {
            if (action == null)
                return;

            Delegate[] delegates = action.GetInvocationList();
            for (int i = 0, count = delegates.Length; i < count; i++)
                action -= (Action<T>) delegates[i];

            action = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static int GetPrime(int number)
        {
            if (number < 0)
                throw new ArgumentOutOfRangeException(nameof(number));

            if (number <= 3)
                return number;
            
            while (!IsPrime(in number)) 
                number++;
            
            return number;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static bool IsPrime(in int number)
        {
            if (number % 2 == 0 || number % 3 == 0)
                return false;

            for (int i = 5; i * i <= number; i += 2)
                if (number % i == 0)
                    return false;

            return true;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Add<T>(ref T[] array, ref int count, in T item)
        {
            array ??= new T[1];
            
            if (count == array.Length)
                Expand(ref array);

            array[count] = item;
            count++;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Expand<T>(ref T[] array)
        {
            int capacity = array.Length;
            int newCapacity = capacity == 0 ? 1 : capacity * 2;

            if ((uint) newCapacity > int.MaxValue)
                newCapacity = int.MaxValue;

            Array.Resize(ref array, newCapacity);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static bool Remove<T>(ref T[] array, ref int count, in T item, in IEqualityComparer<T> comparer)
        {
            if (count == 0)
                return false;

            for (int i = 0; i < count; i++)
            {
                if (!comparer.Equals(array[i], item))
                    continue;

                count--;

                //Shift left:
                for (int j = i; j < count; j++)
                    array[j] = array[j + 1];
                
                return true;
            }

            return false;
        }
    }
}