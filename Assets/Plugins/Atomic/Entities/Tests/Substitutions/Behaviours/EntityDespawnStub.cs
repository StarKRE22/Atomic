namespace Atomic.Entities
{
    public sealed class EntityDespawnStub : IEntityDespawn
    {
        public bool WasDespawn;

        public void OnDespawn(IEntity entity)
        {
            WasDespawn = true;
        }
    }
}