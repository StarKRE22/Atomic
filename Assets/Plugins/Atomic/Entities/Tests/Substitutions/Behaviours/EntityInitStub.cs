namespace Atomic.Entities
{
    public sealed class EntityInitStub : IEntityInit
    {
        public bool WasSpawn;
        
        public void Init(IEntity entity)
        {
            WasSpawn = true;
        }
    }
}