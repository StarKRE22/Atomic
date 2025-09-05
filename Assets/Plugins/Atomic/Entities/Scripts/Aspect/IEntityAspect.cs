namespace Atomic.Entities
{
    /// <summary>
    /// Represents a reusable aspect or feature that can be applied to an <see cref="IEntity"/>.
    /// Aspects encapsulate behavior or state that can be added or removed from an entity at runtime.
    /// </summary>
    public interface IEntityAspect
    {
        /// <summary>
        /// Applies the aspect to the specified entity.
        /// </summary>
        /// <param name="entity">The entity to which the aspect will be applied.</param>
        void Apply(IEntity entity);

        /// <summary>
        /// Discards the aspect from the specified entity.
        /// Reverts any changes or behavior added by <see cref="Apply(IEntity)"/>.
        /// </summary>
        /// <param name="entity">The entity from which the aspect will be removed.</param>
        void Discard(IEntity entity);
    }

    /// <summary>
    /// Represents a type-specific entity aspect that can be applied to entities of type <typeparamref name="E"/>.
    /// Provides automatic forwarding for non-generic <see cref="IEntityAspect"/> methods.
    /// </summary>
    /// <typeparam name="E">The specific type of entity this aspect can be applied to. Must implement <see cref="IEntity"/>.</typeparam>
    public interface IEntityAspect<in E> : IEntityAspect where E : IEntity
    {
        /// <summary>
        /// Applies the aspect to the specified entity of type <typeparamref name="E"/>.
        /// </summary>
        /// <param name="entity">The entity to which the aspect will be applied.</param>
        void Apply(E entity);

        /// <summary>
        /// Discards the aspect from the specified entity of type <typeparamref name="E"/>.
        /// Reverts any changes or behavior added by <see cref="Apply(E)"/>.
        /// </summary>
        /// <param name="entity">The entity from which the aspect will be removed.</param>
        void Discard(E entity);

        /// <summary>
        /// Explicit implementation forwarding for <see cref="IEntityAspect.Apply(IEntity)"/>.
        /// Casts the <see cref="IEntity"/> to <typeparamref name="E"/> and calls <see cref="Apply(E)"/>.
        /// </summary>
        /// <param name="entity">The entity to apply the aspect to.</param>
        void IEntityAspect.Apply(IEntity entity) => this.Apply((E) entity);

        /// <summary>
        /// Explicit implementation forwarding for <see cref="IEntityAspect.Discard(IEntity)"/>.
        /// Casts the <see cref="IEntity"/> to <typeparamref name="E"/> and calls <see cref="Discard(E)"/>.
        /// </summary>
        /// <param name="entity">The entity to discard the aspect from.</param>
        void IEntityAspect.Discard(IEntity entity) => this.Discard((E) entity);
    }
}