using System;

namespace Atomic.Entities
{
    /**
     * Experimental.
     */
    public class EntityFilter : EntityFilterBase
    {
        private readonly Predicate<IEntity> predicate;

        public EntityFilter(Predicate<IEntity> predicate)
        {
            this.predicate = predicate;
        }

        public EntityFilter(IEntityWorld world, Predicate<IEntity> predicate)
        {
            this.predicate = predicate;
            this.Initialize(world);
        }

        protected sealed override bool Matches(IEntity entity)
        {
            return this.predicate.Invoke(entity);
        }
    }
}