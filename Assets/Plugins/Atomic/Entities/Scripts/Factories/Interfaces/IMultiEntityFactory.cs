namespace Atomic.Entities
{
    /// <summary>
    /// A specialized version of <see cref="IMultiEntityFactory{TKey, E}"/> 
    /// where the key type is <see cref="string"/> and the entity type is <see cref="IEntity"/>.
    /// </summary>
    public interface IMultiEntityFactory : IMultiEntityFactory<string, IEntity>
    {
    }
}