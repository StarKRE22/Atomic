using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// Represents an event as object 

#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class BaseEvent : IEvent, IDisposable
    {
        public event Action OnEvent;

        public void Subscribe(Action action) => this.OnEvent += action;
    
        public void Unsubscribe(Action action) => this.OnEvent -= action;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke() => this.OnEvent?.Invoke();

        public void Dispose() => AtomicHelper.Dispose(ref this.OnEvent);
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class BaseEvent<T> : IEvent<T>, IDisposable
    {
        public event Action<T> OnEvent;

        public void Subscribe(Action<T> action) => this.OnEvent += action;

        public void Unsubscribe(Action<T> action) => this.OnEvent -= action;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T arg) => this.OnEvent?.Invoke(arg);

        public void Dispose() => AtomicHelper.Dispose(ref this.OnEvent);
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class BaseEvent<T1, T2> : IEvent<T1, T2>, IDisposable
    {
        public event Action<T1, T2> OnEvent;

        public void Subscribe(Action<T1, T2> action) => this.OnEvent += action;

        public void Unsubscribe(Action<T1, T2> action) => this.OnEvent -= action;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 arg1, T2 arg2) => this.OnEvent?.Invoke(arg1, arg2);

        public void Dispose() => AtomicHelper.Dispose(ref this.OnEvent);
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    [Serializable]
    public class BaseEvent<T1, T2, T3> : IEvent<T1, T2, T3>, IDisposable
    {
        public event Action<T1, T2, T3> OnEvent;

        public void Subscribe(Action<T1, T2, T3> action) => this.OnEvent += action;

        public void Unsubscribe(Action<T1, T2, T3> action) => this.OnEvent -= action;

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 args1, T2 args2, T3 args3) => this.OnEvent?.Invoke(args1, args2, args3);

        public void Dispose() => AtomicHelper.Dispose(ref this.OnEvent);
    }
}