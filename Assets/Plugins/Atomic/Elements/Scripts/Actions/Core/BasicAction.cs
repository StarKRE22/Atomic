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
    public class BasicAction : IAction
    {
        private readonly Action action;
        
        /// <summary>
        /// Initializes a new instance of <see cref="BasicAction"/> with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public BasicAction(Action action) => this.action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action"/> to <see cref="BasicAction"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator BasicAction(Action value) => new(value);

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action, if it exists.
        /// </summary>
        public void Invoke() => this.action?.Invoke();
        
        public override string ToString() => this.action.Method.Name;
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    /// <summary>
    /// Represents an action with one parameter that can be invoked.
    /// Wraps a <see cref="System.Action{T}"/> delegate.
    /// </summary>
    public class BasicAction<T> : IAction<T>
    {
        private readonly Action<T> action;

        /// <summary>
        /// Initializes a new instance with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public BasicAction(Action<T> action) => this.action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action{T}"/> to <see cref="BasicAction{T}"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator BasicAction<T>(Action<T> value) => new(value);

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action with the specified argument.
        /// </summary>
        /// <param name="arg">The argument to pass to the action.</param>
        public void Invoke(T arg) => this.action?.Invoke(arg);
        
        public override string ToString() => this.action.Method.Name;
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    /// <summary>
    /// Represents an action with two parameters that can be invoked.
    /// Wraps a <see cref="System.Action{T1, T2}"/> delegate.
    /// </summary>
    public class BasicAction<T1, T2> : IAction<T1, T2>
    {
        private readonly Action<T1, T2> action;

        /// <summary>
        /// Initializes a new instance with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public BasicAction(Action<T1, T2> action) =>
            this.action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action{T1, T2}"/> to <see cref="BasicAction{T1,T2}"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator BasicAction<T1, T2>(Action<T1, T2> value) => new(value);

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action with the specified arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        public void Invoke(T1 arg1, T2 arg2) => this.action?.Invoke(arg1, arg2);
        
        public override string ToString() => this.action.Method.Name;
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    /// <summary>
    /// Represents an action with three parameters that can be invoked.
    /// Wraps a <see cref="System.Action{T1, T2, T3}"/> delegate.
    /// </summary>
    public class BasicAction<T1, T2, T3> : IAction<T1, T2, T3>
    {
        private readonly Action<T1, T2, T3> action;

        /// <summary>
        /// Initializes a new instance with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public BasicAction(Action<T1, T2, T3> action) =>
            this.action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action{T1, T2, T3}"/> to <see cref="BasicAction{T1,T2,T3}"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator BasicAction<T1, T2, T3>(Action<T1, T2, T3> value) => new(value);

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action with the specified arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3) => this.action?.Invoke(arg1, arg2, arg3);
        
        public override string ToString() => this.action.Method.Name;
    }
}