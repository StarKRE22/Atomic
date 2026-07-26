namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic version of <see cref="MultiEntityPool{K,E}"/> that uses string keys and <see cref="IEntity"/> values.
    /// </summary>
    public class MultiEntityPool<TArgs> : MultiEntityPool<string, IEntity, TArgs>, IMultiEntityPool
        where TArgs : IArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiEntityPool"/> class.
        /// </summary>
        /// <param name="factory">The factory registry used to create and manage entity instances.</param>
        /// <param name="args">The arguments passed to the factory when creating entities.</param>
        /// <param name="expandMode">Determines how the pool expands when empty. Defaults to <see cref="ExpandMode.ExpandByOne"/>.</param>
        public MultiEntityPool(IMultiEntityFactory<string, IEntity, TArgs> factory, TArgs args, ExpandMode expandMode = ExpandMode.ExpandByOne)
            : base(factory, args, expandMode)
        {
        }
    }
}