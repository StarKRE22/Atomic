#if UNITY_5_3_OR_NEWER
using System;
using UnityEngine;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif


namespace Atomic.Elements
{
    /// <summary>
    /// A pointer wrapper for invoking a <see cref="SceneActionDefault"/> from outside the scene context.
    /// </summary>
    /// <remarks>
    /// This class allows referencing and triggering a <see cref="SceneActionDefault"/> as a serializable field,
    /// often useful in serialized action chains or as part of a logic asset.
    /// </remarks>
    [Serializable]
    public sealed class SceneActionPointer : IAction
    {
        /// <summary>
        /// Reference to the actual <see cref="SceneActionDefault"/> component to be invoked.
        /// </summary>
        [SerializeField]
#if ODIN_INSPECTOR
        [SceneObjectsOnly, Required]
#endif
        private SceneActionDefault action;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public SceneActionPointer()
        {
        }

        /// <summary>
        /// Creates a new <see cref="SceneActionPointer"/> with the specified scene action.
        /// </summary>
        /// <param name="action">The <see cref="SceneActionDefault"/> to wrap.</param>
        public SceneActionPointer(SceneActionDefault action) => this.action = action;

        /// <summary>
        /// Assigns a <see cref="SceneActionDefault"/> to this pointer.
        /// </summary>
        /// <param name="action">The scene action to assign.</param>
        public void Construct(SceneActionDefault action) => this.action = action;

        /// <summary>
        /// Invokes the referenced <see cref="SceneActionDefault"/>, if it exists.
        /// </summary>
        public void Invoke()
        {
            if (this.action)
                this.action.Invoke();
        }
    }
}
#endif