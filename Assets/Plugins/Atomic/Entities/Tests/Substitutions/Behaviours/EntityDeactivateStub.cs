namespace Atomic.Entities
{
    public sealed class EntityInactiveStub : IEntityInactive
    {
        public bool WasDisable;

        public void OnInactive(IEntity entity)
        {
            WasDisable = true;
        }
    }
}