namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias of <see cref="IMultiEntityPool{K,E}"/> for managing pools of <see cref="IEntity"/> using string keys.
    /// </summary>
    public interface IMultiEntityPool : IMultiEntityPool<string, IEntity>
    {
    }
}