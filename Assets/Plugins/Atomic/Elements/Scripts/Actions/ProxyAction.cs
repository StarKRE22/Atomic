using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class ProxyAction : IAction
    {
        private Action action;

        public ProxyAction()
        {
        }

        public ProxyAction(Action action)
        {
            this.action = action;
        }
        
        public static implicit operator ProxyAction(Action value)
        {
            return new ProxyAction(value);
        }

        public ProxyAction Compose(Action action)
        {
            this.action = action;
            return this;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke()
        {
            this.action?.Invoke();
        }
    }

   
#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class ProxyAction<T> : IAction<T>
    {
        private Action<T> action;

        public ProxyAction()
        {
        }

        public ProxyAction(Action<T> action)
        {
            this.action = action;
        }
        
        public static implicit operator ProxyAction<T>(Action<T> value)
        {
            return new ProxyAction<T>(value);
        }
        
        public ProxyAction<T> Compose(Action<T> action)
        {
            this.action = action;
            return this;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T arg)
        {
            this.action?.Invoke(arg);
        }
    }
    
   
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    
    [Serializable]
    public class ProxyAction<T1, T2> : IAction<T1, T2>
    {
        private Action<T1, T2> action;

        public ProxyAction()
        {
        }

        public ProxyAction(Action<T1, T2> action)
        {
            this.action = action;
        }
        
        public static implicit operator ProxyAction<T1, T2>(Action<T1, T2> value)
        {
            return new ProxyAction<T1, T2>(value);
        }
        
        public ProxyAction<T1, T2> Compose(Action<T1, T2> action)
        {
            this.action = action;
            return this;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 args1, T2 args2)
        {
            this.action?.Invoke(args1, args2);
        }
    }
    
    
#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class ProxyAction<T1, T2, T3> : IAction<T1, T2, T3>
    {
        private Action<T1, T2, T3> action;

        public ProxyAction()
        {
        }

        public ProxyAction(Action<T1, T2, T3> action)
        {
            this.action = action;
        }
        
        public static implicit operator ProxyAction<T1, T2, T3>(Action<T1, T2, T3> value)
        {
            return new ProxyAction<T1, T2, T3>(value);
        }
        
        public ProxyAction<T1, T2, T3> Compose(Action<T1, T2, T3> action)
        {
            this.action = action;
            return this;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 args1, T2 args2, T3 args3)
        {
            this.action?.Invoke(args1, args2, args3);
        }
    }
}
