namespace Atomic.Entities
{
    public abstract class MonoEntityBakerOptimized<TArgs> : 
        MonoEntityBakerOptimized<string, IEntity, EntityView, TArgs>
        where TArgs : IArgs
    {
    }
}