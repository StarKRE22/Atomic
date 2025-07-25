namespace Atomic.Entities
{
    public interface IEntityViewInstaller<E> where E : IEntity
    {
        void Install(EntityView<E> view);
    }

    public interface IEntityViewInstaller : IEntityViewInstaller<IEntity>
    {
    }
}