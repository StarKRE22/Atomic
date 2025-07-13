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
    public class BaseAction : IAction
    {
        private Action action;

        public BaseAction()
        {
        }

        public BaseAction(Action action)
        {
            this.action = action;
        }
        
        public static implicit operator BaseAction(Action value)
        {
            return new BaseAction(value);
        }

        public BaseAction Compose(Action action)
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
    public class BaseAction<T> : IAction<T>
    {
        private Action<T> action;

        public BaseAction()
        {
        }

        public BaseAction(Action<T> action)
        {
            this.action = action;
        }

        public static implicit operator BaseAction<T>(Action<T> value)
        {
            return new BaseAction<T>(value);
        }
        
        public BaseAction<T> Compose(Action<T> action)
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
    public class BaseAction<T1, T2> : IAction<T1, T2>
    {
        private Action<T1, T2> action;

        public BaseAction()
        {
        }

        public BaseAction(Action<T1, T2> action)
        {
            this.action = action;
        }
        
        public static implicit operator BaseAction<T1, T2>(Action<T1, T2> value)
        {
            return new BaseAction<T1, T2>(value);
        }
        
        public BaseAction<T1, T2> Compose(Action<T1, T2> action)
        {
            this.action = action;
            return this;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 arg1, T2 arg2)
        {
            this.action?.Invoke(arg1, arg2);
        }
    }
    
    
#if ODIN_INSPECTOR
    [InlineProperty]
#endif

    [Serializable]
    public class BaseAction<T1, T2, T3> : IAction<T1, T2, T3>
    {
        private Action<T1, T2, T3> action;

        public BaseAction()
        {
        }

        public BaseAction(Action<T1, T2, T3> action)
        {
            this.action = action;
        }
        
        public static implicit operator BaseAction<T1, T2, T3>(Action<T1, T2, T3> value)
        {
            return new BaseAction<T1, T2, T3>(value);
        }
        
        public BaseAction<T1, T2, T3> Compose(Action<T1, T2, T3> action)
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
