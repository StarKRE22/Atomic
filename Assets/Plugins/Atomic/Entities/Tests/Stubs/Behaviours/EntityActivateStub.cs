namespace Atomic.Entities
{
    public sealed class EntityActiveStub : IEntityActive
    {
        public bool WasEnable;
        
        public void OnActive(IEntity entity)
        {
            WasEnable = true;
        }
    }
}