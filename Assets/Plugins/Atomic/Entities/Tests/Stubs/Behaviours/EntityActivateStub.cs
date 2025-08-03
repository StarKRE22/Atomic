namespace Atomic.Entities
{
    public sealed class EntityActivateStub : IEntityActivate
    {
        public bool WasEnable;
        
        public void OnActivate(IEntity entity)
        {
            WasEnable = true;
        }
    }
}