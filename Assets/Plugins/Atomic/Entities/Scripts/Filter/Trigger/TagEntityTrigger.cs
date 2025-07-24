namespace Atomic.Entities
{
    public class TagEntityTrigger : TagEntityTrigger<IEntity>
    {
    }

    public class TagEntityTrigger<E> : EntityTriggerAbstract<E> where E : IEntity
    {
        private readonly bool _added;
        private readonly bool _removed;

        public TagEntityTrigger(bool added = true, bool removed = true)
        {
            _added = added;
            _removed = removed;
        }

        public override void Observe(E entity)
        {
            if (_added)
                entity.OnTagAdded += this.OnTagAdded;

            if (_removed)
                entity.OnTagDeleted += this.OnTagDeleted;
        }

        public override void Unobserve(E entity)
        {
            if (_added)
                entity.OnTagAdded -= this.OnTagAdded;

            if (_removed)
                entity.OnTagDeleted -= this.OnTagDeleted;
        }

        private void OnTagDeleted(IEntity entity, int tag) => this.Invoke((E) entity);
        private void OnTagAdded(IEntity entity, int _) => this.Invoke((E) entity);
    }
}