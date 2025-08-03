namespace Atomic.Entities
{
    public sealed class EntitySpawnStub : IEntitySpawn
    {
        public bool WasSpawn;
        
        public void OnSpawn(IEntity entity)
        {
            WasSpawn = true;
        }
    }
}