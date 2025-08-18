using Atomic.Elements;
using Atomic.Entities;
using UnityEngine;

namespace RTSGame
{
    [CreateAssetMenu(
        fileName = "TankFactory",
        menuName = "RTSGame/Entities/New TankFactory"
    )]
    public sealed class TankFactory : GameEntityFactory
    {
        private const float MIN_DETECT_DURATION = 0.2f;
        private const float MAX_DETECT_DURATION = 0.3f;

        [SerializeField]
        private Const<float> _moveSpeed = 3;

        [SerializeField]
        private Const<float> _rotationSpeed = 12;

        [SerializeField]
        private Const<float> _attackDistance = 5;

        [SerializeField]
        private int _health;

        [SerializeField]
        private float _attackCooldown = 1;

        [SerializeField]
        private ProjectileFactory _projectileFactory;

        [SerializeField]
        private Const<Vector3> _fireOffset = new Vector3(0, 1, 1);

        protected override void Install(IGameEntity entity)
        {
            IGameContext gameContext = EntryPoint.GameContext;

            entity.AddUnitTag();
            this.InstallMove(entity);
            this.InstallCombat(entity, gameContext);
            this.InstallLife(entity);
            this.InstallAI(entity);
        }

        private void InstallMove(IGameEntity entity)
        {
            entity.AddMoveableTag();
            entity.AddMoveSpeed(_moveSpeed);
            entity.AddRotationSpeed(_rotationSpeed);
            entity.AddMoveRequest(new BaseRequest<Vector3>());
            entity.AddMoveEvent(new BaseEvent<Vector3>());
            entity.WhenFixedUpdate(deltaTime =>
            {
                if (HealthUseCase.IsAlive(entity) &&
                    entity.GetMoveRequest().Consume(out Vector3 direction) &&
                    direction != Vector3.zero)
                {
                    MoveUseCase.MoveStep(entity, direction, deltaTime);
                    RotateUseCase.RotateStep(entity, direction, deltaTime);
                    entity.GetMoveEvent().Invoke(direction);
                }
            });
        }
        
        private void InstallCombat(IGameEntity entity, IGameContext gameContext)
        {
            entity.AddFireCooldown(new Cooldown(_attackCooldown));
            entity.AddFirePoint(new InlineFunction<Vector3>(() => FireUseCase.GetFirePoint(entity, _fireOffset.Value)));
            entity.AddFireRequest(new BaseRequest<IEntity>());
            entity.WhenFixedUpdate(_ =>
            {
                if (HealthUseCase.IsAlive(entity) &&
                    entity.GetFireCooldown().IsCompleted() &&
                    entity.GetFireRequest().Consume(out IEntity target))
                {
                    FireUseCase.FireProjectile(entity, _projectileFactory.name, target, gameContext);
                    entity.GetFireCooldown().ResetTime();
                    entity.GetFireEvent().Invoke(target);
                }
            });

            entity.WhenFixedUpdate(entity.GetFireCooldown().Tick);
            entity.AddFireDistance(_attackDistance);
            entity.AddFireEvent(new BaseEvent<IEntity>());
        }

        private void InstallLife(IGameEntity entity)
        {
            entity.AddDamageableTag();
            entity.AddHealth(new Health(_health, _health));
            entity.AddBehaviour<DeathBehaviour>();
        }

        private void InstallAI(IGameEntity entity)
        {
            entity.AddTarget(new ReactiveVariable<IEntity>());
            entity.AddBehaviour(new DetectTargetBehaviour(new RandomCooldown(MIN_DETECT_DURATION, MAX_DETECT_DURATION)));
            entity.AddBehaviour<AttackBehaviour>();
        }
    }
}