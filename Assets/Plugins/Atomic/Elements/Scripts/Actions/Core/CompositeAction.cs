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
}