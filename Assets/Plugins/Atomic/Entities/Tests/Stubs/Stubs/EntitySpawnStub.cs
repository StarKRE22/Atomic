namespace Atomic.Entities
{
    public sealed class EntitySpawnStub : IEntitySpawn
    {
        public bool WasSpawn;
        
        public void Spawn(IEntity entity)
        {
            WasSpawn = true;
        }
    }
}