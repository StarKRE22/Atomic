using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class ProjectileMoveBehaviour : IEntityInit<IGameEntity>, IEntityFixedUpdate
    {
        private readonly IGameContext _gameContext;

        private IGameEntity _entity;
        private IValue<float> _scale;
        private IValue<IGameEntity> _target;

        private Vector3 _forward;

        public ProjectileMoveBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void Init(IGameEntity entity)
        {
            _entity = entity;
            _target = entity.GetTarget();
            _scale = entity.GetScale();
            _forward = TransformUseCase.GetForward(_entity);
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            IGameEntity target = _target.Value;
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
                GameEntityUseCase.Despawn(_gameContext, _entity);
            }
        }
    }
}