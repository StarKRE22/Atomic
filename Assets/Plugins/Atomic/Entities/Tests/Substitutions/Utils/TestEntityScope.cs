using System;
using System.Collections.Generic;

namespace Atomic.Entities
{
    public class TestEntityScope : IDisposable
    {
        private readonly List<Entity> _entities = new();

        public Entity NewEntity(
            string name = null,
            int tagCapacity = 0,
            int valueCapacity = 0,
            int behaviourCapacity = 0,
            Entity.Settings? settings = null
        )
        {
            Entity entity = new Entity(name, tagCapacity, valueCapacity, behaviourCapacity, settings);
            _entities.Add(entity);
            return entity;
        }

        public void Dispose()
        {
            for (int i = 0, count = _entities.Count; i < count; i++)
                _entities[i].Dispose();

            _entities.Clear();
        }
    }
}