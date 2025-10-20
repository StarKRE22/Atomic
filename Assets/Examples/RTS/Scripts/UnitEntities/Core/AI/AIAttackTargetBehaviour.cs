using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class AIAttackTargetBehaviour : IEntityInit<IUnitEntity>, IEntityFixedTick
    {
        private IUnitEntity _entity;
        private IValue<IUnitEntity> _target;
        private IValue<float> _scale;

        public void Init(IUnitEntity entity)
        {
            _entity = entity;
            _scale = entity.GetScale();
            _target = entity.GetTarget();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            IUnitEntity target = _target.Value;
            if (target is not {Enabled: true} || !LifeUseCase.IsAlive(target))
                return;

            Vector3 vector = TransformUseCase.GetVector(_entity, target);
            float fullDistance = _entity.GetFireDistance().Value + _scale.Value + target.GetScale().Value;
            if (vector.magnitude > fullDistance)
                _entity.GetMoveRequest().Invoke(vector.normalized);
            else
                _entity.GetFireRequest().Invoke(target);
        }
    }
}