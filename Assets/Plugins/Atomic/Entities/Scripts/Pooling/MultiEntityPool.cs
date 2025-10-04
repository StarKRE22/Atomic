namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic version of <see cref="MultiEntityPool{K,E}"/> that uses string keys and <see cref="IEntity"/> values.
    /// </summary>
    public class MultiEntityPool : MultiEntityPool<string, IEntity>, IMultiEntityPool
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MultiEntityPool"/> class.
        /// </summary>
        /// <param name="factory">The factory registry used to create and manage entity instances.</param>
        public MultiEntityPool(IMultiEntityFactory<string, IEntity> factory) : base(factory)
        {
        }
    }
}