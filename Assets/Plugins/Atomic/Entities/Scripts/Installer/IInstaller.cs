namespace Atomic.Entities
{
    public interface IInstaller
    {
        void Install(IEntity entity);
    }

    public interface IInstaller<in T> : IInstaller where T : IEntity
    {
        void IInstaller.Install(IEntity entity) => this.Install((T) entity);
   
        void Install(T entity);
    }
}