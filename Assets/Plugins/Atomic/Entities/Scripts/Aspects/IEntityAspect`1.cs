namespace Atomic.Entities
{
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