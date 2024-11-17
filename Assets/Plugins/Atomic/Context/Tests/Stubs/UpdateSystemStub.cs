namespace Atomic.Contexts
{
    public sealed class UpdateSystemStub : IContextUpdate, IContextFixedUpdate, IContextLateUpdate
    {
        public bool updated;
        public bool fixedUpdated;
        public bool lateUpdated;

        public void OnUpdate(IContext context, float deltaTime)
        {
            this.updated = true;
        }

        public void OnFixedUpdate(IContext context, float deltaTime)
        {
            this.fixedUpdated = true;
        }

        public void OnLateUpdate(IContext context, float deltaTime)
        {
            this.lateUpdated = true;
        }
    }
}