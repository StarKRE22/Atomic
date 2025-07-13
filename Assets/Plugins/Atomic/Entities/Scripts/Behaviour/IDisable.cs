namespace Atomic.Entities
{
    public interface IDisable : IBehaviour
    {
        void Disable(in IEntity entity);
    }

    public interface IDisable<in T> : IDisable where T : IEntity
    {
        void IDisable.Disable(in IEntity entity) => this.Disable((T) entity);
        void Disable(T entity);
    }
}