namespace Atomic.Entities
{
    public interface IEntityDispose
    {
        void Dispose(IEntity entity);
    }
    
    public interface IEntityDispose<in T> : IEntityDispose where T : IEntity
    {
        void IEntityDispose.Dispose(IEntity entity) => this.Dispose((T) entity);
        void Dispose(T entity);
    }
}