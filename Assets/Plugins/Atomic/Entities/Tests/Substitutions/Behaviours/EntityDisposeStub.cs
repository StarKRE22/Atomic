namespace Atomic.Entities
{
    public sealed class EntityDisposeStub : IEntityDispose
    {
        public bool WasDespawn;

        public void Dispose(IEntity entity)
        {
            WasDespawn = true;
        }
    }
}