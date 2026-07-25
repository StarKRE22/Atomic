using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    [Serializable]
    public struct ExpressionMember<R>
    {
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public object Source => _source;
        
        private object _source;
        private Func<R> _func;
        
        public ExpressionMember(Func<R> func)
        {
            _source = null;
            _func = func;
        }
        
        public ExpressionMember(IFunction<R> func)
        {
            _source = null;
            _func = func.Invoke;
        }
        
        public ExpressionMember(object source, Func<R> func)
        {
            _source = source;
            _func = func;
        }
        
        public ExpressionMember(object source, IFunction<R> func)
        {
            _source = source;
            _func = func.Invoke;
        }
        
        public readonly bool EqualsFunction(Func<R> func)
        {
            return _func == func;
        }

        public readonly R Invoke()
        {
            return _func.Invoke();
        }
    }
    
    [Serializable]
    public struct ExpressionMember<T, R>
    {
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public object Source => _source;

        private object _source;
        private Func<T, R> _func;

        public ExpressionMember(Func<T, R> func)
        {
            _source = null;
            _func = func;
        }
        
        public ExpressionMember(object source, Func<T, R> func)
        {
            _source = source;
            _func = func;
        }
        
        public readonly bool EqualsFunction(Func<T, R> func)
        {
            return _func == func;
        }

        public readonly R Invoke(T arg)
        {
            return _func.Invoke(arg);
        }
    }
    
    [Serializable]
    public struct ExpressionMember<T1, T2, R>
    {
#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public object Source => _source;

        private object _source;
        private Func<T1, T2, R> _func;

        public ExpressionMember(Func<T1, T2, R> func)
        {
            _source = null;
            _func = func;
        }
        
        public ExpressionMember(object source, Func<T1, T2, R> func)
        {
            _source = source;
            _func = func;
        }
        
        public readonly bool EqualsFunction(Func<T1, T2, R> func)
        {
            return _func == func;
        }

        public readonly R Invoke(T1 arg1, T2 arg2)
        {
            return _func.Invoke(arg1, arg2);
        }
    }
}