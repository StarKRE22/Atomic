namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic version of <see cref="IEntityCollection{E}"/>, specialized for base <see cref="IEntity"/> types.
    /// Provides a common interface for managing entity collections without specifying the entity type.
    /// </summary>
    public interface IEntityCollection : IEntityCollection<IEntity>, IReadOnlyEntityCollection
    {
    }
}