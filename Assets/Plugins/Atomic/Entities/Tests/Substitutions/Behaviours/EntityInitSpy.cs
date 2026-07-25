namespace Atomic.Entities
{
    public sealed class EntityInitSpy : IEntityInit
    {
        public bool WasInitialized;
        
        public void Init(IEntity entity) => WasInitialized = true;
    }
}