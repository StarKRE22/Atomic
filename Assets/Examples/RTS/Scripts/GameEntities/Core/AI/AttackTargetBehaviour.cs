using Atomic.Entities;
using Unity.Profiling;

namespace RTSGame
{
    public sealed class AttackTargetBehaviour : IEntityFixedTick<IGameEntity>
    {
        private static readonly ProfilerMarker s_fixedTickMarker = new("AttackTargetBehaviour.FixedTickMarker");

        public void FixedTick(IGameEntity entity, float deltaTime)
        {
            using (s_fixedTickMarker.Auto())
            {
                entity.Attack();
            }
        }
    }
}