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
        [SerializeField]
        private Const<float> _radius = 2;

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
        private ProjectileInstaller _projectileConfig;

        [SerializeField]
        private Const<Vector3> _fireOffset = new Vector3(0, 1, 1);

        protected override void Install(IGameEntity entity)
        {
            IGameContext gameContext = EntryPoint.GameContext;

            entity.AddUnitTag();
            this.InstallMove(entity);
            this.InstallCombat(entity, gameContext);
            this.InstallRotation(entity);
            this.InstallLife(entity);
            this.InstallAI(entity);
        }

        private void InstallMove(IGameEntity entity)
        {
            entity.AddMoveableTag();
            entity.AddMoveSpeed(_moveSpeed);
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

        private void InstallAI(IEntity entity)
        {
            entity.AddTarget(new ReactiveVariable<IEntity>());
            entity.AddBehaviour(new DetectTargetBehaviour(new RandomCooldown(0.2f, 0.3f)));
            entity.AddBehaviour<AttackBehaviour>();
        }

        private void InstallRotation(IEntity entity)
        {
            entity.AddRotationSpeed(_rotationSpeed);
        }

        private void InstallLife(IEntity entity)
        {
            entity.AddDamageableTag();
            entity.AddHealth(new Health(_health, _health));
            entity.AddBehaviour<DeathBehaviour>();
        }

        private void InstallCombat(IEntity entity, GameContext gameContext)
        {
            entity.AddFireCooldown(new Cooldown(_attackCooldown));
            entity.AddFirePoint(new BaseFunction<Vector3>(() =>
                FireUseCase.GetFirePoint(entity, _fireOffset.Value))
            );
            entity.AddFireAction(new BaseAction<IEntity>(target =>
            {
                FireUseCase.FireProjectile(entity, _projectileConfig.name, target, gameContext);
                entity.GetFireCooldown().Reset();
            }));
            entity.AddFireCondition(new BaseFunction<IEntity, bool>(_ =>
                HealthUseCase.IsAlive(entity) && entity.GetFireCooldown().IsExpired())
            );

            entity.WhenFixedUpdate(entity.GetFireCooldown().Tick);
            entity.AddFireDistance(_attackDistance);
            entity.AddFireEvent(new BaseEvent<IEntity>());
        }
    }
}