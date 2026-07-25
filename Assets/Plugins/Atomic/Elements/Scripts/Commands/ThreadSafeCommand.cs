using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    public sealed class ThreadSafeCommand : ICommand, MainThreadDispatcher.IFlushable
    {
        public event Action OnEvent;

        private readonly object _lock = new();

        private Func<bool>[] _conditions = new Func<bool>[4];
        private int _conditionCount;

        private Action _action;

#if ODIN_INSPECTOR
    [Button]
#endif
        public bool CanInvoke()
        {
            Func<bool>[] conditions;
            int count;

            lock (_lock)
            {
                conditions = _conditions;
                count = _conditionCount;
            }

            for (int i = 0; i < count; i++)
                if (!conditions[i]())
                    return false;

            return true;
        }

#if ODIN_INSPECTOR
    [Button]
#endif
        public void Invoke()
        {
            Action action;
            lock (_lock)
                action = _action;

            if (!CanInvoke())
                return;

            action?.Invoke();
            MainThreadDispatcher.MarkDirty(this);
        }

        void MainThreadDispatcher.IFlushable.Flush()
        {
            Action handler = OnEvent;
            handler?.Invoke();
        }

        public ICommand AddCondition(Func<bool> condition)
        {
            lock (_lock)
            {
                if (_conditionCount == _conditions.Length)
                    Array.Resize(ref _conditions, _conditions.Length * 2);

                _conditions[_conditionCount++] = condition;
            }

            return this;
        }

        public ICommand RemoveCondition(Func<bool> condition)
        {
            lock (_lock)
            {
                for (int i = 0; i < _conditionCount; i++)
                {
                    if (_conditions[i] == condition)
                    {
                        _conditions[i] = _conditions[_conditionCount - 1];
                        _conditions[--_conditionCount] = null;
                        break;
                    }
                }
            }

            return this;
        }

        public ICommand AddAction(Action action)
        {
            lock (_lock)
                _action += action;

            return this;
        }

        public ICommand RemoveAction(Action action)
        {
            lock (_lock)
                _action -= action;

            return this;
        }
    }

    public sealed class ThreadSafeCommand<T> : ICommand<T>, MainThreadDispatcher.IFlushable
    {
        public event Action<T> OnEvent;

        private readonly object _lock = new();

        private Func<T, bool>[] _conditions = new Func<T, bool>[4];
        private int _conditionCount;

        private Action<T> _action;

        private T _arg;
        private bool _hasValue;

        public void Invoke(T arg)
        {
            Action<T> action;
            Func<T, bool>[] conditions;
            int count;

            lock (_lock)
            {
                action = _action;
                conditions = _conditions;
                count = _conditionCount;
            }

            for (int i = 0; i < count; i++)
            {
                Func<T, bool> cond = conditions[i];
                if (!cond(arg))
                    return;
            }

            action?.Invoke(arg);

            lock (_lock)
            {
                _arg = arg;
                _hasValue = true;
            }

            MainThreadDispatcher.MarkDirty(this);
        }

        void MainThreadDispatcher.IFlushable.Flush()
        {
            T arg;
            Action<T> handler;

            lock (_lock)
            {
                if (!_hasValue)
                    return;

                arg = _arg;
                _hasValue = false;
                handler = OnEvent;
            }

            handler?.Invoke(arg);
        }

#if ODIN_INSPECTOR
    [Button]
#endif
        public bool CanInvoke(T arg)
        {
            Func<T, bool>[] conditions;
            int count;

            lock (_lock)
            {
                conditions = _conditions;
                count = _conditionCount;
            }

            for (int i = 0; i < count; i++)
                if (!conditions[i].Invoke(arg))
                    return false;

            return true;
        }

        public ICommand<T> AddCondition(Func<T, bool> condition)
        {
            lock (_lock)
            {
                if (_conditionCount == _conditions.Length)
                    Array.Resize(ref _conditions, _conditions.Length * 2);

                _conditions[_conditionCount++] = condition;
            }

            return this;
        }

        public ICommand<T> RemoveCondition(Func<T, bool> condition)
        {
            lock (_lock)
            {
                for (int i = 0; i < _conditionCount; i++)
                {
                    if (_conditions[i] == condition)
                    {
                        _conditions[i] = _conditions[_conditionCount - 1];
                        _conditions[--_conditionCount] = null;
                        break;
                    }
                }
            }

            return this;
        }

        public ICommand<T> AddAction(Action<T> action)
        {
            lock (_lock)
                _action += action;

            return this;
        }

        public ICommand<T> RemoveAction(Action<T> action)
        {
            lock (_lock)
                _action -= action;

            return this;
        }
    }

    public sealed class ThreadSafeCommand<T1, T2> : ICommand<T1, T2>, MainThreadDispatcher.IFlushable
    {
        public event Action<T1, T2> OnEvent;

        private readonly object _lock = new();

        private Func<T1, T2, bool>[] _conditions = new Func<T1, T2, bool>[4];
        private int _conditionCount;

        private Action<T1, T2> _action;

        private T1 _arg1;
        private T2 _arg2;
        private bool _hasValue;

        public void Invoke(T1 arg1, T2 arg2)
        {
            Action<T1, T2> action;
            Func<T1, T2, bool>[] conditions;
            int count;

            lock (_lock)
            {
                action = _action;
                conditions = _conditions;
                count = _conditionCount;
            }

            for (int i = 0; i < count; i++)
            {
                Func<T1, T2, bool> cond = conditions[i];
                if (!cond(arg1, arg2))
                    return;
            }

            action?.Invoke(arg1, arg2);

            lock (_lock)
            {
                _arg1 = arg1;
                _arg2 = arg2;
                _hasValue = true;
            }

            MainThreadDispatcher.MarkDirty(this);
        }

        void MainThreadDispatcher.IFlushable.Flush()
        {
            T1 arg1;
            T2 arg2;
            Action<T1, T2> handler;

            lock (_lock)
            {
                if (!_hasValue)
                    return;

                arg1 = _arg1;
                arg2 = _arg2;
                _hasValue = false;
                handler = OnEvent;
            }

            handler?.Invoke(arg1, arg2);
        }

#if ODIN_INSPECTOR
    [Button]
#endif
        public bool CanInvoke(T1 arg1, T2 arg2)
        {
            Func<T1, T2, bool>[] conditions;
            int count;

            lock (_lock)
            {
                conditions = _conditions;
                count = _conditionCount;
            }

            for (int i = 0; i < count; i++)
            {
                Func<T1, T2, bool> cond = conditions[i];
                if (!cond(arg1, arg2))
                    return false;
            }

            return true;
        }

        public ICommand<T1, T2> AddCondition(Func<T1, T2, bool> condition)
        {
            lock (_lock)
            {
                if (_conditionCount == _conditions.Length)
                    Array.Resize(ref _conditions, _conditions.Length * 2);

                _conditions[_conditionCount++] = condition;
            }

            return this;
        }

        public ICommand<T1, T2> RemoveCondition(Func<T1, T2, bool> condition)
        {
            lock (_lock)
            {
                for (int i = 0; i < _conditionCount; i++)
                {
                    if (_conditions[i] == condition)
                    {
                        _conditions[i] = _conditions[_conditionCount - 1];
                        _conditions[--_conditionCount] = null;
                        break;
                    }
                }
            }

            return this;
        }

        public ICommand<T1, T2> AddAction(Action<T1, T2> action)
        {
            lock (_lock)
                _action += action;

            return this;
        }

        public ICommand<T1, T2> RemoveAction(Action<T1, T2> action)
        {
            lock (_lock)
                _action -= action;

            return this;
        }
    }

    public sealed class ThreadSafeCommand<T1, T2, T3> : ICommand<T1, T2, T3>, MainThreadDispatcher.IFlushable
    {
        public event Action<T1, T2, T3> OnEvent;

        private readonly object _lock = new();

        private Func<T1, T2, T3, bool>[] _conditions = new Func<T1, T2, T3, bool>[4];
        private int _conditionCount;

        private Action<T1, T2, T3> _action;

        private T1 _arg1;
        private T2 _arg2;
        private T3 _arg3;
        private bool _hasValue;

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            Action<T1, T2, T3> action;
            Func<T1, T2, T3, bool>[] conditions;
            int count;

            lock (_lock)
            {
                action = _action;
                conditions = _conditions;
                count = _conditionCount;
            }

            for (int i = 0; i < count; i++)
            {
                Func<T1, T2, T3, bool> cond = conditions[i];
                if (!cond(arg1, arg2, arg3))
                    return;
            }

            action?.Invoke(arg1, arg2, arg3);

            lock (_lock)
            {
                _arg1 = arg1;
                _arg2 = arg2;
                _arg3 = arg3;
                _hasValue = true;
            }

            MainThreadDispatcher.MarkDirty(this);
        }

        void MainThreadDispatcher.IFlushable.Flush()
        {
            T1 arg1;
            T2 arg2;
            T3 arg3;
            Action<T1, T2, T3> handler;

            lock (_lock)
            {
                if (!_hasValue)
                    return;

                arg1 = _arg1;
                arg2 = _arg2;
                arg3 = _arg3;
                _hasValue = false;
                handler = OnEvent;
            }

            handler?.Invoke(arg1, arg2, arg3);
        }

