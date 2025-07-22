namespace Atomic.Entities
{
    public interface IViewInstaller<E> where E : IEntity<E>
    {
        void Install(EntityView<E> entityView);
    }
}