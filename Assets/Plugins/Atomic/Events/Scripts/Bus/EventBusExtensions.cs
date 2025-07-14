using System;
using static Atomic.Events.EventBusUtils;

namespace Atomic.Events
{
    public static class EventBusExtensions
    {
        public static void Declare(this IEventBus it, in string key) =>
            it.Declare(NameToId(in key));
        
        public static void Declare<T>(this IEventBus it, in string key) => 
            it.Declare<T>(NameToId(in key));
        
        public static void Declare<T1, T2>(this IEventBus it, in string key) => 
            it.Declare<T1, T2>(NameToId(in key));

        public static void Declare<T1, T2, T3>(this IEventBus it, in string key) =>
            it.Declare<T1, T2, T3>(NameToId(in key));
        
        public static Action Subscribe(this IEventBus it, in string key, in Action action) => 
            it.Subscribe(NameToId(in key), in action);

        public static Action<T> Subscribe<T>(this IEventBus it, in string key, in Action<T> action) => 
            it.Subscribe(NameToId(in key), in action);
        
        public static Action<T1, T2> Subscribe<T1, T2>(this IEventBus it, in string key, in Action<T1, T2> action) => 
            it.Subscribe(NameToId(in key), in action);

        public static Action<T1, T2, T3> Subscribe<T1, T2, T3>(this IEventBus it, in string key, in Action<T1, T2, T3> action) => 
            it.Subscribe(NameToId(in key), in action);
        
        public static void Invoke(this IEventBus it, in string key) => 
            it.Invoke(NameToId(in key));

        public static void Invoke<T>(this IEventBus it, in string key, T arg) => 
            it.Invoke(NameToId(in key), arg);

        public static void Invoke<T1, T2>(this IEventBus it, in string key, T1 arg1, T2 arg2) => 
            it.Invoke(NameToId(in key), arg1, arg2);
        
        public static void Invoke<T1, T2, T3>(this IEventBus it, in string key, T1 arg1, T2 arg2, T3 arg3) => 
            it.Invoke(NameToId(in key), arg1, arg2, arg3);
    }
}