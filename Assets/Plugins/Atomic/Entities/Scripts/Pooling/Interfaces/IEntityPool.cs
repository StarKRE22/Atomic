namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic alias for <see cref="IEntityPool{T}"/> that operates on the base <see cref="IEntity"/> type.
    /// </summary>
    /// <remarks>
    /// Use this interface when you do not need type-specific access to entity creation or pooling.
    /// </remarks>
    public interface IEntityPool : IEntityPool<IEntity>
    {
    }
}