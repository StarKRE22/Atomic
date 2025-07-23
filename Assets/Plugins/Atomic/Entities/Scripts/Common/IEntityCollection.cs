namespace Atomic.Entities
{
    public interface IEntityCollection : IEntityCollection<IEntity>, IReadOnlyEntityCollection
    {
    }

    public interface IEntityCollection<E> : IReadOnlyEntityCollection<E> where E : IEntity
    {
        bool Add(E entity);
        bool Del(E entity);
        void Clear();
    }
}