using System;

namespace Atomic.Entities
{
    /// <summary>
    /// A non-generic version of <see cref="EntityFilter{E}"/> for working with <see cref="IEntity"/> collections.
    /// </summary>
    /// <remarks>
    /// This is a convenience wrapper that avoids specifying a type parameter when filtering general entities.
    /// </remarks>
    public class EntityFilter : EntityFilter<IEntity>, IReadOnlyEntityCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EntityFilter"/> class.
        /// </summary>
        /// <param name="source">The source collection of entities to observe.</param>
        /// <param name="predicate">The predicate that determines inclusion in the filter.</param>
        /// <param name="triggers">Optional triggers for dynamic change tracking.</param>
        public EntityFilter(
            IReadOnlyEntityCollection<IEntity> source,
            Predicate<IEntity> predicate,
            params IEntityTrigger<IEntity>[] triggers
        ) : base(source, predicate, triggers)
        {
        }
    }
}