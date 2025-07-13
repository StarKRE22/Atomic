namespace Atomic.Entities
{
    public interface IDispose
    {
        void Dispose(in IEntity entity);
    }
    
    public interface IDispose<in T> : IDispose where T : IEntity
    {
        void IDispose.Dispose(in IEntity entity) => this.Dispose((T) entity);
        void Dispose(T entity);
    }
}