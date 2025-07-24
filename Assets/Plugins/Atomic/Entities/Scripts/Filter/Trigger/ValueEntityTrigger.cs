namespace Atomic.Entities
{
    public sealed class ValueEntityTrigger<E> : EntityTriggerAbstract<E> where E : IEntity
    {
        private readonly bool _added;
        private readonly bool _removed;
        private readonly bool _changed;
        
        public ValueEntityTrigger(bool added = true, bool removed = true, bool changed = true)
        {
            _added = added;
            _removed = removed;
            _changed = changed;
        }
        
        public override void Observe(E entity)
        {
            if (_added) entity.OnValueAdded += this.OnValueAdded;
            if (_removed) entity.OnValueDeleted += this.OnValueDeleted;
            if (_changed) entity.OnValueChanged += this.OnValueChanged;
        }

        public override void Unobserve(E entity)
        {
            if (_added) entity.OnValueAdded -= this.OnValueAdded;
            if (_removed) entity.OnValueDeleted -= this.OnValueDeleted;
            if (_changed) entity.OnValueChanged -= this.OnValueChanged;
        }

        private void OnValueDeleted(IEntity entity, int key) => _callback?.Invoke((E) entity);
        private void OnValueAdded(IEntity entity, int key) => _callback?.Invoke((E) entity);
        private void OnValueChanged(IEntity entity, int key) => _callback?.Invoke((E) entity);
    }
}