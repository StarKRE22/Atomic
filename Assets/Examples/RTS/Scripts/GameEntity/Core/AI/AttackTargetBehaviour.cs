using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class AttackTargetBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private IGameEntity _entity;
        private IValue<IGameEntity> _target;

        public void OnSpawn(IGameEntity entity)
        {
            _entity = entity;
            _target = entity.GetTarget();
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
            IGameEntity target = _target.Value;
            if (target is not {IsActive: true} || !HealthUseCase.IsAlive(target))
                return;

            Vector3 vector = TransformUseCase.GetVector(_entity, target);
            float fireDistance = _entity.GetFireDistance().Value;

            if (vector.sqrMagnitude > fireDistance * fireDistance)
                _entity.GetMoveRequest().Invoke(vector.normalized);
            else
                _entity.GetFireRequest().Invoke(target);
        }
    }
}