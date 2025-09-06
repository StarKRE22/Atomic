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

    /// <summary>
    /// Represents an aspect of an entity that can be applied or discarded.
    /// </summary>
    /// <typeparam name="E">The type of entity (<see cref="IEntity"/>) this aspect can be applied to.</typeparam>
    public interface IEntityAspect<in E> where E : IEntity
    {
        /// <summary>
        /// Applies this aspect to the specified entity.
        /// </summary>
        /// <param name="entity">The entity to which the aspect should be applied.</param>
        void Apply(E entity);

        /// <summary>
        /// Discards this aspect from the specified entity, reversing any changes made by <see cref="Apply"/>.
        /// </summary>
        /// <param name="entity">The entity from which the aspect should be removed.</param>
        void Discard(E entity);
    }
}