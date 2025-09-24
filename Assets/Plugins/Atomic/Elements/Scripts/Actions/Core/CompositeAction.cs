using System;
using System.Collections.Generic;
using System.Linq;

#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

namespace Atomic.Elements
{
    /// <summary>
    /// Represents a group of actions that implement the <see cref="IAction"/> interface.
    /// Follows the Composite design pattern: the group itself behaves as a single action,
    /// while internally invoking all contained actions sequentially.
    /// </summary>
    [Serializable]
    public class CompositeAction : IAction
    {
        /// <summary>
        /// Collection of actions that belong to this group.
        /// These actions will be invoked in order when the group is triggered.
        /// </summary>
#if UNITY_5_3_OR_NEWER
        [Space, SerializeReference]
#endif
        private IAction[] actions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.
        /// It allows the inspector to create and serialize a default instance of <see cref="CompositeAction"/>.
        /// </remarks>
        public CompositeAction()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction"/> class with the given actions.
        /// </summary>
        /// <param name="actions">One or more actions to include in the group.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="actions"/> is null.</exception>
        public CompositeAction(params IAction[] actions) =>
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction"/> class with the given actions.
        /// </summary>
        /// <param name="actions">A collection of actions to include in the group.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="actions"/> is null.</exception>
        public CompositeAction(IEnumerable<IAction> actions) =>
            this.actions = actions != null ? actions.ToArray() : throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Invokes all actions in the group sequentially.
        /// </summary>
        public void Invoke()
        {
            for (int i = 0, count = actions.Length; i < count; i++)
                this.actions[i].Invoke();
        }
    }

    /// <summary>
    /// Represents a group of actions with one parameter that implement the <see cref="IAction{T1}"/> interface.
    /// Follows the Composite design pattern and executes all contained actions sequentially with the provided argument.
    /// </summary>
    /// <typeparam name="T">The type of the input parameter.</typeparam>
    [Serializable]
    public class CompositeAction<T> : IAction<T>
    {
        /// <summary>
        /// Collection of actions that belong to this group.
        /// </summary>
#if UNITY_5_3_OR_NEWER
        [Space, SerializeReference]
#endif
        private IAction<T>[] actions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.
        /// It allows the inspector to create and serialize a default instance of <see cref="CompositeAction"/>.
        /// </remarks>
        public CompositeAction()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction{T1}"/> class with the given actions.
        /// </summary>
        /// <param name="actions">One or more actions to include in the group.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="actions"/> is null.</exception>
        public CompositeAction(params IAction<T>[] actions) =>
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction{T1}"/> class with the given actions.
        /// </summary>
        /// <param name="actions">A collection of actions to include in the group.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="actions"/> is null.</exception>
        public CompositeAction(IEnumerable<IAction<T>> actions) =>
            this.actions = actions?.ToArray() ?? throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Invokes all actions sequentially with the provided argument.
        /// </summary>
        /// <param name="arg1">The argument passed to each action.</param>
        public void Invoke(T arg)
        {
            for (int i = 0, count = this.actions.Length; i < count; i++)
                this.actions[i].Invoke(arg);
        }
    }

    /// <summary>
    /// Represents a group of actions with two parameters that implement the <see cref="IAction{T1,T2}"/> interface.
    /// Executes all contained actions sequentially with the provided arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    [Serializable]
    public class CompositeAction<T1, T2> : IAction<T1, T2>
    {
#if UNITY_5_3_OR_NEWER
        [Space, SerializeReference]
#endif
        private IAction<T1, T2>[] actions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.
        /// It allows the inspector to create and serialize a default instance of <see cref="CompositeAction"/>.
        /// </remarks>
        public CompositeAction()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction{T1,T2}"/> class with the given actions.
        /// </summary>
        /// <param name="actions">One or more actions to include in the group.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="actions"/> is null.</exception>
        public CompositeAction(params IAction<T1, T2>[] actions) =>
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction{T1,T2}"/> class with the given actions.
        /// </summary>
        /// <param name="actions">A collection of actions to include in the group.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="actions"/> is null.</exception>
        public CompositeAction(IEnumerable<IAction<T1, T2>> actions) =>
            this.actions = actions?.ToArray() ?? throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Invokes all actions sequentially with the provided arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        public void Invoke(T1 arg1, T2 arg2)
        {
            for (int i = 0, count = actions.Length; i < count; i++)
                this.actions[i].Invoke(arg1, arg2);
        }
    }

