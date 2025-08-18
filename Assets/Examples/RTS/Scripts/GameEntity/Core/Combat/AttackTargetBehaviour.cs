using Atomic.Elements;
using Atomic.Entities;

namespace RTSGame
{
    public sealed class AttackTargetBehaviour : IEntityInit, IEntityFixedUpdate
    {
        private IReactiveValue<IEntity> _target;

        public void Init(in IEntity entity)
        {
            _target = entity.GetTarget();
        }

        public void OnFixedUpdate(in IEntity entity, in float deltaTime)
        {
            AttackUseCase.AttackTarget(entity, _target.Value, deltaTime);
        }
    }
}