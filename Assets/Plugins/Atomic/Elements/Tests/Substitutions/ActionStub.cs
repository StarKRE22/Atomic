namespace Atomic.Elements
{
    public sealed class ActionStub : IAction
    {
        public bool wasInvoke; 

        public void Invoke()
        {
            this.wasInvoke = true;
        }
    }
}