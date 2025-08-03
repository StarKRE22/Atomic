namespace Atomic.Entities
{
    public sealed class EntityDespawnedStub : IEntityDespawned
    {
        public bool WasDespawn;

        public void OnDespawn(IEntity entity)
        {
            WasDespawn = true;
        }
    }
}