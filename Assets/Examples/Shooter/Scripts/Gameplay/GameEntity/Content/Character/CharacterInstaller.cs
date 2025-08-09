using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterInstaller : SceneEntityInstaller<IGameEntity>
    {
        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<float> _rotationSpeed = 15;

        [SerializeField]
        private Health _health = new(3);

        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private Weapon _initialWeapon;

        [SerializeField]
        private ReactiveVariable<TeamType> _team;
        
        protected override void Install(IGameEntity entity)
        {
            //Main
            entity.AddPosition(new TransformPositionVariable(_transform));
            entity.AddRotation(new TransformRotationVariable(_transform));
            
            //Team
            entity.AddTeam(new ReactiveVariable<TeamType>(_team));
            entity.AddBehaviour<TeamPhysicsLayerBehaviour>();

            //Life
            entity.AddDamageableTag();
            entity.AddHealth(_health);
            entity.AddTakeDamageEvent(new BaseEvent<DamageArgs>());
            entity.AddTakeDeathEvent(new BaseEvent<DamageArgs>());

            //Combat
            entity.AddFireCondition(new AndExpression(_health.Exists));
            entity.AddFireAction(new CharacterFireAction(entity));
            entity.AddFireEvent(new BaseEvent());
            entity.AddWeapon(_initialWeapon);

            //Movement:
            entity.AddMoveDirection(new ReactiveVector3());
            entity.AddMoveCondition(new AndExpression(entity.GetHealth().Exists));
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddBehaviour<CharacterMoveBehaviour>();
            entity.AddRotationSpeed(_rotationSpeed);
        }
    }
}