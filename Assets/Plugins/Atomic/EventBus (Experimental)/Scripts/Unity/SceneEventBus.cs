using System;
using System.Collections.Generic;
using UnityEngine;
#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Events
{
    [AddComponentMenu("Atomic/Events/Event Bus")]
    [DisallowMultipleComponent, DefaultExecutionOrder(-1000)]
    public partial class SceneEventBus : MonoBehaviour, IEventBus
    {
        private readonly EventBus _eventBus = new();

        protected virtual void OnDestroy() => _eventBus.Dispose();

        public Subscription Subscribe(int key, Action action) =>
            _eventBus.Subscribe(key, action);

        public Subscription<T> Subscribe<T>(int key, Action<T> action) =>
            _eventBus.Subscribe(key, action);

        public Subscription<T1, T2> Subscribe<T1, T2>(int key, Action<T1, T2> action) =>
            _eventBus.Subscribe(key, action);

        public Subscription<T1, T2, T3> Subscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action) =>
            _eventBus.Subscribe(key, action);

        public void Invoke(int key) =>
            _eventBus.Invoke(key);

        public void Invoke<T>(int key, T arg) =>
            _eventBus.Invoke(key, arg);

        public void Invoke<T1, T2>(int key, T1 arg1, T2 arg2) =>
            _eventBus.Invoke(key, arg1, arg2);

        public void Invoke<T1, T2, T3>(int key, T1 arg1, T2 arg2, T3 arg3) =>
            _eventBus.Invoke(key, arg1, arg2, arg3);

        public bool IsSubscribed(int key) =>
            _eventBus.IsSubscribed(key);

        public bool Dispose(int key) =>
            _eventBus.Dispose(key);

        public void Unsubscribe(int key, Action action) =>
            _eventBus.Unsubscribe(key, action);

        public void Unsubscribe<T>(int key, Action<T> action) =>
            _eventBus.Unsubscribe(key, action);

        public void Unsubscribe<T1, T2>(int key, Action<T1, T2> action) =>
            _eventBus.Unsubscribe(key, action);

        public void Unsubscribe<T1, T2, T3>(int key, Action<T1, T2, T3> action) =>
            _eventBus.Unsubscribe(key, action);

        public void Dispose() =>
            _eventBus.Dispose();
    }
}