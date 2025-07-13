using System.Collections.Generic;
using Atomic.Entities;

namespace Atomic.Extensions
{
    public sealed class EntityAspectComposite : IEntityAspect
    {
        private readonly IEnumerable<IEntityAspect> _aspects;

        public EntityAspectComposite(params IEntityAspect[] aspects)
        {
            _aspects = aspects;
        }

        public EntityAspectComposite(IEnumerable<IEntityAspect> aspects)
        {
            _aspects = aspects;
        }

        public void Apply(IEntity entity)
        {
            foreach (IEntityAspect aspect in _aspects) 
                aspect.Apply(entity);
        }

        public void Discard(IEntity entity)
        {
            foreach (IEntityAspect aspect in _aspects) 
                aspect.Discard(entity);
        }
    }
}