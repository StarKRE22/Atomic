namespace Atomic.Entities
{
    public interface IEntityInstaller
    {
        void Install(IEntity entity);
    }

    public interface IEntityInstaller<in T> : IEntityInstaller where T : IEntity
    {
        void IEntityInstaller.Install(IEntity entity) => this.Install((T) entity);
        void Install(T entity);
    }
}