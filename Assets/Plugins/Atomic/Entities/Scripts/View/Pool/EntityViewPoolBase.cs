#if UNITY_5_3_OR_NEWER
using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// Base class for implementing a pool of <see cref="EntityViewBase"/> instances.
    /// Provides abstract methods for renting, returning, and clearing views.
    /// Inherit from this class to implement custom pooling logic.
    /// </summary>
    public abstract class EntityViewPoolBase : MonoBehaviour, IEntityViewPool
    {
        /// <summary>
        /// Retrieves a view instance from the pool associated with the specified name.
        /// If no available instance exists, a new one may be created.
        /// Must be implemented in derived classes.
        /// </summary>
        /// <param name="name">The name identifying the type of view to rent.</param>
        /// <returns>An active <see cref="EntityViewBase"/> instance.</returns>
        public abstract EntityViewBase Rent(string name);

        IReadOnlyEntityView IEntityViewPool.Rent(string name) => this.Rent(name);

        /// <summary>
        /// Returns a previously rented view instance back to the pool for reuse.
        /// Must be implemented in derived classes.
        /// </summary>
        /// <param name="name">The name identifying the type of view being returned.</param>
        /// <param name="view">The <see cref="EntityViewBase"/> instance to return to the pool.</param>
        public abstract void Return(string name, EntityViewBase view);

        void IEntityViewPool.Return(string name, IReadOnlyEntityView view) => this.Return(name, (EntityViewBase) view);

        /// <summary>
        /// Clears all view instances from the pool, releasing any resources held.
        /// Must be implemented in derived classes.
        /// </summary>
        public abstract void Clear();
    }
}
#endif