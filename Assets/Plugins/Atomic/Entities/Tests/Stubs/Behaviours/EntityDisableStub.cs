namespace Atomic.Entities
{
    public sealed class EntityDisableStub : IEntityDisable
    {
        public bool WasDisable;

        public void Disable(IEntity entity)
        {
            WasDisable = true;
        }
    }
}