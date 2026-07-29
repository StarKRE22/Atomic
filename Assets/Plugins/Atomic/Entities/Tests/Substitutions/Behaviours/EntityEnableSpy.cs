namespace Atomic.Entities
{
    public sealed class EntityEnableSpy : IEntityEnable
    {
        public bool WasEnabled;
        
        public void Enable(IEntity entity) => WasEnabled = true;
    }
}