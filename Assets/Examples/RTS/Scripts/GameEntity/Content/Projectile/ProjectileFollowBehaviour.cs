using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class ProjectileFollowBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private readonly IGameContext _gameContext;

        private IGameEntity _entity;
        private IValue<float> _scale;
        private IValue<IEntity> _target;

        private Vector3 _forward;

        public ProjectileFollowBehaviour(IGameContext gameContext)
        {
            _gameContext = gameContext;
        }

        public void OnSpawn(IGameEntity entity)
        {
            _entity = entity;
            _target = entity.GetTarget();
            _scale = entity.GetScale();
            _forward = entity.GetRotation().Value * Vector3.forward;
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            IEntity target = _target.Value;
            if (target is not {IsActive: true})
            {
                MoveUseCase.MoveStep(_entity, _forward, deltaTime);
                return;
            }

            Vector3 vector = TransformUseCase.GetVector(entity, target);
            float radius = _scale.Value;
            if (vector.sqrMagnitude > radius * radius)
            {
                _forward = vector.normalized;
                MoveUseCase.MoveStep(entity, _forward, deltaTime);
            }
            else
            {
                DealDamageUseCase.DealDamage(entity, target);
                EntitiesUseCase.UnspawnEntity(_gameContext, entity);
            }
        }
    }
}