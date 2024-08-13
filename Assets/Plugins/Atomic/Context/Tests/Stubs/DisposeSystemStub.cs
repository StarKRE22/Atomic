namespace Atomic.Contexts
{
    public sealed class DisposeSystemStub : IContextDispose
    {
        public bool destroyed;
        
        public void Dispose(IContext context)
        {
            destroyed = true;
        }
    }
}