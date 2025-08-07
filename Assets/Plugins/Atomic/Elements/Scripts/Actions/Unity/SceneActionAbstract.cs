#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Elements
{
    /// <summary>
    /// An abstract class for scene-based actions that implement the <see cref="IAction"/> interface.
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
#endif