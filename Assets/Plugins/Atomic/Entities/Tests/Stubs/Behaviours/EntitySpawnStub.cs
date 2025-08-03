namespace Atomic.Entities
{
    public sealed class EntitySpawnedStub : IEntitySpawned
    {
        public bool WasSpawn;
        
        public void OnSpawn(IEntity entity)
        {
            WasSpawn = true;
        }
    }
}