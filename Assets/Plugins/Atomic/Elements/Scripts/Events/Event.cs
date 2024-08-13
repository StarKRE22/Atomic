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
    public class Event : IEvent, IDisposable
    {
        public event System.Action OnEvent;

        public System.Action Subscribe(System.Action action)
        {
            this.OnEvent += action;
            return action;
        }

        public void Unsubscribe(System.Action action)
        {
            this.OnEvent -= action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke()
        {
            this.OnEvent?.Invoke();
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnEvent);
        }
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class Event<T> : IEvent<T>, IDisposable
    {
        public event System.Action<T> OnEvent;

        public System.Action<T> Subscribe(System.Action<T> action)
        {
            this.OnEvent += action;
            return action;
        }

        public void Unsubscribe(System.Action<T> action)
        {
            this.OnEvent -= action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T arg)
        {
            this.OnEvent?.Invoke(arg);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnEvent);
        }
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class Event<T1, T2> : IEvent<T1, T2>, IDisposable
    {
        public event System.Action<T1, T2> OnEvent;

        public System.Action<T1, T2> Subscribe(System.Action<T1, T2> action)
        {
            this.OnEvent += action;
            return action;
        }

        public void Unsubscribe(System.Action<T1, T2> action)
        {
            this.OnEvent -= action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 args1, T2 args2)
        {
            this.OnEvent?.Invoke(args1, args2);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnEvent);
        }
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    
    [Serializable]
    public class Event<T1, T2, T3> : IEvent<T1, T2, T3>, IDisposable
    {
        public event System.Action<T1, T2, T3> OnEvent;

        public System.Action<T1, T2, T3> Subscribe(System.Action<T1, T2, T3> action)
        {
            this.OnEvent += action;
            return action;
        }

        public void Unsubscribe(System.Action<T1, T2, T3> action)
        {
            this.OnEvent -= action;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 args1, T2 args2, T3 args3)
        {
            this.OnEvent?.Invoke(args1, args2, args3);
        }

        public void Dispose()
        {
            DelegateUtils.Dispose(ref this.OnEvent);
        }
    }
}
