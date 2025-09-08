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
    public class InlineAction : IAction
    {
        private readonly Action action;

        /// <summary>
        /// Initializes a new instance of <see cref="InlineAction"/> with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public InlineAction(Action action) => this.action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action"/> to <see cref="InlineAction"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator InlineAction(Action value) => new(value);

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action, if it exists.
        /// </summary>
        public void Invoke() => this.action.Invoke();

        public override string ToString() => this.action.Method.Name;
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    /// <summary>
    /// Represents an action with one parameter that can be invoked.
    /// Wraps a <see cref="System.Action{T}"/> delegate.
    /// </summary>
    public class InlineAction<T> : IAction<T>
    {
        private readonly Action<T> action;

        /// <summary>
        /// Initializes a new instance with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public InlineAction(Action<T> action) =>
            this.action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action{T}"/> to <see cref="InlineAction{T}"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator InlineAction<T>(Action<T> value) => new(value);

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action with the specified argument.
        /// </summary>
        /// <param name="arg">The argument to pass to the action.</param>
        public void Invoke(T arg) => this.action.Invoke(arg);

        public override string ToString() => this.action.Method.Name;
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    /// <summary>
    /// Represents an action with two parameters that can be invoked.
    /// Wraps a <see cref="System.Action{T1, T2}"/> delegate.
    /// </summary>
    public class InlineAction<T1, T2> : IAction<T1, T2>
    {
        private readonly Action<T1, T2> action;

        /// <summary>
        /// Initializes a new instance with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public InlineAction(Action<T1, T2> action) =>
            this.action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action{T1, T2}"/> to <see cref="InlineAction{T1,T2}"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator InlineAction<T1, T2>(Action<T1, T2> value) => new(value);

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action with the specified arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        public void Invoke(T1 arg1, T2 arg2) => this.action.Invoke(arg1, arg2);

        public override string ToString() => this.action.Method.Name;
    }

#if ODIN_INSPECTOR
    [InlineProperty]
#endif
    /// <summary>
    /// Represents an action with three parameters that can be invoked.
    /// Wraps a <see cref="System.Action{T1, T2, T3}"/> delegate.
    /// </summary>
    public class InlineAction<T1, T2, T3> : IAction<T1, T2, T3>
    {
        private readonly Action<T1, T2, T3> action;

        /// <summary>
        /// Initializes a new instance with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public InlineAction(Action<T1, T2, T3> action) =>
            this.action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action{T1, T2, T3}"/> to <see cref="InlineAction{T1,T2,T3}"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator InlineAction<T1, T2, T3>(Action<T1, T2, T3> value) => new(value);

#if ODIN_INSPECTOR
        [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action with the specified arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3) => this.action.Invoke(arg1, arg2, arg3);

        public override string ToString() => this.action.Method.Name;
    }

#if ODIN_INSPECTOR
[InlineProperty]
#endif
    /// <summary>
    /// Represents an action with four parameters that can be invoked.
    /// Wraps a <see cref="System.Action{T1, T2, T3, T4}"/> delegate.
    /// </summary>
    public class InlineAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
    {
        private readonly Action<T1, T2, T3, T4> action;

        /// <summary>
        /// Initializes a new instance with the specified action.
        /// </summary>
        /// <param name="action">The action to invoke.</param>
        public InlineAction(Action<T1, T2, T3, T4> action) =>
            this.action = action ?? throw new ArgumentNullException(nameof(action));

        /// <summary>
        /// Allows implicit conversion from <see cref="System.Action{T1, T2, T3, T4}"/> 
        /// to <see cref="InlineAction{T1, T2, T3, T4}"/>.
        /// </summary>
        /// <param name="value">The action to wrap.</param>
        public static implicit operator InlineAction<T1, T2, T3, T4>(Action<T1, T2, T3, T4> value) => new(value);

#if ODIN_INSPECTOR
    [Button]
#endif
        /// <summary>
        /// Invokes the wrapped action with the specified arguments.
        /// </summary>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4) => this.action.Invoke(arg1, arg2, arg3, arg4);

        public override string ToString() => this.action.Method.Name;
    }
}