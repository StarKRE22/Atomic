namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic marker interface for entity aspects.
    /// </summary>
    /// <remarks>
    /// This interface is a concrete specialization of <see cref="IEntityAspect{E}"/>
    /// with <typeparamref name="E"/> fixed to <see cref="IEntity"/>.
    /// Use this when you do not need a generic aspect tied to a specific entity type.
    /// </remarks>
    public interface IEntityAspect : IEntityAspect<IEntity>
    {
    }
}