#if ODIN_INSPECTOR
    [Button]
#endif
        public bool CanInvoke(T1 arg1, T2 arg2, T3 arg3)
        {
            Func<T1, T2, T3, bool>[] conditions;
            int count;

            lock (_lock)
            {
                conditions = _conditions;
                count = _conditionCount;
            }

            for (int i = 0; i < count; i++)
            {
                Func<T1, T2, T3, bool> cond = conditions[i];
                if (!cond(arg1, arg2, arg3))
                    return false;
            }

            return true;
        }

        public ICommand<T1, T2, T3> AddCondition(Func<T1, T2, T3, bool> condition)
        {
            lock (_lock)
            {
                if (_conditionCount == _conditions.Length)
                    Array.Resize(ref _conditions, _conditions.Length * 2);

                _conditions[_conditionCount++] = condition;
            }

            return this;
        }

        public ICommand<T1, T2, T3> RemoveCondition(Func<T1, T2, T3, bool> condition)
        {
            lock (_lock)
            {
                for (int i = 0; i < _conditionCount; i++)
                {
                    if (_conditions[i] == condition)
                    {
                        _conditions[i] = _conditions[_conditionCount - 1];
                        _conditions[--_conditionCount] = null;
                        break;
                    }
                }
            }

            return this;
        }

        public ICommand<T1, T2, T3> AddAction(Action<T1, T2, T3> action)
        {
            lock (_lock)
                _action += action;

            return this;
        }

        public ICommand<T1, T2, T3> RemoveAction(Action<T1, T2, T3> action)
        {
            lock (_lock)
                _action -= action;

            return this;
        }
    }

    public sealed class ThreadSafeCommand<T1, T2, T3, T4> : ICommand<T1, T2, T3, T4>, MainThreadDispatcher.IFlushable
    {
        public event Action<T1, T2, T3, T4> OnEvent;

        private readonly object _lock = new();

        private Func<T1, T2, T3, T4, bool>[] _conditions = new Func<T1, T2, T3, T4, bool>[4];
        private int _conditionCount;

        private Action<T1, T2, T3, T4> _action;

        private T1 _arg1;
        private T2 _arg2;
        private T3 _arg3;
        private T4 _arg4;
        private bool _hasValue;

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Action<T1, T2, T3, T4> action;
            Func<T1, T2, T3, T4, bool>[] conditions;
            int count;

            lock (_lock)
            {
                action = _action;
                conditions = _conditions;
                count = _conditionCount;
            }

            for (int i = 0; i < count; i++)
            {
                Func<T1, T2, T3, T4, bool> cond = conditions[i];
                if (!cond(arg1, arg2, arg3, arg4))
                    return;
            }

            action?.Invoke(arg1, arg2, arg3, arg4);

            lock (_lock)
            {
                _arg1 = arg1;
                _arg2 = arg2;
                _arg3 = arg3;
                _arg4 = arg4;
                _hasValue = true;
            }

            MainThreadDispatcher.MarkDirty(this);
        }

        void MainThreadDispatcher.IFlushable.Flush()
        {
            T1 arg1;
            T2 arg2;
            T3 arg3;
            T4 arg4;
            Action<T1, T2, T3, T4> handler;

            lock (_lock)
            {
                if (!_hasValue)
                    return;

                arg1 = _arg1;
                arg2 = _arg2;
                arg3 = _arg3;
                arg4 = _arg4;
                _hasValue = false;
                handler = OnEvent;
            }

            handler?.Invoke(arg1, arg2, arg3, arg4);
        }

