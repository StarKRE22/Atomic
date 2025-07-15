using System;
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// A pointer wrapper for invoking a <see cref="SceneAction"/> from outside the scene context.
    /// </summary>
    /// <remarks>
    /// This class allows referencing and triggering a <see cref="SceneAction"/> as a serializable field,
    /// often useful in serialized action chains or as part of a logic asset.
    /// </remarks>
    [Serializable]
    public sealed class SceneActionPointer : IAction
    {
        /// <summary>
        /// Reference to the actual <see cref="SceneAction"/> component to be invoked.
        /// </summary>
        [SerializeField]
        private SceneAction action;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SceneActionPointer()
        {
        }

        /// <summary>
        /// Creates a new <see cref="SceneActionPointer"/> with the specified scene action.
        /// </summary>
        /// <param name="action">The <see cref="SceneAction"/> to wrap.</param>
        public SceneActionPointer(SceneAction action) => this.action = action;

        /// <summary>
        /// Assigns a <see cref="SceneAction"/> to this pointer.
        /// </summary>
        /// <param name="action">The scene action to assign.</param>
        public void Construct(SceneAction action) => this.action = action;

        /// <summary>
        /// Invokes the referenced <see cref="SceneAction"/>, if it exists.
        /// </summary>
        public void Invoke()
        {
            if (this.action)
                this.action.Invoke();
        }
    }
}