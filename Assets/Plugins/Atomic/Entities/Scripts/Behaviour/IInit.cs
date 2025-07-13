namespace Atomic.Entities
{
    public interface IInit : IBehaviour
    {
        void Init(in IEntity entity);
    }

    public interface IInit<in T> : IInit where T : IEntity
    {
        void IInit.Init(in IEntity entity) => this.Init((T) entity);
        void Init(T context);
    }
}