#if ODIN_INSPECTOR
    [Button]
#endif
        public bool CanInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            Func<T1, T2, T3, T4, bool>[] conditions;
            int count;

            lock (_lock)
            {
                conditions = _conditions;
                count = _conditionCount;
            }

            for (int i = 0; i < count; i++)
            {
                Func<T1, T2, T3, T4, bool> cond = conditions[i];
                if (!cond(arg1, arg2, arg3, arg4))
                    return false;
            }

            return true;
        }

        public ICommand<T1, T2, T3, T4> AddCondition(Func<T1, T2, T3, T4, bool> condition)
        {
            lock (_lock)
            {
                if (_conditionCount == _conditions.Length)
                    Array.Resize(ref _conditions, _conditions.Length * 2);

                _conditions[_conditionCount++] = condition;
            }

            return this;
        }

        public ICommand<T1, T2, T3, T4> RemoveCondition(Func<T1, T2, T3, T4, bool> condition)
        {
            lock (_lock)
            {
                for (int i = 0; i < _conditionCount; i++)
                {
                    if (_conditions[i] == condition)
                    {
                        _conditions[i] = _conditions[_conditionCount - 1];
                        _conditions[--_conditionCount] = null;
                        break;
                    }
                }
            }

            return this;
        }

        public ICommand<T1, T2, T3, T4> AddAction(Action<T1, T2, T3, T4> action)
        {
            lock (_lock)
                _action += action;

            return this;
        }

        public ICommand<T1, T2, T3, T4> RemoveAction(Action<T1, T2, T3, T4> action)
        {
            lock (_lock)
                _action -= action;

            return this;
        }
    }
}