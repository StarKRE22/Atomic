namespace Atomic.Entities
{
    public class EntityLateTickStub : IEntityLateTick
    {
        public bool WasCalled { get; private set; }
        public float LastDeltaTime { get; private set; }

        public void LateTick(IEntity entity, float deltaTime)
        {
            WasCalled = true;
            LastDeltaTime = deltaTime;
        }
    }
}