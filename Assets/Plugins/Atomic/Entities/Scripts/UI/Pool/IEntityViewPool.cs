namespace Atomic.Entities
{
    /// <summary>
    /// Defines a pool for managing <see cref="EntityView"/> instances.
    /// Provides methods to rent, return, and clear pooled views to optimize memory and performance.
    /// </summary>
    public interface IEntityViewPool
    {
        /// <summary>
        /// Retrieves a view instance from the pool associated with the specified name.
        /// If no available instance exists, a new one may be created.
        /// </summary>
        /// <param name="name">The name identifying the type of view to rent.</param>
        /// <returns>An active <see cref="EntityView"/> instance.</returns>
        IReadOnlyEntityView Rent(string name);

        /// <summary>
        /// Returns a previously rented view instance back to the pool for reuse.
        /// </summary>
        /// <param name="name">The name identifying the type of view being returned.</param>
        /// <param name="view">The <see cref="EntityView"/> instance to return to the pool.</param>
        void Return(string name, IReadOnlyEntityView view);

        /// <summary>
        /// Clears all view instances from the pool, releasing any resources held.
        /// </summary>
        void Clear();
    }
}