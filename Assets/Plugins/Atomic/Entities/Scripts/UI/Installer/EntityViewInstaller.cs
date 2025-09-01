#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Base class for components that handle the installation of <see cref="EntityView"/> instances.
    /// Inherit from this class to implement custom installation logic for entity views.
    /// </summary>
    public abstract class EntityViewInstaller : MonoBehaviour
    {
        /// <summary>
        /// Performs the installation logic for the specified <see cref="EntityView"/>.
        /// Must be implemented in derived classes.
        /// </summary>
        /// <param name="view">The <see cref="EntityView"/> instance to install.</param>
        public abstract void Install(EntityView view);
    }
}
#endif