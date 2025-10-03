namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic registry interface for storing and retrieving entity factories by key.
    /// </summary>
    public interface IMultiEntityFactory : IMultiEntityFactory<string, IEntity>
    {
    }
}