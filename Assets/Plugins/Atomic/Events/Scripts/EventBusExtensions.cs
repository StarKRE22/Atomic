using System;
using static Atomic.Events.EventBusUtils;

namespace Atomic.Events
{
    public static class EventBusExtensions
    {
        public static Subscription Subscribe(this IEventBus it, string key, Action action) => 
            it.Subscribe(NameToId(key), action);

        public static Subscription<T> Subscribe<T>(this IEventBus it, string key, Action<T> action) => 
            it.Subscribe(NameToId(key), action);
        
        public static Subscription<T1, T2> Subscribe<T1, T2>(this IEventBus it, string key, Action<T1, T2> action) => 
            it.Subscribe(NameToId(key), action);

        public static Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(this IEventBus it, string key, Action<T1, T2, T3> action) => 
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
        
        public static bool Dispose(this IEventBus it, string key) => 
            it.Dispose(NameToId(key));
    }
}