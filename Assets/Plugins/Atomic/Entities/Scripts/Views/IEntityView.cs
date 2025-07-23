namespace Atomic.Entities
{
    public interface IEntityView<out E> where E : IEntity
    {
        string Name { get; }
        
        E Entity { get; }
        
        bool IsVisible { get; }
    }
}