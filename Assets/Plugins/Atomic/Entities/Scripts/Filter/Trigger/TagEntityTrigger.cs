namespace Atomic.Entities
{
    public class TagEntityTrigger : TagEntityTrigger<IEntity>
    {
    }

    public class TagEntityTrigger<E> : EntityTriggerBase<E> where E : IEntity
    {
        private readonly bool _added;
        private readonly bool _removed;

        public TagEntityTrigger(bool added = true, bool removed = true)
        {
            _added = added;
            _removed = removed;
        }

        public override void Track(E entity)
        {
            if (_added) entity.OnTagAdded += this.OnTagAdded;
            if (_removed) entity.OnTagDeleted += this.OnTagDeleted;
        }

        public override void Untrack(E entity)
        {
            if (_added) entity.OnTagAdded -= this.OnTagAdded;
            if (_removed) entity.OnTagDeleted -= this.OnTagDeleted;
        }

        private void OnTagDeleted(IEntity entity, int tag) => _action?.Invoke((E) entity);
        private void OnTagAdded(IEntity entity, int _) => _action?.Invoke((E) entity);
    }
}