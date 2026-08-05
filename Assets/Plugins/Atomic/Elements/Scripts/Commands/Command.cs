using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Default implementation of <see cref="ICommand"/>.
    /// </summary>
    /// <remarks>
    /// Stores a collection of execution conditions and actions.
    /// The command can be invoked only when all registered conditions evaluate
    /// to <see langword="true"/>. When successfully invoked, all registered
    /// actions are executed and the <see cref="OnEvent"/> event is raised.
    /// </remarks>
    public sealed class Command : ICommand
    {
        /// <inheritdoc/>
        public event Action OnEvent;
    
        private Func<bool>[] _conditions = new Func<bool>[4];
        private int _count;
    
        private Action _action;
    
    #if ODIN_INSPECTOR
        [Button]
    #endif
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public bool TryInvoke()
        {
            if (!CanInvoke())
                return false;
    
            _action?.Invoke();
            OnEvent?.Invoke();
            return true;
        }
    
    #if ODIN_INSPECTOR
        [Button]
    #endif
        /// <inheritdoc/>
        public void Invoke()
        {
            if (!CanInvoke())
                return;
    
            _action?.Invoke();
            OnEvent?.Invoke();
        }
    
        /// <inheritdoc/>
        public ICommand AddCondition(Func<bool> condition)
        {
            if (_count == _conditions.Length)
                Array.Resize(ref _conditions, _conditions.Length * 2);
    
            _conditions[_count++] = condition;
            return this;
        }
    
        /// <inheritdoc/>
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
    
        /// <inheritdoc/>
        public ICommand AddAction(Action action)
        {
            _action += action;
            return this;
        }
    
        /// <inheritdoc/>
        public ICommand RemoveAction(Action action)
        {
            _action -= action;
            return this;
        }
    }

    /// <summary>
    /// Default implementation of <see cref="ICommand{T}"/>.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    /// <remarks>
    /// Stores a collection of execution conditions and actions.
    /// The command can be invoked only when all registered conditions evaluate
    /// to <see langword="true"/> for the specified argument. When successfully
    /// invoked, all registered actions are executed and the
    /// <see cref="OnEvent"/> event is raised.
    /// </remarks>
    public sealed class Command<T> : ICommand<T>
    {
        /// <inheritdoc/>
        public event Action<T> OnEvent;
    
        private Func<T, bool>[] _conditions = new Func<T, bool>[4];
        private int _count;
    
        private Action<T> _action;
    
    #if ODIN_INSPECTOR
        [Button]
    #endif
        /// <inheritdoc/>
        public bool CanInvoke(T arg)
        {
            for (int i = 0; i < _count; i++)
                if (!_conditions[i](arg))
                    return false;
    
            return true;
        }
    
    #if ODIN_INSPECTOR
        [Button]
    #endif
        /// <inheritdoc/>
        public bool TryInvoke(T arg)
        {
            if (!CanInvoke(arg))
                return false;
    
            _action?.Invoke(arg);
            OnEvent?.Invoke(arg);
            return true;
        }
    
    #if ODIN_INSPECTOR
        [Button]
    #endif
        /// <inheritdoc/>
        public void Invoke(T arg)
        {
            if (!CanInvoke(arg))
                return;
    
            _action?.Invoke(arg);
            OnEvent?.Invoke(arg);
        }
    
        /// <inheritdoc/>
        public ICommand<T> AddCondition(Func<T, bool> condition)
        {
            if (_count == _conditions.Length)
                Array.Resize(ref _conditions, _conditions.Length * 2);
    
            _conditions[_count++] = condition;
            return this;
        }
    
        /// <inheritdoc/>
        public ICommand<T> RemoveCondition(Func<T, bool> condition)
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
    
        /// <inheritdoc/>
        public ICommand<T> AddAction(Action<T> action)
        {
            _action += action;
            return this;
        }
    
        /// <inheritdoc/>
        public ICommand<T> RemoveAction(Action<T> action)
        {
            _action -= action;
            return this;
        }
    }

    /// <summary>
    /// Default implementation of <see cref="ICommand{T1, T2}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first input parameter.</typeparam>
    /// <typeparam name="T2">The type of the second input parameter.</typeparam>
    /// <remarks>
    /// Stores a collection of execution conditions and actions.
    /// The command can be invoked only when all registered conditions evaluate
    /// to <see langword="true"/> for the specified arguments. When successfully
    /// invoked, all registered actions are executed and the
    /// <see cref="OnEvent"/> event is raised.
    /// </remarks>
    public sealed class Command<T1, T2> : ICommand<T1, T2>
    {
        /// <inheritdoc/>
        public event Action<T1, T2> OnEvent;
    
        private Func<T1, T2, bool>[] _conditions = new Func<T1, T2, bool>[4];
        private int _count;
    
        private Action<T1, T2> _action;
    
    #if ODIN_INSPECTOR
        [Button]
    #endif
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public bool TryInvoke(T1 arg1, T2 arg2)
        {
            if (!CanInvoke(arg1, arg2))
                return false;
    
            _action?.Invoke(arg1, arg2);
            OnEvent?.Invoke(arg1, arg2);
            return true;
        }
    
    #if ODIN_INSPECTOR
        [Button]
    #endif
        /// <inheritdoc/>
        public void Invoke(T1 arg1, T2 arg2)
        {
            if (!CanInvoke(arg1, arg2))
                return;
    
            _action?.Invoke(arg1, arg2);
            OnEvent?.Invoke(arg1, arg2);
        }
    
        /// <inheritdoc/>
        public ICommand<T1, T2> AddCondition(Func<T1, T2, bool> condition)
        {
            if (_count == _conditions.Length)
                Array.Resize(ref _conditions, _conditions.Length * 2);
    
            _conditions[_count++] = condition;
            return this;
        }
    
        /// <inheritdoc/>
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
    
        /// <inheritdoc/>
        public ICommand<T1, T2> AddAction(Action<T1, T2> action)
        {
            _action += action;
            return this;
        }
    
        /// <inheritdoc/>
        public ICommand<T1, T2> RemoveAction(Action<T1, T2> action)
        {
            _action -= action;
            return this;
        }
    }

    /// <summary>
    /// Default implementation of <see cref="ICommand{T1, T2, T3}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first input parameter.</typeparam>
    /// <typeparam name="T2">The type of the second input parameter.</typeparam>
    /// <typeparam name="T3">The type of the third input parameter.</typeparam>
    /// <remarks>
    /// Stores a collection of execution conditions and actions.
    /// The command can be invoked only when all registered conditions evaluate
    /// to <see langword="true"/> for the specified arguments. When successfully
    /// invoked, all registered actions are executed and the
    /// <see cref="OnEvent"/> event is raised.
    /// </remarks>
    public sealed class Command<T1, T2, T3> : ICommand<T1, T2, T3>
    {
        /// <inheritdoc/>
        public event Action<T1, T2, T3> OnEvent;
    
        private Func<T1, T2, T3, bool>[] _conditions = new Func<T1, T2, T3, bool>[4];
        private int _count;
    
        private Action<T1, T2, T3> _action;
    
    #if ODIN_INSPECTOR
        [Button]
    #endif
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3)
        {
            if (!CanInvoke(arg1, arg2, arg3))
                return false;
    
            _action?.Invoke(arg1, arg2, arg3);
            OnEvent?.Invoke(arg1, arg2, arg3);
            return true;
        }
    
    #if ODIN_INSPECTOR
        [Button]
    #endif
        /// <inheritdoc/>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            if (!CanInvoke(arg1, arg2, arg3))
                return;
    
            _action?.Invoke(arg1, arg2, arg3);
            OnEvent?.Invoke(arg1, arg2, arg3);
        }
    
        /// <inheritdoc/>
        public ICommand<T1, T2, T3> AddCondition(Func<T1, T2, T3, bool> condition)
        {
            if (_count == _conditions.Length)
                Array.Resize(ref _conditions, _conditions.Length * 2);
    
            _conditions[_count++] = condition;
            return this;
        }
    
        /// <inheritdoc/>
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
    
        /// <inheritdoc/>
        public ICommand<T1, T2, T3> AddAction(Action<T1, T2, T3> action)
        {
            _action += action;
            return this;
        }
    
        /// <inheritdoc/>
        public ICommand<T1, T2, T3> RemoveAction(Action<T1, T2, T3> action)
        {
            _action -= action;
            return this;
        }
    }

    /// <summary>
    /// Default implementation of <see cref="ICommand{T1, T2, T3, T4}"/>.
    /// </summary>
    /// <typeparam name="T1">The type of the first input parameter.</typeparam>
    /// <typeparam name="T2">The type of the second input parameter.</typeparam>
    /// <typeparam name="T3">The type of the third input parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth input parameter.</typeparam>
    /// <remarks>
    /// Stores a collection of execution conditions and actions.
    /// The command can be invoked only when all registered conditions evaluate
    /// to <see langword="true"/> for the specified arguments. When successfully
    /// invoked, all registered actions are executed and the
    /// <see cref="OnEvent"/> event is raised.
    /// </remarks>
    public sealed class Command<T1, T2, T3, T4> : ICommand<T1, T2, T3, T4>
    {
        /// <inheritdoc/>
        public event Action<T1, T2, T3, T4> OnEvent;
    
        private Func<T1, T2, T3, T4, bool>[] _conditions = new Func<T1, T2, T3, T4, bool>[4];
        private int _count;
    
        private Action<T1, T2, T3, T4> _action;
    
    #if ODIN_INSPECTOR
        [Button]
    #endif
        /// <inheritdoc/>
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
        /// <inheritdoc/>
        public bool TryInvoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (!CanInvoke(arg1, arg2, arg3, arg4))
                return false;
    
            _action?.Invoke(arg1, arg2, arg3, arg4);
            OnEvent?.Invoke(arg1, arg2, arg3, arg4);
            return true;
        }
    
    #if ODIN_INSPECTOR
        [Button]
    #endif
        /// <inheritdoc/>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            if (!CanInvoke(arg1, arg2, arg3, arg4))
                return;
    
            _action?.Invoke(arg1, arg2, arg3, arg4);
            OnEvent?.Invoke(arg1, arg2, arg3, arg4);
        }
    
        /// <inheritdoc/>
        public ICommand<T1, T2, T3, T4> AddCondition(Func<T1, T2, T3, T4, bool> condition)
        {
            if (_count == _conditions.Length)
                Array.Resize(ref _conditions, _conditions.Length * 2);
    
            _conditions[_count++] = condition;
            return this;
        }
    
        /// <inheritdoc/>
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
    
        /// <inheritdoc/>
        public ICommand<T1, T2, T3, T4> AddAction(Action<T1, T2, T3, T4> action)
        {
            _action += action;
            return this;
        }
    
        /// <inheritdoc/>
        public ICommand<T1, T2, T3, T4> RemoveAction(Action<T1, T2, T3, T4> action)
        {
            _action -= action;
            return this;
        }
    }
}
