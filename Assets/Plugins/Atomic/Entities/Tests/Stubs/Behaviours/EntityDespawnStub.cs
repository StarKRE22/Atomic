namespace Atomic.Entities
{
    public sealed class EntityDespawnStub : IEntityDespawn
    {
        public bool WasDespawn;

        public void Despawn(IEntity entity)
        {
            WasDespawn = true;
        }
    }
}