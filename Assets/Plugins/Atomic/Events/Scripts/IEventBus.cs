using System;

namespace Atomic.Events
{
    public interface IEventBus : IDisposable
    {
        Subscription Subscribe(int key, Action action);
        Subscription<T> Subscribe<T>(int key, Action<T> action);
        Subscription<T1, T2> Subscribe<T1, T2>(int key, Action<T1, T2> action);
        Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
        
        void Unsubscribe(int key, Action action);
        void Unsubscribe<T>(int key, Action<T> action);
        void Unsubscribe<T1, T2>(int key, Action<T1, T2> action);
        void Unsubscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action);
        
        void Invoke(int key);
        void Invoke<T>(int key, T arg);
        void Invoke<T1, T2>(int key, T1 arg1, T2 arg2);
        void Invoke<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3);
        
        bool IsSubscribed(int key);
        bool Dispose(int key);
    }
}