    /// <summary>
    /// Represents a group of actions with three parameters that implement the <see cref="IAction{T1,T2,T3}"/> interface.
    /// Executes all contained actions sequentially with the provided arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    [Serializable]
    public class CompositeAction<T1, T2, T3> : IAction<T1, T2, T3>
    {
#if UNITY_5_3_OR_NEWER
        [Space, SerializeReference]
#endif
        private IAction<T1, T2, T3>[] actions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.
        /// It allows the inspector to create and serialize a default instance of <see cref="CompositeAction"/>.
        /// </remarks>
        public CompositeAction()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction{T1,T2,T3}"/> class with the given actions.
        /// </summary>
        /// <param name="actions">One or more actions to include in the group.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="actions"/> is null.</exception>
        public CompositeAction(params IAction<T1, T2, T3>[] actions) =>
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction{T1,T2,T3}"/> class with the given actions.
        /// </summary>
        /// <param name="actions">A collection of actions to include in the group.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="actions"/> is null.</exception>
        public CompositeAction(IEnumerable<IAction<T1, T2, T3>> actions) =>
            this.actions = actions?.ToArray() ?? throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Invokes all actions sequentially with the provided arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            for (int i = 0, count = actions.Length; i < count; i++)
                this.actions[i].Invoke(arg1, arg2, arg3);
        }
    }

    /// <summary>
    /// Represents a group of actions with four parameters that implement the <see cref="IAction{T1,T2,T3,T4}"/> interface.
    /// Executes all contained actions sequentially with the provided arguments.
    /// </summary>
    /// <typeparam name="T1">The type of the first parameter.</typeparam>
    /// <typeparam name="T2">The type of the second parameter.</typeparam>
    /// <typeparam name="T3">The type of the third parameter.</typeparam>
    /// <typeparam name="T4">The type of the fourth parameter.</typeparam>
    [Serializable]
    public class CompositeAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
    {
#if UNITY_5_3_OR_NEWER
        [Space, SerializeReference]
#endif
        private IAction<T1, T2, T3, T4>[] actions;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction"/> class.
        /// </summary>
        /// <remarks>
        /// This constructor is intended **only for use by the Unity Inspector** when using `[SerializeReference]`.
        /// It allows the inspector to create and serialize a default instance of <see cref="CompositeAction"/>.
        /// </remarks>
        public CompositeAction()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction{T1,T2,T3,T4}"/> class with the given actions.
        /// </summary>
        /// <param name="actions">One or more actions to include in the group.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="actions"/> is null.</exception>
        public CompositeAction(params IAction<T1, T2, T3, T4>[] actions) =>
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Initializes a new instance of the <see cref="CompositeAction{T1,T2,T3,T4}"/> class with the given actions.
        /// </summary>
        /// <param name="actions">A collection of actions to include in the group.</param>
        /// <exception cref="ArgumentNullException">Thrown if <paramref name="actions"/> is null.</exception>
        public CompositeAction(IEnumerable<IAction<T1, T2, T3, T4>> actions) =>
            this.actions = actions?.ToArray() ?? throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Invokes all actions sequentially with the provided arguments.
        /// </summary>
        /// <param name="arg1">The first argument.</param>
        /// <param name="arg2">The second argument.</param>
        /// <param name="arg3">The third argument.</param>
        /// <param name="arg4">The fourth argument.</param>
        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            for (int i = 0, count = actions.Length; i < count; i++)
                this.actions[i].Invoke(arg1, arg2, arg3, arg4);
        }
    }
}