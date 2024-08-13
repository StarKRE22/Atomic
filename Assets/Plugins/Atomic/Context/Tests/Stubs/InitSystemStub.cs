namespace Atomic.Contexts
{
    public sealed class InitSystemStub : IContextInit
    {
        public bool initialized;
        
        public void Init(IContext context)
        {
            this.initialized = true;
        }
    }
}