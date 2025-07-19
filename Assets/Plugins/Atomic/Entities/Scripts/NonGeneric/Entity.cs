using System.Collections.Generic;

namespace Atomic.Entities
{
    public sealed class Entity : Entity<Entity>, IEntity
    {
        public Entity()
        {
        }

        public Entity(string name) : base(name)
        {
        }

        public Entity(
            string name = null,
            IEnumerable<int> tags = null,
            IEnumerable<KeyValuePair<int, object>> values = null,
            IEnumerable<IBehaviour<Entity>> behaviours = null) :
            base(name, tags, values, behaviours)
        {
        }

        public Entity(
            string name = null,
            int tagCapacity = 0,
            int valueCapacity = 0,
            int behaviourCapacity = 0
        ) : base(name, tagCapacity, valueCapacity, behaviourCapacity)
        {
        }
    }
}