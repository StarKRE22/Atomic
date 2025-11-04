using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a convenience specialization of <see cref="TypedEntityFilter{E, B}"/> 
    /// for filtering entities directly from an <see cref="IReadOnlyEntityCollection{IEntity}"/>.
    /// </summary>
    /// <typeparam name="E">
    /// The concrete entity type to include in the filtered view.
    /// Must implement <see cref="IEntity"/>.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// This class is a shorthand for creating a <see cref="TypedEntityFilter{E, B}"/> 
    /// where the base type parameter <typeparamref name="B"/> is fixed to <see cref="IEntity"/>.
    /// </para>
    /// <para>
    /// Use this type when you have a heterogeneous entity source (e.g., all entities in a world)
    /// but want to maintain a live, type-safe subset of a specific derived type.
    /// </para>
    /// </remarks>
    /// <example>
    /// <code>
    /// var filter = new TypedEntityFilter<PlayerEntity>(
    ///     world.Entities,
    ///     player => player.Health > 0
    /// );
    ///
    /// foreach (var player in filter)
    /// {
    ///     // Process all alive players
    /// }
    /// </code>
    /// </example>
    /// <seealso cref="TypedEntityFilter{E, B}"/>
    /// <seealso cref="IEntity"/>
    /// <seealso cref="IEntityTrigger{TEntity}"/>
    public class TypedEntityFilter<E> : TypedEntityFilter<E, IEntity> where E : IEntity
    {
        public TypedEntityFilter(
            IReadOnlyEntityCollection<IEntity> source,
            Predicate<E> predicate,
            params IEntityTrigger<E>[] triggers
        ) : base(source, predicate, triggers)
        {
        }
    }
}