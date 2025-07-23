namespace Atomic.Entities
{
    public partial interface IEntityWorld<E> : IEntityCollection<E>, IEntityLoop where E : IEntity
    {
        string Name { get; }
        
        new void Clear();

        void IEntityLoop.Clear() => Clear();
    }
}