namespace Atomic.Elements
{
    public sealed class ActionSpy : IAction
    {
        public int InvokeCount => _invokeCount;
        
        public bool WasInvoked => _invokeCount > 0;
        
        private int _invokeCount;
        
        public void Invoke()
        {
            _invokeCount++;
        }
    }
}