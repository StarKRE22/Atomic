namespace Atomic.Entities
{
    public sealed class EntityInitStub : IEntityInit
    {
        public bool WasInit;
        
        public void Init(IEntity entity)
        {
            WasInit = true;
        }
    }
}