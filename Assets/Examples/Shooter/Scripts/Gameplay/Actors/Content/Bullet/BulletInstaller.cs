using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class BulletInstaller : SceneEntityInstaller<IActor>
    {
        [SerializeField]
        private GameObject _gameObject;
        
        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<int> _damage = 1;

        [SerializeField]
        private TriggerEvents _trigger;

        [SerializeField]
        private Cooldown _lifetime = 5;

        public override void Install(IActor entity)
        {
            GameContext.TryGetInstance(out GameContext gameContext);
            
            //Transform:
            entity.AddPosition(new TransformPositionVariable(_transform));
            entity.AddRotation(new TransformRotationVariable(_transform));

            //Lifetime
            entity.AddLifetime(_lifetime);
            entity.AddBehaviour<LifetimeBehaviour>();

            //Team
            entity.AddTeamType(new ReactiveVariable<TeamType>());
            entity.AddBehaviour<TeamPhysicsLayerBehaviour>();

            //Move
            entity.AddMovementSpeed(_moveSpeed);
            entity.AddBehaviour<BulletMoveBehaviour>();

            //Physics
            entity.AddTrigger(_trigger);
            entity.AddPhysicsLayer(new InlineVariable<int>(
                getter: () => _gameObject.layer,
                setter: value => _gameObject.layer = value
            ));
            entity.AddBehaviour(new BulletCollisionBehaviour(gameContext));

            //Damage dealing:
            entity.AddDamage(_damage);
            entity.AddDestroyAction(new InlineAction(() => BulletUseCase.Despawn(gameContext, entity)));
        }
    }
}