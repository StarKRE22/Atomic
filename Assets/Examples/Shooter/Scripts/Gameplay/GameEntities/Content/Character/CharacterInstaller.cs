using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;
using Event = Atomic.Elements.Event;

namespace ShooterGame.Gameplay
{
    public sealed class CharacterInstaller : GameEntityInstaller
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
        private Const<float> _stoppingDistance = 0.25f;

        [SerializeField]
        private Const<LayerMask> _obstacleLayerMask;

        [SerializeField]
        private Health _health = new(3);

        [SerializeField]
        private Weapon _initialWeapon;

        [SerializeField]
        private ReactiveVariable<TeamType> _teamType;

        public override void Install(IGameEntity entity)
        {
            GameContext.TryGetInstance(out GameContext gameContext);
            
            //Transform:
            entity.AddPosition(new TransformPositionVariable(_transform));
            entity.AddRotation(new TransformRotationVariable(_transform));

            //Team:
            entity.AddTeamType(_teamType);
            entity.AddBehaviour(new TeamPhysicsLayerBehaviour(gameContext));

            //Life:
            entity.AddDamageableTag();
            entity.AddHealth(_health);
            entity.AddTakeDamageEvent(new Event<DamageArgs>());
            entity.AddTakeDeathEvent(new Event<DamageArgs>());
            entity.AddRespawnEvent(new Event());
            
            //Combat:
            entity.AddFireCondition(new AndExpression(_health.Exists));
            entity.AddFireAction(new CharacterFireAction(entity));
            entity.AddFireEvent(new Event());
            entity.AddWeapon(_initialWeapon);

            //Movement:
            entity.AddMovementSpeed(_moveSpeed);
            entity.AddMovementDirection(new ReactiveVector3());
            entity.AddMovementCondition(new AndExpression(_health.Exists));
            entity.AddMovementEvent(new Event<Vector3>());
            entity.AddBehaviour(new KinematicMovementBehaviour(_stoppingDistance, _obstacleLayerMask));

            //Rotation:
            entity.AddRotationSpeed(_rotationSpeed);
            entity.AddRotationDirection(new ReactiveVariable<Vector3>());
            entity.AddRotationCondition(new AndExpression(_health.Exists));
            entity.AddRotationEvent(new Event<Vector3>());
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