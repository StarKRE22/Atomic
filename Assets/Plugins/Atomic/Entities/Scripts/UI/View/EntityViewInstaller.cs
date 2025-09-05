#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Base class for components that handle the installation of <see cref="BehaviourEntityView"/> instances.
    /// Inherit from this class to implement custom installation logic for entity views.
    /// </summary>
    public abstract class EntityViewInstaller : MonoBehaviour
    {
        /// <summary>
        /// Performs the installation logic for the specified <see cref="BehaviourEntityView"/>.
        /// Must be implemented in derived classes.
        /// </summary>
        /// <param name="view">The <see cref="BehaviourEntityView"/> instance to install.</param>
        public abstract void Install(BehaviourEntityView view);
    }
}
#endif