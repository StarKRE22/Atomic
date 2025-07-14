using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// An abstract base class for scene-based actions that implement the <see cref="IAction"/> interface.
    /// Inherit from this class to define custom actions as MonoBehaviours.
    /// </summary>
    public abstract class SceneActionAbstract : MonoBehaviour, IAction
    {
        /// <summary>
        /// Executes the action logic.
        /// Must be implemented by derived classes.
        /// </summary>
        public abstract void Invoke();
    }
}