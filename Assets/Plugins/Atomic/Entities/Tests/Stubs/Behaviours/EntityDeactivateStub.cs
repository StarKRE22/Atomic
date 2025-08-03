namespace Atomic.Entities
{
    public sealed class EntityDeactivateStub : IEntityDeactivate
    {
        public bool WasDisable;

        public void OnDeactivate(IEntity entity)
        {
            WasDisable = true;
        }
    }
}