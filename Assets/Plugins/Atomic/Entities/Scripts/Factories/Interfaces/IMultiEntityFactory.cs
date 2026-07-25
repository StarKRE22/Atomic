namespace Atomic.Entities
{
    public interface IMultiEntityFactory<in TArgs> : 
        IMultiEntityFactory<string, IEntity, TArgs>
        where TArgs : IArgs
    {
    }
}