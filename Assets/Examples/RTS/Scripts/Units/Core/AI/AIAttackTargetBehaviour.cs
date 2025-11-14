using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class AIAttackTargetBehaviour : IEntityInit<IUnit>, IEntityFixedTick
    {
        private IUnit _entity;
        private IValue<IUnit> _target;
        private IValue<float> _scale;

        public void Init(IUnit entity)
        {
            _entity = entity;
            _scale = entity.GetScale();
            _target = entity.GetTarget();
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            IUnit target = _target.Value;
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