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
    /// Executes all contained actions sequentially when invoked.
    /// </summary>
    [Serializable]
    public class CompositeAction : IAction
    {
        /// <summary>
        /// Array of actions that belong to this group.
        /// These actions will be invoked in order when the group is triggered.
        /// </summary>
#if UNITY_5_3_OR_NEWER
        [Space, SerializeReference]
#endif
        private IAction[] actions;

        /// <summary>
        /// Initializes the group with the given actions.
        /// </summary>
        /// <param name="actions">A list of actions to include in the group.</param>
        public CompositeAction(params IAction[] actions) =>
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));

        public CompositeAction(IEnumerable<IAction> actions) => this.actions =
            actions != null ? actions.ToArray() : throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Invokes all actions in the group sequentially.
        /// </summary>
        public void Invoke() => this.actions.InvokeRange();
    }

    /// <summary>
    /// Executes a sequence of actions with one parameter.
    /// </summary>
    /// <typeparam name="T1">Type of the input parameter.</typeparam>
    [Serializable]
    public class CompositeAction<T1> : IAction<T1>
    {
        /// <summary>
        /// The actions that will be invoked in sequence.
        /// </summary>
#if UNITY_5_3_OR_NEWER
        [Space, SerializeReference]
#endif
        private IAction<T1>[] actions;

        /// <summary>
        /// Creates a composite action from the given actions.
        /// </summary>
        public CompositeAction(params IAction<T1>[] actions) =>
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Creates a composite action from an enumerable of actions.
        /// </summary>
        public CompositeAction(IEnumerable<IAction<T1>> actions) =>
            this.actions = actions?.ToArray() ?? throw new ArgumentNullException(nameof(actions));

        /// <summary>
        /// Invokes all actions sequentially with the provided argument.
        /// </summary>
        public void Invoke(T1 arg1)
        {
            foreach (var action in actions)
                action.Invoke(arg1);
        }
    }

    /// <summary>
    /// Executes a sequence of actions with two parameters.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter.</typeparam>
    /// <typeparam name="T2">Type of the second parameter.</typeparam>
    [Serializable]
    public class CompositeAction<T1, T2> : IAction<T1, T2>
    {
#if UNITY_5_3_OR_NEWER
        [Space, SerializeReference]
#endif
        private IAction<T1, T2>[] actions;

        public CompositeAction(params IAction<T1, T2>[] actions) =>
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));

        public CompositeAction(IEnumerable<IAction<T1, T2>> actions) =>
            this.actions = actions?.ToArray() ?? throw new ArgumentNullException(nameof(actions));

        public void Invoke(T1 arg1, T2 arg2)
        {
            foreach (var action in actions)
                action.Invoke(arg1, arg2);
        }
    }

    /// <summary>
    /// Executes a sequence of actions with three parameters.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter.</typeparam>
    /// <typeparam name="T2">Type of the second parameter.</typeparam>
    /// <typeparam name="T3">Type of the third parameter.</typeparam>
    [Serializable]
    public class CompositeAction<T1, T2, T3> : IAction<T1, T2, T3>
    {
#if UNITY_5_3_OR_NEWER
        [Space, SerializeReference]
#endif
        private IAction<T1, T2, T3>[] actions;

        public CompositeAction(params IAction<T1, T2, T3>[] actions) =>
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));

        public CompositeAction(IEnumerable<IAction<T1, T2, T3>> actions) =>
            this.actions = actions?.ToArray() ?? throw new ArgumentNullException(nameof(actions));

        public void Invoke(T1 arg1, T2 arg2, T3 arg3)
        {
            foreach (var action in actions)
                action.Invoke(arg1, arg2, arg3);
        }
    }

    /// <summary>
    /// Executes a sequence of actions with four parameters.
    /// </summary>
    /// <typeparam name="T1">Type of the first parameter.</typeparam>
    /// <typeparam name="T2">Type of the second parameter.</typeparam>
    /// <typeparam name="T3">Type of the third parameter.</typeparam>
    /// <typeparam name="T4">Type of the fourth parameter.</typeparam>
    [Serializable]
    public class CompositeAction<T1, T2, T3, T4> : IAction<T1, T2, T3, T4>
    {
#if UNITY_5_3_OR_NEWER
        [Space, SerializeReference]
#endif
        private IAction<T1, T2, T3, T4>[] actions;

        public CompositeAction(params IAction<T1, T2, T3, T4>[] actions) =>
            this.actions = actions ?? throw new ArgumentNullException(nameof(actions));

        public CompositeAction(IEnumerable<IAction<T1, T2, T3, T4>> actions) =>
            this.actions = actions?.ToArray() ?? throw new ArgumentNullException(nameof(actions));

        public void Invoke(T1 arg1, T2 arg2, T3 arg3, T4 arg4)
        {
            foreach (var action in actions)
                action.Invoke(arg1, arg2, arg3, arg4);
        }
    }
}