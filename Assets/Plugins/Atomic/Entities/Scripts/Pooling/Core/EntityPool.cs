namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic version of <see cref="EntityPool{E}"/> that operates on base <see cref="IEntity"/> types.
    /// </summary>
    /// <remarks>
    /// Use this when pooling a variety of entities that share a common interface but do not require strong typing.
    /// </remarks>
    public class EntityPool<TArgs> : EntityPool<IEntity, TArgs>, IEntityPool
        where TArgs : IArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityPool"/> class with the specified entity factory.
        /// </summary>
        /// <param name="factory">The factory used to create <see cref="IEntity"/> instances.</param>
        /// <param name="args">The arguments passed to the factory when creating entities.</param>
        /// <param name="expandMode">Determines how the pool expands when empty. Defaults to <see cref="ExpandMode.ExpandByOne"/>.</param>
        public EntityPool(IEntityFactory<IEntity, TArgs> factory, TArgs args, ExpandMode expandMode = ExpandMode.ExpandByOne)
            : base(factory, args, expandMode)
        {
        }
    }
}