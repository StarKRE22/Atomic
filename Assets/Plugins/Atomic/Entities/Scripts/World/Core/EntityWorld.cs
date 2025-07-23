using System;
using System.Collections.Generic;

#if ODIN_INSPECTOR
using Sirenix.OdinInspector;
#endif

namespace Atomic.Entities
{
    public class EntityWorld : EntityWorld<IEntity>
    {
    }

    public partial class EntityWorld<E> : EntityRunner<E>, IEntityWorld<E> where E : IEntity
    {
        public override event Action OnStateChanged;

#if ODIN_INSPECTOR
        [ShowInInspector]
#endif
        public string Name
        {
            get => _name;
            set => _name = value;
        }

        private string _name;

        public EntityWorld() => _name = string.Empty;

        public EntityWorld(params E[] entities)
        {
            _name = string.Empty;
            this.AddRange(entities);
        }

        public EntityWorld(string name = null, params E[] entities)
        {
            _name = name;
            this.AddRange(entities);
        }

        public EntityWorld(string name, IEnumerable<E> entities)
        {
            _name = name;
            this.AddRange(entities);
        }

        protected override void OnAdd(E entity)
        {
            this.AddTags(entity);
            this.AddValues(entity);
            this.Subscribe(entity);
        }

        protected override void OnRemove(E entity)
        {
            this.Unsubscribe(entity);
            this.RemoveTags(entity);
            this.RemoveValues(entity);
        }

        private void Subscribe(E entity)
        {
            entity.OnTagAdded += this.OnTagAdded;
            entity.OnTagDeleted += this.OnTagRemoved;

            entity.OnValueAdded += this.OnValueAdded;
            entity.OnValueDeleted += this.OnValueRemoved;
        }

        private void Unsubscribe(E entity)
        {
            entity.OnTagAdded -= this.OnTagAdded;
            entity.OnTagDeleted -= this.OnTagRemoved;

            entity.OnValueAdded -= this.OnValueAdded;
            entity.OnValueDeleted -= this.OnValueRemoved;
        }
    }
}