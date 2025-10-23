namespace Atomic.Entities
{
    /// <summary>
    /// Non-generic version of <see cref="IEntityFactory{T}"/> that produces a base <see cref="IEntity"/> instance.
    /// This interface is useful when working with heterogeneous entity types in a shared context, such as registries or catalogs.
    /// </summary>
    public interface IEntityFactory : IEntityFactory<IEntity>
    {
    }
}