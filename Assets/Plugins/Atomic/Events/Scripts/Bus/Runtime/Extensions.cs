using System;
using Atomic.Events.Atomic.Events;
using static Atomic.Events.EventKeyStore;

namespace Atomic.Events
{
    public static class Extensions
    {
        public static Subscription Subscribe(this IEventBus it, string key, Action action) =>
            it.Subscribe(NameToId(key), action);

        public static Subscription<T> Subscribe<T>(this IEventBus it, string key, Action<T> action) =>
            it.Subscribe(NameToId(key), action);

        public static Subscription<T1, T2> Subscribe<T1, T2>(this IEventBus it, string key, Action<T1, T2> action) =>
            it.Subscribe(NameToId(key), action);

        public static Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(this IEventBus it, string key,
            Action<T1, T2, T3> action) =>
            it.Subscribe(NameToId(key), action);

        public static void Invoke(this IEventBus it, string key) =>
            it.Invoke(NameToId(key));

        public static void Invoke<T>(this IEventBus it, string key, T arg) =>
            it.Invoke(NameToId(key), arg);

        public static void Invoke<T1, T2>(this IEventBus it, string key, T1 arg1, T2 arg2) =>
            it.Invoke(NameToId(key), arg1, arg2);

        public static void Invoke<T1, T2, T3>(this IEventBus it, string key, T1 arg1, T2 arg2, T3 arg3) =>
            it.Invoke(NameToId(key), arg1, arg2, arg3);
        
        public static void Unsubscribe(this IEventBus it, string key, Action action) =>
            it.Unsubscribe(NameToId(key), action);

        public static void Unsubscribe<T>(this IEventBus it, string key, Action<T> action) =>
            it.Unsubscribe(NameToId(key), action);

        public static void Unsubscribe<T1, T2>(this IEventBus it, string key, Action<T1, T2> action) =>
            it.Unsubscribe(NameToId(key), action);

        public static void Unsubscribe<T1, T2, T3>(this IEventBus it, string key, Action<T1, T2, T3> action) =>
            it.Unsubscribe(NameToId(key), action);

        public static bool IsSubscribed(this IEventBus it, string key) =>
            it.IsSubscribed(NameToId(key));

        public static bool Dispose(this IEventBus it, string key) =>
            it.Dispose(NameToId(key));
        
        public static Subscription Subscribe<TBus>(this TBus it, EventKey<TBus> key, Action action)
            where TBus : IEventBus =>
            it.Subscribe(key.Id, action);

        public static Subscription<T> Subscribe<TBus, T>(this TBus it, EventKey<TBus, T> key, Action<T> action)
            where TBus : IEventBus =>
            it.Subscribe(key.Id, action);

        public static Subscription<T1, T2> Subscribe<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key,
            Action<T1, T2> action)
            where TBus : IEventBus =>
            it.Subscribe(key.Id, action);

        public static Subscription<T1, T2, T3> Subscribe<TBus, T1, T2, T3>(this TBus it,
            EventKey<TBus, T1, T2, T3> key, Action<T1, T2, T3> action)
            where TBus : IEventBus =>
            it.Subscribe(key.Id, action);

        public static void Invoke<TBus>(this TBus it, EventKey<TBus> key)
            where TBus : IEventBus =>
            it.Invoke(key.Id);

        public static void Invoke<TBus, T>(this TBus it, EventKey<TBus, T> key, T arg)
            where TBus : IEventBus =>
            it.Invoke(key.Id, arg);

        public static void Invoke<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key, T1 arg1, T2 arg2)
            where TBus : IEventBus =>
            it.Invoke(key.Id, arg1, arg2);

        public static void Invoke<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key, T1 arg1, T2 arg2,
            T3 arg3)
            where TBus : IEventBus =>
            it.Invoke(key.Id, arg1, arg2, arg3);

        public static void Unsubscribe<TBus>(this TBus it, EventKey<TBus> key, Action action)
            where TBus : IEventBus =>
            it.Unsubscribe(key.Id, action);

        public static void Unsubscribe<TBus, T>(this TBus it, EventKey<TBus, T> key, Action<T> action)
            where TBus : IEventBus =>
            it.Unsubscribe(key.Id, action);

        public static void Unsubscribe<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key,
            Action<T1, T2> action)
            where TBus : IEventBus =>
            it.Unsubscribe(key.Id, action);

        public static void Unsubscribe<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key,
            Action<T1, T2, T3> action)
            where TBus : IEventBus =>
            it.Unsubscribe(key.Id, action);
        
        public static bool IsSubscribed<TBus>(this TBus it, EventKey<TBus> key)
            where TBus : IEventBus =>
            it.IsSubscribed(key.Id);

        public static bool IsSubscribed<TBus, T>(this TBus it, EventKey<TBus, T> key)
            where TBus : IEventBus =>
            it.IsSubscribed(key.Id);

        public static bool IsSubscribed<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key)
            where TBus : IEventBus =>
            it.IsSubscribed(key.Id);

        public static bool IsSubscribed<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key)
            where TBus : IEventBus =>
            it.IsSubscribed(key.Id);
        
        public static bool Dispose<TBus>(this TBus it, EventKey<TBus> key)
            where TBus : IEventBus =>
            it.Dispose(key.Id);

        public static bool Dispose<TBus, T>(this TBus it, EventKey<TBus, T> key)
            where TBus : IEventBus =>
            it.Dispose(key.Id);

        public static bool Dispose<TBus, T1, T2>(this TBus it, EventKey<TBus, T1, T2> key)
            where TBus : IEventBus =>
            it.Dispose(key.Id);

        public static bool Dispose<TBus, T1, T2, T3>(this TBus it, EventKey<TBus, T1, T2, T3> key)
            where TBus : IEventBus =>
            it.Dispose(key.Id);
    }
}