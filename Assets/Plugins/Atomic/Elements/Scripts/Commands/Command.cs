using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    public sealed class Command : ICommand
    {
        public event Action OnEvent;

        private Func<bool>[] _conditions = new Func<bool>[4];
        private int _count;

        private Action _action;

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool CanInvoke()
        {
            for (int i = 0; i < _count; i++)
                if (!_conditions[i]())
                    return false;

            return true;
        }
        
#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke()
        {
            if (!CanInvoke())
                return;

            _action?.Invoke();
            OnEvent?.Invoke();
        }

        public ICommand AddCondition(Func<bool> condition)
        {
            if (_count == _conditions.Length)
                Array.Resize(ref _conditions, _conditions.Length * 2);

            _conditions[_count++] = condition;
            return this;
        }

        public ICommand RemoveCondition(Func<bool> condition)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_conditions[i] == condition)
                {
                    for (int j = i; j < _count - 1; j++)
                        _conditions[j] = _conditions[j + 1];

                    _conditions[--_count] = null;
                    break;
                }
            }

            return this;
        }

        public ICommand AddAction(Action action)
        {
            _action += action;
            return this;
        }

        public ICommand RemoveAction(Action action)
        {
            _action -= action;
            return this;
        }
    }

    public sealed class Command<T1> : ICommand<T1>
    {
        public event Action<T1> OnEvent;

        private Func<T1, bool>[] _conditions = new Func<T1, bool>[4];
        private int _count;

        private Action<T1> _action;

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool CanInvoke(T1 arg1)
        {
            for (int i = 0; i < _count; i++)
                if (!_conditions[i](arg1))
                    return false;

            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 arg1)
        {
            if (!CanInvoke(arg1))
                return;

            _action?.Invoke(arg1);
            OnEvent?.Invoke(arg1);
        }

        public ICommand<T1> AddCondition(Func<T1, bool> condition)
        {
            if (_count == _conditions.Length)
                Array.Resize(ref _conditions, _conditions.Length * 2);

            _conditions[_count++] = condition;
            return this;
        }

        public ICommand<T1> RemoveCondition(Func<T1, bool> condition)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_conditions[i] == condition)
                {
                    for (int j = i; j < _count - 1; j++)
                        _conditions[j] = _conditions[j + 1];

                    _conditions[--_count] = null;
                    break;
                }
            }

            return this;
        }

        public ICommand<T1> AddAction(Action<T1> action)
        {
            _action += action;
            return this;
        }

        public ICommand<T1> RemoveAction(Action<T1> action)
        {
            _action -= action;
            return this;
        }
    }

    public sealed class Command<T1, T2> : ICommand<T1, T2>
    {
        public event Action<T1, T2> OnEvent;

        private Func<T1, T2, bool>[] _conditions = new Func<T1, T2, bool>[4];
        private int _count;

        private Action<T1, T2> _action;

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool CanInvoke(T1 arg1, T2 arg2)
        {
            for (int i = 0; i < _count; i++)
                if (!_conditions[i](arg1, arg2))
                    return false;

            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 arg1, T2 arg2)
        {
            if (!CanInvoke(arg1, arg2))
                return;

            _action?.Invoke(arg1, arg2);
            OnEvent?.Invoke(arg1, arg2);
        }

        public ICommand<T1, T2> AddCondition(Func<T1, T2, bool> condition)
        {
            if (_count == _conditions.Length)
                Array.Resize(ref _conditions, _conditions.Length * 2);

            _conditions[_count++] = condition;
            return this;
        }

        public ICommand<T1, T2> RemoveCondition(Func<T1, T2, bool> condition)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_conditions[i] == condition)
                {
                    for (int j = i; j < _count - 1; j++)
                        _conditions[j] = _conditions[j + 1];

                    _conditions[--_count] = null;
                    break;
                }
            }

            return this;
        }

        public ICommand<T1, T2> AddAction(Action<T1, T2> action)
        {
            _action += action;
            return this;
        }

        public ICommand<T1, T2> RemoveAction(Action<T1, T2> action)
        {
            _action -= action;
            return this;
        }
    }

    public sealed class Command<T1, T2, T3> : ICommand<T1, T2, T3>
    {
        public event Action<T1, T2, T3> OnEvent;

        private Func<T1, T2, T3, bool>[] _conditions = new Func<T1, T2, T3, bool>[4];
        private int _count;

        private Action<T1, T2, T3> _action;

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool CanInvoke(T1 arg1, T2 arg2, T3 arg3)
        {
            for (int i = 0; i < _count; i++)
                if (!_conditions[i](arg1, arg2, arg3))
                    return false;

            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            if (!CanInvoke(arg1, arg2, arg3))
                return;

            _action?.Invoke(arg1, arg2, arg3);
            OnEvent?.Invoke(arg1, arg2, arg3);
        }

        public ICommand<T1, T2, T3> AddCondition(Func<T1, T2, T3, bool> condition)
        {
            if (_count == _conditions.Length)
                Array.Resize(ref _conditions, _conditions.Length * 2);

            _conditions[_count++] = condition;
            return this;
        }

        public ICommand<T1, T2, T3> RemoveCondition(Func<T1, T2, T3, bool> condition)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_conditions[i] == condition)
                {
                    for (int j = i; j < _count - 1; j++)
                        _conditions[j] = _conditions[j + 1];

                    _conditions[--_count] = null;
                    break;
                }
            }

            return this;
        }

        public ICommand<T1, T2, T3> AddAction(Action<T1, T2, T3> action)
        {
            _action += action;
            return this;
        }

        public ICommand<T1, T2, T3> RemoveAction(Action<T1, T2, T3> action)
        {
            _action -= action;
            return this;
        }
    }

    public sealed class Command<T1, T2, T3, T4> : ICommand<T1, T2, T3, T4>
    {
        public event Action<T1, T2, T3, T4> OnEvent;

        private Func<T1, T2, T3, T4, bool>[] _conditions = new Func<T1, T2, T3, T4, bool>[4];
        private int _count;

        private Action<T1, T2, T3, T4> _action;

#if ODIN_INSPECTOR
        [Button]
#endif
        public bool CanInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            for (int i = 0; i < _count; i++)
                if (!_conditions[i](arg1, arg2, arg3, arg4))
                    return false;

            return true;
        }

#if ODIN_INSPECTOR
        [Button]
#endif
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (!CanInvoke(arg1, arg2, arg3, arg4))
                return;

            _action?.Invoke(arg1, arg2, arg3, arg4);
            OnEvent?.Invoke(arg1, arg2, arg3, arg4);
        }

        public ICommand<T1, T2, T3, T4> AddCondition(Func<T1, T2, T3, T4, bool> condition)
        {
            if (_count == _conditions.Length)
                Array.Resize(ref _conditions, _conditions.Length * 2);

            _conditions[_count++] = condition;
            return this;
        }

        public ICommand<T1, T2, T3, T4> RemoveCondition(Func<T1, T2, T3, T4, bool> condition)
        {
            for (int i = 0; i < _count; i++)
            {
                if (_conditions[i] == condition)
                {
                    for (int j = i; j < _count - 1; j++)
                        _conditions[j] = _conditions[j + 1];

                    _conditions[--_count] = null;
                    break;
                }
            }

            return this;
        }

        public ICommand<T1, T2, T3, T4> AddAction(Action<T1, T2, T3, T4> action)
        {
            _action += action;
            return this;
        }

        public ICommand<T1, T2, T3, T4> RemoveAction(Action<T1, T2, T3, T4> action)
        {
            _action -= action;
            return this;
        }
    }
}