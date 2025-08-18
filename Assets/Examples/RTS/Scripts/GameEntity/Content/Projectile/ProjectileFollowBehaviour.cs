using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    public sealed class ProjectileFollowBehaviour : IEntitySpawn<IGameEntity>, IEntityFixedUpdate
    {
        private GameContext _gameContext;
        private IValue<float> _radius;
        private IValue<IEntity> _target;

        private Vector3 _forward;
        
        public void OnSpawn(IGameEntity entity)
        {
            _target = entity.GetTarget();
            _radius = entity.`();
            _gameContext = GameContext.Instance;
        }

        public void OnFixedUpdate(IEntity entity, float deltaTime)
        {
        }
        
        public void Init(in IEntity entity)
        {
       
        }
        
        public void Enable(in IEntity entity)
        {
            _forward = entity.GetRotation().Value * Vector3.forward;
        }

        public void OnFixedUpdate(in IEntity entity, in float deltaTime)
        {
            IEntity target = _target.Value;
            if (target is not {Enabled: true})
            {
                MoveUseCase.MoveStep(entity, _forward, deltaTime);
                return;
            }

            Vector3 vector = TransformUseCase.GetVector(entity, target);
            float radius = _radius.Value;
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