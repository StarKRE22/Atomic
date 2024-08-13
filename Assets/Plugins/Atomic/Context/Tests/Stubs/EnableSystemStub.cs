namespace Atomic.Contexts
{
    public sealed class EnableSystemStub : IContextEnable
    {
        public bool enabled;
        
        public void Enable(IContext context)
        {
            this.enabled = true;
        }
    }
}