namespace Atomic.Entities
{
    public interface IEntityInstaller<in E> where E : IEntity<E>
    {
        void Install(E entity);
    }
}