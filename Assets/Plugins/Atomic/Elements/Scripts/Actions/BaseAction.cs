using System;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Elements
{
#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    /// <summary>
    /// Represents a parameterless action that can be invoked.
    /// Wraps a standard <see cref="System.Action"/> delegate.
    /// </summary>
    [Serializable]
    public class BaseAction : IAction
    {
        private Action action;

        /// <summary>
        /// Initializes a new, empty <see cref="BaseAction"/>.
        /// </summary>
        public BaseAction()
        {
        }

        /// <summary>
        /// Initializes a new instance of <see cref="BaseAction"/> with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public BaseAction(Action action) => this.action = action;

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action"/> to <see cref="BaseAction"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator BaseAction(Action value) => new BaseAction(value);

        /// <summary>
        /// Sets or replaces the internal action.
        /// </summary>
        /// <param name="action">The new action to assign.</param>
        /// <returns>The current instance for chaining.</returns>
        public void Construct(Action action) => this.action = action;

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action, if it exists.
        /// </summary>
        public void Invoke() => this.action?.Invoke();
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    /// <summary>
    /// Represents an action with one parameter that can be invoked.
    /// Wraps a <see cref="System.Action{T}"/> delegate.
    /// </summary>
    [Serializable]
    public class BaseAction<T> : IAction<T>
    {
        private Action<T> action;

        /// <summary>
        /// Initializes a new, empty <see cref="BaseAction{T}"/>.
        /// </summary>
        public BaseAction()
        {
        }

        /// <summary>
        /// Initializes a new instance with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public BaseAction(Action<T> action) => this.action = action;

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action{T}"/> to <see cref="BaseAction{T}"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator BaseAction<T>(Action<T> value) => new BaseAction<T>(value);

        /// <summary>
        /// Sets or replaces the internal action.
        /// </summary>
        /// <param name="action">The new action to assign.</param>
        /// <returns>The current instance for chaining.</returns>
        public void Construct(Action<T> action) => this.action = action;

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action with the specified argument.
        /// </summary>
        /// <param name="arg">The argument to pass to the action.</param>
        public void Invoke(T arg) => this.action?.Invoke(arg);
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    /// <summary>
    /// Represents an action with two parameters that can be invoked.
    /// Wraps a <see cref="System.Action{T1, T2}"/> delegate.
    /// </summary>
    [Serializable]
    public class BaseAction<T1, T2> : IAction<T1, T2>
    {
        private Action<T1, T2> action;

        /// <summary>
        /// Initializes a new, empty <see cref="BaseAction{T1, T2}"/>.
        /// </summary>
        public BaseAction()
        {
        }

        /// <summary>
        /// Initializes a new instance with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public BaseAction(Action<T1, T2> action) => this.action = action;

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action{T1, T2}"/> to <see cref="BaseAction{T1, T2}"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator BaseAction<T1, T2>(Action<T1, T2> value) => new(value);

        /// <summary>
        /// Sets or replaces the internal action.
        /// </summary>
        /// <param name="action">The new action to assign.</param>
        /// <returns>The current instance for chaining.</returns>
        public void Construct(Action<T1, T2> action) => this.action = action;

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action with the specified arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        public void Invoke(T1 arg1, T2 arg2) => this.action?.Invoke(arg1, arg2);
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    /// <summary>
    /// Represents an action with three parameters that can be invoked.
    /// Wraps a <see cref="System.Action{T1, T2, T3}"/> delegate.
    /// </summary>
    [Serializable]
    public class BaseAction<T1, T2, T3> : IAction<T1, T2, T3>
    {
        private Action<T1, T2, T3> action;

        /// <summary>
        /// Initializes a new, empty <see cref="BaseAction{T1, T2, T3}"/>.
        /// </summary>
        public BaseAction()
        {
        }

        /// <summary>
        /// Initializes a new instance with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public BaseAction(Action<T1, T2, T3> action) => this.action = action;

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action{T1, T2, T3}"/> to <see cref="BaseAction{T1, T2, T3}"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator BaseAction<T1, T2, T3>(Action<T1, T2, T3> value) => new(value);

        /// <summary>
        /// Sets or replaces the internal action.
        /// </summary>
        /// <param name="action">The new action to assign.</param>
        /// <returns>The current instance for chaining.</returns>
        public void Construct(Action<T1, T2, T3> action) => this.action = action;

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action with the specified arguments.
        /// </summary>
        /// <param name="args1">The first argument.</param>
        /// <param name="args2">The second argument.</param>
        /// <param name="args3">The third argument.</param>
        public void Invoke(T1 args1, T2 args2, T3 args3) => this.action?.Invoke(args1, args2, args3);
    }
}
