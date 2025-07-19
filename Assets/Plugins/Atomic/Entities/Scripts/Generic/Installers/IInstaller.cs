namespace Atomic.Entities
{
    public interface IInstaller<in E> where E : IEntity<E>
    {
        void Install(E entity);
    }
}