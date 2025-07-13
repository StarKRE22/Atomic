using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    internal static partial class AtomicHelper
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Dispose(ref Action action)
        {
            if (action == null)
                return;

            Delegate[] delegates = action.GetInvocationList();
            for (int i = 0, count = delegates.Length; i < count; i++)
                action -= (Action) delegates[i];

            action = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Dispose<T>(ref T del) where T : Delegate
        {
            if (del == null)
                return;

            Delegate[] delegates = del.GetInvocationList();
            foreach (Delegate value in delegates)
                del = (T) Delegate.Remove(del, value);

            del = null;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static void Dispose<T>(ref Action<T> action)
        {
            if (action == null)
                return;

            Delegate[] delegates = action.GetInvocationList();
            foreach (var del in delegates)
                action -= (Action<T>) del;

            action = null;
        }

        //+
        internal static int NextPrime(int number)
        {
            if (number < 2)
                return 2;

            do number++;
            while (!IsPrime(number));
            return number;
        }

        //+
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
        
        internal static bool Contains<T>(in IEnumerable<T> other, in T item, in IEqualityComparer<T> comparer)
        {
            foreach (T o in other)
                if (comparer.Equals(o, item))
                    return true;

            return false;
        }
    }
}