namespace Atomic.Entities
{
    public interface IEnable : IBehaviour
    {
        void Enable(in IEntity entity);
    }

    public interface IEnable<in T> : IEnable where T : IEntity
    {
        void IEnable.Enable(in IEntity entity) => this.Enable((T) entity);
        void Enable(T entity);
    }
}