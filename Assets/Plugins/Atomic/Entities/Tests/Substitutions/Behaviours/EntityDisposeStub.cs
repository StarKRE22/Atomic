namespace Atomic.Entities
{
    public sealed class EntityDisposeStub : IEntityDispose
    {
        public bool WasDispose;

        public void Dispose(IEntity entity)
        {
            WasDispose = true;
        }
    }
}