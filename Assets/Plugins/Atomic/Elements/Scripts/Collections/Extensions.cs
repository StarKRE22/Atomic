using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Atomic.Elements
{
    public partial class Extensions
    {
        public static void CopyFrom<T>(this ICollection<T> destination, IEnumerable<T> source)
        {
            destination.Clear();
            foreach (T item in source)
                destination.Add(item);
        }

        public static void CopyTo<T>(this IEnumerable<T> source, ICollection<T> destination)
        {
            destination.Clear();
            foreach (T item in source)
                destination.Add(item);
        }

        public static void AddRange<T>(this ICollection<T> it, params T[] items)
        {
            for (int i = 0, count = items.Length; i < count; i++)
                it.Add(items[i]);
        }

        public static void AddRange<T>(this ICollection<T> it, IEnumerable<T> items)
        {
            foreach (T item in items)
                it.Add(item);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveCollection<T>.StateChangedSubscription SubscribeState<T>(
            this IReadOnlyReactiveCollection<T> it, Action action) => new(it, action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveCollection<T>.ItemAddedSubscription SubscribeAdded<T>(
            this IReadOnlyReactiveCollection<T> it, Action<T> action) => new(it, action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveCollection<T>.ItemRemovedSubscription SubscribeRemoved<T>(
            this IReadOnlyReactiveCollection<T> it, Action<T> action) => new(it, action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveArray<T>.StateChangedSubscription SubscribeState<T>(
            this IReadOnlyReactiveArray<T> it, Action action) => new(it, action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveArray<T>.ItemChangedSubscription SubscribeChanged<T>(
            this IReadOnlyReactiveArray<T> it, Action<int, T> action) => new(it, action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveList<T>.StateChangedSubscription SubscribeState<T>(
            this IReadOnlyReactiveList<T> it, Action action) => new(it, action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveList<T>.ItemAddedSubscription SubscribeAdded<T>(
            this IReadOnlyReactiveList<T> it, Action<int, T> action) => new(it, action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveList<T>.ItemRemovedSubscription SubscribeRemoved<T>(
            this IReadOnlyReactiveList<T> it, Action<int, T> action) => new(it, action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveDictionary<K, V>.StateChangedSubscription SubscribeState<K, V>(
            this IReadOnlyReactiveDictionary<K, V> it, Action action) => new(it, action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveDictionary<K, V>.ItemAddedSubscription SubscribeAdded<K, V>(
            this IReadOnlyReactiveDictionary<K, V> it, Action<K, V> action) => new(it, action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveDictionary<K, V>.ItemRemovedSubscription SubscribeRemoved<K, V>(
            this IReadOnlyReactiveDictionary<K, V> it, Action<K, V> action) => new(it, action);

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static IReadOnlyReactiveDictionary<K, V>.ItemChangedSubscription SubscribeChanged<K, V>(
            this IReadOnlyReactiveDictionary<K, V> it, Action<K, V> action) => new(it, action);

        public static ReactiveDictionary<K, V> ToReactiveDictionary<K, V>(
            this IEnumerable<V> collection,
            Func<V, K> keySelector
        )
        {
            if (collection == null)
                throw new ArgumentNullException(nameof(collection));

            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            
            ReactiveDictionary<K, V> result = new ReactiveDictionary<K, V>();
            foreach (V v in collection)
                result.Add(keySelector.Invoke(v), v);

            return result;
        }
    }
}