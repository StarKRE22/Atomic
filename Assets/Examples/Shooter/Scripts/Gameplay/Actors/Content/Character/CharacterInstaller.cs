using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterInstaller : SceneEntityInstaller<IActor>
    {
        [SerializeField]
        private GameObject _gameObject;

        [SerializeField]
        private Transform _transform;

        [SerializeField]
        private Rigidbody _rigidbody;

        [SerializeField]
        private TriggerEvents _triggerEvents;

        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<float> _rotationSpeed = 15;

        [SerializeField]
        private Health _health = new(3);

        [SerializeField]
        private Weapon _initialWeapon;

        [SerializeField]
        private ReactiveVariable<TeamType> _teamType;

        public override void Install(IActor entity)
        {
            //Transform:
            entity.AddPosition(new TransformPositionVariable(_transform));
            entity.AddRotation(new TransformRotationVariable(_transform));

            //Team:
            entity.AddTeamType(_teamType);
            entity.AddBehaviour<TeamPhysicsLayerBehaviour>();

            //Life:
            entity.AddDamageableTag();
            entity.AddHealth(_health);
            entity.AddTakeDamageEvent(new BaseEvent<DamageArgs>());
            entity.AddTakeDeathEvent(new BaseEvent<DamageArgs>());
            entity.AddRespawnEvent(new BaseEvent());
            
            //Combat:
            entity.AddFireCondition(new AndExpression(_health.Exists));
            entity.AddFireAction(new CharacterFireAction(entity));
            entity.AddFireEvent(new BaseEvent());
            entity.AddWeapon(_initialWeapon);

            //Movement:
            entity.AddMovementSpeed(_moveSpeed);
            entity.AddMovementDirection(new ReactiveVector3());
            entity.AddMovementCondition(new AndExpression(_health.Exists));
            entity.AddMovementEvent(new BaseEvent<Vector3>());
            entity.AddBehaviour<KinematicMovementBehaviour>();

            //Rotation:
            entity.AddRotationSpeed(_rotationSpeed);
            entity.AddRotationDirection(new ReactiveVariable<Vector3>());
            entity.AddRotationCondition(new AndExpression(_health.Exists));
            entity.AddRotationEvent(new BaseEvent<Vector3>());
            entity.AddBehaviour<RotationBehaviour>();

            //Physics:
            entity.AddRigidbody(_rigidbody);
            entity.AddTrigger(_triggerEvents);
            entity.AddPhysicsLayer(new InlineVariable<int>(
                getter: () => _gameObject.layer,
                setter: value => _gameObject.layer = value
            ));

            //Other:
            entity.AddBehaviour<CharacterMoveBehaviour>();
            entity.AddBehaviour<CharacterNameBehaviour>();
        }
    }
}