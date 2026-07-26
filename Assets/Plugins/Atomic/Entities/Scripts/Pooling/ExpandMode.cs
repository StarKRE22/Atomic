namespace Atomic.Entities
{
    /// <summary>
    /// Determines how a pool behaves when it runs out of pre-instantiated entities
    /// and a new entity is requested via <see cref="IEntityPool{TEntity}.Rent"/>.
    /// </summary>
    public enum ExpandMode
    {
        /// <summary>
        /// Creates one new entity each time the pool is empty.
        /// This is the default behavior.
        /// </summary>
        ExpandByOne,

        /// <summary>
        /// When the pool is empty, creates new entities equal to the current pooled count,
        /// effectively doubling the inventory (e.g. 10 → 20, 20 → 40).
        /// If the pool has never been populated, creates 1 entity as a seed.
        /// Useful for reducing frequent small allocations.
        /// </summary>
        ExpandByDoubling,

        /// <summary>
        /// Throws an <see cref="System.InvalidOperationException"/> when the pool
        /// is empty and a new entity is requested.
        /// Use this to enforce a fixed pool size.
        /// </summary>
        NoExpand
    }
}
