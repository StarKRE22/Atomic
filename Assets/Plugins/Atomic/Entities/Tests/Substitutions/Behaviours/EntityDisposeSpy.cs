namespace Atomic.Entities
{
    public sealed class EntityDisposeSpy : IEntityDispose
    {
        public bool WasDisposed;

        public void Dispose(IEntity entity) => WasDisposed = true;
    }
}