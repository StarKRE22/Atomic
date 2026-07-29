namespace Atomic.Entities
{
    public sealed class EntityDisableSpy : IEntityDisable
    {
        public bool WasDisabled;

        public void Disable(IEntity entity) => WasDisabled = true;
    }
}