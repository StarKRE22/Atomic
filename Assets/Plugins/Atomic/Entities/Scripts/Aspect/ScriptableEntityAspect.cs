using UnityEngine;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic base class for scriptable entity aspects.
    /// </summary>
    /// <remarks>
    /// This class is a concrete specialization of <see cref="ScriptableEntityAspect{E}"/> 
    /// with <typeparamref name="E"/> fixed to <see cref="IEntity"/>. 
    /// It implements <see cref="IEntityAspect"/> and can be used as a ScriptableObject asset.
    /// </remarks>
    public abstract class ScriptableEntityAspect : ScriptableEntityAspect<IEntity>, IEntityAspect
    {
    }

    /// <summary>
    /// Represents a scriptable entity aspect that can be applied or discarded on a specific entity type.
    /// </summary>
    /// <typeparam name="E">The type of entity (<see cref="IEntity"/>) this aspect can be applied to.</typeparam>
    /// <remarks>
    /// Inherit from this class to create reusable ScriptableObject assets that encapsulate
    /// logic to apply and discard behaviors or properties on entities.
    /// </remarks>
    public abstract class ScriptableEntityAspect<E> : ScriptableObject, IEntityAspect<E> where E : IEntity
    {
        /// <summary>
        /// Applies this aspect to the specified entity.
        /// </summary>
        /// <param name="entity">The entity to which the aspect should be applied.</param>
        public abstract void Apply(E entity);

        /// <summary>
        /// Discards this aspect from the specified entity, reversing any changes made by <see cref="Apply"/>.
        /// </summary>
        /// <param name="entity">The entity from which the aspect should be removed.</param>
        public abstract void Discard(E entity);
    }
}