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
        public MultiEntityPool(IMultiEntityFactory<string, IEntity, TArgs> factory, TArgs args) : base(factory, args)
        {
        }
    }
}