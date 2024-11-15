namespace Atomic.Contexts
{
    public sealed class DisableSystemStub : IContextDisable
    {
        public bool disabled;
        
        public void Disable(IContext context)
        {
            disabled = true;
        }
    }
}