namespace Atomic.Entities
{
    public interface IEntityDisable : IEntityBehaviour
    {
        void Disable(IEntity entity);
    }

    public interface IEntityDisable<in T> : IEntityDisable where T : IEntity
    {
        void IEntityDisable.Disable(IEntity entity) => this.Disable((T) entity);
        void Disable(T entity);
    }
}