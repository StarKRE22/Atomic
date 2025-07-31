namespace Atomic.Entities
{
    public sealed class EntityEnableStub : IEntityEnable
    {
        public bool WasEnable;
        
        public void Enable(IEntity entity)
        {
            WasEnable = true;
        }
    }
}