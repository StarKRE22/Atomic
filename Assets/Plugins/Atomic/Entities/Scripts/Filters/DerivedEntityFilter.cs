using System;

namespace Atomic.Entities
{
    /// <summary>
    /// Represents a convenience specialization of <see cref="DerivedEntityFilter{E,B}"/> 
    /// for filtering entities directly from an <see cref="IReadOnlyEntityCollection{IEntity}"/>.
    /// </summary>
    /// <typeparam name="T">
    /// The concrete entity type to include in the filtered view.
    /// Must implement <see cref="IEntity"/>.
    /// </typeparam>
    /// <remarks>
    /// <para>
    /// This class is a shorthand for creating a <see cref="DerivedEntityFilter{E,B}"/> 
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
    /// <seealso cref="DerivedEntityFilter{E,B}"/>
    /// <seealso cref="IEntity"/>
    /// <seealso cref="IEntityTrigger{TEntity}"/>
    public class DerivedEntityFilter<T> : DerivedEntityFilter<T, IEntity> where T : IEntity
    {
        public DerivedEntityFilter(
            IReadOnlyEntityCollection<IEntity> source,
            Predicate<T> predicate,
            params IEntityTrigger<T>[] triggers
        ) : base(source, predicate, triggers)
        {
        }
    }
}