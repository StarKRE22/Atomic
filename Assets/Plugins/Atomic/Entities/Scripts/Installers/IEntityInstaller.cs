namespace Atomic.Entities
{
    public interface IEntityInstaller<E> where E : IEntity<E>
    {
        void Install(IEntity<E> entity);
    }
}