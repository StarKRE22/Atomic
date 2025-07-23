namespace Atomic.Entities
{
    public interface IEntityViewInstaller<E> where E : IEntity
    {
        void Install(EntityView<E> abstractEntityView);
    }
}