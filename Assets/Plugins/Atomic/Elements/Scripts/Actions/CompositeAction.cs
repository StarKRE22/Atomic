using System;
#if UNITY_5_3_OR_NEWER
using UnityEngine;
#endif

// ReSharper disable NotAccessedField.Local

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
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
        /// Default constructor. Initializes an empty action group.
        /// </summary>
        public CompositeAction()
        {
        }

        /// <summary>
        /// Initializes the group with the given actions.
        /// </summary>
        /// <param name="actions">A list of actions to include in the group.</param>
        public CompositeAction(params IAction[] actions) => this.actions = actions;

        /// <summary>
        /// Replaces the current action list with a new set of actions.
        /// </summary>
        /// <param name="actions">The actions to assign to this group.</param>
        public void Construct(params IAction[] actions) => this.actions = actions;

        /// <summary>
        /// Invokes all actions in the group sequentially.
        /// </summary>
        public void Invoke() => this.actions.InvokeRange();
    }
}