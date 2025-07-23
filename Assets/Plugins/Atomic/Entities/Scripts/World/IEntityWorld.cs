namespace Atomic.Entities
{
    public interface IEntityWorld : IEntityWorld<IEntity>
    {
    }

    public interface IEntityWorld<E> : IEntityCollection<E>, ISpawnable, IUpdatable, IActivatable where E : IEntity
    {
        string Name { get; }
    }
}