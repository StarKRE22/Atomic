using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class ProjectileMoveBehaviour : IEntityInit<IUnit>, IEntityFixedTick
    {
        private readonly IGameContext _gameContext;

        private IUnit _entity;
        private IValue<float> _scale;
        private IValue<IUnit> _target;

        private Vector3 _forward;

        public ProjectileMoveBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IUnit entity)
        {
            _entity = entity;
            _target = entity.GetTarget();
            _scale = entity.GetScale();
            _forward = TransformUseCase.GetForward(_entity);
        }

        public void FixedTick(IEntity entity, float deltaTime)
        {
            IUnit target = _target.Value;
            if (target is not {Enabled: true})
            {
                MoveUseCase.MoveStep(_entity, _forward, deltaTime);
                return;
            }

            Vector3 vector = TransformUseCase.GetVector(_entity, target);
            float scale = _scale.Value;
            if (vector.sqrMagnitude > scale * scale)
            {
                _forward = vector.normalized;
                MoveUseCase.MoveStep(_entity, _forward, deltaTime);
            }
            else if (DamageUseCase.DealDamage(_entity, target))
            {
                UnitsUseCase.Despawn(_gameContext, _entity);
            }
        }
    }
}